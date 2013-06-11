Option Strict On
Public Class clsValues
    Public Shared Function ReturnValues(ByVal strSiteCode As String, ByVal strVariableCode As String, ByVal Database As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As String
        Dim strValues As String
        Dim objValues As NWISUV.TimeSeriesResponseType
        If Database = "DV" Then
            strValues = GetValues_DV(strSiteCode, strVariableCode, StartDate, EndDate)
        ElseIf Database = "GW" Then
            strValues = GetValues_GW(strSiteCode, strVariableCode, StartDate, EndDate)
        ElseIf Database = "IID" Then
            strValues = GetValues_IID(strSiteCode, strVariableCode, StartDate, EndDate)
        ElseIf Database = "UV" Then
            strValues = GetValues_UV(strSiteCode, strVariableCode, StartDate, EndDate)
        End If

        Return strValues
    End Function

    Private Shared Function GetValues_DV(ByVal strSiteCode As String, ByVal strVariableCode As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As String
        Dim strValues As String
        Dim objNWISDV As New NWISDV.WaterOneFlowSoapClient
        strValues = objNWISDV.GetValues("NWISDV:" & strSiteCode, "NWISDV:" & strVariableCode, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"), "")
        Return strValues
    End Function

    Private Shared Function GetValues_GW(ByVal strSiteCode As String, ByVal strVariableCode As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As String
        Dim strValues As String
        Dim objNWISGW As New NWISGW.WaterOneFlowSoapClient
        strValues = objNWISGW.GetValues("NWISGW:" & strSiteCode, "NWISGW:" & strVariableCode, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"), "")
        Return strValues
    End Function

    Private Shared Function GetValues_IID(ByVal strSiteCode As String, ByVal strVariableCode As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As String
        Dim strValues As String
        Dim objNWISIID As New NWISIID.WaterOneFlowSoapClient
        strValues = objNWISIID.GetValues("NWISIID:" & strSiteCode, "NWISIID:" & strVariableCode, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"), "")
        Return strValues
    End Function

    Private Shared Function GetValues_UV(ByVal strSiteCode As String, ByVal strVariableCode As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As String
        Dim strValues As String
        Dim objNWISUV As New NWISUV.WaterOneFlowSoapClient
        strValues = objNWISUV.GetValues("NWISUV:" & strSiteCode, "NWISUV:" & strVariableCode, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"), "")
        Return strValues
    End Function
End Class
