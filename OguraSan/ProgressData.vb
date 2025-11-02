Public Class ProgressData
    Public Sub New()
        EndedQuestions = New List(Of Integer)()
        Record = 0
    End Sub
    Dim _Index As Integer
    Public Property EndedQuestions As List(Of Integer)
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
            Return _EndedQuestions.Count() + 1
        End Get
    End Property
    Public Property IsFirstTrial As Boolean
    Public Property Record As Integer
End Class
