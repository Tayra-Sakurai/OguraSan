Public Class ProgressData
    Private _Record As Integer
    Public Property EndedQuestions As List(Of Integer)
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
        Get
            Return _Record
        End Get
        Set(value As Integer)
            If value > QuestionsCount Then
                _Record = QuestionsCount
            Else
                _Record = value
            End If
        End Set
    End Property
End Class
