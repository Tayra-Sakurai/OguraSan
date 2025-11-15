Public Class ProgressData
    Private _Record As Integer = 0
    Public Property EndedQuestions As List(Of Integer)
    Public Sub New()
        If IsNothing(_EndedQuestions) Then
            _EndedQuestions = New List(Of Integer)()
        End If
    End Sub
    Dim _Index As Integer
    Public Property Index As Integer
        Get
            Return _Index
        End Get
        Set(value As Integer)
            _Index = value
            If Not _EndedQuestions.Contains(value) Then
                _EndedQuestions.Add(value)
            End If
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
