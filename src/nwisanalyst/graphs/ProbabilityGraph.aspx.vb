Option Strict On
Imports Gigasoft.ProEssentials.Enums
Imports System.Data

Public Class GraphsProbabilityGraph
    Inherits System.Web.UI.Page
    Protected WithEvents PesgoWeb As Gigasoft.ProEssentials.PesgoWeb

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
        If Not nwisanalyst.GraphsProbability.mobjDataTable Is Nothing Then
            Plot(nwisanalyst.GraphsProbability.mobjDataTable, nwisanalyst.GraphsProbability.mstrStationName, nwisanalyst.GraphsProbability.mstrVariableName, nwisanalyst.GraphsProbability.mobjOptions)
        End If
    End Sub

    Public Sub Plot(ByRef objDataTable As DataTable, ByVal strStationName As String, ByVal strVariableName As String, ByRef objOptions As nwisanalyst.OptionsPlotOptions)
        Dim i As Integer

        'Verify that data exists in the table
        If objDataTable Is Nothing Then Exit Sub

        'Prepare images in memory
        PesgoWeb.PeConfigure.PrepareImages = True

        'Set dimensions
        PesgoWeb.Width = System.Web.UI.WebControls.Unit.Pixel(507)
        PesgoWeb.Height = System.Web.UI.WebControls.Unit.Pixel(390)
        PesgoWeb.PeData.Subsets = 1
        PesgoWeb.PeData.Points = objDataTable.Rows.Count

        'Control the X-axis
        PesgoWeb.PeGrid.Option.ShowXAxis = ShowAxis.LabelOnly
        PesgoWeb.PeGrid.Configure.ManualScaleControlX = ManualScaleControl.MinMax
        PesgoWeb.PeGrid.Configure.ManualMinX = -4
        PesgoWeb.PeGrid.Configure.ManualMaxX = 4
        For i = 0 To 20
            PesgoWeb.PeAnnotation.Line.XAxis(i) = Convert.ToDouble(Split(GetProbabilityLabel(i), "|")(1))
            PesgoWeb.PeAnnotation.Line.XAxisType(i) = LineAnnotationType.GridLine
            PesgoWeb.PeAnnotation.Line.XAxisText(i) = "|h" & Split(GetProbabilityLabel(i), "|")(0)
        Next

        'Plot data points
        PesgoWeb.PeData.UsingXDataii = True   ' Double precision x data
        PesgoWeb.PeData.UsingYDataii = True   ' Double precision y data
        PesgoWeb.PeData.Xii.Item(0, 0) = 0D
        PesgoWeb.PeData.Yii.Item(0, 0) = 0D
        For i = 0 To objDataTable.Rows.Count - 1
            PesgoWeb.PeData.Xii.Item(0, i) = CalculateXPosition(i / objDataTable.Rows.Count)
            PesgoWeb.PeData.Yii.Item(0, i) = Convert.ToDouble(objDataTable.Rows(i).Item("TSValue"))
        Next

        'Set graph properties
        PesgoWeb.PeUserInterface.Allow.Zooming = AllowZooming.HorzAndVert
        PesgoWeb.PeGrid.Style = GridStyle.Dot
        'PesgoWeb.PePlot.Method = SGraphPlottingMethod.PointsPlusBestFitLine
        PesgoWeb.PePlot.Method = SGraphPlottingMethod.Point
        PesgoWeb.PeAnnotation.Line.BottomMargin = "99"
        PesgoWeb.PeAnnotation.Show = True

        'Set titles and labels
        PesgoWeb.PeString.MainTitle = strStationName
        PesgoWeb.PeString.SubTitle = String.Empty
        PesgoWeb.PeString.XAxisLabel = "Cumulative Frequency < Stated Value %"
        PesgoWeb.PeString.YAxisLabel = strVariableName
    End Sub

    Private Function GetProbabilityLabel(ByVal index As Integer) As String
        'X-axis labels and values used to create a normal distribution
        'of the probabilities
        Select Case (index)
            Case 0 : Return "0.01|-3.892" 'label|x value
            Case 1 : Return "0.02|-3.5"
            Case 2 : Return "0.1|-3.095"
            Case 3 : Return "1|-2.323"
            Case 4 : Return "2|-2.055"
            Case 5 : Return "5|-1.645"
            Case 6 : Return "10|-1.282"
            Case 7 : Return "20|-0.842"
            Case 8 : Return "30|-0.524"
            Case 9 : Return "40|-0.254"
            Case 10 : Return "50|0"
            Case 11 : Return "60|0.254"
            Case 12 : Return "70|0.524"
            Case 13 : Return "80|0.842"
            Case 14 : Return "90|1.282"
            Case 15 : Return "95|1.645"
            Case 16 : Return "98|2.055"
            Case 17 : Return "99|2.323"
            Case 18 : Return "99.9|3.095"
            Case 19 : Return "99.98|3.5"
            Case 20 : Return "99.99|3.892"
        End Select
    End Function

    Private Function CalculateXPosition(ByVal freq As Double) As Double
        'Adapts the probabilities to the normal distribution
        Return 4.91 * (freq ^ 0.14 - (1.0# - freq) ^ 0.14)
    End Function
End Class
