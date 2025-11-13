Imports System.Text.Json
Imports System.Threading
Imports System.Windows.Media.Animation
Imports Microsoft.EntityFrameworkCore

Class MainWindow
    ''' <summary>
    ''' The database instance
    ''' </summary>
    Dim Ogura As New OguraEntities()

    ''' <summary>
    ''' Questioned item
    ''' </summary>
    ''' <returns>
    ''' <see cref="Table"/>.
    ''' The Table Object of the current question
    ''' </returns>
    Friend Property CurrentItem As Table

    ''' <summary>
    ''' Progress Data
    ''' </summary>
    ''' <returns>
    ''' <see cref="ProgressData"/>.
    ''' Serializable Progress data.
    ''' </returns>
    Friend Property CurrentProgress As ProgressData

    ''' <summary>
    ''' Setting data
    ''' </summary>
    Private WithEvents SettingsMe As New MySettings()

    ''' <summary>
    ''' Fade in animation
    ''' </summary>
    Private SuperFadeIn As New Storyboard()

    ''' <summary>
    ''' Fade out animation
    ''' </summary>
    Private SuperFadeOut As New Storyboard()

    ''' <summary>
    ''' The font settings.
    ''' </summary>
    Friend SuperFontSettings As FontSetting

    ''' <summary>
    ''' Initialization
    ''' </summary>
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        ' Load the EntityFrameworkCore
        Ogura.Table.Load()
        ' Selects the first question
        Dim rnd As New Random()
        ' The question index.
        Dim index As Integer = rnd.Next(Ogura.Table.Count())
        CurrentItem = Ogura.Table.Local.Skip(index).FirstOrDefault()

        ' Gets the data settings
        If Not (String.IsNullOrEmpty(SettingsMe.ProgressDataCurrent)) Then
            ' The JSON String of the progress data.
            Dim JProgress As String = SettingsMe.ProgressDataCurrent
            CurrentProgress = JsonSerializer.Deserialize(Of ProgressData)(JProgress)
            index = CurrentProgress.Index
            CurrentItem = Ogura.Table.Local.Skip(index).FirstOrDefault()
        Else
            CurrentProgress = New ProgressData()
            With CurrentProgress
                .Index = index
                .IsFirstTrial = True
            End With
            SettingsMe.ProgressDataCurrent = JsonSerializer.Serialize(Of ProgressData)(CurrentProgress)
            SettingsMe.DataSaved = True
            SettingsMe.Save()
        End If

        ' Animation setups
        ' Fade in

        'The animation of fade in itself.
        Dim FadeInAnimation As New DoubleAnimation With {
            .Duration = New Duration(TimeSpan.FromSeconds(0.2)),
            .From = 0,
            .To = 1,
            .AutoReverse = False
            }
        SuperFadeIn.Children.Add(FadeInAnimation)
        Storyboard.SetTargetName(FadeInAnimation, SuperKaminoku.Name)
        Storyboard.SetTargetProperty(FadeInAnimation, New PropertyPath(TextBlock.OpacityProperty))

        ' Fade Out

        ' The Animation
        Dim FadeOutSuperAnimation As New DoubleAnimation()
        With FadeOutSuperAnimation
            .From = 1
            .To = 0
            .Duration = New Duration(TimeSpan.FromSeconds(0.2))
            .AutoReverse = False
        End With
        SuperFadeOut.Children.Add(FadeOutSuperAnimation)
        Storyboard.SetTargetName(FadeOutSuperAnimation, SuperKaminoku.Name)
        Storyboard.SetTargetProperty(FadeOutSuperAnimation, New PropertyPath(TextBlock.OpacityProperty))

        SuperFontSettings = New FontSetting(SuperKaminoku)
    End Sub

    ''' <summary>
    ''' Check the answer.
    ''' </summary>
    ''' <seealso cref="Refresh()"/>
    ''' <seealso cref="Update_Item()"/>
    Private Sub Check_Result()
        ' Entered Answer
        Dim ans As String = SuperAnswer.Text
        If ans = CurrentItem.Shimonoku Then
            MessageBox.Show("正解", "解答結果", MessageBoxButton.OK, MessageBoxImage.Information)
            CurrentProgress.Record += 1
            Update_Item()
            Refresh()
        ElseIf Not CurrentProgress.IsFirstTrial Then
            ' Add the item to black list
            MessageBox.Show($"不正解です．解答は「{CurrentItem.Shimonoku}」でした．", "解答結果", MessageBoxButton.OK, MessageBoxImage.Error)
            SuperAnswer.Text = CurrentItem.Shimonoku
            CurrentItem.IsPrviousCorrect = False
            Ogura.SaveChanges()
            Update_Item()
            Refresh()
        Else
            MessageBox.Show("不正解", "解答結果", MessageBoxButton.OK, MessageBoxImage.Error)
            CurrentProgress.IsFirstTrial = False
            SuperAnswer.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Updates the CurrentItem.
    ''' </summary>
    Private Sub Update_Item()
        CurrentProgress.IsFirstTrial = True
        If CurrentProgress.QuestionsCount < Ogura.Table.Local.Count Then
            ' Random generator
            Dim rand As New Random()
            ' The index
            Dim i As Integer = 0
            Do While CurrentProgress.EndedQuestions.Contains(i)
                i = rand.Next(Ogura.Table.Local.Count)
            Loop
            CurrentProgress.Index = i
            CurrentItem = Ogura.Table.Local.Skip(i).FirstOrDefault()
            SettingsMe.ProgressDataCurrent = JsonSerializer.Serialize(Of ProgressData)(CurrentProgress)
            SettingsMe.Save()
        Else
            ' Last message
            Dim lastmsg As New LastMessage()
            If lastmsg.ShowDialog() OrElse True Then
                SettingsMe.ProgressDataCurrent = Nothing
                SettingsMe.DataSaved = False
                SettingsMe.LastRecord = CurrentProgress.Record
                SettingsMe.Save()
                Application.Current.Shutdown()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Refresh the contents.
    ''' Display new record.
    ''' Update settings.
    ''' </summary>
    Private Sub Refresh()
        ' Fade out animation.
        SuperFadeOut.Begin(Me)
        ' Check out the mode.
        If SettingsMe.Kimariji Then
            ' In kimariji mode,
            ' the item's kimariji is displayed.
            SuperKaminoku.Text = CurrentItem.Kimariji
        Else
            ' In normal mode
            ' the kaminoku is displayed.
            SuperKaminoku.Text = CurrentItem.Kaminoku
        End If
        ' Fade in the question.
        SuperFadeIn.Begin(Me)
        ' Update the maximum record textbox if necessary.
        If CurrentProgress.Record > SettingsMe.MaxRecord Then
            ' Update.
            SettingsMe.MaxRecord = CurrentProgress.Record
        End If
        ' Update every record boxes.
        SuperQuestionsCount.Text = CStr(CurrentProgress.QuestionsCount) + "/" + CStr(Ogura.Table.Count())
        SuperMaxRecord.Text = CStr(SettingsMe.MaxRecord)
        SuperLastRecord.Text = CStr(SettingsMe.LastRecord)
        SuperRecord.Text = CStr(CurrentProgress.Record)
        ' Reset the answer space.
        SuperAnswer.Text = ""
    End Sub

    ''' <summary>
    ''' Initilizes the components.
    ''' </summary>
    ''' <param name="sender">
    ''' <see cref="Object"/>.
    ''' The event's sender.
    ''' </param>
    ''' <param name="e">
    ''' <see cref="RoutedEventArgs"/>.
    ''' Event arguments.
    ''' </param>
    Private Sub SuperWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles SuperWindow.Loaded
        Refresh()
    End Sub

    ''' <summary>
    ''' Button Actions.
    ''' </summary>
    ''' <param name="sender">
    ''' <see cref="Object"/>.
    ''' The sender of the event
    ''' </param>
    ''' <param name="e">
    ''' <see cref="RoutedEventArgs"/>.
    ''' Event arguments.
    ''' </param>
    Private Sub SuperSubmitButton_Click(sender As Object, e As RoutedEventArgs) Handles SuperSubmitButton.Click
        Check_Result()
    End Sub

    ''' <summary>
    ''' Opens the font setting window.
    ''' </summary>
    ''' <param name="sender">
    ''' <see cref="Object"/>.
    ''' The sender of the event.
    ''' </param>
    ''' <param name="e">
    ''' <see cref="RoutedEventArgs"/>.
    ''' Event arguments.
    ''' </param>
    Private Sub SuperFont_Click(sender As Object, e As RoutedEventArgs) Handles SuperFont.Click
        ' Font setting dialog
        Dim fm As New SuperDialogWindow(SuperFontSettings)
        fm.Owner = Me
        fm.ShowDialog()
    End Sub

    ''' <summary>
    ''' Activates Kimariji mode.
    ''' </summary>
    ''' <param name="sender">
    ''' <see cref="Object"/>.
    ''' The sender of the event.
    ''' </param>
    ''' <param name="e">
    ''' <see cref="RoutedEventArgs"/>.
    ''' Event arguments.
    ''' </param>
    ''' <seealso cref="Refresh()"/>
    Private Sub SuperMenuItemKimariji_Checked(sender As Object, e As RoutedEventArgs) Handles SuperMenuItemKimariji.Checked
        SettingsMe.Kimariji = True
        Refresh()
    End Sub

    ''' <summary>
    ''' Deactivates the Kimariji mode.
    ''' </summary>
    ''' <param name="sender">
    ''' <see cref="Object"/>.
    ''' The sender.
    ''' </param>
    ''' <param name="e">
    ''' <see cref="RoutedEventArgs"/>.
    ''' Event arguments.
    ''' </param>
    ''' <seealso cref="SuperMenuItemKimariji_Checked(Object, RoutedEventArgs)"/>
    ''' <seealso cref="Refresh()"/>
    Private Sub SuperMenuItemKimariji_Unchecked(sender As Object, e As RoutedEventArgs) Handles SuperMenuItemKimariji.Unchecked
        SettingsMe.Kimariji = False
        Refresh()
    End Sub

    ''' <summary>
    ''' Shortcuts to the event.
    ''' </summary>
    ''' <param name="sender">
    ''' <see cref="Object"/>.
    ''' The sender.
    ''' </param>
    ''' <param name="e">
    ''' <see cref="KeyEventArgs"/>.
    ''' Event arguments.
    ''' </param>
    ''' <seealso cref="SuperSubmitButton_Click(Object, RoutedEventArgs)"/>
    Private Sub SuperAnswer_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles SuperAnswer.PreviewKeyDown
        ' The key type
        Dim key As Key = e.Key
        If key = Key.Return Then
            Check_Result()
        End If
    End Sub
End Class
