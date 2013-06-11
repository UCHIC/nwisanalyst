<%@ Control Language="vb" AutoEventWireup="false" Codebehind="PlotOptions.ascx.vb" Inherits="nwisanalyst.OptionsPlotOptions" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="optionsTable">
    <tr>
        <td id="optionsCell">
	        <div class="groupLabel" style="width:35px">Options</div>
	        <table id="outerTable"><tr><td class="groupCell" valign="top">
		        <div class="groupLabel" style="width:54px">Time&nbsp;Series</div>
		        <table class="groupTable" style="width:100%">
                    <tr>
                        <td class="groupCell">
			                Plot Type<br>
			                <img src="images/spacer.gif" alt="" width="1" height="2" border="0" /><br>
			                <asp:radiobuttonlist id="rblTimeSeries" repeatdirection="Horizontal" autopostback="True" runat="server">
				                <asp:listitem value="line" runat="server">Line&nbsp;&nbsp;</asp:listitem>
				                <asp:listitem value="point" runat="server">Point&nbsp;&nbsp;</asp:listitem>
				                <asp:listitem value="both" selected="true" runat="server">Both&nbsp;&nbsp;</asp:listitem>
			                </asp:radiobuttonlist>
			                <img src="images/spacer.gif" alt="" width="1" height="8" border="0" /><br>
			                <table width="100%">
				                <tr>
					                <td width="100%">Control Line</td>
					                <td align="right">
						                <asp:linkbutton id="lnkApply" runat="server"><img src="images/apply.gif" width=35 height=15 border=0 alt="Apply Control Line" title="Apply Control Line" name="apply" onmouseover="changeImages('apply', 'images/apply-over.gif'); return true;" onmouseout="changeImages('apply', 'images/apply.gif'); return true;" /></asp:linkbutton>
					                </td>
					                <td align="right" style="padding-left:5px">
						                <asp:linkbutton id="lnkCancel" runat="server"><img src="images/clear.gif" width=35 height=15 border=0 alt="Clear Control Line" title="Apply Control Line" name="clear" onmouseover="changeImages('clear', 'images/clear-over.gif'); return true;" onmouseout="changeImages('clear', 'images/clear.gif'); return true;" /></asp:linkbutton>
					                </td>
				                </tr>
			                </table>
			                <table style="margin-top:4px">
				                <tr>
					                <td class="inputLabel" style="padding-left:10px">Label</td>
					                <td><asp:textbox id="txtLabel" cssclass="formElement" width="120" textmode="SingleLine" maxlength=50 runat="server" /></td>
				                </tr>
				                <tr>
					                <td class="inputLabel" style="padding-left:10px">Value</td>
					                <td><asp:textbox id="txtValue" cssclass="formElement" width="40" textmode="SingleLine" maxlength=10 runat="server" /></td>
				                </tr>
				                <tr>
					                <td class="inputLabel" style="padding-left:10px">Color</td>
					                <td style="padding-top:1px">
						                <asp:dropdownlist id="cboColor" cssclass="formElement" runat="server">
							                <asp:listitem value="Black">Black</asp:listitem>
							                <asp:listitem value="Blue">Blue</asp:listitem>
							                <asp:listitem value="Brown">Brown</asp:listitem>
							                <asp:listitem value="Cyan">Cyan</asp:listitem>
							                <asp:listitem value="Gray">Gray</asp:listitem>
							                <asp:listitem value="Green">Green</asp:listitem>
							                <asp:listitem value="ForestGreen">Forest Green</asp:listitem>
							                <asp:listitem value="Magenta">Magenta</asp:listitem>
							                <asp:listitem value="Maroon">Maroon</asp:listitem>
							                <asp:listitem value="Navy">Navy Blue</asp:listitem>
							                <asp:listitem value="Orange">Orange</asp:listitem>
							                <asp:listitem value="Pink">Pink</asp:listitem>
							                <asp:listitem value="Purple">Purple</asp:listitem>
							                <asp:listitem value="Red">Red</asp:listitem>
							                <asp:listitem value="Yellow">Yellow</asp:listitem>
						                </asp:dropdownlist>
					                </td>
				                </tr>
			                </table>			
		                </td>
                    </tr>
                </table>
		        <div class="groupLabel" style="width:59px">Box&nbsp;Whisker</div>
		        <table class="groupTable" style="width:100%">
                    <tr>
                        <td class="groupCell">
			                Plot Type<br>
			                <img src="images/spacer.gif" alt="" width="1" height="2" border="0" />
			                <asp:radiobuttonlist id="rblBoxWhiskerType" width="100%" repeatcolumns="2" repeatdirection="Horizontal" autopostback="True" runat="server">
				                <asp:listitem value="monthly" selected="True">Monthly</asp:listitem>
				                <asp:listitem value="seasonal">Seasonal</asp:listitem>
				                <asp:listitem value="yearly">Yearly</asp:listitem>
				                <asp:listitem value="overall">Overall</asp:listitem>
			                </asp:radiobuttonlist>
			                <img src="images/spacer.gif" alt="" width="1" height="8" border="0" /></br>
			                Connect</br>
			                <img src="images/spacer.gif" alt="" width="1" height="2" border="0" /></br>
			                <asp:radiobuttonlist id="rblBoxWhiskerLine" width="100%" repeatdirection="Horizontal" autopostback="True" runat="server">
				                <asp:listitem value="mean">Means</asp:listitem>
				                <asp:listitem value="median" selected="True">Medians</asp:listitem>
			                </asp:radiobuttonlist>
		                </td>
                    </tr>
                </table>		
	            </td>
            </tr>
            </table>
        </td>
    </tr>
</table>