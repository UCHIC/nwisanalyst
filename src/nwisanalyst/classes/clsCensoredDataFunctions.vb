Option Strict On

Imports System.Data

Public Class clsCensoredDataFunctions

    Public Function CalculateCensoredData(ByRef table As DataTable) As DataTable
        'Modify the censored data in the table to fit the normal distribution curve
        'censored data has a value = -9999, but also must kill values that are equal to zero because the screw up the analysis, so I assume zero values are censored.
        'Inputs:  table ->the dataTable of data to move through
        'Outputs: none
        Dim i As Integer 'counter
        Dim freq As Double

        'get all non-censored data
        Dim numCensoredData As Integer = Convert.ToInt32(table.Compute("Count(TSValue)", "TSValue <= 0")) 'the number of values in m_Table that are <= 0 (won't be viewed by the user unless the user specifies to)
        Dim numNonCensoredData As Integer = table.Rows.Count - numCensoredData 'the number of values in m_Table that are not censored (will be viewed by the user so aren't censored)
        Dim nonCensoredData As DataRow() = table.Select("TSValue > 0") 'the selection of nonCensored data from table
        Dim count As Integer = nonCensoredData.GetLength(0) 'the # of fields/columns in the first row of nonCensoredData
        Dim xp(count - 1) As Double 'an array to create a table of values -> holds the normal-distribution value for the data
        Dim yp(count - 1) As Double 'an array to create a table of value -> holds the log values for the data

        'calculate xP and yP from non-censored data
        For i = 0 To count - 1
            xp(i) = (CDbl((i + 1) + numCensoredData) - 0.375) / (CDbl(numNonCensoredData + numCensoredData) + 1.0 - 0.375 * 2.0)
            'Calculate the z score
            freq = xp(i)
            xp(i) = 4.91 * (freq ^ 0.14 - (1.0 - freq) ^ 0.14)
            yp(i) = Math.Log10(Convert.ToDouble(nonCensoredData(i).Item("TSValue")))
        Next i
        'release nonCensoredData resources
        nonCensoredData = Nothing

        Dim slp As Double 'the slope of the line
        Dim intercept As Double 'the intercept for the line
        'calculate the straight line of the non-censored data
        CalculateStraightLine(xp, yp, slp, intercept)

        'recalculate stats with all data
        count = table.Rows.Count
        ReDim xp(count - 1)
        ReDim yp(count - 1)

        'calculate xP and yP with all data
        For i = 0 To count - 1
            xp(i) = (CDbl(i + 1) - 0.375) / (CDbl(numNonCensoredData + numCensoredData) + 1.0 - 0.375 * 2.0)
            'Calculate the z score
            freq = xp(i)
            xp(i) = 4.91 * (freq ^ 0.14 - (1.0 - freq) ^ 0.14)

            If (Convert.ToDouble(table.Rows(i).Item("TSValue")) > 0) Then
                yp(i) = Convert.ToDouble(table.Rows(i).Item("TSValue"))
            Else
                yp(i) = intercept + slp * xp(i)
                yp(i) = 10 ^ (yp(i))
            End If
        Next i

        'fill the table with the new info
        For i = 0 To count - 1
            table.Rows(i).Item("TSValue") = yp(i)
        Next i

        Return table

    End Function

    'Private Function CalculateZScore(ByVal freq As Double) As Double
    '    'Caculates the position along the x-axis to place the dot
    '    'Based on a normal curve distribution
    '    'NOTE: Code is from Dr. Stevens
    '    'Inputs:  freq -> used to calculate the position so has a normal distribution look 
    '    'Outputs: Double -> the x-position to plot the point at

    '    Try
    '        Return 4.91 * (freq ^ 0.14 - (1.0 - freq) ^ 0.14)
    '    Catch ex As System.Exception
    '        ShowErrorBox("CalculateZscore()", ex.Message)
    '    End Try
    'End Function

    Private Sub CalculateStraightLine(ByVal x() As Double, ByVal y() As Double, ByRef slp As Double, ByRef intercept As Double)
        'Calculates the best fit line from the array of points.
        'Inputs:  x() -> the array of x-values
        '         y() -> the array of y-values
        '         slp -> (ByRef) the slope of the best fit line (this is calculated and returned)
        '         intercept -> (ByRef) the y-intercept of the best fit line (this is calculated and returned)
        'Outputs: slp -> (ByRef) the slope of the best fit line, "m" in y = mx + b
        '         intercept -> (ByRef) the y-interspt of the best fit line, "b" in y = mx + b
        Dim i As Integer 'counter
        Dim xSum As Double 'sum so far of the x-values in x()
        Dim ySum As Double 'sum so far of the y-values in y()
        Dim xAvg As Double 'the average value of the x-values in x()
        Dim yAvg As Double 'the average value of the y-values in y()

        'make sure x,y have data
        If x.Length = 0 Or y.Length = 0 Then Exit Sub

        'find the average x and y
        For i = 0 To x.Length - 1
            xSum += x(i)
            ySum += y(i)
        Next
        xAvg = xSum / x.Length
        yAvg = ySum / y.Length

        xSum = 0
        ySum = 0
        For i = LBound(y) To UBound(y)
            xSum += (x(i) - xAvg) * (x(i) - xAvg)
            ySum += (y(i) - yAvg) * (x(i) - xAvg)
        Next i

        'calculate the slope of the line
        If (xSum <> 0) Then
            slp = ySum / xSum
        Else
            slp = 0
        End If

        'calculate the intercept of the line, y = mx + b
        intercept = yAvg - slp * xAvg
    End Sub

End Class
