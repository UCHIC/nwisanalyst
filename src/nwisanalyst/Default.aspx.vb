Option Strict On
Imports System.Xml
Imports System.Data
Public Class _Default
    Inherits System.Web.UI.Page

    Protected WithEvents frmAnalyzer As System.Web.UI.HtmlControls.HtmlForm
    Protected WithEvents mnuFilePrint As System.Web.UI.WebControls.HyperLink
    Protected WithEvents mnuFilePrintAction As System.Web.UI.WebControls.LinkButton
    Protected WithEvents mnuFileExit As System.Web.UI.WebControls.HyperLink
    Protected WithEvents mnuGraphPlot As System.Web.UI.WebControls.LinkButton
    Protected WithEvents mnuGraphClear As System.Web.UI.WebControls.LinkButton
    Protected WithEvents mnuDataView As System.Web.UI.WebControls.HyperLink
    Protected WithEvents mnuDataViewAction As System.Web.UI.WebControls.LinkButton
    Protected WithEvents mnuDataExport As System.Web.UI.WebControls.LinkButton
    Protected WithEvents mnuHelpDescription As System.Web.UI.WebControls.HyperLink
    Protected WithEvents mnuHelpTutorial As System.Web.UI.WebControls.HyperLink
    Protected WithEvents miPrint As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents miExit As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents miPlot As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents miClear As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents miView As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents miExport As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents miDescription As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents miTutorial As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lnkTimeSeries As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkProbability As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkHistogram As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkBoxWhisker As System.Web.UI.WebControls.LinkButton
    Protected WithEvents tabTimeSeries As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents tabProbability As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents tabHistogram As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents tabBoxWhisker As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents lnkSummary As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkPlotOptions As System.Web.UI.WebControls.LinkButton
    Protected WithEvents tabSummary As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents tabPlotOptions As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents txtStation As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblStation As System.Web.UI.WebControls.Label
    Protected WithEvents ddlDatabase As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtVariable As System.Web.UI.WebControls.TextBox
    Protected WithEvents reqVariable As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblVariable As System.Web.UI.WebControls.Label
    Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEndDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnPlotGraph As System.Web.UI.WebControls.Button
    Protected WithEvents btnClearGraph As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearchStations As System.Web.UI.WebControls.LinkButton
    Protected WithEvents btnSearchVariables As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblStationError As System.Web.UI.WebControls.Label
    Protected WithEvents objTimeSeries As GraphsTimeSeries
    Protected WithEvents objProbability As GraphsProbability
    Protected WithEvents objHistogram As GraphsHistogram
    Protected WithEvents objBoxWhisker As GraphsBoxWhisker
    Protected WithEvents objSummary As OptionsSummary
    Protected WithEvents objPlotOptions As OptionsPlotOptions

    Private mobjGraphs As New Collection
    Private mobjOptions As New Collection
    Protected WithEvents reqStation As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents regStation As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents cmpStartDate As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents cmpEndDate As System.Web.UI.WebControls.CompareValidator
    Protected WithEvents regVariable As System.Web.UI.WebControls.RegularExpressionValidator
    Private mobjDataSet As DataSet

    ReadOnly Property Station() As String
        Get
            Return lblStation.Text
        End Get
    End Property

    ReadOnly Property Variable() As String
        Get
            Return lblVariable.Text
        End Get
    End Property

    ReadOnly Property DateSpan() As String
        Get
            Dim dtmEndDate As Date
            If txtEndDate.Text <> String.Empty Then
                dtmEndDate = Convert.ToDateTime(txtEndDate.Text)
            Else
                dtmEndDate = Now
            End If
            Dim dtmStartDate As Date = Convert.ToDateTime(txtStartDate.Text)
            Return dtmStartDate.ToString("MMMM d, yyyy") & " &#150; " & dtmEndDate.ToString("MMMM d, yyyy")
        End Get
    End Property

    ReadOnly Property TimeSeries() As GraphsTimeSeries
        Get
            Return objTimeSeries
        End Get
    End Property

    ReadOnly Property Probability() As GraphsProbability
        Get
            Return objProbability
        End Get
    End Property

    ReadOnly Property Histogram() As GraphsHistogram
        Get
            Return objHistogram
        End Get
    End Property

    ReadOnly Property BoxWhisker() As GraphsBoxWhisker
        Get
            Return objBoxWhisker
        End Get
    End Property

    ReadOnly Property Summary() As OptionsSummary
        Get
            Return objSummary
        End Get
    End Property

    ReadOnly Property Options() As OptionsPlotOptions
        Get
            Return objPlotOptions
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim blnReplot As Boolean = False
        If Session("DataSet") Is Nothing Then
            'Refresh the session
            Session.Clear()
            SessionFunctions.Load()
            If IsPostBack Then blnReplot = True
        End If

        If Not IsPostBack Then
            'Main Menu
            ActivateMenuItem(CType(mnuFilePrint, WebControl), "fileMenu", miPrint)
            ActivateMenuItem(CType(mnuFileExit, WebControl), "fileMenu", miExit)
            ActivateMenuItem(CType(mnuGraphPlot, WebControl), "graphMenu", miPlot)
            ActivateMenuItem(CType(mnuGraphClear, WebControl), "graphMenu", miClear)
            ActivateMenuItem(CType(mnuDataView, WebControl), "dataMenu", miView)
            ActivateMenuItem(CType(mnuDataExport, WebControl), "dataMenu", miExport)
            ActivateMenuItem(CType(mnuHelpDescription, WebControl), "helpMenu", miDescription)
            ActivateMenuItem(CType(mnuHelpTutorial, WebControl), "helpMenu", miTutorial)





            btnClearGraph_Click(sender, e)
        End If

        'Fill the graph tabs collection
        Dim arrTimeSeries() As Object = {lnkTimeSeries, tabTimeSeries, objTimeSeries}
        Dim arrProbability() As Object = {lnkProbability, tabProbability, objProbability}
        Dim arrHistogram() As Object = {lnkHistogram, tabHistogram, objHistogram}
        Dim arrBoxWhisker() As Object = {lnkBoxWhisker, tabBoxWhisker, objBoxWhisker}
        mobjGraphs.Add(arrTimeSeries)
        mobjGraphs.Add(arrProbability)
        mobjGraphs.Add(arrHistogram)
        mobjGraphs.Add(arrBoxWhisker)

        'Fill the option tabs collection
        Dim arrSummary() As Object = {lnkSummary, tabSummary, objSummary}
        Dim arrPlotOptions() As Object = {lnkPlotOptions, tabPlotOptions, objPlotOptions}
        mobjOptions.Add(arrSummary)
        mobjOptions.Add(arrPlotOptions)

        'Bind session objects
        mobjDataSet = CType(Session("DataSet"), DataSet)

        If blnReplot Then
            btnPlotGraph_Click(sender, e)
        End If
    End Sub

    Private Sub ddlDatabase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDatabase.SelectedIndexChanged
        'If they change the database, clear and deactivate the plots, menus and tabs
        btnClearGraph_Click(sender, e)
    End Sub

    Private Sub txtStation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStation.Load
        '==============================================
        'Allows users to launch with a selected station
        '==============================================
        'Fill the station text box
        If Not IsPostBack Then
            'Select the station specified in URL, if provided
            If Request.QueryString.Get("Station") <> Nothing Then
                txtStation.Text = Request.QueryString.Get("Station")

                'Check to see if they have specified other parameters in the URL string and then plot the 
                'graph if they have provided enough information to do so
                If Request.QueryString.Get("PlotGraph") = "True" Then
                    'Make sure they have specified a database
                    If Request.QueryString.Get("Database") <> Nothing Then

                        ddlDatabase_Load(Me, New System.EventArgs)

                        'Get the start date and end date if they specified them
                        txtStartDate_Load(Me, New System.EventArgs)
                        txtEndDate_Load(Me, New System.EventArgs)

                        'Get the variable if they supplied one
                        txtVariable_Load(Me, New System.EventArgs)

                        'Check to make sure they have specified enough information and then plot the graph
                        If Request.QueryString.Get("Variable") <> Nothing Then
                            btnPlotGraph_Click(Me, New System.EventArgs)
                        End If

                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ddlDatabase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDatabase.Load
        '===============================================
        'Allows users to launch with a selected database
        '===============================================
        'Change the database type based on what the user specifies
        Dim Database As String = Nothing
        If Not IsPostBack Then
            'Select the database specified in URL, if provided
            Database = Request.QueryString.Get("Database")
            If Database <> Nothing Then
                If Database = "DV" Then
                    ddlDatabase.SelectedValue = "DV"
                ElseIf Database = "GW" Then
                    ddlDatabase.SelectedValue = "GW"
                ElseIf Database = "UV" Then
                    ddlDatabase.SelectedValue = "UV"
                ElseIf Database = "IID" Then
                    ddlDatabase.SelectedValue = "IID"
                End If
                'ddlDatabase_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub txtVariable_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVariable.Load
        '===============================================
        'Allows users to launch with a selected Variable
        '===============================================
        Dim Variable As String = Nothing
        'Fill the Variable text box
        If Not IsPostBack Then
            Variable = Request.QueryString.Get("Variable")
            'Select the Variable specified in URL, if provided
            If Variable <> Nothing Then
                txtVariable.Text = Variable
            End If
        End If
    End Sub

    Private Sub txtStartDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStartDate.Load
        '=================================================
        'Allows users to launch with a selected start date
        '=================================================
        'Fill the start date text box
        Dim StartDate As String = Nothing
        If Not IsPostBack Then
            'Select the start date specified in URL, if provided
            StartDate = Request.QueryString.Get("StartDate")
            If StartDate <> Nothing Then
                txtStartDate.Text = StartDate
            End If
        End If
    End Sub

    Private Sub txtEndDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEndDate.Load
        '===============================================
        'Allows users to launch with a selected end date
        '===============================================
        'Fill the end date text box
        Dim EndDate As String = Nothing
        If Not IsPostBack Then
            'Select the end date specified in URL, if provided
            EndDate = Request.QueryString.Get("EndDate")
            If EndDate <> Nothing Then
                txtEndDate.Text = EndDate
            End If
        End If
    End Sub

    ''''''''''''''''''''''
    ' PLOTTING FUNCTIONS '
    ''''''''''''''''''''''
    Private Sub btnPlotGraph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlotGraph.Click
        'Format text fields, as needed
        If Trim(txtStartDate.Text) = String.Empty Then
            txtStartDate.Text = "01/01/1900"
        End If
        If Trim(txtEndDate.Text) = String.Empty Then
            txtEndDate.Text = Now.ToString("MM/dd/yyyy")
        End If
        If Date.Parse(txtStartDate.Text) > Date.Parse(txtEndDate.Text) Then
            Dim strTemp As String = txtStartDate.Text
            txtStartDate.Text = txtEndDate.Text
            txtEndDate.Text = strTemp
        End If

        'Get the station name
        Dim strStationName As String
        Dim objXMLDocument As New XmlDocument
        Dim objSiteInfo As New clsSiteInfo
        Try
            objXMLDocument.LoadXml(objSiteInfo.ReturnSiteInfo(txtStation.Text, ddlDatabase.SelectedValue.ToString))
            strStationName = objXMLDocument.GetElementsByTagName("siteName").Item(0).InnerText
            lblStation.Text = strStationName
        Catch ex As Exception
            lblStation.Text = "<span style=""color:Red; font-weight:Bold"">Invalid station. Please try again.</span>"
            ClearGraphs()
            Exit Sub
        End Try

        'Get the variable name
        Dim strVariableName As String
        Dim objVariableInfo As New clsVariableInfo
        Try
            objXMLDocument.LoadXml(objVariableInfo.ReturnVariableInfo(txtVariable.Text, ddlDatabase.SelectedValue.ToString))
            strVariableName = objXMLDocument.GetElementsByTagName("variableName").Item(0).InnerText '& ", " & objXMLDocument.GetElementsByTagName("Units").Item(0).InnerText
            lblVariable.Text = strVariableName
        Catch ex As Exception
            lblVariable.Text = "<span style=""color:Red; font-weight:Bold"">Invalid variable code. Please try again.</span>"
            ClearGraphs()
            Exit Sub
        End Try

        'Get the values
        Dim XMLtext As String
        Dim objValues As New clsValues
        Dim objMemoryStream As System.IO.MemoryStream
        Dim objTextWriter As System.IO.TextWriter
        Dim objXMLTextWriter As System.Xml.XmlTextWriter
        Dim objXMLTextReader As XmlTextReader

        Try
            XmlText = objValues.ReturnValues(txtStation.Text, txtVariable.Text, ddlDatabase.SelectedValue, Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text))
            objMemoryStream = New System.IO.MemoryStream
            objTextWriter = New System.IO.StreamWriter(objMemoryStream)
            objXMLTextWriter = New XmlTextWriter(objTextWriter)
            objXMLTextWriter.WriteRaw(XmlText)
            objXMLTextWriter.Flush()
            objMemoryStream.Seek(0, IO.SeekOrigin.Begin)
            objXMLTextReader = New XmlTextReader(objMemoryStream)
        Catch ex As Exception
            lblVariable.Text = "<span style=""color:Red; font-weight:Bold"">No data available for variable. Please try again.</span>"
            ClearGraphs()
            Exit Sub
        End Try

        ''''Write out the xmltext string to a text file to see if I am getting the whole thing
        '''Dim fso As New Scripting.FileSystemObject
        '''Dim XMLFile As Scripting.TextStream
        '''XMLFile = fso.CreateTextFile("C:\Inetpub\wwwroot\nwisanalyst\XMLfile.xml", True)
        '''XMLFile.WriteLine(XMLtext)
        '''XMLFile.Close()
        '''ControlFile = Nothing

        'Declare tables that will hold the values so that I can create the plots
        Dim objValuesTable As New DataTable
        objValuesTable.Columns.Add("TSValue", Type.GetType("System.Double"))
        Dim objDatedValuesTable As New DataTable
        objDatedValuesTable.Columns.Add("HydroCode", Type.GetType("System.String"))
        'Jeff
        objDatedValuesTable.Columns.Add("TSTypeID", Type.GetType("System.String"))
        objDatedValuesTable.Columns.Add("TSTypeName", Type.GetType("System.String"))
        objDatedValuesTable.Columns.Add("TSDateTime", Type.GetType("System.DateTime"))
        objDatedValuesTable.Columns.Add("TSValue", Type.GetType("System.Double"))
        objDatedValuesTable.Columns.Add("DateMonth", Type.GetType("System.Int32"))
        objDatedValuesTable.Columns.Add("TSCensorCode", Type.GetType("System.String"))
        objDatedValuesTable.Columns.Add("TSQualifier", Type.GetType("System.String"))

        'Fill data set
        mobjDataSet.Tables.Clear()
        mobjDataSet.Tables.Add(objValuesTable)
        mobjDataSet.Tables.Add(objDatedValuesTable)

        'Load the values into the tables required to create the plots and do the statistical summary
        Dim i As Integer
        Dim dtmDateTime As DateTime
        Dim strQualifier As String
        Dim strCensorCode As String
        Dim dblValue As Double
        Dim objDatedRow(7) As Object
        Dim dblArrayList As New System.Collections.ArrayList

        While (objXMLTextReader.Read())
            If (objXMLTextReader.NodeType = XmlNodeType.Element) Then
                If (objXMLTextReader.Name = "value") Then

                    dtmDateTime = Convert.ToDateTime(objXMLTextReader.Item("dateTime"))
                    strCensorCode = Convert.ToString(objXMLTextReader.Item("censorCode"))
                    strQualifier = Convert.ToString(objXMLTextReader.Item("qualifiers"))

                    While objXMLTextReader.NodeType <> XmlNodeType.Text
                        objXMLTextReader.Read()
                    End While

                    dblValue = Convert.ToDouble(objXMLTextReader.Value)

                    objDatedRow(0) = txtStation.Text
                    objDatedRow(1) = txtVariable.Text
                    objDatedRow(2) = lblVariable.Text
                    objDatedRow(3) = dtmDateTime
                    objDatedRow(4) = dblValue
                    objDatedRow(5) = Month(dtmDateTime)
                    objDatedRow(6) = strCensorCode
                    objDatedRow(7) = strQualifier

                    mobjDataSet.Tables(1).Rows.Add(objDatedRow)

                    'Check to see if the value is censored, if so, change the value to a -9999 before adding it to the arraylist
                    If strCensorCode = "lt" Then
                        dblValue = -9999
                    End If

                    dblArrayList.Add(dblValue)

                    'read to the end element
                    While objXMLTextReader.NodeType <> XmlNodeType.EndElement
                        objXMLTextReader.Read()
                    End While

                End If
            End If
        End While

        'Sort the values ascending to send to the summary statistics routine
        dblArrayList.Sort()
        Dim dbl As Double
        Dim objRow(0) As Object
        For i = 0 To dblArrayList.Count - 1
            objRow(0) = dblArrayList.Item(i)
            mobjDataSet.Tables(0).Rows.Add(objRow)
        Next

        'Update form
        If mobjDataSet.Tables(0).Rows.Count > 0 AndAlso mobjDataSet.Tables(1).Rows.Count > 0 Then
            'Only plot the graphs if there are 2 or more observations above the detection limit
            If Convert.ToInt32(mobjDataSet.Tables(0).Compute("Count(TSValue)", "TSValue > 0")) >= 2 Then
                'There are enough data aboe the detection limit to do the plots and the summary
                objTimeSeries.Plot(mobjDataSet.Tables.Item(1), strStationName, strVariableName, objPlotOptions)
                objProbability.Plot(mobjDataSet.Tables.Item(0), strStationName, strVariableName, objPlotOptions)
                objHistogram.Plot(mobjDataSet.Tables.Item(0), strStationName, strVariableName, objPlotOptions)
                objBoxWhisker.Plot(mobjDataSet.Tables.Item(1), strStationName, strVariableName, objPlotOptions)
                objSummary.Fill(mobjDataSet.Tables.Item(0))
                mnuFilePrint.Enabled = True
                ActivateMenuItem(CType(mnuFilePrint, WebControl), "fileMenu", miPrint)
                mnuDataView.Enabled = True
                ActivateMenuItem(CType(mnuDataView, WebControl), "dataMenu", miView)
                mnuDataExport.Enabled = True
                ActivateMenuItem(CType(mnuDataExport, WebControl), "dataMenu", miExport)
            Else
                'There is only 1 or zero values above the detection limit, so populate the summary, but deactivate the plots.
                objSummary.Fill(mobjDataSet.Tables.Item(0))
                mnuDataView.Enabled = True
                ActivateMenuItem(CType(mnuDataView, WebControl), "dataMenu", miView)
                mnuDataExport.Enabled = True
                ActivateMenuItem(CType(mnuDataExport, WebControl), "dataMenu", miExport)
                'Deactivate the plots
                objTimeSeries.Clear()
                objProbability.Clear()
                objHistogram.Clear()
                objBoxWhisker.Clear()
                mnuFilePrint.Enabled = False
                DeactivateMenuItem(CType(mnuFilePrint, WebControl), "fileMenu", miPrint)
                objSummary.Message("TOO FEW DATA TO PLOT")
            End If

        Else
            'For some reason there are no data at all in the tables
            btnClearGraph_Click(sender, e)
            objSummary.Message("NO DATA AVAILABLE")
        End If

        'relase resources
        objMemoryStream.Close()
        objTextWriter.Close()
        objXMLTextWriter.Close()
        objXMLTextReader.Close()

    End Sub

    ''''''''''''''''''''''
    ' CLEARING FUNCTIONS '
    ''''''''''''''''''''''
    Private Sub btnClearGraph_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearGraph.Click
        lblStation.Text = String.Empty
        lblVariable.Text = String.Empty
        objTimeSeries.Clear()
        objProbability.Clear()
        objHistogram.Clear()
        objBoxWhisker.Clear()
        objSummary.Clear()
        mnuFilePrint.Enabled = False
        DeactivateMenuItem(CType(mnuFilePrint, WebControl), "fileMenu", miPrint)
        mnuDataView.Enabled = False
        DeactivateMenuItem(CType(mnuDataView, WebControl), "dataMenu", miView)
        mnuDataExport.Enabled = False
        DeactivateMenuItem(CType(mnuDataExport, WebControl), "dataMenu", miExport)
    End Sub

    Private Sub ClearGraphs()
        objTimeSeries.Clear()
        objProbability.Clear()
        objHistogram.Clear()
        objBoxWhisker.Clear()
        objSummary.Clear()
        mnuFilePrint.Enabled = False
        DeactivateMenuItem(CType(mnuFilePrint, WebControl), "fileMenu", miPrint)
        mnuDataView.Enabled = False
        DeactivateMenuItem(CType(mnuDataView, WebControl), "dataMenu", miView)
        mnuDataExport.Enabled = False
        DeactivateMenuItem(CType(mnuDataExport, WebControl), "dataMenu", miExport)
    End Sub

    '''''''''''''''''
    ' TAB FUNCTIONS '
    '''''''''''''''''
    Private Sub SelectTab(ByRef objSelectedButton As LinkButton, ByRef objOptions As Collection)
        Dim arr() As Object
        Dim objLinkButton As LinkButton
        Dim objTab As HtmlGenericControl
        Dim objUserControl As UserControl

        For Each arr In objOptions
            objLinkButton = CType(arr(0), LinkButton)
            objTab = CType(arr(1), HtmlGenericControl)
            objUserControl = CType(arr(2), UserControl)

            'Show the selected tab and hide all others
            If objLinkButton Is objSelectedButton Then
                objLinkButton.Attributes.Remove("onmouseover")
                objLinkButton.Attributes.Remove("onmouseout")
                objTab.Attributes.Item("class") = "activeTab"
                objUserControl.Visible = True
            Else
                SetInactive(objLinkButton, objTab)
                objUserControl.Visible = False
            End If
        Next
    End Sub

    Private Sub SetInactive(ByRef objLinkButton As LinkButton, ByRef objTab As HtmlGenericControl)
        objLinkButton.Attributes.Add("onmouseover", "document.getElementById('" & objTab.ID & "').style.backgroundColor = 'White'")
        objLinkButton.Attributes.Add("onmouseout", "document.getElementById('" & objTab.ID & "').style.backgroundColor = '#E6E6E6'")
        objTab.Attributes.Item("class") = "inactiveTab"
    End Sub

    ' GRAPHS
    Private Sub lnkProbability_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProbability.Load
        If Not IsPostBack() Then
            SetInactive(lnkProbability, tabProbability)
        End If
    End Sub

    Private Sub lnkHistogram_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHistogram.Load
        If Not IsPostBack() Then
            SetInactive(lnkHistogram, tabHistogram)
        End If
    End Sub

    Private Sub lnkBoxWhisker_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBoxWhisker.Load
        If Not IsPostBack() Then
            SetInactive(lnkBoxWhisker, tabBoxWhisker)
        End If
    End Sub

    Private Sub lnkTimeSeries_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTimeSeries.Click
        SelectTab(lnkTimeSeries, mobjGraphs)
    End Sub

    Private Sub lnkProbability_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkProbability.Click
        SelectTab(lnkProbability, mobjGraphs)
    End Sub

    Private Sub lnkHistogram_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkHistogram.Click
        SelectTab(lnkHistogram, mobjGraphs)
    End Sub

    Private Sub lnkBoxWhisker_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBoxWhisker.Click
        SelectTab(lnkBoxWhisker, mobjGraphs)
    End Sub

    ' OPTIONS
    Private Sub lnkPlotOptions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPlotOptions.Load
        If Not IsPostBack() Then
            SetInactive(lnkPlotOptions, tabPlotOptions)
        End If
    End Sub

    Private Sub lnkSummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSummary.Click
        SelectTab(lnkSummary, mobjOptions)
    End Sub

    Private Sub lnkPlotOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPlotOptions.Click
        SelectTab(lnkPlotOptions, mobjOptions)
    End Sub

    ''''''''''''''''''
    ' MENU FUNCTIONS '
    ''''''''''''''''''
    Private Sub mnuGraphPlot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraphPlot.Click
        btnPlotGraph_Click(sender, e)
    End Sub

    Private Sub mnuGraphClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGraphClear.Click
        btnClearGraph_Click(sender, e)
    End Sub

    Private Sub mnuDataExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDataExport.Click
        DataGridToExcel.DataGridToExcel(Response, mobjDataSet.Tables.Item(1))
    End Sub

    Private Sub mnuFilePrintAction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFilePrintAction.Click
        Server.Transfer("print.aspx")
    End Sub

    Private Sub ActivateMenuItem(ByRef mnuItem As WebControl, ByVal strMenu As String, ByRef mi As HtmlGenericControl)
        mnuItem.Attributes.Item("onmouseover") = "openMenu('" & strMenu & "'); document.getElementById('" & mi.ID & "').className='menuItemOver'"
        mnuItem.Attributes.Item("onmouseout") = "closeMenu('" & strMenu & "'); document.getElementById('" & mi.ID & "').className='menuItem'"
        mi.Attributes.Item("class") = "menuItem"
    End Sub

    Private Sub DeactivateMenuItem(ByRef mnuItem As WebControl, ByVal strMenu As String, ByRef mi As HtmlGenericControl)
        mnuItem.Attributes.Item("onmouseover") = "openMenu('" & strMenu & "')"
        mnuItem.Attributes.Item("onmouseout") = "closeMenu('" & strMenu & "')"
        mi.Attributes.Item("class") = "menuItemDisabled"
    End Sub

    Private Sub btnSearchStations_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchStations.Click

    End Sub

    Private Sub btnSearchVariables_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchVariables.Click

    End Sub

End Class