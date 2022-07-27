using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Vs.Recruit
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();
            PrintingSystem.AfterMarginsChange += PrintingSystem_AfterMarginsChange;
            PrintingSystem.PageSettingsChanged += PrintingSystem_PageSettingsChanged;
            Loaddata();
        }
        private void Loaddata()
        {
            string s = @"
<body lang=EN-US style='tab-interval:.5in;word-wrap:break-word'>

<div class=WordSection1>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 align=left
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-table-lspace:9.0pt;
 margin-left:6.75pt;mso-table-rspace:9.0pt;margin-right:6.75pt;mso-table-anchor-vertical:
 paragraph;mso-table-anchor-horizontal:column;mso-table-left:left;mso-padding-alt:
 0in 0in 0in 0in'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>
  <td width=50 valign=top style='width:37.8pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center;mso-element:frame;
  mso-element-frame-hspace:9.0pt;mso-element-wrap:around;mso-element-anchor-vertical:
  paragraph;mso-element-anchor-horizontal:column;mso-height-rule:exactly'><b
  style='mso-bidi-font-weight:normal'><span style='font-size:10.5pt;font-family:
  Cambria,serif;text-transform:uppercase;mso-no-proof:yes'><img width=35
  height=37 id=_x0000_i1026
  src=9.%20&#272;ánh%20giá%20tình%20tr&#7841;ng%20h&#7885;c%20vi&#7879;c%20th&#7917;%20vi&#7879;c%20c&#7911;a%20NL&#272;_files/image001.png></span></b></p>
  </td>
  <td width=231 valign=top style='width:173.45pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center;mso-element:frame;
  mso-element-frame-hspace:9.0pt;mso-element-wrap:around;mso-element-anchor-vertical:
  paragraph;mso-element-anchor-horizontal:column;mso-height-rule:exactly'><b><span
  style='font-size:9.0pt;font-family:Times New Roman,serif;text-transform:
  uppercase'>Công ty </span></b><b><span lang=VI style='font-size:9.0pt;
  font-family:Times New Roman,serif;text-transform:uppercase;mso-ansi-language:
  VI'>C&#7892; PH&#7846;N </span></b><b><span style='font-size:9.0pt;
  font-family:Times New Roman,serif;text-transform:uppercase'>MAY DUY MINH</span></b></p>
  <p class=MsoNormal align=center style='text-align:center;mso-element:frame;
  mso-element-frame-hspace:9.0pt;mso-element-wrap:around;mso-element-anchor-vertical:
  paragraph;mso-element-anchor-horizontal:column;mso-height-rule:exactly'><i><span
  style='font-size:9.0pt;font-family:Times New Roman,serif;text-transform:
  uppercase'>DUY MINH GARMENT </span></i><i><span lang=VI style='font-size:
  9.0pt;font-family:Times New Roman,serif;text-transform:uppercase;
  mso-ansi-language:VI'>jsc</span></i></p>
  </td>
 </tr>
</table>

<p class=MsoNormal align=center style='text-align:center'><b><span
style='font-family:Times New Roman,serif'>&nbsp;</span></b><o:p></o:p></p>

<p class=MsoNormal align=center style='text-align:center'><span
style='position:relative;z-index:251658240'><span style='left:458px;position:
absolute;top:-40px'><span style='mso-no-proof:yes'><img width=179 height=33
id=_x0000_i1025
src=9.%20&#272;ánh%20giá%20tình%20tr&#7841;ng%20h&#7885;c%20vi&#7879;c%20th&#7917;%20vi&#7879;c%20c&#7911;a%20NL&#272;_files/image002.png
alt=Tuy&#7879;t m&#7853;t / Confidential></span><b><span style='font-family:
Times New Roman,serif'>&#272;ÁNH GIÁ KÝ H&#7906;P &#272;&#7890;NG LAO &#272;&#7896;NG</span></b></p>

<p class=MsoHeading8 style='margin-top:6.0pt;margin-right:0in;margin-bottom:
3.0pt;margin-left:0in'><span style='font-size:12.0pt;font-family:Times New Roman,serif;
font-weight:normal'>SIGNING CONTRACT ASSESSMENT</span></p>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=660
 style='width:495.0pt;border-collapse:collapse;mso-yfti-tbllook:1184;
 mso-padding-alt:0in 0in 0in 0in'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;height:32.6pt'>
  <td width=126 valign=top style='width:94.5pt;padding:0in 5.4pt 0in 5.4pt;
  height:32.6pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>H&#7885;</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>và</span> <span class=SpellE>tên</span> </span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Full
  Name</span></i></p>
  </td>
  <td width=48 valign=top style='width:36.05pt;padding:0in 5.4pt 0in 5.4pt;
  height:32.6pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>:
  </span></p>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=216 valign=top style='width:161.95pt;padding:0in 5.4pt 0in 5.4pt;
  height:32.6pt'>
  <p class=MsoNormal><span class=SpellE><b><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Bùi</span></b></span><b><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>Th&#7883;</span> <span class=SpellE>Hi&#7873;n</span></span></b></p>
  </td>
  <td width=108 valign=top style='width:81.0pt;padding:0in 5.4pt 0in 5.4pt;
  height:32.6pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Mã</span></span><span style='font-size:
  12.0pt;font-family:Times New Roman,serif'> <span class=SpellE>s&#7889;</span>
  <span class=SpellE>th&#7867;</span> </span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Emp.
  No</span></i></p>
  </td>
  <td width=19 valign=top style='width:14.2pt;padding:0in 5.4pt 0in 5.4pt;
  height:32.6pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>:</span></p>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=143 valign=top style='width:107.3pt;padding:0in 5.4pt 0in 5.4pt;
  height:32.6pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>DMT000883</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;height:30.15pt'>
  <td width=126 valign=top style='width:94.5pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Ch&#7913;c</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>v&#7909;</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Position</span></i></p>
  </td>
  <td width=48 valign=top style='width:36.05pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>:</span></p>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=216 valign=top style='width:161.95pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>TT
  <span class=SpellE>chuy&#7873;n</span> may</span></p>
  </td>
  <td width=108 valign=top style='width:81.0pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Phòng</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> ban</span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Department</span></i></p>
  </td>
  <td width=19 valign=top style='width:14.2pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>:</span></p>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=143 valign=top style='width:107.3pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal style='margin-right:-9.75pt'><span class=SpellE><span
  style='font-size:11.0pt;font-family:Times New Roman,serif'>S&#7843;n</span></span><span
  style='font-size:11.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>xu&#7845;t</span></span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2;height:46.1pt'>
  <td width=126 valign=top style='width:94.5pt;padding:0in 5.4pt 0in 5.4pt;
  height:46.1pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Ngày</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>hi&#7879;u</span> <span class=SpellE>l&#7921;c</span> <span
  class=SpellE>h&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Effective
  Date</span></i></p>
  </td>
  <td width=48 valign=top style='width:36.05pt;padding:0in 5.4pt 0in 5.4pt;
  height:46.1pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>:</span></p>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=216 valign=top style='width:161.95pt;padding:0in 5.4pt 0in 5.4pt;
  height:46.1pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>11/6/2022</span></p>
  </td>
  <td width=108 valign=top style='width:81.0pt;padding:0in 5.4pt 0in 5.4pt;
  height:46.1pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Ngày</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>h&#7871;t</span> <span class=SpellE>hi&#7879;u</span> <span
  class=SpellE>l&#7921;c</span> <span class=SpellE>h&#7907;p</span> <span
  class=SpellE>&#273;&#7891;ng</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Expiry
  date</span></i></p>
  </td>
  <td width=19 valign=top style='width:14.2pt;padding:0in 5.4pt 0in 5.4pt;
  height:46.1pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>:</span></p>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=143 valign=top style='width:107.3pt;padding:0in 5.4pt 0in 5.4pt;
  height:46.1pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>10/6/2023</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3;mso-yfti-lastrow:yes;height:30.15pt'>
  <td width=126 valign=top style='width:94.5pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Ng&#432;&#7901;i</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>&#273;ánh</span> <span class=SpellE>giá</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Reviewer</span></i></p>
  </td>
  <td width=48 valign=top style='width:36.05pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>:</span></p>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=216 valign=top style='width:161.95pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><b><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Tr&#432;&#417;ng
  T Thu <span class=SpellE>H&#432;&#417;ng</span></span></b></p>
  </td>
  <td width=108 valign=top style='width:81.0pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Ngày</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>&#273;ánh</span> <span class=SpellE>giá</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Review
  date</span></i></p>
  </td>
  <td width=19 valign=top style='width:14.2pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>:</span></p>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=143 valign=top style='width:107.3pt;padding:0in 5.4pt 0in 5.4pt;
  height:30.15pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>…………………</span></p>
  </td>
 </tr>
</table>

<p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0in 0in 0in 0in'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
  <td width=168 valign=top style='width:125.9pt;border:solid black 1.0pt;
  padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></p>
  </td>
  <td width=122 valign=top style='width:91.4pt;border:solid black 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span class=SpellE><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>Xu&#7845;t</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>s&#7855;c</span> <i>Excellent</i></span></p>
  </td>
  <td width=122 valign=top style='width:91.45pt;border:solid black 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span class=SpellE><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>T&#7889;t</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'><o:p></o:p></span></p>
  <p class=MsoNormal align=center style='text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>Good</span></i></p>
  </td>
  <td width=122 valign=top style='width:91.4pt;border:solid black 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span class=SpellE><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>Trung</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>bình</span></span></p>
  <p class=MsoNormal align=center style='text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>Fair</span></i></p>
  </td>
  <td width=122 valign=top style='width:91.45pt;border:solid black 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span class=SpellE><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>Kém</span></span></p>
  <p class=MsoNormal align=center style='text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>Poor</span></i></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1'>
  <td width=168 valign=top style='width:125.9pt;border:solid black 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Ki&#7871;n</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>th&#7913;c</span> <span class=SpellE>công</span> <span
  class=SpellE>vi&#7879;c</span> <i>Job knowledge</i></span></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:2'>
  <td width=168 valign=top style='width:125.9pt;border:solid black 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Hi&#7879;u</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>qu&#7843;</span> <span class=SpellE>công</span> <span
  class=SpellE>vi&#7879;c</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Job
  performance</span></i></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:3'>
  <td width=168 valign=top style='width:125.9pt;border:solid black 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Thái</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>&#273;&#7897;</span> <span class=SpellE>công</span> <span
  class=SpellE>vi&#7879;c</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Work
  attitude</span></i></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:4'>
  <td width=168 valign=top style='width:125.9pt;border:solid black 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Tuân</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>th&#7911;</span> <span class=SpellE>n&#7897;i</span> <span
  class=SpellE>quy</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Punctuality</span></i></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:5;mso-yfti-lastrow:yes'>
  <td width=168 valign=top style='width:125.9pt;border:solid black 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span style='font-size:12.0pt;font-family:Times New Roman,serif'>T&#7892;NG
  TH&#7874;</span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>OVERALL</span></i></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.4pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=122 style='width:91.45pt;border-top:none;border-left:none;
  border-bottom:solid black 1.0pt;border-right:solid black 1.0pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
 </tr>
</table>

<p class=MsoNormal><span class=SpellE><b><span style='font-size:12.0pt;
font-family:Times New Roman,serif'>Nh&#7853;n</span></b></span><b><span
style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
class=SpellE>xét</span> <span class=SpellE>và</span> <span class=SpellE>&#273;ánh</span>
<span class=SpellE>giá</span> <span class=SpellE>chung</span>:</span></b></p>

<p class=MsoNormal><b><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Overall
assessment &amp; recommendation: </span></b></p>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0in 0in 0in 0in'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>
  <td width=38 valign=top style='width:28.65pt;border:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span style='font-size:18.0pt;font-family:Wingdings'>o</span><span
  style='font-size:18.0pt;font-family:Times New Roman,serif'> </span></p>
  </td>
  <td width=290 valign=top style='width:217.15pt;border:solid windowtext 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Ký</span></span><span style='font-size:
  12.0pt;font-family:Times New Roman,serif'> <span class=SpellE>h&#7907;p</span>
  <span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>lao</span> <span
  class=SpellE>&#273;&#7897;ng</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Sign
  employment contract</span></i></p>
  </td>
  <td width=39 valign=top style='width:28.95pt;border:solid windowtext 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span style='font-size:18.0pt;font-family:Wingdings'>o</span></p>
  </td>
  <td width=289 valign=top style='width:216.85pt;border:solid windowtext 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>Không</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>ký</span> <span class=SpellE>h&#7907;p</span> <span
  class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>lao</span> <span
  class=SpellE>&#273;&#7897;ng</span></span></p>
  <p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Not
  sign employment contract</span></i></p>
  </td>
 </tr>
 <tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>
  <td width=655 colspan=4 valign=top style='width:491.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal style='margin-bottom:10.0pt'><span class=SpellE><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>Nh&#7853;n</span></span><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>xét</span> <span class=SpellE>khác</span> <span class=SpellE>c&#7911;a</span>
  <span class=SpellE>ng&#432;&#7901;i</span> <span class=SpellE>&#273;ánh</span>
  <span class=SpellE>giá</span>/<i>Reviewer’s comments:</i></span></p>
  <p class=MsoNormal style='margin-bottom:10.0pt'><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>…………………………..…………………………..………………………………………………</span></p>
  <p class=MsoNormal style='margin-bottom:10.0pt'><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>…………………………..…………………………..………………………………………………</span></p>
  <p class=MsoNormal style='margin-bottom:10.0pt'><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>…………………………..…………………………..………………………………………………</span></p>
  <p class=MsoNormal style='margin-bottom:10.0pt'><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'>…………………………..…………………………..………………………………………………</span></p>
  </td>
 </tr>
</table>

<p class=MsoNormal><span class=SpellE><span style='font-size:12.0pt;font-family:
Times New Roman,serif'>Vui</span></span><span style='font-size:12.0pt;
font-family:Times New Roman,serif'> <span class=SpellE>lòng</span> <span
class=SpellE>hoàn</span> <span class=SpellE>thành</span> <span class=SpellE>và</span>
<span class=SpellE>g&#7917;i</span> <span class=SpellE>b&#7843;n</span> <span
class=SpellE>&#273;ánh</span> <span class=SpellE>giá</span> <span class=SpellE>này</span>
<span class=SpellE>v&#7873;</span> <span class=SpellE>phòng</span> <span
class=SpellE>nhân</span> <span class=SpellE>s&#7921;</span> 3 <span
class=SpellE>ngày</span> <span class=SpellE>tr&#432;&#7899;c</span> <span
class=SpellE>ngày</span> <span class=SpellE>h&#7871;t</span> <span
class=SpellE>h&#7841;n</span> H&#272; <span class=SpellE>th&#7917;</span> <span
class=SpellE>vi&#7879;c</span>/ <span class=SpellE>h&#7885;c</span> <span
class=SpellE>ngh&#7873;</span> <span class=SpellE>ho&#7863;c</span> 17 <span
class=SpellE>ngày</span> <span class=SpellE>tr&#432;&#7899;c</span> <span
class=SpellE>khi</span> <span class=SpellE>h&#7871;t</span> H&#272;L&#272; <span
class=SpellE>hi&#7879;n</span> <span class=SpellE>t&#7841;i</span>. Sau <span
class=SpellE>th&#7901;i</span> <span class=SpellE>gian</span> <span
class=SpellE>này</span>, <span class=SpellE>phòng</span> <span class=SpellE>nhân</span>
<span class=SpellE>s&#7921;</span> <span class=SpellE>s&#7869;</span> <span
class=SpellE>ti&#7871;n</span> <span class=SpellE>hành</span> <span
class=SpellE>ký</span> H&#272;L&#272; <span class=SpellE>v&#7899;i</span> <span
class=SpellE>nhân</span> <span class=SpellE>viên</span> <span class=SpellE>theo</span>
<span class=SpellE>quy</span> <span class=SpellE>&#273;&#7883;nh</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>B&#7897;</span> <span
class=SpellE>lu&#7853;t</span> <span class=SpellE>lao</span> <span
class=SpellE>&#273;&#7897;ng</span>.</span></p>

<p class=MsoNormal><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Please
complete and return this form to HR department at least 3 days in prior to the
probation/vocational contract expire or 17 days in prior to the current
employment contract expire. After this time, HR department will sign employment
contract with the employee as stipulated in the Labor Code. </span></i></p>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=660
 style='width:494.75pt;border-collapse:collapse;mso-yfti-tbllook:1184;
 mso-padding-alt:0in 0in 0in 0in'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>
  <td width=160 valign=top style='width:120.1pt;border:solid black 1.0pt;
  padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><span
  class=SpellE><b><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Ng&#432;&#7901;i</span></b></span><b><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>&#273;ánh</span> <span
  class=SpellE>giá</span></span></b><span style='font-size:12.0pt;font-family:
  Times New Roman,serif'><br>
  <i>Assessed employee</i></span></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></i></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></i></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><span
  class=SpellE><span style='font-size:10.0pt;font-family:Times New Roman,serif'>Ngày</span></span><span
  style='font-size:10.0pt;font-family:Times New Roman,serif'>/<i>Date</i>:
  ....../…<span class=GramE>.....</span>/………</span></p>
  </td>
  <td width=146 valign=top style='width:109.15pt;border:solid black 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><span
  class=SpellE><b><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Giám</span></b></span><b><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>sát</span> <span class=SpellE>tr&#7921;c</span> <span
  class=SpellE>ti&#7871;p</span></span></b><span style='font-size:12.0pt;
  font-family:Times New Roman,serif'><br>
  <i>Direct supervisor</i></span></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></i></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></i></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><span
  class=SpellE><span style='font-size:10.0pt;font-family:Times New Roman,serif'>Ngày</span></span><span
  style='font-size:10.0pt;font-family:Times New Roman,serif'>/<i>Date</i>:
  ....../…<span class=GramE>.....</span>/………</span></p>
  </td>
  <td width=156 valign=top style='width:117.0pt;border:solid black 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><span
  class=SpellE><b><span style='font-size:12.0pt;font-family:Times New Roman,serif'>Tr&#432;&#7903;ng</span></b></span><b><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>b&#7897;</span> <span class=SpellE>ph&#7853;n</span></span></b><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'><br>
  <i>Department manager</i></span></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></i></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><i><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></i></p>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><span
  class=SpellE><span style='font-size:10.0pt;font-family:Times New Roman,serif'>Ngày</span></span><span
  style='font-size:10.0pt;font-family:Times New Roman,serif'>/<i>Date</i>:
  ....../…<span class=GramE>.....</span>/………</span></p>
  </td>
  <td width=198 valign=top style='width:148.5pt;border:solid black 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-bottom:10.0pt;text-align:center'><span
  class=SpellE><b><span style='font-size:12.0pt;font-family:Times New Roman,serif'>T&#7893;ng</span></b></span><b><span
  style='font-size:12.0pt;font-family:Times New Roman,serif'> <span
  class=SpellE>Giám</span> <span class=SpellE>&#273;&#7889;c</span> <span
  class=SpellE>&#273;i&#7873;u</span> <span class=SpellE>hành</span><br>
  </span></b><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'>General
  Manager</span></i></p>
  <p class=MsoNormal style='margin-bottom:10.0pt'><i><span style='font-size:
  12.0pt;font-family:Times New Roman,serif'>&nbsp;</span></i></p>
  <p class=MsoNormal style='margin-bottom:10.0pt'><span style='font-size:10.0pt;
  font-family:Times New Roman,serif'>&nbsp;</span></p>
  <p class=MsoNormal align=center style='text-align:center'><span class=SpellE><span
  style='font-size:10.0pt;font-family:Times New Roman,serif'>Ngày</span></span><span
  style='font-size:10.0pt;font-family:Times New Roman,serif'>/<i>Date</i>: </span></p>
  <p class=MsoNormal align=center style='text-align:center'><span
  style='font-size:10.0pt;font-family:Times New Roman,serif'>....../…...../………</span></p>
  </td>
 </tr>
</table>

<p class=MsoNormal style='line-height:115%'><!--[if gte vml 1]><v:shapetype
 id=_x0000_t75 coordsize=21600,21600 o:spt=75 o:preferrelative=t
 path=m@4@5l@4@11@9@11@9@5xe filled=f stroked=f>
 <v:stroke joinstyle=miter/>
 <v:formulas>
  <v:f eqn=if lineDrawn pixelLineWidth 0/>
  <v:f eqn=sum @0 1 0/>
  <v:f eqn=sum 0 0 @1/>
  <v:f eqn=prod @2 1 2/>
  <v:f eqn=prod @3 21600 pixelWidth/>
  <v:f eqn=prod @3 21600 pixelHeight/>
  <v:f eqn=sum @0 0 1/>
  <v:f eqn=prod @6 1 2/>
  <v:f eqn=prod @7 21600 pixelWidth/>
  <v:f eqn=sum @8 21600 0/>
  <v:f eqn=prod @7 21600 pixelHeight/>
  <v:f eqn=sum @10 21600 0/>
 </v:formulas>
 <v:path o:extrusionok=f gradientshapeok=t o:connecttype=rect/>
 <o:lock v:ext=edit aspectratio=t/>
</v:shapetype><v:shape id=Picture_x0020_2 o:spid=_x0000_s1026 type=#_x0000_t75
 style='position:absolute;margin-left:0;margin-top:0;width:93.75pt;height:21pt;
 z-index:251658240;visibility:visible;mso-wrap-style:square;
 mso-width-percent:0;mso-height-percent:0;mso-wrap-distance-left:9pt;
 mso-wrap-distance-top:3.75pt;mso-wrap-distance-right:9pt;
 mso-wrap-distance-bottom:3.75pt;mso-position-horizontal:left;
 mso-position-horizontal-relative:text;mso-position-vertical:absolute;
 mso-position-vertical-relative:line;mso-width-percent:0;mso-height-percent:0;
 mso-width-relative:page;mso-height-relative:page' o:allowoverlap=f>
 <v:imagedata src=9.%20&#272;ánh%20giá%20tình%20tr&#7841;ng%20h&#7885;c%20vi&#7879;c%20th&#7917;%20vi&#7879;c%20c&#7911;a%20NL&#272;_files/image003.png/>
 <w:wrap type=square anchory=line/>
</v:shape><![endif]--><![if !vml]><img width=125 height=28
src=9.%20&#272;ánh%20giá%20tình%20tr&#7841;ng%20h&#7885;c%20vi&#7879;c%20th&#7917;%20vi&#7879;c%20c&#7911;a%20NL&#272;_files/image003.png
align=left hspace=12 vspace=5 v:shapes=Picture_x0020_2><![endif]><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman'><o:p></o:p></span></p>

</div>

</body>
";
            xrRichText1.Html = s;
        }
        private void PrintingSystem_AfterMarginsChange(object sender, DevExpress.XtraPrinting.MarginsChangeEventArgs e)
        {
            Convert.ToInt32(Math.Round(e.Value));
            switch (e.Side)
            {
                case DevExpress.XtraPrinting.MarginSide.Left:
                    Margins = new System.Drawing.Printing.Margins((int)e.Value, Margins.Right, Margins.Top, Margins.Bottom);
                    CreateDocument();
                    break;
                case DevExpress.XtraPrinting.MarginSide.Right:
                    Margins = new System.Drawing.Printing.Margins(Margins.Left, (int)e.Value, Margins.Top, Margins.Bottom);
                    CreateDocument();
                    break;
                case DevExpress.XtraPrinting.MarginSide.All:
                    Margins = (sender as DevExpress.XtraPrinting.PrintingSystemBase).PageSettings.Margins;
                    CreateDocument();
                    break;
                default:
                    break;
            }
        }
        private void PrintingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            XtraPageSettingsBase pageSettings = ((PrintingSystemBase)sender).PageSettings;
            PaperKind = pageSettings.PaperKind;
            Landscape = pageSettings.Landscape;
            Margins = new System.Drawing.Printing.Margins(pageSettings.LeftMargin, pageSettings.RightMargin, pageSettings.TopMargin, pageSettings.BottomMargin);
            CreateDocument();
        }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
