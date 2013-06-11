<%@ Register TagPrefix="graphs" Namespace="Gigasoft.ProEssentials" Assembly="Gigasoft.ProEssentialsWeb" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Probability.ascx.vb" Inherits="nwisanalyst.GraphsProbability" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="graphTable">
	<tr>
		<td id="graphCell"><graphs:pesgoweb id=PesgoWeb width=507 height=390 runat="server" /></td>
	</tr>
</table>