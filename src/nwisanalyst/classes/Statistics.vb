Option Strict On

Imports System.Data

Public Class Statistics
    Shared Function ArithmeticMean(ByRef objDataTable As DataTable) As Double
        Return Convert.ToDouble(objDataTable.Compute("Avg(TSValue)", ""))
    End Function

    Shared Function GeometricMean(ByRef objDataTable As DataTable) As Double
        Dim dblTotal As Double = 0
        Dim objDataRow As DataRow
        For Each objDataRow In objDataTable.Rows
            If Convert.ToDouble(objDataRow.Item("TSValue")) > 0 Then
                dblTotal += Math.Log10(Convert.ToDouble(objDataRow.Item("TSValue")))
            End If
        Next
        Return 10 ^ (dblTotal / Count(objDataTable))
    End Function

    Shared Function Mean(ByRef objDataTable As DataTable) As Double
        Return ArithmeticMean(objDataTable)
    End Function

    Shared Function Median(ByRef objDataTable As DataTable) As Double
        If (Count(objDataTable) Mod 2 = 0) Then
            Dim intRow As Integer = Convert.ToInt32(Math.Floor(Count(objDataTable) * 0.5))
            Return (Convert.ToDouble(objDataTable.Rows(intRow).Item("TSValue")) + Convert.ToDouble(objDataTable.Rows(intRow - 1).Item("TSValue"))) / 2
        Else
            Return Percentile(objDataTable, 50)
        End If
    End Function

    Shared Function Minimum(ByRef objDataTable As DataTable) As Double
        Return Convert.ToDouble(objDataTable.Compute("MIN(TSValue)", ""))
    End Function

    Shared Function Maximum(ByRef objDataTable As DataTable) As Double
        Return Convert.ToDouble(objDataTable.Compute("MAX(TSValue)", ""))
    End Function

    Shared Function Range(ByRef objDataTable As DataTable) As Double
        Return Maximum(objDataTable) - Minimum(objDataTable)
    End Function

    Shared Function UpperQuartile(ByRef objDataTable As DataTable) As Double
        Return Percentile(objDataTable, 75)
    End Function

    Shared Function LowerQuartile(ByRef objDataTable As DataTable) As Double
        Return Percentile(objDataTable, 25)
    End Function

    Shared Function InterquartileRange(ByRef objDataTable As DataTable) As Double
        Return UpperQuartile(objDataTable) - LowerQuartile(objDataTable)
    End Function

    Shared Function UpperAdjacent(ByRef objDataTable As DataTable) As Double
        If (UpperQuartile(objDataTable) + InterquartileRange(objDataTable) * 1.5 > Maximum(objDataTable)) Then
            Return Maximum(objDataTable)
        Else
            Return UpperQuartile(objDataTable) + InterquartileRange(objDataTable) * 1.5
        End If
    End Function

    Shared Function LowerAdjacent(ByRef objDataTable As DataTable) As Double
        If (LowerQuartile(objDataTable) - InterquartileRange(objDataTable) * 1.5 < Minimum(objDataTable)) Then
            Return Minimum(objDataTable)
        Else
            Return LowerQuartile(objDataTable) - InterquartileRange(objDataTable) * 1.5
        End If
    End Function

    Shared Function UpperConfidenceLimit(ByRef objDataTable As DataTable) As Double
        Return Median(objDataTable) + StandardDeviation(objDataTable) / Math.Sqrt(Count(objDataTable))
    End Function

    Shared Function LowerConfidenceLimit(ByRef objDataTable As DataTable) As Double
        Return Median(objDataTable) - StandardDeviation(objDataTable) / Math.Sqrt(Count(objDataTable))
    End Function

    Shared Function UpperConfidenceInterval(ByRef objDataTable As DataTable) As Double
        Return Mean(objDataTable) + StandardDeviation(objDataTable) / Math.Sqrt(Count(objDataTable))
    End Function

    Shared Function LowerConfidenceInterval(ByRef objDataTable As DataTable) As Double
        Return Mean(objDataTable) - StandardDeviation(objDataTable) / Math.Sqrt(Count(objDataTable))
    End Function

    Shared Function Percentile(ByRef objDataTable As DataTable, ByVal intPercentile As Integer) As Double
        Dim intRow As Integer = Convert.ToInt32(Math.Floor(Count(objDataTable) * (intPercentile / 100)))
        Return Convert.ToDouble(objDataTable.Rows(intRow).Item("TSValue"))
    End Function

    Shared Function StandardDeviation(ByRef objDataTable As DataTable) As Double
        If objDataTable.Rows.Count > 1 Then
            Return Convert.ToDouble(objDataTable.Compute("STDEV(TSValue)", ""))
        Else
            Return 0
        End If
    End Function

    Shared Function CoefficientOfVariation(ByRef objDataTable As DataTable) As Double
        If ArithmeticMean(objDataTable) <> 0 Then
            Return StandardDeviation(objDataTable) / ArithmeticMean(objDataTable)
        Else
            Return 0
        End If
    End Function

    Shared Function Count(ByRef objDataTable As DataTable) As Integer
        Return objDataTable.Rows.Count
    End Function
End Class