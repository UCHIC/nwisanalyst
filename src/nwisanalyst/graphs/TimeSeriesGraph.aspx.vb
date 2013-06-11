Option Strict On
Imports Gigasoft.ProEssentials.Enums
Imports System.Data

Public Class GraphsTimeSeriesGraph
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not nwisanalyst.GraphsTimeSeries.mobjDataTable Is Nothing Then
            Plot(nwisanalyst.GraphsTimeSeries.mobjDataTable, nwisanalyst.GraphsTimeSeries.mstrStationName, nwisanalyst.GraphsTimeSeries.mstrVariableName, nwisanalyst.GraphsTimeSeries.mobjOptions)
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
        PesgoWeb.PeData.UsingXDataii = True   ' Double precision x data
        PesgoWeb.PeData.UsingYDataii = True   ' Double precision y data

        'Set date handling properties
        PesgoWeb.PeData.StartTime = Convert.ToDateTime(objDataTable.Compute("MIN(TSDateTime)", "")).ToOADate
        PesgoWeb.PeData.EndTime = Convert.ToDateTime(objDataTable.Compute("MAX(TSDateTime)", "")).ToOADate

        'Plot data points
        'Only plot data points if they are not censored.  If the values are censored set equal
        'to the null data value so that they are ignored.
        For i = 0 To objDataTable.Rows.Count - 1
            If Convert.ToString(objDataTable.Rows(i).Item("TSCensorCode")) = "<" Then
                PesgoWeb.PeData.Xii.Item(0, i) = PesgoWeb.PeData.NullDataValueX
                PesgoWeb.PeData.Yii.Item(0, i) = PesgoWeb.PeData.NullDataValue
            Else
                PesgoWeb.PeData.Xii.Item(0, i) = Convert.ToDateTime(objDataTable.Rows(i).Item("TSDateTime")).ToOADate
                PesgoWeb.PeData.Yii.Item(0, i) = Convert.ToDouble(objDataTable.Rows(i).Item("TSValue"))
            End If
        Next

        'Set graph properties
        PesgoWeb.PeData.DateTimeMode = True
        PesgoWeb.PeUserInterface.Allow.Zooming = AllowZooming.HorzAndVert
        PesgoWeb.PeGrid.Style = GridStyle.Dot
        PesgoWeb.PeGrid.Option.YearLabelType = YearLabelType.FourCharacters
        PesgoWeb.PePlot.Option.NullDataGaps = True
        PesgoWeb.PeGrid.Configure.AutoMinMaxPadding = 6

        'Set plotting method
        Select Case objOptions.PlottingMethod
            Case "line"
                PesgoWeb.PePlot.Method = SGraphPlottingMethod.Line
            Case "point"
                PesgoWeb.PePlot.Method = SGraphPlottingMethod.Point
            Case "both"
                PesgoWeb.PePlot.Method = SGraphPlottingMethod.PointsPlusLine
        End Select

        'Set control line
        If objOptions.ControlLineValue <> Nothing Then
            PesgoWeb.PeAnnotation.Line.YAxis.Item(0) = objOptions.ControlLineValue
            PesgoWeb.PeAnnotation.Line.YAxisColor.Item(0) = objOptions.ControlLineColor
            PesgoWeb.PeAnnotation.Line.YAxisType.Item(0) = LineAnnotationType.ThinSolid
            PesgoWeb.PeAnnotation.Line.YAxisText.Item(0) = objOptions.ControlLineLabel
            PesgoWeb.PeAnnotation.Line.TextSize = 100
            PesgoWeb.PeAnnotation.Line.YAxisShow = True
            PesgoWeb.PeAnnotation.Show = True
        End If

        'Set titles and labels
        PesgoWeb.PeString.MainTitle = strStationName
        PesgoWeb.PeString.SubTitle = String.Empty
        PesgoWeb.PeString.YAxisLabel = strVariableName
        PesgoWeb.PeString.XAxisLabel = "Date"
    End Sub
End Class
