<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TimeSeries.ascx.vb" Inherits="nwisanalyst.GraphsTimeSeries" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="graphs" Namespace="Gigasoft.ProEssentials" Assembly="Gigasoft.ProEssentialsWeb" %>
<table id="graphTable">
	<tr>
		<td id="graphCell"><graphs:pesgoweb id="PesgoWeb" width="507" height="390" runat="server" /></td>
	</tr>
</table>