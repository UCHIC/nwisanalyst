Option Strict On
Public Class Station
	Private mstrStationId As String
	Private mstrStationName As String
    Private mobjVariables As SortedList

	Public Sub New()
		mstrStationId = String.Empty
		mstrStationName = String.Empty
        mobjVariables = New SortedList
	End Sub

    Public Sub New(ByVal strStationId As String, ByVal strStationName As String, ByVal objVariables As SortedList)
        mstrStationId = strStationId
        mstrStationName = strStationName
        mobjVariables = objVariables
    End Sub

    Property Id() As String
        Get
            Return mstrStationId
        End Get
        Set(ByVal strStationId As String)
            mstrStationId = strStationId
        End Set
    End Property

    Property Name() As String
        Get
            Return mstrStationName
        End Get
        Set(ByVal strStationName As String)
            mstrStationName = strStationName
        End Set
    End Property

    Property Variables() As SortedList
        Get
            Return mobjVariables
        End Get
        Set(ByVal objVariables As SortedList)
            mobjVariables = objVariables
        End Set
    End Property

End Class