<%@ Page Language="vb" EnableSessionState="readonly" AutoEventWireup="false" Codebehind="view.aspx.vb" Inherits="nwisanalyst.RootView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
	<title>NWIS Time Series Analyst - Data View</title>
</head>
<body>
<form id="frmExport" method="post" runat="server">
	<asp:datagrid id="dgStreamflow" width="100%" bordercolor="#999999" borderstyle="None" borderwidth="1px" backcolor="White" cellpadding="3" gridlines="Vertical" enableviewstate="False" autogeneratecolumns="False" runat="server">
		<headerstyle font-name="Arial" font-size="10pt" font-bold="True" forecolor="White" backcolor="#000084"></headerstyle>
		<alternatingitemstyle backcolor="Gainsboro"></alternatingitemstyle>
		<itemstyle font-name="Arial" font-size="10pt" forecolor="Black" backcolor="#EEEEEE"></itemstyle>
		
		<columns>
			<asp:boundcolumn datafield="HydroCode" readonly="True" headertext="Station"></asp:boundcolumn>
			<asp:boundcolumn datafield="TSDateTime" dataformatstring="{0:g}" readonly="True" headertext="Date"></asp:boundcolumn>
			<asp:boundcolumn datafield="TSTypeID" readonly="True" headertext="Variable Code"></asp:boundcolumn>
			<asp:boundcolumn datafield="TSTypeName" readonly="True" headertext="Variable Name"></asp:boundcolumn>
			<asp:boundcolumn datafield="TSValue" readonly="True" headertext="Value"></asp:boundcolumn>
			<asp:BoundColumn DataField="TSQualifier" ReadOnly ="True" HeaderText="Qualifier"></asp:BoundColumn>
			<asp:BoundColumn DataField="TSCensorCode" ReadOnly = "True" HeaderText="CensorCode"></asp:BoundColumn>
		</columns>
	</asp:datagrid>
</form>
</body>
</html>
