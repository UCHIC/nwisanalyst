Option Strict On
Public Class Variable
    Private mintVariableId As Integer
    Private mstrVariableDescription As String
    Private mstrStartYear As String
    Private mstrEndYear As String

    Sub New()
        mintVariableId = 0
        mstrVariableDescription = String.Empty
        mstrStartYear = String.Empty
        mstrEndYear = String.Empty
    End Sub

    Sub New(ByRef objVariable As Variable)
        mintVariableId = objVariable.Id
        mstrVariableDescription = objVariable.Description
        mstrStartYear = objVariable.StartYear
        mstrEndYear = objVariable.EndYear
    End Sub

    Sub New(ByVal intVariableId As Integer, ByVal strVariableDescription As String, ByVal strStartYear As String, ByVal strEndYear As String)
        mintVariableId = intVariableId
        mstrVariableDescription = strVariableDescription
        mstrStartYear = strStartYear
        mstrEndYear = strEndYear
    End Sub

    Public Property Id() As Integer
        Get
            Return mintVariableId
        End Get
        Set(ByVal intVariableId As Integer)
            mintVariableId = intVariableId
        End Set
    End Property

    Public Property Description() As String
        Get
            Return mstrVariableDescription
        End Get
        Set(ByVal strVariableDescription As String)
            mstrVariableDescription = strVariableDescription
        End Set
    End Property

    Property StartYear() As String
        Get
            Return mstrStartYear
        End Get
        Set(ByVal strStartYear As String)
            mstrStartYear = strStartYear
        End Set
    End Property

    Property EndYear() As String
        Get
            Return mstrEndYear
        End Get
        Set(ByVal strEndYear As String)
            mstrEndYear = strEndYear
        End Set
    End Property

    Public Sub AdjustSpan(ByRef objVariable As Variable)
        If Convert.ToInt32(objVariable.StartYear) < Convert.ToInt32(mstrStartYear) Then
            mstrStartYear = objVariable.StartYear
        End If
        If Convert.ToInt32(objVariable.EndYear) > Convert.ToInt32(mstrEndYear) Then
            mstrEndYear = objVariable.EndYear
        End If
    End Sub
End Class