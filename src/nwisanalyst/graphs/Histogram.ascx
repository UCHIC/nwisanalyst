<%@ Register TagPrefix="graphs" Namespace="Gigasoft.ProEssentials" Assembly="Gigasoft.ProEssentialsWeb" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Histogram.ascx.vb" Inherits="nwisanalyst.GraphsHistogram" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="graphTable">
	<tr>
		<td id="graphCell"><graphs:pegoweb id="PegoWeb" width="507" height="390" runat="server" /></td>
	</tr>
</table>
