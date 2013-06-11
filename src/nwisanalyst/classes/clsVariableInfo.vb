Option Strict On
Public Class clsVariableInfo
    Public Shared Function ReturnVariableInfo(ByVal strVariableCode As String, ByVal Database As String) As String
        Dim strVariableInfo As String
        If Database = "DV" Then
            strVariableInfo = GetVariableInfo_DV(strVariableCode)
        ElseIf Database = "GW" Then
            strVariableInfo = GetVariableInfo_GW(strVariableCode)
        ElseIf Database = "IID" Then
            strVariableInfo = GetVariableInfo_IID(strVariableCode)
        ElseIf Database = "UV" Then
            strVariableInfo = GetVariableInfo_UV(strVariableCode)
        End If

        Return strVariableInfo
    End Function

    Private Shared Function GetVariableInfo_DV(ByVal strVariableCode As String) As String
        Dim strVariableInfo As String
        Dim objNWISDV As New NWISDV.WaterOneFlowSoapClient
        strVariableInfo = objNWISDV.GetVariableInfo("NWISDV:" & strVariableCode, "")
        Return strVariableInfo
    End Function

    Private Shared Function GetVariableInfo_GW(ByVal strVariableCode As String) As String
        Dim strVariableInfo As String
        Dim objNWISGW As New NWISGW.WaterOneFlowSoapClient
        strVariableInfo = objNWISGW.GetVariableInfo("NWISGW:" & strVariableCode, "")
        Return strVariableInfo
    End Function

    Private Shared Function GetVariableInfo_IID(ByVal strVariableCode As String) As String
        Dim strVariableInfo As String
        Dim objNWISIID As New NWISIID.WaterOneFlowSoapClient
        strVariableInfo = objNWISIID.GetVariableInfo("NWISIID:" & strVariableCode, "")
        Return strVariableInfo
    End Function

    Private Shared Function GetVariableInfo_UV(ByVal strVariableCode As String) As String
        Dim strVariableInfo As String
        Dim objNWISUV As New NWISUV.WaterOneFlowSoapClient
        strVariableInfo = objNWISUV.GetVariableInfo("NWISUV:" & strVariableCode, "")
        Return strVariableInfo
    End Function
End Class
