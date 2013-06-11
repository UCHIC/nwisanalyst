Imports System.Drawing

Public MustInherit Class OptionsPlotOptions
    Inherits System.Web.UI.UserControl
    Protected WithEvents rblTimeSeries As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents lnkApply As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkCancel As System.Web.UI.WebControls.LinkButton
    Protected WithEvents rblBoxWhiskerType As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents rblBoxWhiskerLine As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txtLabel As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValue As System.Web.UI.WebControls.TextBox
    Protected WithEvents cboColor As System.Web.UI.WebControls.DropDownList

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

    ReadOnly Property PlottingMethod() As String
        Get
            Return rblTimeSeries.SelectedItem.Value
        End Get
    End Property

    ReadOnly Property ControlLineLabel() As String
        Get
            Return txtLabel.Text
        End Get
    End Property

    ReadOnly Property ControlLineValue() As Double
        Get
            If Trim(txtValue.Text) <> String.Empty Then
                Return Convert.ToDouble(txtValue.Text)
            Else
                Return Nothing
            End If
        End Get
    End Property

    ReadOnly Property ControlLineColor() As Color
        Get
            Return Color.FromName(cboColor.SelectedItem.Text)
        End Get
    End Property

    ReadOnly Property BoxWhiskerType() As String
        Get
            Return rblBoxWhiskerType.SelectedValue
        End Get
    End Property

    ReadOnly Property BoxWhiskerLine() As String
        Get
            Return rblBoxWhiskerLine.SelectedValue
        End Get
    End Property

    Private Sub lnkApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkApply.Click
        CType(Page, _Default).TimeSeries.mobjOptions = Me
        CType(Page, _Default).TimeSeries.Replot()
    End Sub

    Private Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
        txtLabel.Text = String.Empty
        txtValue.Text = String.Empty
        cboColor.SelectedIndex = -1
        CType(Page, _Default).TimeSeries.mobjOptions = Me
        CType(Page, _Default).TimeSeries.Replot()
    End Sub

    Private Sub rblTimeSeries_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblTimeSeries.SelectedIndexChanged
        CType(Page, _Default).TimeSeries.mobjOptions = Me
        CType(Page, _Default).TimeSeries.Replot()
    End Sub

    Private Sub rblBoxWhiskerType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblBoxWhiskerType.SelectedIndexChanged
        CType(Page, _Default).BoxWhisker.mobjOptions = Me
        CType(Page, _Default).BoxWhisker.Replot()
    End Sub

    Private Sub rblBoxWhiskerLine_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblBoxWhiskerLine.SelectedIndexChanged
        CType(Page, _Default).BoxWhisker.mobjOptions = Me
        CType(Page, _Default).BoxWhisker.Replot()
    End Sub
End Class
