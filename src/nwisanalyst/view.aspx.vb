Option Strict On

Imports System.Data

Public Class RootView
    Inherits System.Web.UI.Page
    Protected WithEvents dgStreamflow As System.Web.UI.WebControls.DataGrid

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objDataSet As DataSet = CType(Session("DataSet"), DataSet)

        dgStreamflow.DataSource = objDataSet.Tables.Item(1)
        dgStreamflow.DataBind()
    End Sub
End Class
