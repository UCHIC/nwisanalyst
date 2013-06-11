Option Strict On
Imports nwisanalyst.Global
Imports nwisanalyst.Statistics
Imports System.Data

Public MustInherit Class OptionsSummary
    Inherits System.Web.UI.UserControl
    Protected WithEvents txtGeometricMean As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMaximum As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMinimum As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtStandardDeviation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCoefficientOfVariation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txt10Percentile As System.Web.UI.WebControls.TextBox
    Protected WithEvents txt25Percentile As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMedian As System.Web.UI.WebControls.TextBox
    Protected WithEvents txt75Percentile As System.Web.UI.WebControls.TextBox
    Protected WithEvents txt90Percentile As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumberOfObservations As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNumberCensoredObservations As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtArithmeticMean As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblMessage As System.Web.UI.WebControls.Label

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

    Private mobjObservations As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Public Sub Fill(ByRef objObservations As DataTable)
        Dim clsCDFunctions As New nwisanalyst.clsCensoredDataFunctions

        'Load data
        If Not objObservations Is Nothing Then
            mobjObservations = objObservations
        End If

        'Check to see if there are censored data
        If Convert.ToInt32(objObservations.Compute("Count(TSValue)", "TSValue = -9999")) > 0 Then
            'There are censored data so modify the values in the data table before calling the statistical functions
            txtNumberCensoredObservations.Text = Convert.ToString(objObservations.Compute("Count(TSValue)", "TSValue = -9999"))

            'Check to make sure that there are at least 2 non-censored observations or else we can't compute the statistics
            If Convert.ToInt32(objObservations.Compute("Count(TSValue)", "TSValue > 0")) >= 2 Then
                'There are at least two values above the dection limit, so call the censored data functions
                objObservations = clsCDFunctions.CalculateCensoredData(objObservations)
                txtMinimum.Text = "BDL"
            ElseIf Convert.ToInt32(objObservations.Compute("Count(TSValue)", "TSValue > 0")) = 1 Then
                'There is only one value above the detection limit, so just report what I can
                txtArithmeticMean.Text = String.Empty
                txtGeometricMean.Text = String.Empty
                txtMinimum.Text = "BDL"
                txtMaximum.Text = Convert.ToString(objObservations.Compute("Max(TSValue)", "TSValue > 0"))
                txtStandardDeviation.Text = String.Empty
                txtCoefficientOfVariation.Text = String.Empty
                txt10Percentile.Text = String.Empty
                txt25Percentile.Text = String.Empty
                txtMedian.Text = String.Empty
                txt75Percentile.Text = String.Empty
                txt90Percentile.Text = String.Empty
                txtNumberOfObservations.Text = Count(objObservations).ToString
                Exit Sub
            Else
                'All of the values are censored
                txtArithmeticMean.Text = String.Empty
                txtGeometricMean.Text = String.Empty
                txtMinimum.Text = "BDL"
                txtMaximum.Text = "BDL"
                txtStandardDeviation.Text = String.Empty
                txtCoefficientOfVariation.Text = String.Empty
                txt10Percentile.Text = String.Empty
                txt25Percentile.Text = String.Empty
                txtMedian.Text = String.Empty
                txt75Percentile.Text = String.Empty
                txt90Percentile.Text = String.Empty
                txtNumberOfObservations.Text = Count(objObservations).ToString
                Exit Sub
            End If

        Else
            'There are no censored data, so just call the statistical functions and fill in the form
            txtMinimum.Text = Minimum(objObservations).ToString("G4")
            txtNumberCensoredObservations.Text = "0"
        End If

        'Fill the rest of the form
        txtArithmeticMean.Text = ArithmeticMean(objObservations).ToString("G4")
        txtGeometricMean.Text = GeometricMean(objObservations).ToString("G4")
        txtMaximum.Text = Maximum(objObservations).ToString("G4")
        txtStandardDeviation.Text = StandardDeviation(objObservations).ToString("G4")
        txtCoefficientOfVariation.Text = CoefficientOfVariation(objObservations).ToString("##0%")
        txt10Percentile.Text = Percentile(objObservations, 10).ToString("G4")
        txt25Percentile.Text = LowerQuartile(objObservations).ToString("G4")
        txtMedian.Text = Median(objObservations).ToString("G4")
        txt75Percentile.Text = UpperQuartile(objObservations).ToString("G4")
        txt90Percentile.Text = Percentile(objObservations, 90).ToString("G4")
        txtNumberOfObservations.Text = Count(objObservations).ToString

    End Sub

    Public Sub Clear()
        'Clear form
        txtArithmeticMean.Text = String.Empty
        txtGeometricMean.Text = String.Empty
        txtMaximum.Text = String.Empty
        txtMinimum.Text = String.Empty
        txtStandardDeviation.Text = String.Empty
        txtCoefficientOfVariation.Text = String.Empty
        txt10Percentile.Text = String.Empty
        txt25Percentile.Text = String.Empty
        txtMedian.Text = String.Empty
        txt75Percentile.Text = String.Empty
        txt90Percentile.Text = String.Empty
        txtNumberOfObservations.Text = String.Empty
        txtNumberCensoredObservations.Text = String.Empty
    End Sub

    Public Sub Message(ByVal strMessage As String)
        lblMessage.Text = strMessage
    End Sub
End Class
