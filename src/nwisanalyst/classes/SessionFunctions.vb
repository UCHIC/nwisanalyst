Option Strict On

Imports System.Data

Public Class SessionFunctions
    Public Shared Sub Load()
        'Initialize DataSet
        System.Web.HttpContext.Current.Session("DataSet") = New DataSet
    End Sub

    Private Shared Function GetList(ByRef objDataRows() As DataRow, ByVal strColumn As String) As String
        Dim objDataRow As DataRow
        Dim arr(objDataRows.Length - 1) As String
        Dim i As Integer = 0

        For Each objDataRow In objDataRows
            arr(i) = Convert.ToString(objDataRow.Item(strColumn))
            i += 1
        Next

        Return String.Join(",", arr)
    End Function
End Class
