Option Strict On
Imports Gigasoft.ProEssentials.Enums
Imports System.Data

Public Class GraphsHistogramGraph
    Inherits System.Web.UI.Page
    Protected WithEvents PegoWeb As Gigasoft.ProEssentials.PegoWeb

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
        If Not nwisanalyst.GraphsHistogram.mobjDataTable Is Nothing Then
            Plot(nwisanalyst.GraphsHistogram.mobjDataTable, nwisanalyst.GraphsHistogram.mstrStationName, nwisanalyst.GraphsHistogram.mstrVariableName, nwisanalyst.GraphsHistogram.mobjOptions)
        End If
    End Sub

    Public Sub Plot(ByRef objDataTable As DataTable, ByVal strStationName As String, ByVal strVariableName As String, ByRef objOptions As nwisanalyst.OptionsPlotOptions)
        Dim i As Integer

        'Verify that data exists in the table
        If objDataTable Is Nothing Then Exit Sub

        'Prepare images in memory
        PegoWeb.PeConfigure.PrepareImages = True

        'Set dimensions
        PegoWeb.Width = System.Web.UI.WebControls.Unit.Pixel(507)
        PegoWeb.Height = System.Web.UI.WebControls.Unit.Pixel(390)
        PegoWeb.PeData.Subsets = 1
        PegoWeb.PeData.Points = objDataTable.Rows.Count
        PegoWeb.PeData.UsingYDataii = True   ' Double precision y data

        'Plot data points
        For i = 0 To objDataTable.Rows.Count - 1
            PegoWeb.PeData.Yii.Item(0, i) = Convert.ToDouble(objDataTable.Rows(i).Item("TSValue"))
        Next

        'Set graph properties
        PegoWeb.PeUserInterface.Allow.Zooming = AllowZooming.HorzAndVert
        PegoWeb.PeGrid.Style = GridStyle.Dot
        PegoWeb.PePlot.Method = GraphPlottingMethod.Histogram

        'Set titles and labels
        PegoWeb.PeString.MainTitle = strStationName
        PegoWeb.PeString.SubTitle = String.Empty
        PegoWeb.PeString.YAxisLabel = strVariableName
    End Sub
End Class
