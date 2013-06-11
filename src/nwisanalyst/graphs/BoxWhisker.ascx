<%@ Register TagPrefix="graphs" Namespace="Gigasoft.ProEssentials" Assembly="Gigasoft.ProEssentialsWeb" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="BoxWhisker.ascx.vb" Inherits="nwisanalyst.GraphsBoxWhisker" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="graphTable">
	<tr>
		<td id="graphCell"><graphs:pesgoweb id="PesgoWeb" width="507" height="390" runat="server" /></td>
	</tr>
</table>
