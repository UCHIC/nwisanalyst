Imports System.Drawing
Imports System.Data

Public Class DataGridToExcel
    Inherits System.ComponentModel.Component

#Region " Component Designer generated code "

    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        Container.Add(Me)
    End Sub

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Component overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Public Shared Sub DataGridToExcel(ByVal objResponse As HttpResponse, ByRef objDataTable As DataTable)
        Dim stringWrite As New System.IO.StringWriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        Dim dg As New DataGrid

        'Set the datagrid attributes and columns, bind, and render
        dg.DataSource = objDataTable
        dg.BorderColor = Color.FromArgb(153, 153, 153)
        dg.BorderStyle = BorderStyle.None
        dg.BorderWidth = Unit.Pixel(1)
        dg.BackColor = Color.White
        dg.CellPadding = 3
        dg.GridLines = GridLines.Vertical
        dg.EnableViewState = False
        dg.AutoGenerateColumns = False

        dg.HeaderStyle.Font.Bold = True
        dg.HeaderStyle.BackColor = Color.FromArgb(0, 0, 132)
        dg.HeaderStyle.ForeColor = Color.White
        dg.AlternatingItemStyle.BackColor = Color.Gainsboro
        dg.ItemStyle.BackColor = Color.FromArgb(238, 238, 238)

        'Station column
        Dim colStation As BoundColumn = New BoundColumn
        colStation.DataField = "HydroCode"
        colStation.ReadOnly = True
        colStation.HeaderText = "Station"
        dg.Columns.Add(colStation)

        'Date column
        Dim colDate As BoundColumn = New BoundColumn
        colDate.DataField = "TSDateTime"
        colDate.ReadOnly = True
        colDate.DataFormatString = "{0:g}"
        colDate.HeaderText = "Date"
        dg.Columns.Add(colDate)

        'Variable code column
        Dim colVariableCode As BoundColumn = New BoundColumn
        colVariableCode.DataField = "TSTypeID"
        colVariableCode.ReadOnly = True
        colVariableCode.HeaderText = "Variable Code"
        dg.Columns.Add(colVariableCode)

        'Variable name column
        Dim colVariableName As BoundColumn = New BoundColumn
        colVariableName.DataField = "TSTypeName"
        colVariableName.ReadOnly = True
        colVariableName.HeaderText = "Variable Name"
        dg.Columns.Add(colVariableName)

        'Value column
        Dim colValue As BoundColumn = New BoundColumn
        colValue.DataField = "TSValue"
        colValue.ReadOnly = True
        colValue.HeaderText = "Value"
        dg.Columns.Add(colValue)

        'Qualifier column
        Dim colQualifier As BoundColumn = New BoundColumn
        colQualifier.DataField = "TSQualifier"
        colQualifier.ReadOnly = True
        colQualifier.HeaderText = "Qualifier"
        dg.Columns.Add(colQualifier)

        'CensorCode Column
        Dim colCensorCode As BoundColumn = New BoundColumn
        colCensorCode.DataField = "TSCensorCode"
        colCensorCode.ReadOnly = True
        colCensorCode.HeaderText = "CensorCode"
        dg.Columns.Add(colCensorCode)

        'Bind
        dg.DataBind()

        'Render
        dg.RenderControl(htmlWrite)

        'Initialize response object and output HTML
        objResponse.Clear()
        objResponse.Charset = ""
        objResponse.ContentType = "application/vnd.ms-excel"
        objResponse.AddHeader("Content-Disposition", "attachment;filename=waterdata.xls")
        objResponse.Write(stringWrite.ToString)
        objResponse.End()
    End Sub

End Class
