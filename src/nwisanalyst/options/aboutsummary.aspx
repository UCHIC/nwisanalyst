<%@ Page Language="vb" AutoEventWireup="false" Codebehind="aboutsummary.aspx.vb" Inherits="nwisanalyst.aboutsummary"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>About the TS Analyst Statisitcal Summary</title>
    <link href="../Styles.css" rel=stylesheet>
  </head>
  <body>
    <form id="frmAboutSummary" method="post" runat="server">
		<table>
			<tr>
				<td class="groupCell">Summary statistics for datasets that have censored data are calculated using robust methods described in <A href="http://water.usgs.gov/pubs/twri/twri4a3/" target="_blank">Helsel and Hirsch (2002)</A>.  The summary statstics presented are subject to the following constraints:</td>
			</tr>
			<tr>
				<td class="groupCell">
					<ul>
						<li>Censored data statistics are calculated only for a single censoring level.  Multiple censoring levels are not currently supported.</li>
						<li>Censored data statistics are calculated only for datasets with observations below a censoring level.  Datasets with values above a censoring level are not currently supported.</li>
					</ul>
				</td>
			</tr>			
		</div>
		</table>
    </form>
  </body>
</html>
