Public Class SuperDialogWindow
    ''' <summary>
    ''' Font setters
    ''' </summary>
    Dim FSet As FontSetting

    ''' <summary>
    ''' When Fset is created, this changes to True.
    ''' </summary>
    Dim IsActivated As Boolean = False

    ''' <summary>
    ''' Initializations
    ''' </summary>
    ''' <param name="FSettings">
    ''' (FontSetting)
    ''' The font setting object.
    ''' </param>
    Public Sub New(FSettings As FontSetting)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Debug.WriteLine("Is it OK?")
        FSet = FSettings
    End Sub

    ''' <summary>
    ''' rollback the settings.
    ''' </summary>
    Private Sub Set_FSet()
        If IsActivated Then
            FSet.Family = SuperListBox.SelectedItem
            FSet.Size = SuperSlider.Value
        End If
    End Sub

    Private Sub SuperSetButton_Click(sender As Object, e As RoutedEventArgs) Handles SuperSetButton.Click
        Set_FSet()
        DialogResult = True
    End Sub

    Private Sub SuperSlider_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        Set_FSet()
    End Sub

    Private Sub SuperListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles SuperListBox.SelectionChanged
        Set_FSet()
    End Sub

    Private Sub SuperDialogWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded
        Debug.WriteLine("0")
        Debug.WriteLine(FSet.FamilyName)
        SuperSlider.Value = FSet.Size
        Debug.WriteLine("1")
        SuperListBox.ItemsSource = Fonts.SystemFontFamilies
        SuperListBox.SelectedItem = FSet.Family
        IsActivated = True
    End Sub
End Class
