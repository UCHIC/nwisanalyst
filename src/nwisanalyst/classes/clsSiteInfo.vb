Option Strict On
Public Class clsSiteInfo
    Public Shared Function ReturnSiteInfo(ByVal strSiteCode As String, ByVal Database As String) As String
        Dim strSiteInfo As String
        If Database = "DV" Then
            strSiteInfo = GetSiteInfo_DV(strSiteCode)
        ElseIf Database = "GW" Then
            strSiteInfo = GetSiteInfo_GW(strSiteCode)
        ElseIf Database = "IID" Then
            strSiteInfo = GetSiteInfo_IID(strSiteCode)
        ElseIf Database = "UV" Then
            strSiteInfo = GetSiteInfo_UV(strSiteCode)
        End If

        Return strSiteInfo
    End Function

    Private Shared Function GetSiteInfo_DV(ByVal strSiteCode As String) As String
        Dim strSiteInfo As String
        Dim objNWISDV As New NWISDV.WaterOneFlowSoapClient
        strSiteInfo = objNWISDV.GetSiteInfo("NWISDV:" & strSiteCode, "")
        Return strSiteInfo
    End Function

    Private Shared Function GetSiteInfo_GW(ByVal strSiteCode As String) As String
        Dim strSiteInfo As String
        Dim objNWISGW As New NWISGW.WaterOneFlowSoapClient
        strSiteInfo = objNWISGW.GetSiteInfo("NWISGW:" & strSiteCode, "")
        Return strSiteInfo
    End Function

    Private Shared Function GetSiteInfo_IID(ByVal strSiteCode As String) As String
        Dim strSiteInfo As String
        Dim objNWISIID As New NWISIID.WaterOneFlowSoapClient
        strSiteInfo = objNWISIID.GetSiteInfo("NWISIID:" & strSiteCode, "")
        Return strSiteInfo
    End Function

    Private Shared Function GetSiteInfo_UV(ByVal strSiteCode As String) As String
        Dim strSiteInfo As String
        Dim objNWISUV As New NWISUV.WaterOneFlowSoapClient
        strSiteInfo = objNWISUV.GetSiteInfo("NWISUV:" & strSiteCode, "")
        Return strSiteInfo
    End Function
End Class
