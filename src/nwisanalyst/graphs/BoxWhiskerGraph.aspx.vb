Option Strict On
Imports Gigasoft.ProEssentials.Enums
Imports nwisanalyst.Statistics
Imports nwisanalyst.BoxPlot
Imports System.Data

Public Class GraphsBoxWhiskerGraph
    Inherits System.Web.UI.Page
    Protected WithEvents PesgoWeb As Gigasoft.ProEssentials.PesgoWeb

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

    Dim intAnnotationIndex As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not nwisanalyst.GraphsBoxWhisker.mobjDataTable Is Nothing Then
            Plot(nwisanalyst.GraphsBoxWhisker.mobjDataTable, nwisanalyst.GraphsBoxWhisker.mstrStationName, nwisanalyst.GraphsBoxWhisker.mstrVariableName, nwisanalyst.GraphsBoxWhisker.mobjOptions)
        End If
    End Sub

    Public Sub Plot(ByRef objDataTable As DataTable, ByVal strStationName As String, ByVal strVariableName As String, ByRef objOptions As nwisanalyst.OptionsPlotOptions)
        'Verify that data exists in the table
        If objDataTable Is Nothing Then Exit Sub

        'Prepare images in memory
        PesgoWeb.PeConfigure.PrepareImages = True

        'Set dimensions
        PesgoWeb.Width = System.Web.UI.WebControls.Unit.Pixel(507)
        PesgoWeb.Height = System.Web.UI.WebControls.Unit.Pixel(390)
        PesgoWeb.PeData.Subsets = 1
        PesgoWeb.PeData.UsingXDataii = True   ' Double precision x data
        PesgoWeb.PeData.UsingYDataii = True   ' Double precision y data

        'Control the X-axis
        PesgoWeb.PeGrid.Option.ShowXAxis = ShowAxis.LabelOnly
        PesgoWeb.PeGrid.Configure.ManualScaleControlX = ManualScaleControl.MinMax
        PesgoWeb.PeGrid.Configure.ManualMinX = 0
        PesgoWeb.PeGrid.Configure.ManualMaxX = 100

        'Control the Y-axis
        PesgoWeb.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.MinMax
        PesgoWeb.PeGrid.Configure.ManualMaxY = Maximum(objDataTable) + Range(objDataTable) * 0.05
        PesgoWeb.PeGrid.Configure.ManualMinY = Minimum(objDataTable) - Range(objDataTable) * 0.05

        'Plot according to type
        Select Case objOptions.BoxWhiskerType
            Case "monthly"
                PlotMonthly(objDataTable, objOptions)
            Case "seasonal"
                PlotSeasonal(objDataTable, objOptions)
            Case "yearly"
                PlotYearly(objDataTable, objOptions)
            Case "overall"
                PlotOverall(objDataTable)
        End Select

        'Set graph properties
        PesgoWeb.PeUserInterface.Allow.Zooming = AllowZooming.HorzAndVert
        PesgoWeb.PeGrid.Style = GridStyle.Dot
        PesgoWeb.PePlot.Method = SGraphPlottingMethod.Line
        PesgoWeb.PeAnnotation.Graph.TextDodge = 0
        PesgoWeb.PeAnnotation.InFront = False
        PesgoWeb.PeAnnotation.Show = True

        'Set titles and labels
        PesgoWeb.PeString.MainTitle = strStationName
        PesgoWeb.PeString.SubTitle = String.Empty
        PesgoWeb.PeString.YAxisLabel = strVariableName
    End Sub

    Private Sub PlotMonthly(ByRef objDataTable As DataTable, ByRef objOptions As nwisanalyst.OptionsPlotOptions)
        Dim i As Integer
        Dim dblX As Double
        Dim objDataRow As DataRow
        Dim objMonthDataTable As DataTable
        Dim objBoxPlot As nwisanalyst.BoxPlot
        Const intNumMonths As Integer = 12

        'Configure X-axis
        For i = 1 To intNumMonths
            PesgoWeb.PeAnnotation.Line.XAxis(i) = Convert.ToDouble(i * (PesgoWeb.PeGrid.Configure.ManualMaxX / (intNumMonths + 1)))
            PesgoWeb.PeAnnotation.Line.XAxisType(i) = LineAnnotationType.GridLine
            PesgoWeb.PeAnnotation.Line.XAxisText(i) = "|h" & MonthName(i)
        Next
        PesgoWeb.PeString.XAxisLabel = "Month"
        PesgoWeb.PeAnnotation.Line.BottomMargin = "99"

        'Plot data
        PesgoWeb.PeData.Points = 0
        For i = 1 To intNumMonths
            'Create data table for the month
            objMonthDataTable = objDataTable.Clone
            For Each objDataRow In objDataTable.Select("DateMonth = " & i.ToString, "TSValue")
                objMonthDataTable.ImportRow(objDataRow)
            Next

            If (objMonthDataTable.Rows.Count > 0) Then
                PesgoWeb.PeData.Points += 1

                'Set X-axis location
                dblX = Convert.ToDouble(i * (PesgoWeb.PeGrid.Configure.ManualMaxX / (intNumMonths + 1)))

                'Plot mean or median line
                PesgoWeb.PeData.Xii.Item(0, i) = dblX
                Select Case (objOptions.BoxWhiskerLine)
                    Case "mean"
                        PesgoWeb.PeData.Yii.Item(0, i) = Mean(objMonthDataTable)
                    Case "median"
                        PesgoWeb.PeData.Yii.Item(0, i) = Median(objMonthDataTable)
                End Select

                'Draw Box Plot
                objBoxPlot = New nwisanalyst.BoxPlot(intAnnotationIndex, dblX, 4D, objMonthDataTable, PesgoWeb)
                objBoxPlot.Draw()
                intAnnotationIndex = objBoxPlot.AnnotationIndex
            End If
        Next
    End Sub

    Private Sub PlotSeasonal(ByRef objDataTable As DataTable, ByRef objOptions As nwisanalyst.OptionsPlotOptions)
        Dim i As Integer
        Dim dblX As Double
        Dim objDataRow As DataRow
        Dim objSeasonDataTable As DataTable
        Dim objBoxPlot As nwisanalyst.BoxPlot
        Dim arrSeasons() As String = {"Winter", "Spring", "Summer", "Fall"}
        Dim arrSeasonMonths() As String = {"1,2,3", "4,5,6", "7,8,9", "10,11,12"}
        Const intNumSeasons As Integer = 4

        'Configure X-axis
        For i = 1 To intNumSeasons
            PesgoWeb.PeAnnotation.Line.XAxis(i) = Convert.ToDouble(i * (PesgoWeb.PeGrid.Configure.ManualMaxX / (intNumSeasons + 1)))
            PesgoWeb.PeAnnotation.Line.XAxisType(i) = LineAnnotationType.GridLine
            PesgoWeb.PeAnnotation.Line.XAxisText(i) = "|h" & arrSeasons(i - 1)
        Next
        PesgoWeb.PeString.XAxisLabel = "Season"
        PesgoWeb.PeAnnotation.Line.BottomMargin = "99"

        'Plot data
        PesgoWeb.PeData.Points = 0
        For i = 1 To intNumSeasons
            'Create data table for the season
            objSeasonDataTable = objDataTable.Clone
            For Each objDataRow In objDataTable.Select("DateMonth IN (" & arrSeasonMonths(i - 1) & ")", "TSValue")
                objSeasonDataTable.ImportRow(objDataRow)
            Next

            If (objSeasonDataTable.Rows.Count > 0) Then
                PesgoWeb.PeData.Points += 1

                'Set X-axis location
                dblX = Convert.ToDouble(i * (PesgoWeb.PeGrid.Configure.ManualMaxX / (intNumSeasons + 1)))

                'Plot mean or median line
                PesgoWeb.PeData.Xii.Item(0, i) = dblX
                Select Case (objOptions.BoxWhiskerLine)
                    Case "mean"
                        PesgoWeb.PeData.Yii.Item(0, i) = Mean(objSeasonDataTable)
                    Case "median"
                        PesgoWeb.PeData.Yii.Item(0, i) = Median(objSeasonDataTable)
                End Select

                'Draw Box Plot
                objBoxPlot = New nwisanalyst.BoxPlot(intAnnotationIndex, dblX, 4D, objSeasonDataTable, PesgoWeb)
                objBoxPlot.Draw()
                intAnnotationIndex = objBoxPlot.AnnotationIndex
            End If
        Next
    End Sub

    Private Sub PlotYearly(ByRef objDataTable As DataTable, ByRef objOptions As nwisanalyst.OptionsPlotOptions)
        Dim i As Integer
        Dim dblX As Double
        Dim dblWidth As Double = 4D
        Dim objDataRow As DataRow
        Dim objYearDataTable As DataTable
        Dim objBoxPlot As nwisanalyst.BoxPlot
        Dim intMaxYear As Integer = Convert.ToDateTime(objDataTable.Compute("MAX(TSDateTime)", "")).Year
        Dim intMinYear As Integer = Convert.ToDateTime(objDataTable.Compute("MIN(TSDateTime)", "")).Year
        Dim intNumYears As Integer = intMaxYear - intMinYear + 1
        If (intNumYears > 10) Then dblWidth = 4D / (intNumYears / 10D)

        'Configure X-axis
        For i = 1 To intNumYears
            PesgoWeb.PeAnnotation.Line.XAxis(i) = Convert.ToDouble(i * (PesgoWeb.PeGrid.Configure.ManualMaxX / (intNumYears + 1)))
            PesgoWeb.PeAnnotation.Line.XAxisType(i) = LineAnnotationType.GridLine
            PesgoWeb.PeAnnotation.Line.XAxisText(i) = "|B" & Convert.ToString(intMinYear + (i - 1))
        Next
        PesgoWeb.PeString.XAxisLabel = "Year"
        PesgoWeb.PeAnnotation.Line.BottomMargin = "9999"

        'Plot data
        PesgoWeb.PeData.Points = 0
        For i = 1 To intNumYears
            'Create data table for the season
            objYearDataTable = objDataTable.Clone
            For Each objDataRow In objDataTable.Select("TSDateTime > #1/1/" & Convert.ToString(intMinYear + (i - 1)) & "# AND TSDateTime < #12/31/" & Convert.ToString(intMinYear + (i - 1)) & "# AND TSValue >= 0", "TSValue")
                objYearDataTable.ImportRow(objDataRow)
            Next

            If (objYearDataTable.Rows.Count > 0) Then
                PesgoWeb.PeData.Points += 1

                'Set X-axis location
                dblX = Convert.ToDouble(i * (PesgoWeb.PeGrid.Configure.ManualMaxX / (intNumYears + 1)))

                'Plot mean or median line
                PesgoWeb.PeData.Xii.Item(0, i) = dblX
                Select Case (objOptions.BoxWhiskerLine)
                    Case "mean"
                        PesgoWeb.PeData.Yii.Item(0, i) = Mean(objYearDataTable)
                    Case "median"
                        PesgoWeb.PeData.Yii.Item(0, i) = Median(objYearDataTable)
                End Select

                'Draw Box Plot
                objBoxPlot = New nwisanalyst.BoxPlot(intAnnotationIndex, dblX, dblWidth, objYearDataTable, PesgoWeb)
                objBoxPlot.Draw()
                intAnnotationIndex = objBoxPlot.AnnotationIndex
            End If
        Next
    End Sub

    Private Sub PlotOverall(ByRef objDataTable As DataTable)
        Dim objDataRow As DataRow
        Dim objOverallDataTable As DataTable
        Dim objBoxPlot As nwisanalyst.BoxPlot
        Dim intMaxYear As Integer = Convert.ToDateTime(objDataTable.Compute("MAX(TSDateTime)", "")).Year
        Dim intMinYear As Integer = Convert.ToDateTime(objDataTable.Compute("MIN(TSDateTime)", "")).Year

        'Configure X-axis
        PesgoWeb.PeAnnotation.Line.XAxis(0) = 50
        PesgoWeb.PeAnnotation.Line.XAxisType(0) = LineAnnotationType.GridLine
        PesgoWeb.PeAnnotation.Line.XAxisText(0) = "|h" & intMinYear.ToString
        PesgoWeb.PeString.XAxisLabel = "Year"
        If (intMinYear <> intMaxYear) Then
            PesgoWeb.PeAnnotation.Line.XAxisText(0) &= " - " & intMaxYear.ToString
            PesgoWeb.PeString.XAxisLabel &= "s"
        End If
        PesgoWeb.PeAnnotation.Line.BottomMargin = "99"

        'Create data table
        objOverallDataTable = objDataTable.Clone
        For Each objDataRow In objDataTable.Select("", "TSValue")
            objOverallDataTable.ImportRow(objDataRow)
        Next

        'Plot data
        PesgoWeb.PeData.Points = 0
        If (objOverallDataTable.Rows.Count > 0) Then
            PesgoWeb.PeData.Points += 1

            'Draw Box Plot
            objBoxPlot = New nwisanalyst.BoxPlot(intAnnotationIndex, 50D, 10D, objOverallDataTable, PesgoWeb)
            objBoxPlot.Draw()
        End If
    End Sub
End Class
