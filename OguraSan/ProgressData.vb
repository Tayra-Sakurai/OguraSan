Public Class ProgressData
    Public ReadOnly Property EndedQuestions As List(Of Integer)
    Public Sub New()
        If IsNothing(_EndedQuestions) Then
            _EndedQuestions = New List(Of Integer)()
        End If
        Record = 0
    End Sub
    Dim _Index As Integer
    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
            _EndedQuestions.Add(value)
        End Set
    End Property
    Public ReadOnly Property QuestionsCount As Integer
        Get
            Return _EndedQuestions.Count()
        End Get
    End Property
    Public Property IsFirstTrial As Boolean
    Public Property Record As Integer
End Class
