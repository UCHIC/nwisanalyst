<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="nwisanalyst._Default" %>
<%@ Register TagPrefix="graphs" TagName="TimeSeries" Src="~/graphs/TimeSeries.ascx" %>
<%@ Register TagPrefix="graphs" TagName="Probability" Src="~/graphs/Probability.ascx" %>
<%@ Register TagPrefix="graphs" TagName="Histogram" Src="~/graphs/Histogram.ascx" %>
<%@ Register TagPrefix="graphs" TagName="BoxWhisker" Src="~/graphs/BoxWhisker.ascx" %>
<%@ Register TagPrefix="options" TagName="Summary" Src="~/options/Summary.ascx" %>
<%@ Register TagPrefix="options" TagName="PlotOptions" Src="~/options/PlotOptions.ascx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>NWIS Time Series Analyst</title>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form id="frmAnalyzer" method="post" runat="server" onsubmit="document.body.style.cursor='wait'">
	
    <!-- Main Menu -->
    <div id="mainMenu">
	    <table>
		    <tr>
			    <td class="menu" id="fileMenuCell" onmouseover="openMenu('fileMenu')" onmouseout="closeMenu('fileMenu')">File</td>
			    <td class="menu" id="graphMenuCell" onmouseover="openMenu('graphMenu')" onmouseout="closeMenu('graphMenu')">Graph</td>
			    <td class="menu" id="dataMenuCell" onmouseover="openMenu('dataMenu')" onmouseout="closeMenu('dataMenu')">Data</td>
			    <td class="menu" id="helpMenuCell" onmouseover="openMenu('helpMenu')" onmouseout="closeMenu('helpMenu')">Help</td> 
		    </tr>
	    </table>
    </div>
    <div id="fileMenu" onmouseover="openMenu('fileMenu')" onmouseout="closeMenu('fileMenu')">
	    <asp:hyperlink id="mnuFilePrint" navigateurl="javascript:printVersion()" runat="server"><div id="miPrint" class="menuItem" runat="server">Print Version</div></asp:hyperlink>
	    <asp:linkbutton id="mnuFilePrintAction" runat="server" />
	    <table class="separator">
		    <tr>
			    <td></td>
		    </tr>
	    </table>
	    <asp:hyperlink id="mnuFileExit" navigateurl="javascript:self.close()" runat="server"><div id="miExit" class="menuItem" runat="server">Exit</div></asp:hyperlink>
    </div>
    <div id="graphMenu" onmouseover="openMenu('graphMenu')" onmouseout="closeMenu('graphMenu')">
	    <img src="images/spacer.gif" alt="" width="80" height="1" border="0" />
	    <br/>
	    <asp:linkbutton id="mnuGraphPlot" runat="server"><div id="miPlot" class="menuItem" runat="server">Plot</div></asp:linkbutton>
	    <asp:linkbutton id="mnuGraphClear" runat="server"><div id="miClear" class="menuItem" runat="server">Clear</div></asp:linkbutton>
    </div>
    <div id="dataMenu" onmouseover="openMenu('dataMenu')" onmouseout="closeMenu('dataMenu')">
	    <img src="images/spacer.gif" alt="" width="80" height="1" border="0" />
	    <br/>
	    <asp:hyperlink id="mnuDataView" navigateurl="view.aspx" target="_blank" runat="server"><div id="miView" class="menuItem" runat="server">View</div></asp:hyperlink>
	    <asp:linkbutton id="mnuDataViewAction" runat="server" />
	    <asp:linkbutton id="mnuDataExport" runat="server"><div id="miExport" class="menuItem" runat="server">Export&#133;</div></asp:linkbutton>
    </div>
    <div id="helpMenu" onmouseover="openMenu('helpMenu')" onmouseout="closeMenu('helpMenu')">
	    <img src="images/spacer.gif" alt="" width="80" height="1" border="0" />
	    <br/>
	    <asp:hyperlink id="mnuHelpDescription" navigateurl="documents/description.pdf" target="_blank" runat="server"><div id="miDescription" class="menuItem" runat="server">Description</div></asp:hyperlink>
	    <asp:hyperlink id="mnuHelpTutorial" navigateurl="documents/tutorial.pdf" target="_blank" runat="server"><div id="miTutorial" class="menuItem" runat="server">Tutorial</div></asp:hyperlink>
    </div>

    <!-- Tabbed Graphs -->
    <div id="tabbedGraphs">
	    <!-- Tabs -->
	    <table width="100%">
		    <tr valign="bottom">
			    <td><asp:linkbutton id="lnkTimeSeries" runat="server"><div id="tabTimeSeries" class="activeTab" runat="server">Time&nbsp;Series</div></asp:linkbutton></td>
			    <td><asp:linkbutton id="lnkProbability" runat="server"><div id="tabProbability" class="inactiveTab" runat="server">Probability</div></asp:linkbutton></td>
			    <td><asp:linkbutton id="lnkHistogram" runat="server"><div id="tabHistogram" class="inactiveTab" runat="server">Histogram</div></asp:linkbutton></td>
			    <td><asp:linkbutton id="lnkBoxWhisker" runat="server"><div id="tabBoxWhisker" class="inactiveTab" runat="server">Box&nbsp;Whisker</div></asp:linkbutton></td>
			    <td width="100%"><div class="tabLine">&nbsp;</div></td>
		    </tr>
	    </table>
	
	    <!-- Pages -->
	    <graphs:timeseries id="objTimeSeries" visible="True" runat="server" />
	    <graphs:probability id="objProbability" visible="False" runat="server" />
	    <graphs:histogram id="objHistogram" visible="False" runat="server" />
	    <graphs:boxwhisker id="objBoxWhisker" visible="False" runat="server" />
    </div>

    <!-- Tabbed Options -->
    <div id="tabbedOptions">
	    <!-- Tabs -->
	    <table width="100%">
		    <tr valign="bottom">
			    <td><asp:linkbutton id="lnkSummary" runat="server"><div id="tabSummary" class="activeTab" runat="server">Summary</div></asp:linkbutton></td>
			    <td><asp:linkbutton id="lnkPlotOptions" runat="server"><div id="tabPlotOptions" class="inactiveTab" runat="server">Plot&nbsp;Options</div></asp:linkbutton></td>
			    <td width="100%"><div class="tabLine">&nbsp;</div></td>
		    </tr>
	    </table>
	
	    <!-- Pages -->
	    <options:summary id="objSummary" visible="True" runat="server" />
	    <options:plotoptions id="objPlotOptions" visible="False" runat="server" />
    </div>

    <!-- Control Panel -->
    <div id="controlPanel">
    <table>
	    <tr>
		    <td>
			    <!-- Enter -->
			    <div class="groupLabel" style="WIDTH:165px">Enter Data Selection Parameters</div>
			    <table class="groupTable" width="430" height="142">
				    <tr>
					    <td class="groupCell">
						    NWIS Database:&nbsp;&nbsp;
						    <asp:dropdownlist id="ddlDatabase" autopostback="True" runat="server">
							    <asp:listitem value="DV" selected="True">Daily Value Data</asp:listitem>
							    <asp:listitem value="GW">Ground Water Level Data</asp:listitem>
							    <asp:listitem value="UV">Unit Value Data</asp:listitem>
							    <asp:listitem value="IID">Instantaneous Irregular Data</asp:listitem>
						    </asp:dropdownlist>
						    <br/>
						    <img src="images/spacer.gif" alt="" width="1" height="20" border="0" />
						    Station: <asp:label id="lblStation" runat="server" />
						    <br/>
						    <input type="button" id="btnSearchStations" onclick="searchstations()" value="Search" />
						    <asp:textbox id="txtStation" width="180px" maxlength="15" cssclass="formElement" runat="server" />&nbsp;&nbsp;
						    <asp:requiredfieldvalidator id="reqStation" controltovalidate="txtStation" display="Dynamic" forecolor="Red" font-bold="True" errormessage="Please enter a station number." runat="server">* Required</asp:requiredfieldvalidator>
						    <br/>
						    <img src="images/spacer.gif" alt="" width="1" height="20" border="0" />
						    Variable: <asp:label id="lblVariable" runat="server" />
						    <br/>
						    <input type="button" id="btnSearchVariables" onclick="searchvariables()" value="Search" />
						    <asp:textbox id="txtVariable" width="180px" maxlength="50" cssclass="formElement" runat="server" />&nbsp;&nbsp;
                            <asp:requiredfieldvalidator id="reqVariable" controltovalidate="txtVariable" display="Dynamic" forecolor="Red" font-bold="True" errormessage="Please enter a Variable code." runat="server">* Required</asp:requiredfieldvalidator>
					    </td>
				    </tr>
			    </table>
		    </td>
		    <td><img src="images/spacer.gif" alt="" width="8" height="1" border="0" /></td>
		    <td>
			    <!-- Date Range -->
			    <div class="groupLabel" style="WIDTH:55px">Date&nbsp;Range</div>
			    <table class="groupTable" width="202" height="142">
				    <tr>
					    <td class="groupCell">
						    Start Date: <br/>
						    <asp:textbox id="txtStartDate" width="80px" maxlength="10" cssclass="formElement" runat="server" />&nbsp;&nbsp;
						    <asp:comparevalidator id="cmpStartDate" controltovalidate="txtStartDate" type="Date" operator="DataTypeCheck" display="Dynamic" forecolor="Red" font-bold="True" errormessage="Enter a valid start date." runat="server">* Invalid date</asp:comparevalidator>
						    <br/>
						    <img src="images/spacer.gif" alt="" width="1" height="6" border="0" />
						    <br/>
						    End Date: <br/>
						    <asp:textbox id="txtEndDate" width="80px" maxlength="10" cssclass="formElement" runat="server" />&nbsp;&nbsp;
						    <asp:comparevalidator id="cmpEndDate" controltovalidate="txtEndDate" type="Date" operator="DataTypeCheck" display="Dynamic" forecolor="Red" font-bold="True" errormessage="Enter a valid end date." runat="server">* Invalid date</asp:comparevalidator>
						    <br/>
					    </td>
				    </tr>
			    </table>
		    </td>
		    <td><img src="images/spacer.gif" alt="" width="8" height="1" border="0" /></td>
		    <td>
			    <!-- Commands -->
			    <div class="groupLabel" style="WIDTH:51px">Commands</div>
			    <table class="groupTable" height="142">
				    <tr>
					    <td class="groupCell">
						    <asp:button id="btnPlotGraph" width="92px" text="Plot Graph" cssclass="formElement" runat="server" />
						    <br>
						    <br>
						    <asp:button id="btnClearGraph" width="92px" text="Clear Graph" causesvalidation="False" cssclass="formElement" runat="server" />
						    <br>
					    </td>
				    </tr>
			    </table>
		    </td>
	    </tr>
    </table>
    </div>

    <!-- Logos -->
    <div id="logos">
	    <table class="groupTable" width=764>
		    <tr>
			    <td><img src="images/spacer.gif" alt="" width="1" height="6" border="0" /></td>
		    </tr>
		    <tr>
			    <td align="right">Created by:&nbsp;&nbsp;</td>
			    <td align="left" valign="middle"><a href="http://www.usu.edu/" target="_blank"><img src="images/usu.gif" width="116" height="37" border="0" alt="Utah State University" title="Utah State University" /></a></td>	
			    <td align="right">Data provided by:&nbsp;&nbsp;</td>
			    <td align="left" valign="middle"><a href="http://www.usgs.gov/" target="_blank"><img src="images/usgs.gif" width="100" height="37" border="0" alt="United States Geological Survey" title="United States Geological Survey" /></a></td>
		    </tr>
		    <tr>
			    <td><img src="~/images/spacer.gif" alt="" width="1" height="6" border="0" /></td>
		    </tr>
		    <tr>
			    <td colspan="4" align="center">This is a <b><font color="red">PROVISIONAL application</font></b> that is under development.  Please send any questions or comments to <a href="mailto:jeff.horsburgh@usu.edu">Jeff Horsburgh</a>.</td>
		    </tr>
		    <tr>
			    <td colspan="4" align="center">This application uses web services developed by the CUAHSI Hydrologic Information System Project.  For more details, see <a href="http://his.cuahsi.org" target="_blank">http://his.cuahsi.org</a>.</td>
		    </tr>
            <tr>
			    <td><img src="images/spacer.gif" alt="" width="1" height="6" border="0" /></td>
            </tr>
	    </table>
    </div>

    </form>
</asp:Content>
