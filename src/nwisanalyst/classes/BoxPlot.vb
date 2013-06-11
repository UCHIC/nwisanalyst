Option Strict On
Imports Gigasoft.ProEssentials.Enums
Imports System.Data
Imports System.Drawing

Public Class BoxPlot
    Private mintAnnotationIndex As Integer 'Annotation counter for drawing box plot
    Private mdblX As Double 'X-axis location of the box plot
    Private mdblWidth As Double 'Width of the box plot
    Private mobjDataTable As DataTable 'Table containing the box plot data
    Private mobjPesgoWeb As Gigasoft.ProEssentials.PesgoWeb 'Box plot graph

    ReadOnly Property AnnotationIndex() As Integer
        Get
            Return mintAnnotationIndex
        End Get
    End Property

    Public Sub New(ByVal intAnnotationIndex As Integer, ByVal dblX As Double, ByVal dblWidth As Double, ByRef objDataTable As DataTable, ByRef objPesgoWeb As Gigasoft.ProEssentials.PesgoWeb)
        mintAnnotationIndex = intAnnotationIndex
        mdblX = dblX
        mdblWidth = dblWidth
        mobjDataTable = objDataTable
        mobjPesgoWeb = objPesgoWeb
    End Sub

    Public Sub Draw()
        DrawRectangles()
        DrawMedian()
        DrawWhiskers()
        DrawMean()
        DrawConfidenceInterval()
        DrawOutliers()
    End Sub

    Private Sub DrawRectangles()
        Dim intIndex As Integer

        'Upper Rectangle
        With mobjPesgoWeb.PeAnnotation.Graph
            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.TopLeft
            .X.Item(intIndex) = mdblX - mdblWidth / 2
            .Y.Item(intIndex) = Statistics.UpperQuartile(mobjDataTable)

            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.BottomRight
            .X.Item(intIndex) = mdblX + mdblWidth / 2
            .Y.Item(intIndex) = Statistics.UpperConfidenceLimit(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.FromArgb(200, 200, 200)
            .Type.Item(intIndex) = GraphAnnotationType.RectFill

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.RectThin
        End With

        'Lower Rectangle
        With mobjPesgoWeb.PeAnnotation.Graph
            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.TopLeft
            .X.Item(intIndex) = mdblX - mdblWidth / 2
            .Y.Item(intIndex) = Statistics.LowerConfidenceLimit(mobjDataTable)

            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.BottomRight
            .X.Item(intIndex) = mdblX + mdblWidth / 2
            .Y.Item(intIndex) = Statistics.LowerQuartile(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.FromArgb(200, 200, 200)
            .Type.Item(intIndex) = GraphAnnotationType.RectFill

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.RectThin
        End With
    End Sub

    Private Sub DrawMedian()
        Dim intIndex As Integer

        With mobjPesgoWeb.PeAnnotation.Graph
            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.StartPoly
            .X.Item(intIndex) = mdblX - mdblWidth / 2
            .Y.Item(intIndex) = Statistics.UpperConfidenceLimit(mobjDataTable)

            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.AddPolyPoint
            .X.Item(intIndex) = mdblX - mdblWidth / 4
            .Y.Item(intIndex) = Statistics.Median(mobjDataTable)

            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.AddPolyPoint
            .X.Item(intIndex) = mdblX - mdblWidth / 2
            .Y.Item(intIndex) = Statistics.LowerConfidenceLimit(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.EndPolyLineThin
            .X.Item(intIndex) = mdblX + mdblWidth / 2
            .Y.Item(intIndex) = Statistics.LowerConfidenceLimit(mobjDataTable)

            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.StartPoly
            .X.Item(intIndex) = mdblX + mdblWidth / 2
            .Y.Item(intIndex) = Statistics.UpperConfidenceLimit(mobjDataTable)

            intIndex = NextIndex()
            .Type.Item(intIndex) = GraphAnnotationType.AddPolyPoint
            .X.Item(intIndex) = mdblX + mdblWidth / 4
            .Y.Item(intIndex) = Statistics.Median(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.EndPolyLineThin
            .X.Item(intIndex) = mdblX + mdblWidth / 2
            .Y.Item(intIndex) = Statistics.LowerConfidenceLimit(mobjDataTable)
        End With
    End Sub

    Private Sub DrawWhiskers()
        Dim intIndex As Integer

        'Upper Whisker
        With mobjPesgoWeb.PeAnnotation.Graph
            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.ThinSolidLine
            .X.Item(intIndex) = mdblX
            .Y.Item(intIndex) = Statistics.UpperQuartile(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.LineContinue
            .X.Item(intIndex) = mdblX
            .Y.Item(intIndex) = Statistics.UpperAdjacent(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.ThinSolidLine
            .X.Item(intIndex) = mdblX + mdblWidth / 4D
            .Y.Item(intIndex) = Statistics.UpperAdjacent(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.LineContinue
            .X.Item(intIndex) = mdblX - mdblWidth / 4D
            .Y.Item(intIndex) = Statistics.UpperAdjacent(mobjDataTable)
        End With

        'Lower Whisker
        With mobjPesgoWeb.PeAnnotation.Graph
            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.ThinSolidLine
            .X.Item(intIndex) = mdblX
            .Y.Item(intIndex) = Statistics.LowerQuartile(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.LineContinue
            .X.Item(intIndex) = mdblX
            .Y.Item(intIndex) = Statistics.LowerAdjacent(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.ThinSolidLine
            .X.Item(intIndex) = mdblX + mdblWidth / 4D
            .Y.Item(intIndex) = Statistics.LowerAdjacent(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Black
            .Type.Item(intIndex) = GraphAnnotationType.LineContinue
            .X.Item(intIndex) = mdblX - mdblWidth / 4D
            .Y.Item(intIndex) = Statistics.LowerAdjacent(mobjDataTable)
        End With
    End Sub

    Private Sub DrawMean()
        Dim intIndex As Integer

        With mobjPesgoWeb.PeAnnotation.Graph
            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Red
            .Type.Item(intIndex) = GraphAnnotationType.SmallUpTriangleSolid
            .X.Item(intIndex) = mdblX
            .Y.Item(intIndex) = Statistics.Mean(mobjDataTable)
        End With
    End Sub

    Private Sub DrawConfidenceInterval()
        Dim intIndex As Integer

        With mobjPesgoWeb.PeAnnotation.Graph
            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Red
            .Type.Item(intIndex) = GraphAnnotationType.ThinSolidLine
            .X.Item(intIndex) = mdblX
            .Y.Item(intIndex) = Statistics.UpperConfidenceInterval(mobjDataTable)

            intIndex = NextIndex()
            .Color.Item(intIndex) = Color.Red
            .Type.Item(intIndex) = GraphAnnotationType.LineContinue
            .X.Item(intIndex) = mdblX
            .Y.Item(intIndex) = Statistics.LowerConfidenceInterval(mobjDataTable)
        End With
    End Sub

    Private Sub DrawOutliers()
        Dim intIndex As Integer
        Dim objDataRow As DataRow

        For Each objDataRow In mobjDataTable.Select("TSValue > " & Statistics.UpperAdjacent(mobjDataTable).ToString & " OR TSValue < " & Statistics.LowerAdjacent(mobjDataTable).ToString, "")
            With mobjPesgoWeb.PeAnnotation.Graph
                intIndex = NextIndex()
                .Color.Item(intIndex) = Color.Black
                .Type.Item(intIndex) = GraphAnnotationType.SmallDotSolid
                .X.Item(intIndex) = mdblX
                .Y.Item(intIndex) = Convert.ToDouble(objDataRow.Item("TSValue"))
            End With
        Next
    End Sub

    Private Function NextIndex() As Integer
        mintAnnotationIndex += 1
        Return mintAnnotationIndex
    End Function
End Class
