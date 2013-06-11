Option Strict On
Imports nwisanalyst.Statistics
Imports System.Data

Public Class RootPrint
    Inherits System.Web.UI.Page
    Protected WithEvents lblStation As System.Web.UI.WebControls.Label
    Protected WithEvents lblVariable As System.Web.UI.WebControls.Label
    Protected WithEvents lblDateSpan As System.Web.UI.WebControls.Label
    Protected WithEvents imgGraph As System.Web.UI.WebControls.Image
    Protected WithEvents lblArithmeticMean As System.Web.UI.WebControls.Label
    Protected WithEvents lblGeometricMean As System.Web.UI.WebControls.Label
    Protected WithEvents lblMaximum As System.Web.UI.WebControls.Label
    Protected WithEvents lblMinimum As System.Web.UI.WebControls.Label
    Protected WithEvents lblStandardDeviation As System.Web.UI.WebControls.Label
    Protected WithEvents lblCoefficientOfVariation As System.Web.UI.WebControls.Label
    Protected WithEvents lbl10Percentile As System.Web.UI.WebControls.Label
    Protected WithEvents lbl25Percentile As System.Web.UI.WebControls.Label
    Protected WithEvents lblMedian As System.Web.UI.WebControls.Label
    Protected WithEvents lbl75Percentile As System.Web.UI.WebControls.Label
    Protected WithEvents lbl90Percentile As System.Web.UI.WebControls.Label
    Protected WithEvents lblNumberOfObservations As System.Web.UI.WebControls.Label

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mobjRootDefault As nwisanalyst._Default = CType(Context.Handler, nwisanalyst._Default)
        Dim objDataSet As DataSet = CType(Session("DataSet"), DataSet)

        If Not IsPostBack Then
            'Heading
            lblStation.Text = mobjRootDefault.Station
            lblVariable.Text = mobjRootDefault.Variable
            lblDateSpan.Text = mobjRootDefault.DateSpan

            'Graph
            If (mobjRootDefault.TimeSeries.Visible) Then
                mobjRootDefault.TimeSeries.Replot()
                imgGraph.ImageUrl = "graphs/TimeSeriesGraph.aspx"
            ElseIf (mobjRootDefault.Probability.Visible) Then
                mobjRootDefault.Probability.Replot()
                imgGraph.ImageUrl = "graphs/ProbabilityGraph.aspx"
            ElseIf (mobjRootDefault.Histogram.Visible) Then
                mobjRootDefault.Histogram.Replot()
                imgGraph.ImageUrl = "graphs/HistogramGraph.aspx"
            ElseIf (mobjRootDefault.BoxWhisker.Visible) Then
                mobjRootDefault.BoxWhisker.Replot()
                imgGraph.ImageUrl = "graphs/BoxWhiskerGraph.aspx"
            End If

            'Summary
            Dim objDataTable As DataTable = objDataSet.Tables.Item(0)
            lblArithmeticMean.Text = ArithmeticMean(objDataTable).ToString("G4")
            lblGeometricMean.Text = GeometricMean(objDataTable).ToString("G4")
            lblMaximum.Text = Maximum(objDataTable).ToString
            lblMinimum.Text = Minimum(objDataTable).ToString
            lblStandardDeviation.Text = StandardDeviation(objDataTable).ToString("G4")
            lblCoefficientOfVariation.Text = CoefficientOfVariation(objDataTable).ToString("###%")
            lbl10Percentile.Text = Percentile(objDataTable, 10).ToString("G4")
            lbl25Percentile.Text = LowerQuartile(objDataTable).ToString("G4")
            lblMedian.Text = Median(objDataTable).ToString("G4")
            lbl75Percentile.Text = UpperQuartile(objDataTable).ToString("G4")
            lbl90Percentile.Text = Percentile(objDataTable, 90).ToString("G4")
            lblNumberOfObservations.Text = Count(objDataTable).ToString
        End If
    End Sub

End Class
