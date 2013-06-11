<%@ Reference Page="default.aspx" %>
<%@ Page Language="vb" EnableSessionState="readonly" AutoEventWireup="false" Codebehind="print.aspx.vb" Inherits="nwisanalyst.RootPrint" %>
<!doctype html public "-//w3c//dtd html 4.0 transitional//en">
<html>
	<head>
		<title>Streamflow Analyst - Print View</title>
		<style>
			.heading { font-size:24px; font-family:'Times New Roman',Roman,Georgia,serif }
			.subheading { font-size:18px; font-family:'Times New Roman',Roman,Georgia,serif }
			.content { font-size:16px; font-family:'Times New Roman',Roman,Georgia,serif }
			td { font-size:13px; font-family:Arial,Helvetica,sans-serif }
		</style>
	</head>
	<body>
		<form id="frmPrint" method="post" runat="server">
			<div align="center">
				<asp:label id="lblStation" cssclass="heading" runat="server" /><br>
				<asp:label id="lblVariable" cssclass="subheading" runat="server" /><br>
				<br>
				<asp:label id="lblDateSpan" cssclass="content" runat="server" /><br>
				<br>
				<br>
				<asp:image id="imgGraph" width=507 height=370 runat="server" /><br>
				<br>
				<br>
				<table cellpadding=4>
					<tr>
						<td><b>Arithmetic Mean</b></td>
						<td width=20></td>
						<td align="right"><asp:label id="lblArithmeticMean" runat="server" /></td>
						<td width=60></td>
						<td><b>10%</b></td>
						<td width=20></td>
						<td align="right"><asp:label id="lbl10Percentile" runat="server" /></td>
					</tr>
					<tr>
						<td><b>Geometric Mean</b></td>
						<td></td>
						<td align="right"><asp:label id="lblGeometricMean" runat="server" /></td>
						<td></td>
						<td><b>25%</b></td>
						<td></td>
						<td align="right"><asp:label id="lbl25Percentile" runat="server" /></td>
					</tr>
					<tr>
						<td><b>Maximum</b></td>
						<td></td>
						<td align="right"><asp:label id="lblMaximum" runat="server" /></td>
						<td></td>
						<td><b>Median, 50%</b></td>
						<td></td>
						<td align="right"><asp:label id="lblMedian" runat="server" /></td>
					</tr>
					<tr>
						<td><b>Minimum</b></td>
						<td></td>
						<td align="right"><asp:label id="lblMinimum" runat="server" /></td>
						<td></td>
						<td><b>75%</b></td>
						<td></td>
						<td align="right"><asp:label id="lbl75Percentile" runat="server" /></td>
					</tr>
					<tr>
						<td><b>Standard Deviation</b></td>
						<td></td>
						<td align="right"><asp:label id="lblStandardDeviation" runat="server" /></td>
						<td></td>
						<td><b>90%</b></td>
						<td></td>
						<td align="right"><asp:label id="lbl90Percentile" runat="server" /></td>
					</tr>
					<tr>
						<td><b>Coefficient of Variation</b></td>
						<td></td>
						<td align="right"><asp:label id="lblCoefficientOfVariation" runat="server" /></td>
						<td></td>
						<td><b># of Observations</b></td>
						<td></td>
						<td align="right"><asp:label id="lblNumberOfObservations" runat="server" /></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</html>
