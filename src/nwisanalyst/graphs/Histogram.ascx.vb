Imports System.Data

Public MustInherit Class GraphsHistogram
    Inherits System.Web.UI.UserControl
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

    Public Shared mobjDataTable As DataTable
    Public Shared mstrStationName As String
    Public Shared mstrVariableName As String
    Public Shared mobjOptions As nwisanalyst.OptionsPlotOptions

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PegoWeb.PeUserInterface.HotSpot.Data = True
    End Sub

    Public Sub Plot(ByRef objDataTable As DataTable, ByVal strStationName As String, ByVal strVariableName As String, ByRef objOptions As nwisanalyst.OptionsPlotOptions)
        mobjDataTable = objDataTable
        mstrStationName = strStationName
        mstrVariableName = strVariableName
        mobjOptions = objOptions
        PegoWeb.AlternateText = "Histogram of " & strVariableName & " at " & strStationName
        PegoWeb.ImageUrl = "graphs/HistogramGraph.aspx?" & Now.Ticks.ToString
    End Sub

    Public Sub Replot()
        PegoWeb.ImageUrl = "graphs/HistogramGraph.aspx?" & Now.Ticks.ToString
    End Sub

    Public Sub Clear()
        PegoWeb.AlternateText = "No graph loaded"
        PegoWeb.ImageUrl = "images/blankgraph.gif"
    End Sub
End Class
