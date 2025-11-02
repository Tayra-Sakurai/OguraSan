Imports Microsoft.EntityFrameworkCore

Public Class LastMessage
    Dim db As New OguraEntities()

    ''' <summary>
    ''' Initializations
    ''' </summary>
    Public Sub New()
        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        ' Loaded
        db.Table.Load()
    End Sub

    Private Sub LastMessage1_Loaded(sender As Object, e As RoutedEventArgs) Handles LastMessage1.Loaded
        ' Data source
        Dim TableViewSource As CollectionViewSource = CType(Me.FindResource("OguraViewSource"), CollectionViewSource)
        TableViewSource.Source = db.Table.Local.ToObservableCollection()
        HyperDataGrid.Items.SortDescriptions.Add(New ComponentModel.SortDescription("Id", ComponentModel.ListSortDirection.Ascending))
    End Sub

    Private Sub HyperButton_Click(sender As Object, e As RoutedEventArgs) Handles HyperButton.Click
        DialogResult = True
    End Sub
End Class
