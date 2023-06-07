using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Microsoft.ApplicationBlocks.Data;
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
            //PrintingSystem.AfterMarginsChange += PrintingSystem_AfterMarginsChange;
            //PrintingSystem.PageSettingsChanged += PrintingSystem_PageSettingsChanged;
           
            //Loaddata();
        }
        private void Loaddata()
        {
            string s = @"<body lang=EN-US style='tab-interval:.5in;word-wrap:break-word'>

<div class=WordSection1>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.5pt;
margin-bottom:1.3pt;margin-left:.5pt;text-align:center;line-height:115%'><b
style='mso-bidi-font-weight:normal'><span style='font-size:14.0pt;line-height:
115%;font-family:Times New Roman,serif;text-transform:uppercase'>C&#7897;ng
Hòa Xã H&#7897;i Ch&#7911; Ngh&#297;a Vi&#7879;t Nam<o:p></o:p></span></b></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.5pt;
margin-bottom:1.3pt;margin-left:.5pt;text-align:center;line-height:115%'><span
class=SpellE><b style='mso-bidi-font-weight:normal'><span style='font-size:
14.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;&#7897;c</span></b></span><b
style='mso-bidi-font-weight:normal'><span style='font-size:14.0pt;line-height:
115%;font-family:Times New Roman,serif'> <span class=SpellE>l&#7853;p</span>
– <span class=SpellE>T&#7921;</span> do – <span class=SpellE>H&#7841;nh</span> <span
class=SpellE>phúc</span><o:p></o:p></span></b></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.5pt;
margin-bottom:1.3pt;margin-left:.5pt;text-align:center;line-height:115%'><b
style='mso-bidi-font-weight:normal'><i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;text-transform:uppercase'>Socialist
Republic of Vietnam<o:p></o:p></span></i></b></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.5pt;
margin-bottom:1.3pt;margin-left:.5pt;text-align:center;line-height:115%'><b
style='mso-bidi-font-weight:normal'><i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Independence – Freedom –
Happiness<o:p></o:p></span></i></b></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.5pt;
margin-bottom:1.3pt;margin-left:0in;text-align:center;text-indent:0in;
line-height:115%'><b style='mso-bidi-font-weight:normal'><span
style='font-size:18.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.5pt;
margin-bottom:1.3pt;margin-left:.5pt;text-align:center;line-height:115%'><b
style='mso-bidi-font-weight:normal'><span style='font-size:18.0pt;line-height:
115%;font-family:Times New Roman,serif'>H&#7906;P &#272;&#7890;NG NGUYÊN T&#7854;C</span></b><span
style='font-size:18.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p></o:p></span></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.3pt;
margin-bottom:0in;margin-left:.5pt;text-align:center;line-height:115%'><b
style='mso-bidi-font-weight:normal'><i><span style='font-size:18.0pt;
line-height:115%;font-family:Times New Roman,serif'>PRINCIPAL CONTRACT</span></i></b><i><span
style='font-size:18.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p></o:p></span></i></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:0in;
margin-bottom:.4pt;margin-left:.5pt;text-align:center;line-height:115%'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>(V/v:
Gia <span class=SpellE>công</span> <span class=SpellE>s&#7843;n</span> <span
class=SpellE>ph&#7849;m</span> <span class=SpellE>d&#7879;t</span> may)<o:p></o:p></span></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.25pt;
margin-bottom:.4pt;margin-left:.5pt;text-align:center;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>(Ref:
Manufacturing &amp; processing of garments)<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:.25pt;margin-bottom:.4pt;
margin-left:.5pt;line-height:115%'><i><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:.25pt;
margin-bottom:.4pt;margin-left:.5pt;text-align:center;line-height:115%'><span
class=SpellE><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'>S&#7889;</span></i></span><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>/ <span
class=GramE>No:<span style='color:windowtext'>scot</span></span></span></i><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
color:windowtext'>1<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:.25pt;margin-bottom:.4pt;
margin-left:.5pt;line-height:115%'><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:6.85pt;text-indent:-6.85pt;line-height:115%;mso-list:l0 level1 lfo1'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp; </span></span><![endif]><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'>  </span><span class=SpellE>C&#259;n</span> <span
class=SpellE>c&#7913;</span> <span class=SpellE>B&#7897;</span> <span
class=SpellE>lu&#7853;t</span> <span class=SpellE>Dân</span> <span
class=SpellE>s&#7921;</span> <span class=SpellE>s&#7889;</span> 91/2015/QH13 <span
class=SpellE>d&#432;&#7907;c</span> <span class=SpellE>Qu&#7889;c</span> <span
class=SpellE>h&#7897;i</span> <span class=SpellE>n&#432;&#7899;c</span> <span
class=SpellE>C&#7897;ng</span> <span class=SpellE>hoà</span> <span
class=SpellE>xã</span> <span class=SpellE>h&#7897;i</span> <span class=SpellE>ch&#7911;</span>
<span class=SpellE>ngh&#297;a</span> <span class=SpellE>Vi&#7879;t</span> Nam <span
class=SpellE>thông</span> qua <span class=SpellE>ngày</span> 24 <span
class=SpellE>tháng</span> 11 <span class=SpellE>n&#259;m</span> 2015.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:6.85pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Pursuant to the
Civil Code No. 91/2015/QH13 approved on 24&quot; November 2015 by the National
Assembly of the Socialist Republic of Vietnam.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.3pt;
margin-left:6.85pt;text-indent:-6.85pt;line-height:115%;mso-list:l0 level1 lfo1'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp; </span></span><![endif]><span
class=SpellE><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>C&#259;n</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>c&#7913;</span> <span class=SpellE>Lu&#7853;t</span> <span
class=SpellE>Th&#432;&#417;ng</span> <span class=SpellE>m&#7841;i</span> <span
class=SpellE>s&#7889;</span> 36/2005/QH 11 do <span class=SpellE>Qu&#7889;c</span>
<span class=SpellE>h&#7897;i</span> <span class=SpellE>n&#432;&#7899;c</span> <span
class=SpellE>C&#7897;ng</span> <span class=SpellE>hòa</span> <span
class=SpellE>xã</span> <span class=SpellE>h&#7897;i</span> <span class=SpellE>chú</span>
<span class=SpellE>ngh&#297;a</span> <span class=SpellE>Vi&#7879;t</span> Nam <span
class=SpellE>thông</span> qua <span class=SpellE>ngày</span> 14 <span
class=SpellE>tháng</span> 6 <span class=SpellE>n&#259;m</span> 2005.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:6.85pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Pursuant to the
Law on Commerce No. 36/2005/QH 11 approved on 14&quot; June 2005 by the
National Assembly of the Socialist Republic of Vietnam.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.4pt;
margin-left:6.85pt;text-indent:-6.85pt;line-height:115%;mso-list:l0 level1 lfo1'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp; </span></span><![endif]><span
class=SpellE><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>C&#259;n</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>c&#7913;</span> <span class=SpellE>nhu</span> <span class=SpellE>c&#7847;u</span>
<span class=SpellE>và</span> <span class=SpellE>kh&#7843;</span> <span
class=SpellE>n&#259;ng</span> <span class=SpellE>các</span> <span class=SpellE>Bên</span>,
<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.3pt;
margin-left:6.85pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>With reference to
the demand and capacity of the Parties <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.8pt;
margin-left:0in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:133.45pt;margin-bottom:
.25pt;margin-left:.5pt;line-height:115%'><span class=SpellE><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>Hôm</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>
nay, <span class=SpellE>ngày</span> </span><i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;color:windowtext'>scot2</span></i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>chúng</span> <span class=SpellE>tôi</span> <span class=SpellE>g&#7891;m</span>:<span
style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:133.45pt;margin-bottom:
.25pt;margin-left:.5pt;line-height:115%'><i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Today, </span></i><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
color:windowtext'>scot3 </span></i><i><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'>we are: <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><b style='mso-bidi-font-weight:
normal'><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><b style='mso-bidi-font-weight:
normal'><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>BÊN
&#272;&#7862;T GIA CÔNG (<i style='mso-bidi-font-style:normal'>PRINCIPLE</i><span
class=GramE>) :</span> </span></b><i><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'>scot4</span></i><b style='mso-bidi-font-weight:
normal'><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
background:yellow;mso-highlight:yellow'><o:p></o:p></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.4pt;
margin-left:.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;&#7883;a</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>ch&#7881;</span> (<i style='mso-bidi-font-style:normal'>Address</i><span
class=GramE>) :</span></span></b><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'> <span
style='mso-spacerun:yes'> </span><span style='mso-bidi-font-style:italic'>scot5<i><o:p></o:p></i></span></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.4pt;
margin-left:.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;i&#7879;n</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>tho&#7841;i</span> <i style='mso-bidi-font-style:normal'>(Phone)</i>:</span></b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> </span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>scot6<span
style='background:yellow;mso-highlight:yellow'><o:p></o:p></span></span></p>

<p class=MsoNormal style='margin:0in;text-indent:0in;line-height:115%'><span
class=SpellE><b><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif;mso-fareast-font-family:Times New Roman;color:windowtext'>S&#7889;</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>tài</span> <span class=SpellE>kho&#7843;n</span> <span
class=SpellE>ngân</span> <span class=SpellE>hàng</span> (<i style='mso-bidi-font-style:
normal'>Bank Account</i>)</span></b><i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>: </span></i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>scot7<span style='background:yellow;
mso-highlight:yellow'><o:p></o:p></span></span></p>

<p class=MsoNormal style='margin:0in;text-indent:0in;line-height:115%'><span
class=SpellE><b><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif;mso-fareast-font-family:Times New Roman;color:windowtext'>Tên</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>Ngân</span> <span class=SpellE>Hàng</span> (<i style='mso-bidi-font-style:
normal'>Bank Name</i>)</span></b><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif;mso-fareast-font-family:Times New Roman;
color:windowtext'>: scot8</span><span style='font-size:12.0pt;line-height:115%;
font-family:Times New Roman,serif;color:#202124;background:yellow;mso-highlight:
yellow;mso-shading:white'><o:p></o:p></span></p>

<p class=MsoNormal style='margin:0in;text-indent:0in;line-height:115%'><span
class=SpellE><b><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'>Mã</span></b></span><b><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'> <span class=SpellE>s&#7889;</span>
<span class=SpellE>thu&#7871;</span> (<i style='mso-bidi-font-style:normal'>Tax
Code</i>):</span></b><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'> </span><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif;mso-fareast-font-family:Times New Roman;
color:windowtext'>scot9<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.4pt;
margin-left:.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;&#7841;i</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>di&#7879;n</span> (<i>Represented by</i>):</span></b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> </span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>scot10 </span><span
class=SpellE><b><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'>Ch&#7913;c</span></b></span><b><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>v&#7909;</span> (Position<span class=GramE>) :</span> </span></b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>scot11</span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
background:yellow;mso-highlight:yellow'><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.3pt;
margin-left:.5pt;line-height:115%'><b><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span>(Sau <span class=SpellE>&#273;ây</span> <span
class=SpellE>g&#7885;i</span> <span class=SpellE>là</span> “<span class=SpellE>Bên</span>
`A”) <i>(Hereinafter referred to as &quot;Party A&quot;) <o:p></o:p></i></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:165.25pt;margin-bottom:
.25pt;margin-left:.5pt;line-height:115%'><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;background:yellow;
mso-highlight:yellow'><span style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:.2pt;margin-bottom:.25pt;
margin-left:.5pt;line-height:115%'><b style='mso-bidi-font-weight:normal'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>BÊN
GIA CÔNG<i><span style='mso-bidi-font-weight:bold'>:</span></i> </span></b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>scot12<b
style='mso-bidi-font-weight:normal'><span style='background:yellow;mso-highlight:
yellow'><o:p></o:p></span></b></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:.2pt;margin-bottom:.25pt;
margin-left:.5pt;line-height:115%'><b><i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>MANUFACTURER:</span></i></b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-bidi-font-weight:bold;mso-bidi-font-style:italic'> scot13<b><i><span
style='background:yellow;mso-highlight:yellow'><o:p></o:p></span></i></b></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.3pt;
margin-left:.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;&#7883;a</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>ch&#7881;</span> (Address): </span></b><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>scot14<span
style='background:yellow;mso-highlight:yellow'><o:p></o:p></span></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:135.7pt;margin-bottom:
1.3pt;margin-left:.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;i&#7879;n</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>tho&#7841;i</span> (<i style='mso-bidi-font-style:normal'>Phone
Number</i>)</span></b><span style='font-size:12.0pt;line-height:115%;
font-family:Times New Roman,serif'>: scot15</span><span style='font-family:
Times New Roman,serif'> </span><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif;background:yellow;mso-highlight:yellow'><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:135.7pt;margin-bottom:
1.3pt;margin-left:.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>Mã</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>s&#7889;</span> <span class=SpellE>thu&#7871;</span> (<i
style='mso-bidi-font-style:normal'>Tax Code</i><span class=GramE>):<span
style='font-weight:normal'>scot</span></span></span></b><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>16<span
style='background:yellow;mso-highlight:yellow'><o:p></o:p></span></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.3pt;
margin-left:.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
background:white'>Tài</span></b></span><b><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;background:white'> <span
class=SpellE>kho&#7843;n</span> <span class=SpellE>ngân</span> <span
class=SpellE>hàng</span> (<i style='mso-bidi-font-style:normal'>Bank Account</i>):
</span></b><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
background:white;mso-bidi-font-weight:bold'>scot17<b><span style='background:
yellow;mso-highlight:yellow'><o:p></o:p></span></b></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.3pt;
margin-left:.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
background:white'>Tên</span></b></span><b><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;background:white'> <span
class=SpellE>Ngân</span> <span class=SpellE>Hàng</span> (<i style='mso-bidi-font-style:
normal'>Bank Name</i>):</span></b><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif;background:white;mso-bidi-font-weight:
bold'> scot18<b><span style='background:yellow;mso-highlight:yellow'><o:p></o:p></span></b></span></p>

<p class=MsoNormal align=left style='margin-top:0in;margin-right:0in;
margin-bottom:1.3pt;margin-left:.5pt;text-align:left;line-height:115%'><span
class=SpellE><b><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'>&#272;&#7841;i</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>di&#7879;n</span> (<i>Represented by)</i>: </span></b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-bidi-font-weight:bold'>scot19<b> <span class=SpellE>Ch&#7913;c</span> <span
class=SpellE>v&#7909;</span> (<i>Position)</i>:</b></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> scot20<span
style='background:yellow;mso-highlight:yellow'><o:p></o:p></span></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.3pt;
margin-left:.5pt;line-height:115%'><b><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'>(Sau <span class=SpellE>&#273;ây</span>
<span class=SpellE>g&#7885;i</span> <span class=SpellE>là</span> “<span
class=SpellE>Bên</span> B”) <i>(Hereinafter referred to as &quot;Party B&quot;)
<o:p></o:p></i></span></b></p>

<p class=MsoListParagraph style='margin:0in;mso-add-space:auto;text-indent:
0in;line-height:normal'><b><i><span style='font-size:12.0pt;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>

<h1 style='margin-top:0in;margin-right:3.0in;margin-bottom:.25pt;margin-left:
0in;text-align:justify;text-indent:0in;line-height:115%;mso-list:none'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>XÉT
R&#7856;NG: <o:p></o:p></span></h1>

<h1 style='margin-top:0in;margin-right:3.0in;margin-bottom:.25pt;margin-left:
0in;text-align:justify;text-indent:0in;line-height:115%;mso-list:none'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>WHERE
AS</span></i><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>:
<o:p></o:p></span></h1>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:1.3pt;
margin-left:.5in;text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span style='font-size:12.0pt;line-height:115%;
font-family:Times New Roman,serif'>Sau <span class=SpellE>khi</span> <span
class=SpellE>th&#7887;a</span> <span class=SpellE>thu&#7853;n</span>, <span
class=SpellE>hai</span> <span class=SpellE>Bên</span> <span class=SpellE>cùng</span>
<span class=SpellE>&#273;&#7891;ng</span> ý <span class=SpellE>ký</span> <span
class=SpellE>k&#7871;t</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>nguyên</span> <span
class=SpellE>t&#7855;c</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>vi&#7879;c</span> <span class=SpellE>gia</span> <span
class=SpellE>công</span> <span class=SpellE>qu&#7847;n</span> <span
class=SpellE>áo</span> <span class=SpellE>v&#7899;i</span> <span class=SpellE>các</span>
<span class=SpellE>&#273;i&#7873;u</span> <span class=SpellE>kho&#7843;n</span>
<span class=SpellE>sau</span>:<span style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Upon agreement,
the Parties hereby agree to <span class=GramE>enter into</span> the Principal
Contract on garment manufacturing – (Hereinafter referred to a
&quot;Contract&quot;) with terms and conditions as follows: <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></p>

<h1 style='margin-top:0in;margin-right:73.55pt;margin-bottom:.25pt;margin-left:
.25in;text-align:justify;text-indent:-.25in;line-height:115%;mso-list:none'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;I&#7872;U
1: CÁC NGUYÊN T&#7854;C CHUNG<o:p></o:p></span></h1>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.25in;text-indent:-.25in;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>ARTICLE
1: GENERAL PRINCIPLES <o:p></o:p></span></i></b></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
.25pt;margin-left:35.45pt;mso-add-space:auto;text-indent:-35.45pt;line-height:
115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span style='font-size:12.0pt;line-height:115%;
font-family:Times New Roman,serif'>Hai <span class=SpellE>Bên</span> <span
class=SpellE>tham</span> <span class=SpellE>gia</span> <span class=SpellE>ký</span>
<span class=SpellE>k&#7871;t</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>trên</span> <span class=SpellE>c&#417;</span> <span class=SpellE>s&#7903;</span>
<span class=SpellE>quan</span> <span class=SpellE>h&#7879;</span> <span
class=SpellE>B&#7841;n</span> <span class=SpellE>hàng</span> <span
class=SpellE>bình</span> <span class=SpellE>&#273;&#7859;ng</span> <span
class=SpellE>và</span> <span class=SpellE>c&#361;ng</span> <span class=SpellE>có</span>
<span class=SpellE>l&#7907;i</span> <span class=SpellE>theo</span> <span
class=SpellE>&#273;úng</span> <span class=SpellE>các</span> <span class=SpellE>quy</span>
<span class=SpellE>&#273;&#7883;nh</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>Pháp</span> <span class=SpellE>lu&#7853;t</span>.<span
style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:31.5pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>The Parties <span
class=GramE>enter into</span> this Contract the basis of Partnership, equality,
and mutual benefits accordance with the laws. </span></i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:-31.5pt;line-height:
115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Các</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>n&#7897;i</span> dung <span class=SpellE>trong</span> <span
class=SpellE>b&#7843;n</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>nguyên</span> <span
class=SpellE>t&#7855;c</span> <span class=SpellE>này</span> <span class=SpellE>ch&#7881;</span>
<span class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>s&#7917;a</span>
<span class=SpellE>&#273;&#7893;i</span> <span class=SpellE>khi</span> <span
class=SpellE>có</span> <span class=SpellE>s&#7921;</span> <span class=SpellE>th&#7887;a</span>
<span class=SpellE>thu&#7853;n</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>các</span> <span class=SpellE>Bên</span> <span class=SpellE>và</span>
<span class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>th&#7889;ng</span>
<span class=SpellE>nh&#7845;t</span> <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>v&#259;n</span> <span class=SpellE>b&#7843;n</span>. <span
class=SpellE>V&#259;n</span> <span class=SpellE>b&#7843;n</span> <span
class=SpellE>thay</span> <span class=SpellE>&#273;&#7893;i</span> <span
class=SpellE>n&#7897;i</span> dung <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>nguyên</span> <span
class=SpellE>t&#7855;c</span> <span class=SpellE>này</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
<span class=SpellE>xem</span> <span class=SpellE>là</span> <span class=SpellE>Ph&#7909;</span>
<span class=SpellE>l&#7909;c</span> <span class=SpellE>và</span> <span
class=SpellE>là</span> <span class=SpellE>m&#7897;t</span> <span class=SpellE>ph&#7847;n</span>
<span class=SpellE>không</span> <span class=SpellE>th&#7875;</span> <span
class=SpellE>tách</span> <span class=SpellE>r&#7901;i</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:31.5pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Contents herein
are only amended when it is agreed in writing and approved by the Parties
Amendment to this Contract <span class=GramE>is considered to be</span>
Appendix and an integral part of the Contract. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:31.5pt;text-indent:0in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Các</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>n&#7897;i</span> dung <span class=SpellE>h&#7907;p</span> <span
class=SpellE>tác</span> <span class=SpellE>mua</span> <span class=SpellE>bán</span>
<span class=SpellE>hàng</span> <span class=SpellE><span class=GramE>hóa</span></span><span
class=GramE><span style='mso-spacerun:yes'>  </span><span class=SpellE>theo</span></span>
<span class=SpellE>t&#7915;ng</span> <span class=SpellE>th&#7901;i</span> <span
class=SpellE>&#273;i&#7875;m</span> <span class=SpellE>s&#7869;</span> <span
class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>c&#7909;</span> <span
class=SpellE>th&#7875;</span> <span class=SpellE>hóa</span> <span class=SpellE>trong</span>
<span class=SpellE>các</span> <span class=SpellE>v&#259;n</span> <span
class=SpellE>b&#7843;n</span> <span class=SpellE>Ph&#7909;</span> <span
class=SpellE>l&#7909;c</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>trên</span> <span
class=SpellE>t&#7915;ng</span> <span class=SpellE>&#273;&#417;n</span> <span
class=SpellE>hàng</span>. <span class=SpellE>&#272;i&#7873;u</span> <span
class=SpellE>kho&#7843;n</span> <span class=SpellE>nào</span> <span
class=SpellE>trong</span> <span class=SpellE>Ph&#7909;</span> <span
class=SpellE>l&#7909;c</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>mua</span> <span
class=SpellE>bán</span> <span class=SpellE>mâu</span> <span class=SpellE>thu&#7851;n</span>
<span class=SpellE>v&#7899;i</span> <span class=SpellE>các</span> <span
class=SpellE>&#273;i&#7873;u</span> <span class=SpellE>kho&#7843;n</span> <span
class=SpellE>trong</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>thì</span> <span class=SpellE>s&#7869;</span> <span class=SpellE>th&#7921;c</span>
<span class=SpellE>hi&#7879;n</span> <span class=SpellE>theo</span> <span
class=SpellE>các</span> <span class=SpellE>&#273;i&#7873;u</span> <span
class=SpellE>kho&#7843;n</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
qui <span class=SpellE>&#273;&#7883;nh</span> <span class=SpellE>trong</span> <span
class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span>. <span
style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>The specific
cooperation and manufacturing &amp; processing contents from time to time shall
concretized in the separate specific Sales appendixes in each PO. If any
provisions of the Sales Contract conflict with provisions of this Contract,
provisions this Contract shall prevail. </span></i><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman'><span style='mso-spacerun:yes'> </span><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:31.5pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><span class=SpellE><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman'>M&#7897;t</span></i></b></span><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman'> <span class=SpellE>s&#7889;</span> <span
class=SpellE>quy</span> <span class=SpellE>&#273;&#7883;nh</span> <span
class=SpellE>chung</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>&#273;&#7843;m</span> <span class=SpellE>b&#7843;o</span> <span
class=SpellE>s&#7921;</span> <span class=SpellE>tuân</span> <span class=SpellE>th&#7911;</span>
<span class=SpellE>v&#7873;</span> <span class=SpellE>quy</span> <span
class=SpellE>t&#7855;c</span> <span class=SpellE>lao</span> <span class=SpellE>&#273;&#7897;ng</span>,
<span class=GramE>an</span> <span class=SpellE>toàn</span> <span class=SpellE>và</span>
<span class=SpellE>môi</span> <span class=SpellE>tr&#432;&#7901;ng</span>: <o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman'>General regulations on ensuring compliance with labor
regulations, <span class=GramE>safety</span> and environment:<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-31.5pt;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>hoàn</span> <span class=SpellE>toàn</span> <span class=SpellE>trách</span>
<span class=SpellE>nhi&#7879;m</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>vi&#7879;c</span> <span class=SpellE>tuân</span> <span
class=SpellE>th&#7911;</span> <span class=SpellE>các</span> <span class=SpellE>quy</span>
<span class=SpellE>t&#7855;c</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>lao</span> <span class=SpellE>&#273;&#7897;ng</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>khách</span> <span
class=SpellE>hàng</span> <span class=SpellE>và</span> <span class=SpellE>tuân</span>
<span class=SpellE>th&#7911;</span> <span class=SpellE>Lu&#7853;t</span> <span
class=SpellE>lao</span> <span class=SpellE>&#273;&#7897;ng</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>n&#432;&#7899;c</span> <span
class=SpellE>C&#7897;ng</span> <span class=SpellE>Hòa</span> <span
class=SpellE>Xã</span> <span class=SpellE>H&#7897;i</span> <span class=SpellE>Ch&#7911;</span>
<span class=SpellE>Ngh&#297;a</span> <span class=SpellE>Vi&#7879;t</span> Nam<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party B is in full compliance with buyer's
regulations and Socialist Republic of Vietnam which is applicable.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:-31.5pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-31.5pt;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>s&#7869;</span> <span
class=SpellE>t&#7921;</span> <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>th&#7921;c</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>công</span> <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>theo</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>v&#7899;i</span> <span
class=SpellE>s&#7921;</span> <span class=SpellE>quan</span> <span class=SpellE>tâm</span>
<span class=SpellE>cao</span> <span class=SpellE>nh&#7845;t</span> <span
class=SpellE>&#273;&#7871;n</span> an <span class=SpellE>toàn</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>nhân</span> <span
class=SpellE>viên</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>mình</span>, <span class=SpellE>nhân</span> <span class=SpellE>viên</span>
<span class=SpellE>c&#7911;a</span> Công ty <span class=SpellE>ho&#7863;c</span>
<span class=SpellE>nh&#7919;ng</span> <span class=SpellE>ng&#432;&#7901;i</span>
<span class=SpellE>t&#7841;i</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>vùng</span> <span class=SpellE>lân</span> <span class=SpellE>c&#7853;n</span>
<span class=SpellE>c&#7911;a</span> <span class=SpellE>bên</span> B.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:-31.5pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party B shall be solely responsible for
carrying out the work under the Contract having the highest regard for the
safety of its employees, the Company’s <span class=GramE>employees</span> or
persons at or in the vicinity of the Site.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:-31.5pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-31.5pt;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>tuân</span> <span class=SpellE>th&#7911;</span> <span
class=SpellE>và</span> <span class=SpellE>ch&#7883;u</span> <span class=SpellE>trách</span>
<span class=SpellE>nhi&#7879;m</span> <span class=SpellE>&#273;&#7843;m</span> <span
class=SpellE>b&#7843;o</span> <span class=SpellE>r&#7857;ng</span> <span
class=SpellE>t&#7845;t</span> <span class=SpellE>c&#7843;</span> <span
class=SpellE>nhân</span> <span class=SpellE>viên</span> <span class=SpellE>c&#7911;a</span>
<span class=SpellE>mình</span> <span class=SpellE>tuân</span> <span
class=SpellE>th&#7911;</span> <span class=SpellE>các</span> <span class=SpellE>quy</span>
<span class=SpellE>&#273;&#7883;nh</span> <span class=SpellE>pháp</span> <span
class=SpellE>Lu&#7853;t</span> <span class=SpellE>Vi&#7879;t</span> Nam <span
class=SpellE>liên</span> <span class=SpellE>quan</span> <span class=SpellE>&#273;&#7871;n</span>
an <span class=SpellE>toàn</span> <span class=SpellE>s&#7913;c</span> <span
class=SpellE>kh&#7887;e</span> <span class=SpellE>và</span> <span class=SpellE>môi</span>
<span class=SpellE>tr&#432;&#7901;ng</span> (EHS)<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:-31.5pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party B shall comply and shall be
responsible for ensuring that all its employees comply with the relevant
Vietnam EHS statutory regulations. </span></i><i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman'><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin:0in;text-indent:0in;line-height:115%'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:31.5pt;text-indent:-31.5pt;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>Các</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>tài</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>có</span> <span class=SpellE>liên</span> <span class=SpellE>quan</span>
<span class=SpellE>và</span> <span class=SpellE>g&#7855;n</span> <span
class=SpellE>li&#7873;n</span> <span class=SpellE>v&#7899;i</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>này</span> bao <span class=SpellE>g&#7891;m</span>: <o:p></o:p></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><b><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>The documents
related and associated to this Contract include: <o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><b><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>

<p class=MsoListParagraphCxSpFirst style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:-31.5pt;
line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Các</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>mua</span> <span class=SpellE>bán</span> <span class=SpellE>c&#7909;</span>
<span class=SpellE>th&#7875;</span> <span class=SpellE>theo</span> <span
class=SpellE>t&#7915;ng</span> <span class=SpellE>th&#7901;i</span> <span
class=SpellE>&#273;i&#7875;m</span> <span class=SpellE>trong</span> <span
class=SpellE>th&#7901;i</span> <span class=SpellE>gian</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>nguyên</span> <span class=SpellE>t&#7855;c</span> <span
class=SpellE>này</span> <span class=SpellE>có</span> <span class=SpellE>hi&#7879;u</span>
<span class=SpellE>l&#7921;c</span> <span class=SpellE>trong</span> <span
class=SpellE>vòng</span> 12 (<span class=SpellE>m&#432;&#7901;i</span> <span
class=SpellE>hai</span>) <span class=SpellE>tháng</span> <span class=SpellE>k&#7875;</span>
<span class=SpellE>t&#7915;</span> <span class=SpellE>các</span> <span
class=SpellE>Bên</span> <span class=SpellE>ký</span> <span class=SpellE>h&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>nguyên</span> <span
class=SpellE>t&#7855;c</span>. <o:p></o:p></span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:0in;
line-height:115%'><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'>Specific sales contract from time to time during the
Contract validity is valid within 12 (twelve) months since the principal
contract is signed. <o:p></o:p></span></i></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:0in;
line-height:115%'><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:-31.5pt;
line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>H&#7907;p</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>nguyên</span> <span
class=SpellE>t&#7855;c</span> <span class=SpellE>ch&#7881;</span> <span
class=SpellE>có</span> <span class=SpellE>hi&#7879;u</span> <span class=SpellE>l&#7921;c</span>
<span class=SpellE>khi</span> <span class=SpellE>các</span> <span class=SpellE>Bên</span>
<span class=SpellE>ký</span> <span class=SpellE>k&#7871;t</span> <span
class=SpellE>và</span> <span class=SpellE>làm</span> <span class=SpellE>vi&#7879;c</span>
<span class=SpellE>v&#7899;i</span> <span class=SpellE>nhau</span> <span
class=SpellE>b&#7857;ng</span> <span class=SpellE>các</span> <span
class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>h&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>gia</span> <span class=SpellE>công</span> <span class=SpellE>c&#7909;</span>
<span class=SpellE>th&#7875;</span> <span class=SpellE>theo</span> <span
class=SpellE>t&#7915;ng</span> <span class=SpellE>th&#7901;i</span> <span
class=SpellE>&#273;i&#7875;m</span> (<span class=SpellE>trong</span> <span
class=SpellE>vòng</span> 12 <span class=SpellE>tháng</span>) <span
class=SpellE>và</span> <span class=SpellE>m&#7863;c</span> <span class=SpellE>&#273;&#7883;nh</span>
<span class=SpellE>vô</span> <span class=SpellE>hi&#7879;u</span> <span
class=SpellE>khi</span> <span class=SpellE>các</span> <span class=SpellE>Bên</span>
<span class=SpellE>không</span> <span class=SpellE>có</span> <span
class=SpellE>b&#7845;t</span> <span class=SpellE>k&#7923;</span> <span
class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>gia</span> <span class=SpellE>công</span> <span class=SpellE>nào</span>
<span class=SpellE>v&#7899;i</span> <span class=SpellE>nhau</span>. <span
class=SpellE>N&#7871;u</span> <span class=SpellE>ti&#7871;p</span> <span
class=SpellE>t&#7909;c</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>tác</span> <span class=SpellE>s&#7869;</span> <span class=SpellE>th&#7887;a</span>
<span class=SpellE>thu&#7853;n</span> <span class=SpellE>&#273;&#7875;</span> <span
class=SpellE>th&#7889;ng</span> <span class=SpellE>nh&#7845;t</span> <span
class=SpellE>gia</span> <span class=SpellE>h&#7841;n</span> <span class=SpellE>H&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>v&#259;n</span> <span class=SpellE>b&#7843;n</span>. <o:p></o:p></span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:0in;
line-height:115%'><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'>The principal contract shall be only valid since the
two parties sign with detail processing appendixes in specific time (within 12
months) and the position when the two parties do not have any contracts. If we
continue to cooperate, we will agree to extend the contract by written
document. <o:p></o:p></span></i></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:0in;
line-height:115%'><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraphCxSpLast style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:0in;
line-height:115%'><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.25in;text-indent:-.25in;line-height:115%'><b><u><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>&#272;I&#7872;U 2:</span></u></b><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> HÀNG HÓA, S&#7888;
L&#431;&#7906;NG, GIÁ GIA CÔNG</span></b><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:151.6pt;text-indent:-151.6pt;line-height:115%'><b><i><u><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>ARTICLE 2: </span></u></i></b><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>COMMODITY – QUANTITY
- PROCESSING PRICE OF GOODS<o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraphCxSpFirst style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:-31.5pt;
line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Hàng</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> <span class=SpellE>hóa</span> <span
class=SpellE>thu&#7897;c</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>g&#7891;m</span> <span class=SpellE>các</span> <span class=SpellE>ch&#7911;ng</span>
<span class=SpellE>lo&#7841;i</span> <span class=SpellE>hàng</span> scot21. <o:p></o:p></span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:0in;
line-height:115%'><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif;mso-fareast-font-family:Times New Roman;color:windowtext'>Commodities
of goods subjected to this contract are scot22.<o:p></o:p></span></i></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:0in;
line-height:115%'><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif;mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraphCxSpLast style='margin-top:0in;margin-right:0in;
margin-bottom:.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:-31.5pt;
line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Hàng</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> <span class=SpellE>hóa</span>, <span
class=SpellE>quy</span> <span class=SpellE>cách</span>, <span class=SpellE>ch&#7845;t</span>
<span class=SpellE>l&#432;&#7907;ng</span>, <span class=SpellE>s&#7889;</span> <span
class=SpellE>l&#432;&#7907;ng</span>, <span class=SpellE>&#273;&#417;n</span> <span
class=SpellE>giá</span> <span class=SpellE>gia</span> <span class=SpellE>công</span>,
<span class=SpellE>ngày</span> <span class=SpellE>giao</span> <span
class=SpellE>hàng</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>các</span> <span class=SpellE>Bên</span> <span class=SpellE>th&#7887;a</span>
<span class=SpellE>thu&#7853;n</span>, <span class=SpellE>và</span> <span
class=SpellE>nêu</span> <span class=SpellE>rõ</span> <span class=SpellE>trong</span>
<span class=SpellE>t&#7915;ng</span> <span class=SpellE>Ph&#7909;</span> <span
class=SpellE>l&#7909;c</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>d&#7921;a</span> <span
class=SpellE>theo</span> <span class=SpellE>nguyên</span> <span class=SpellE>t&#7855;c</span>
<span class=SpellE>và</span> <span class=SpellE>các</span> <span class=SpellE>&#273;i&#7873;u</span>
<span class=SpellE>kho&#7843;n</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
cam <span class=SpellE>k&#7871;t</span> <span class=SpellE>trên</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>Nguyên</span> <span class=SpellE>t&#7855;c</span> <span
class=SpellE>này</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:31.5pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Product, specification, quality
requirements, quantity, unit CMPT price, date of delivery will be agreed and
mentioned clearly in appendixes following with this Principal contract.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:31.5pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
.25pt;margin-left:31.5pt;mso-add-space:auto;text-indent:-31.5pt;line-height:
115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>&#272;&#417;n</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>giá</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>các</span> <span class=SpellE>Bên</span> <span class=SpellE>th&#7889;ng</span>
<span class=SpellE>nh&#7845;t</span> <span class=SpellE>b&#7857;ng</span> VND <span
class=SpellE>t&#7915;ng</span> <span class=SpellE>&#273;&#417;n</span> <span
class=SpellE>theo</span> <span class=SpellE>t&#7915;ng</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>t&#7841;i</span> <span class=SpellE>t&#7915;ng</span> <span
class=SpellE>th&#7901;i</span> <span class=SpellE>&#273;i&#7875;m</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>&#273;&#417;n</span> <span
class=SpellE>hàng</span>. <span class=SpellE>&#272;&#417;n</span> <span
class=SpellE>giá</span> <span class=SpellE>này</span> <span class=SpellE>là</span>
<span class=SpellE>c&#7889;</span> <span class=SpellE>&#273;&#7883;nh</span> <span
class=SpellE>và</span> <span class=SpellE>không</span> <span class=SpellE>thay</span>
<span class=SpellE>&#273;&#7893;i</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:31.5pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Unit price is agreed by both parties in <span
class=SpellE>Vietnnamdong</span> for each order according to each appendix at each
time of the order. This unit price is fixed and unchanged.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5pt;line-height:115%'><b><u><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>&#272;I&#7872;U 3:</span></u></b><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> MÁY MÓC, CÔNG SU&#7844;T,
CH&#7844;T L&#431;&#7906;NG, YÊU C&#7846;U K&#296; THU&#7852;T VÀ NGUYÊN
PH&#7908; LI&#7878;U<u><o:p></o:p></u></span></b></p>

<p class=MsoNormal style='margin:0in;text-indent:0in;line-height:115%'><b><i><u><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>ARTICLE 3:</span></u></i></b><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> MACHINE, CAPACITY,
QUALITY, TECHNICAL INFORMATION, MATERIAL SPECIFICATIONS<o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:147.9pt;margin-bottom:
0in;margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraphCxSpFirst style='margin-top:0in;margin-right:4.5pt;
margin-bottom:0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;
line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>s&#7869;</span> <span
class=SpellE>ph&#7843;i</span> <span class=SpellE>cung</span> <span
class=SpellE>c&#7845;p</span> <span class=SpellE>cho</span> <span class=SpellE>Bên</span>
B <span class=SpellE>các</span> <span class=SpellE>thông</span> tin <span
class=SpellE>và</span> <span class=SpellE>tài</span> <span class=SpellE>li&#7879;u</span>
<span class=SpellE>c&#7847;n</span> <span class=SpellE>thi&#7871;t</span> <span
class=SpellE>v&#7873;</span> <span class=SpellE>nguyên</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>li&#7879;u</span>, <span
class=SpellE>s&#7843;n</span> <span class=SpellE>ph&#7849;m</span> <span
class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span> <span
class=SpellE>theo</span> <span class=SpellE>nh&#7919;ng</span> <span
class=SpellE>th&#7901;i</span> <span class=SpellE>h&#7841;n</span> <span
class=SpellE>h&#7907;p</span> <span class=SpellE>lý</span> <span class=SpellE>theo</span>
<span class=SpellE>t&#7915;ng</span> <span class=SpellE>mã</span> <span
class=SpellE>hàng</span> <span class=SpellE>c&#7909;</span> <span class=SpellE>th&#7875;</span>
do <span class=SpellE>hai</span> <span class=SpellE>bên</span> <span
class=SpellE>th&#7887;a</span> <span class=SpellE>thu&#7853;n</span> <span
class=SpellE>trong</span> <span class=SpellE>t&#7915;ng</span> <span
class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>. <span
class=SpellE>Bên</span> A <span class=SpellE>có</span> <span class=SpellE>trách</span>
<span class=SpellE>nhi&#7879;m</span> <span class=SpellE>h&#432;&#7899;ng</span>
<span class=SpellE>d&#7851;n</span> <span class=SpellE>bên</span> B <span
class=SpellE>v&#7873;</span> <span class=SpellE>m&#7863;t</span> <span
class=SpellE>k&#7929;</span> <span class=SpellE>thu&#7853;t</span> <span
class=SpellE>&#273;&#7875;</span> <span class=SpellE>&#273;&#7843;m</span> <span
class=SpellE>b&#7843;o</span> <span class=SpellE>&#273;úng</span> <span
class=SpellE>tiêu</span> <span class=SpellE>chu&#7849;n</span> <span
class=SpellE>ch&#7845;t</span> <span class=SpellE>l&#432;&#7907;ng</span> <span
class=SpellE>theo</span> <span class=SpellE>quy</span> <span class=SpellE>&#273;&#7883;nh</span>
<span class=SpellE>c&#7911;a</span> <span class=SpellE>Khách</span> <span
class=SpellE>hàng</span>.<o:p></o:p></span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:4.5pt;
margin-bottom:0in;margin-left:28.35pt;mso-add-space:auto;text-indent:0in;
line-height:115%;tab-stops:31.5pt'><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif;mso-fareast-font-family:Times New Roman;
color:windowtext'><span style='mso-spacerun:yes'> </span><i>Party A shall
provide Party B with all the necessary information and documents regarding the
materials, accessories, and finished products by a reasonable timeline
according to the specific styles agreed by two party in each annex. Party A
shall instruct party B technical requirements to meet quality standard of
Customer.<o:p></o:p></i></span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:0in;margin-right:4.5pt;
margin-bottom:0in;margin-left:.5in;mso-add-space:auto;text-indent:0in;
line-height:115%;tab-stops:31.5pt'><i><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif;mso-fareast-font-family:Times New Roman;
color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraphCxSpLast style='margin-top:0in;margin-right:4.5pt;
margin-bottom:0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;
line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>&#272;&#7875;</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>bên</span> B <span class=SpellE>có</span> <span class=SpellE>nguyên</span>
<span class=SpellE>v&#7853;t</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>s&#7843;n</span> <span class=SpellE>xu&#7845;t</span> <span
class=SpellE>&#273;&#7911;</span> <span class=SpellE>&#273;&#417;n</span> <span
class=SpellE>hàng</span>, <span class=SpellE>bên</span> A <span class=SpellE>chuy&#7875;n</span>
<span class=SpellE>cho</span> <span class=SpellE>bên</span> B <span
class=SpellE>thêm</span> 3% <span class=SpellE>s&#7889;</span> <span
class=SpellE>l&#432;&#7907;ng</span> <span class=SpellE>nguyên</span> <span
class=SpellE>v&#7853;t</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>trên</span> <span class=SpellE>m&#7895;i</span> <span
class=SpellE>&#273;&#417;n</span> <span class=SpellE>hàng</span> (<span
class=SpellE>&#273;ã</span> bao <span class=SpellE>g&#7891;m</span> 3% <span
class=SpellE>trong</span> YY <span class=SpellE>&#273;&#7889;i</span> <span
class=SpellE>v&#7899;i</span> <span class=SpellE>v&#7843;i</span>). <span
class=SpellE>N&#7871;u</span> <span class=SpellE>bên</span> B <span
class=SpellE>không</span> <span class=SpellE>th&#7875;</span> <span
class=SpellE>giao</span> <span class=SpellE>&#273;&#7911;</span> <span
class=SpellE>&#273;&#417;n</span> <span class=SpellE>hàng</span>, <span
class=SpellE>bên</span> B <span class=SpellE>s&#7869;</span> <span
class=SpellE>ph&#7843;i</span> <span class=SpellE>thanh</span> <span
class=SpellE>toán</span> <span class=SpellE>cho</span> <span class=SpellE>bên</span>
A <span class=SpellE>m&#7885;i</span> chi <span class=SpellE>phí</span> <span
class=SpellE>phát</span> <span class=SpellE>sinh</span> <span class=SpellE>và</span>
<span class=SpellE>thi&#7879;t</span> <span class=SpellE>hai</span> <span
class=SpellE>liên</span> <span class=SpellE>quan</span>.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party A provide
extra 3 % of material and accessories of each order (3% of fabric included in
YY). If party B cannot deliver full original order quantity, party B shall pay
party A all extra cost and all other related cost for remake or penalty <span
class=GramE>later on</span>. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>có</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>cung</span> <span class=SpellE>c&#7845;p</span> <span
class=SpellE>&#273;&#7847;y</span> <span class=SpellE>&#273;&#7911;</span>, <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>b&#7897;</span> <span
class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span> <span class=SpellE>&#273;&#7843;m</span> <span
class=SpellE>b&#7843;o</span> <span class=SpellE>s&#7889;</span> <span
class=SpellE>l&#432;&#7907;ng</span> <span class=SpellE>hàng</span> <span
class=SpellE>t&#7891;n</span> 2 <span class=SpellE>ngày</span> <span
class=SpellE>theo</span> <span class=SpellE>công</span> <span class=SpellE>su&#7845;t</span>
<span class=SpellE>c&#7911;a</span> <span class=SpellE>bên</span> B <span
class=SpellE>nh&#432;</span> 2 <span class=SpellE>bên</span> <span
class=SpellE>th&#7887;a</span> <span class=SpellE>thu&#7853;n</span> <span
class=SpellE>cho</span> <span class=SpellE>t&#7915;ng</span> <span
class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span>. Hai <span
class=SpellE>bên</span> <span class=SpellE>cùng</span> <span class=SpellE>ki&#7875;m</span>
<span class=SpellE>&#273;&#7871;m</span> <span class=SpellE>và</span> <span
class=SpellE>ký</span> <span class=SpellE>vào</span> <span class=SpellE>biên</span>
<span class=SpellE>b&#7843;n</span> <span class=SpellE>bàn</span> <span
class=SpellE>giao</span> <span class=SpellE>ngay</span> <span class=SpellE>t&#7841;i</span>
<span class=SpellE>th&#7901;i</span> <span class=SpellE>&#273;i&#7875;m</span> <span
class=SpellE>giao</span> <span class=SpellE>nh&#7853;n</span>. <span
class=SpellE>S&#7889;</span> <span class=SpellE>l&#432;&#7907;ng</span> <span
class=SpellE>thi&#7871;u</span> <span class=SpellE>s&#7869;</span> <span
class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>b&#7893;</span>
sung <span class=SpellE>trong</span> <span class=SpellE>vòng</span> 3 <span
class=SpellE>ngày</span>. Sau <span class=SpellE>khi</span> <span class=SpellE>giao</span>
<span class=SpellE>nh&#7853;n</span>, <span class=SpellE>Bên</span> B <span
class=SpellE>ki&#7875;m</span> <span class=SpellE>tra</span> <span
class=SpellE>ch&#7845;t</span> <span class=SpellE>l&#432;&#7907;ng</span> <span
class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span> <span class=SpellE>và</span> <span class=SpellE>báo</span>
<span class=SpellE>l&#7841;i</span> <span class=SpellE>cho</span> <span
class=SpellE>bên</span> A <span class=SpellE>n&#7871;u</span> <span
class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span> <span class=SpellE>có</span> <span class=SpellE>l&#7895;i</span>
<span class=SpellE>tr&#432;&#7899;c</span> <span class=SpellE>khi</span> <span
class=SpellE>lên</span> <span class=SpellE>chuy&#7873;n</span> <span
class=SpellE>và</span> <span class=SpellE>Bên</span> B <span class=SpellE>s&#7869;</span>
<span class=SpellE>l&#7845;y</span> <span class=SpellE>xác</span> <span
class=SpellE>nh&#7853;n</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>Bên</span> A <span class=SpellE>tr&#432;&#7899;c</span> <span
class=SpellE>khi</span> <span class=SpellE>s&#7843;n</span> <span class=SpellE>xu&#7845;t</span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party A shall
provide to Party B full material and accessories enough to keep WIP 2 days for
sewing at Party B base on the production capacity of party B agreed by both
parties on each annex contract.<span style='mso-spacerun:yes'>  </span>When
deliver material and accessories, both parties check quantity and sign at
delivery report for confirmation. Any shortage (if any) shall be delivered to
cover within 3days. After delivery, Party B inspects quality of material and
accessories and inform to party A any defect before production and Party B will
get approval for material <span class=GramE>quality<span
style='mso-spacerun:yes'>  </span>from</span> Party A before production<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>s&#7843;n</span> <span class=SpellE>xu&#7845;t</span> <span
class=SpellE>hàng</span> <span class=SpellE>hóa</span> <span class=SpellE>theo</span>
<span class=SpellE>t&#7915;ng</span> <span class=SpellE>Ph&#7909;</span> <span
class=SpellE>l&#7909;c</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>&#273;&#7843;m</span> <span
class=SpellE>b&#7843;o</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>s&#7889;</span> <span class=SpellE>l&#432;&#7907;ng</span>, <span
class=SpellE>mã</span> <span class=SpellE>hàng</span>, <span class=SpellE>giao</span>
<span class=SpellE>hàng</span> <span class=SpellE>và</span> <span class=SpellE>các</span>
<span class=SpellE>yêu</span> <span class=SpellE>c&#7847;u</span> <span
class=SpellE>khác</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>bên</span> A. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party B are
responsible to undertake manufacturing goods as separate annex following this
contract in terms of quantity, style, delivery and other instructions by Party <span
class=GramE>A;</span><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>&#273;&#7843;m</span>
<span class=SpellE>b&#7843;o</span> <span class=SpellE>&#273;&#7847;y</span> <span
class=SpellE>&#273;&#7911;</span> <span class=SpellE>máy</span> <span
class=SpellE>móc</span>, <span class=SpellE>trang</span> <span class=SpellE>thi&#7871;t</span>
<span class=SpellE>b&#7883;</span>, <span class=SpellE>nhân</span> <span
class=SpellE>công</span> bao <span class=SpellE>g&#7891;m</span> <span
class=SpellE>công</span> <span class=SpellE>nhân</span> <span class=SpellE>lành</span>
<span class=SpellE>ngh&#7873;</span>, <span class=SpellE>k&#297;</span> <span
class=SpellE>thu&#7853;t</span> <span class=SpellE>viên</span> <span
class=SpellE>&#273;&#7875;</span> <span class=SpellE>th&#7921;c</span> <span
class=SpellE>hi&#7879;n</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>và</span> <span class=SpellE>các</span> <span class=SpellE>Ph&#7909;</span>
<span class=SpellE>l&#7909;c</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>liên</span> <span
class=SpellE>quan</span>. <span class=SpellE>Bên</span> A <span class=SpellE>không</span>
<span class=SpellE>có</span> <span class=SpellE>trách</span> <span
class=SpellE>nhi&#7879;m</span> <span class=SpellE>cung</span> <span
class=SpellE>c&#7845;p</span> <span class=SpellE>các</span> <span class=SpellE>h&#7841;ng</span>
<span class=SpellE>m&#7909;c</span> <span class=SpellE>này</span>.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party B guarantee
all machines, equipment and employees including qualified workers and
technicians to perform this contract and following the annex detail. Party A is
not responsible for provide these machines, <span class=GramE>equipment’s,...</span><span
style='mso-spacerun:yes'>  </span>to Party B<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>s&#7855;p</span> <span
class=SpellE>x&#7871;p</span> <span class=SpellE>nhân</span> <span
class=SpellE>viên</span> <span class=SpellE>k&#7929;</span> <span class=SpellE>thu&#7853;t</span>
may <span class=SpellE>và</span> QC <span class=SpellE>t&#7841;i</span> <span
class=SpellE>nhà</span> <span class=SpellE>máy</span> <span class=SpellE>bên</span>
B <span class=SpellE>&#273;&#7875;</span> <span class=SpellE>giám</span> <span
class=SpellE>sát</span> <span class=SpellE>ch&#7845;t</span> <span
class=SpellE>l&#432;&#7907;ng</span> <span class=SpellE>theo</span> <span
class=SpellE>tiêu</span> <span class=SpellE>chu&#7849;n</span> <span
class=SpellE>s&#7843;n</span> <span class=SpellE>xu&#7845;t</span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party A arrange
Garment Technician and QC to station at Party B to monitor quality standard
along production.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>s&#7855;p</span> <span
class=SpellE>x&#7871;p</span> QC <span class=SpellE>&#273;&#432;&#7907;c</span>
<span class=SpellE>ch&#7913;ng</span> <span class=SpellE>nh&#7853;n</span> <span
class=SpellE>t&#7915;</span> <span class=SpellE>bên</span> <span class=SpellE>th&#7913;</span>
3 (Third Party) <span class=SpellE>&#273;&#7875;</span> <span class=SpellE>th&#7921;c</span>
<span class=SpellE>hi&#7879;n</span> <span class=SpellE>ki&#7875;m</span> <span
class=SpellE>hàng</span> <span class=SpellE>trên</span> <span class=SpellE>chuy&#7873;n</span>
may <span class=SpellE>t&#7841;i</span> <span class=SpellE>khu</span> <span
class=SpellE>v&#7921;c</span> <span class=SpellE>s&#7843;n</span> <span
class=SpellE>xu&#7845;t</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>bên</span> B<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party A arrange
Third party certified QC to conduct inline inspection at party B during
production.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>s&#7855;p</span> <span
class=SpellE>x&#7871;p</span> QC <span class=SpellE>ki&#7875;m</span> <span
class=SpellE>tra</span> <span class=SpellE>ch&#7845;t</span> <span
class=SpellE>l&#432;&#7907;ng</span> <span class=SpellE>hàng</span> <span
class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span> <span
class=SpellE>t&#7841;i</span> <span class=SpellE>bên</span> B <span
class=SpellE>tr&#432;&#7899;c</span> <span class=SpellE>khi</span> <span
class=SpellE>xu&#7845;t</span> <span class=SpellE>hàng</span> <span
class=SpellE>theo</span> <span class=SpellE>tiêu</span> <span class=SpellE>chu&#7849;n</span>
AQL <span class=SpellE>yêu</span> <span class=SpellE>c&#7847;u</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>m&#7895;i</span> <span
class=SpellE>khách</span> <span class=SpellE>hàng</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
<span class=SpellE>th&#7875;</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>trong</span> <span class=SpellE>b&#7843;ng</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>l&#7909;c</span><span
style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party A arrange QC
to conduct final inspection at party B following AQL level of each customer
dedicated in the appendix before delivering full quantity of finished products
to Party A.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>v&#7873;</span> <span class=SpellE>ch&#7845;t</span> <span
class=SpellE>l&#432;&#7907;ng</span> <span class=SpellE>theo</span> <span
class=SpellE>yêu</span> <span class=SpellE>c&#7847;u</span> <span class=SpellE>b&#7903;i</span>
<span class=SpellE>Bên</span> A, <span class=SpellE>d&#7921;a</span> <span
class=SpellE>trên</span> <span class=SpellE>s&#7843;n</span> <span
class=SpellE>ph&#7849;m</span> <span class=SpellE>m&#7851;u</span> <span
class=SpellE>s&#7843;n</span> <span class=SpellE>xu&#7845;t</span> <span
class=SpellE>&#273;ã</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>&#272;&#7841;i</span> <span class=SpellE>di&#7879;n</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>Bên</span> A <span
class=SpellE>duy&#7879;t</span> qua <span class=SpellE>và</span> <span
class=SpellE>tài</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>k&#297;</span> <span class=SpellE>thu&#7853;t</span>. <span
class=SpellE>Bên</span> B <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>có</span> <span class=SpellE>quy</span> <span class=SpellE>trình</span>
<span class=SpellE>ki&#7875;m</span> <span class=SpellE>trên</span> <span
class=SpellE>chuy&#7873;n</span> <span class=SpellE>và</span> <span
class=SpellE>ki&#7875;m</span> 100% <span class=SpellE>cu&#7889;i</span> <span
class=SpellE>chuy&#7873;n</span> <span class=SpellE>tr&#432;&#7899;c</span> <span
class=SpellE>khi</span> <span class=SpellE>chuy&#7875;n</span> <span
class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span> <span
class=SpellE>cho</span> QC <span class=SpellE>bên</span> <span class=GramE>A .</span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party B <span
class=GramE>are in charge of</span> product quality following Party A’s requirements,
based on the samples approved by a representative of Party A and the <span
class=SpellE>techpack</span>. Party B shall have in-line inspection and 100%
end-line inspection before moving garment to party A's QC.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>s&#7869;</span> <span
class=SpellE>không</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>phép</span> <span class=SpellE>bán</span> <span class=SpellE>ho&#7863;c</span>
<span class=SpellE>chuy&#7875;n</span> <span class=SpellE>nh&#432;&#7907;ng</span>
<span class=SpellE>cho</span> <span class=SpellE>b&#7845;t</span> <span
class=SpellE>c&#7913;</span> <span class=SpellE>bên</span> <span class=SpellE>th&#7913;</span>
<span class=SpellE>ba</span> <span class=SpellE>nào</span> <span class=SpellE>các</span>
<span class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span> <span class=SpellE>nói</span> <span
class=SpellE>trên</span>, <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>s&#7917;</span> <span class=SpellE>d&#7909;ng</span> <span
class=SpellE>vào</span> <span class=SpellE>m&#7909;c</span> <span class=SpellE>&#273;ích</span>
<span class=SpellE>khác</span> <span class=SpellE>mà</span> <span class=SpellE>không</span>
<span class=SpellE>có</span> <span class=SpellE>s&#7921;</span> <span
class=SpellE>&#273;&#7891;ng</span> ý <span class=SpellE>c&#7911;a</span> <span
class=SpellE>bên</span> A <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>v&#259;n</span> <span class=SpellE>b&#7843;n</span>.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party B shall not
sell or hand over to any third party any above materials or use for other
purpose without Party A's permission by written documents. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>v&#7873;</span> <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>&#273;&#7843;m</span> <span class=SpellE>b&#7843;o</span> <span
class=SpellE>v&#7879;</span> <span class=SpellE>sinh</span> <span class=SpellE>môi</span>
<span class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>s&#7843;n</span>
<span class=SpellE>xu&#7845;t</span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:28.35pt;text-indent:0in;line-height:115%;tab-stops:31.5pt'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Party B is
responsible for insuring conditions of hygiene environment in production.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>&#272;I&#7872;U 4:
GIAO HÀNG<o:p></o:p></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>ARTICLE 4: DELIVERY
<o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>cung</span> <span class=SpellE>c&#7845;p</span> <span
class=SpellE>cho</span> <span class=SpellE>Bên</span> B <span class=SpellE>k&#7871;</span>
<span class=SpellE>ho&#7841;ch</span> <span class=SpellE>s&#7843;n</span> <span
class=SpellE>xu&#7845;t</span>, bao <span class=SpellE>g&#7891;m</span> <span
class=SpellE>th&#7901;i</span> <span class=SpellE>gian</span> <span
class=SpellE>giao</span> <span class=SpellE>hàng</span>, <span class=SpellE>mã</span>
<span class=SpellE>hàng</span>, <span class=SpellE>s&#7889;</span> <span
class=SpellE>l&#432;&#7907;ng</span> <span class=SpellE>và</span> <span
class=SpellE>y&#7873;u</span> <span class=SpellE>c&#7847;u</span> <span
class=SpellE>v&#7873;</span> <span class=SpellE><span class=GramE>ch&#7845;t</span></span><span
class=GramE><span style='mso-spacerun:yes'>  </span><span class=SpellE>l&#432;&#7907;ng</span></span>
<span class=SpellE>hàng</span> <span class=SpellE>hóa</span> <span
class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>th&#7875;</span> <span
class=SpellE>hi&#7879;n</span> <span class=SpellE>rõ</span> <span class=SpellE>trong</span>
<span class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party A shall provide Party B with a
production schedule, including the delivery time, the style, quantity, and requirements
for quality of goods by appendixes<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>các</span> chi <span class=SpellE>phí</span> <span class=SpellE>v&#7853;n</span>
<span class=SpellE>chuy&#7875;n</span> <span class=SpellE>nguyên</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>t&#7915;</span> <span class=SpellE>kho</span> <span class=SpellE>bên</span>
A <span class=SpellE>t&#7899;i</span> <span class=SpellE>bên</span> B <span
class=SpellE><span class=GramE>và</span></span><span class=GramE><span
style='mso-spacerun:yes'>  </span><span class=SpellE>bên</span></span> B <span
class=SpellE>ch&#7883;u</span> chi <span class=SpellE>phí</span> <span
class=SpellE>v&#7853;n</span> <span class=SpellE>chuy&#7875;n</span> <span
class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span> <span
class=SpellE>t&#7915;</span> <span class=SpellE>kho</span> <span class=SpellE>bên</span>
B <span class=SpellE>t&#7899;i</span> <span class=SpellE>&#273;&#7883;a</span> <span
class=SpellE>ch&#7881;</span> <span class=SpellE>y&#7873;u</span> <span
class=SpellE>c&#7847;u</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>bên</span> A <span class=SpellE>và</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
<span class=SpellE>tính</span> <span class=SpellE>vào</span> <span
class=SpellE>giá</span> <span class=SpellE>thành</span> <span class=SpellE>gia</span>
<span class=SpellE>công</span> <span class=SpellE>s&#7843;n</span> <span
class=SpellE>ph&#7849;m</span>. Chi <span class=SpellE>phí</span> <span
class=SpellE>b&#7889;c</span> <span class=SpellE>x&#7871;p</span> <span
class=SpellE>t&#7841;i</span> <span class=SpellE>&#273;&#7847;u</span> <span
class=SpellE>bên</span> <span class=SpellE>nào</span> <span class=SpellE>bên</span>
<span class=SpellE>&#273;ó</span> <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>thanh</span> <span class=SpellE>toán</span> <span class=SpellE>theo</span>
<span class=SpellE>nh&#432;</span> <span class=SpellE>th&#7901;i</span> <span
class=SpellE>gian</span> <span class=SpellE>yêu</span> <span class=SpellE>c&#7847;u</span>
<span class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>th&#7875;</span>
<span class=SpellE>hi&#7879;n</span> <span class=SpellE>rõ</span> <span
class=SpellE>trong</span> <span class=SpellE>các</span> <span class=SpellE>Ph&#7909;</span>
<span class=SpellE>l&#7909;c</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>cho</span> <span
class=SpellE>t&#7915;ng</span> <span class=SpellE>&#273;&#417;n</span> <span
class=SpellE>hàng</span>. Hai <span class=SpellE>bên</span> <span class=SpellE>&#273;&#7891;ng</span>
ý <span class=SpellE>s&#7889;</span> <span class=SpellE>l&#7847;n</span> <span
class=SpellE>giao</span> <span class=SpellE>nh&#7853;n</span> <span
class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span> <span class=SpellE>là</span> 2 <span
class=SpellE>l&#7847;n</span> <span class=SpellE>và</span> <span class=SpellE>s&#7889;</span>
<span class=SpellE>l&#7847;n</span> <span class=SpellE>giao</span> <span
class=SpellE>nh&#7853;n</span> <span class=SpellE>hàng</span> <span
class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span> <span
class=SpellE>là</span> 1 <span class=SpellE>l&#7847;n</span> <span
class=SpellE>&#273;&#7889;i</span> <span class=SpellE>v&#7899;i</span> <span
class=SpellE>&#273;&#417;n</span> <span class=SpellE>hàng</span> <span
class=SpellE>kho&#7843;ng</span> 15,000 <span class=SpellE>chi&#7871;c</span>, <span
class=SpellE>&#273;&#7889;i</span> <span class=SpellE>v&#7899;i</span> <span
class=SpellE>&#273;&#417;n</span> <span class=SpellE>hàng</span> <span
class=SpellE>kho&#7843;ng</span> <span class=SpellE>t&#7915;</span> 33,000 <span
class=SpellE>chi&#7871;c</span> <span class=SpellE>cho</span> <span
class=SpellE>phép</span> 2 <span class=SpellE>l&#7847;n</span> <span
class=SpellE>giao</span> <span class=SpellE>nh&#7853;n</span> <span
class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span> <span class=SpellE>và</span> 2 <span
class=SpellE>l&#7847;n</span> <span class=SpellE>giao</span> <span
class=SpellE>nh&#7853;n</span> <span class=SpellE>hàng</span> <span
class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span>. <span
class=SpellE>N&#7871;u</span> <span class=SpellE>s&#7889;</span> <span
class=SpellE>l&#7847;n</span> <span class=SpellE>giao</span> <span
class=SpellE>nh&#7853;n</span> <span class=SpellE>nhi&#7873;u</span> <span
class=SpellE>h&#417;n</span> so <span class=SpellE>v&#7899;i</span> <span
class=SpellE>quy</span> <span class=SpellE>&#273;&#7883;nh</span>, <span
class=SpellE>bên</span> <span class=SpellE>nào</span> <span class=SpellE>gây</span>
<span class=SpellE>ra</span> <span class=SpellE>s&#7921;</span> <span
class=SpellE>vi&#7879;c</span> <span class=SpellE>s&#7869;</span> <span
class=SpellE>ph&#7843;i</span> <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>các</span> chi <span class=SpellE>phí</span> <span class=SpellE>phát</span>
<span class=SpellE>sinh</span> <span class=SpellE>liên</span> <span
class=SpellE>quan</span>.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party A will bear the transportation cost
to arrange fabric and sub materials from warehouse's party A to party B <span
class=GramE>and<span style='mso-spacerun:yes'>  </span>Party</span> B will bear
the transportation cost to delivery garments back to Party A and account in to
CM cost follow date in Appendix. The cost of loading and unloading on which
party is responsible for payment which will be stated clearly in contract
appendixes. Both sides allow to have 2 times for receive material + 1 time for
sending FG to 15k per contract and 2 times for receive material + 2 time for
sending FG to 33k per contract. More than this will charge back incurred cost
for who respond on it.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>N&#7871;u</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>có</span> <span class=SpellE>s&#7921;</span> <span class=SpellE>ch&#7853;m</span>
<span class=SpellE>tr&#7877;</span> <span class=SpellE>nào</span> <span
class=SpellE>trong</span> <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>giao</span> <span class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span>
<span class=SpellE>li&#7879;u</span> <span class=SpellE>mà</span> <span
class=SpellE>vi&#7879;c</span> <span class=SpellE>này</span> <span
class=SpellE>&#7843;nh</span> <span class=SpellE>h&#432;&#7903;ng</span> <span
class=SpellE>t&#7899;i</span> <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>ti&#7871;n</span> <span class=SpellE>hành</span> <span
class=SpellE>s&#7843;n</span> <span class=SpellE>xu&#7845;t</span> <span
class=SpellE>ho&#7863;c</span> <span class=SpellE>d&#7915;ng</span> <span
class=SpellE>s&#7843;n</span> <span class=SpellE>xu&#7845;t</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>bên</span> B <span
class=SpellE>thì</span> <span class=SpellE>ngày</span> <span class=SpellE>giao</span>
<span class=SpellE>hàng</span> <span class=SpellE>c&#361;ng</span> <span
class=SpellE>ph&#7843;i</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>t&#259;ng</span> <span class=SpellE>thêm</span> <span
class=SpellE>t&#432;&#417;ng</span> <span class=SpellE>&#273;&#432;&#417;ng</span>.
<span class=SpellE>Trong</span> <span class=SpellE>tr&#432;&#7901;ng</span> <span
class=SpellE>h&#7907;p</span> <span class=SpellE>bên</span> A <span
class=SpellE>không</span> <span class=SpellE>cung</span> <span class=SpellE>c&#7845;p</span>
<span class=SpellE>&#273;&#7911;</span> <span class=SpellE>nguyên</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>&#273;&#7843;m</span> <span class=SpellE>b&#7843;o</span> <span
class=SpellE>bên</span> B <span class=SpellE>n&#7889;i</span> <span
class=SpellE>chuy&#7873;n</span> <span class=SpellE>nh&#432;</span> <span
class=SpellE>&#273;ã</span> <span class=SpellE>th&#7889;ng</span> <span
class=SpellE>nh&#7845;t</span> <span class=SpellE>trong</span> <span
class=SpellE>t&#7915;ng</span> <span class=SpellE>Ph&#7909;</span> <span
class=SpellE>l&#7909;c</span>, <span class=SpellE>bên</span> A <span
class=SpellE>s&#7869;</span> <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>thanh</span> <span class=SpellE>toán</span> chi <span
class=SpellE>phí</span> <span class=SpellE>nhân</span> <span class=SpellE>công</span>
<span class=SpellE>phát</span> <span class=SpellE>sinh</span> <span
class=SpellE>cho</span> <span class=SpellE>bên</span> B, <span class=SpellE>tr&#7915;</span>
<span class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>h&#7907;p</span>
<span class=SpellE>lý</span> do <span class=SpellE>khách</span> <span
class=SpellE>quan</span> <span class=SpellE>n&#7857;m</span> <span
class=SpellE>ngoài</span> <span class=SpellE>t&#7847;m</span> <span
class=SpellE>ki&#7875;m</span> <span class=SpellE>soát</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>bên</span> A <span
class=SpellE>nh&#432;</span>: <span class=SpellE>nguyên</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>giao</span> <span class=SpellE>mu&#7897;n</span>, <span
class=SpellE>b&#7883;</span> <span class=SpellE>l&#7895;i</span> do <span
class=SpellE>nhà</span> <span class=SpellE>cung</span> <span class=SpellE>c&#7845;p</span>,
<span class=SpellE>thay</span> <span class=SpellE>&#273;&#7893;i</span> <span
class=SpellE>thông</span> tin do <span class=SpellE>khách</span> <span
class=SpellE>hàng</span>, <span class=SpellE>các</span> <span class=SpellE>v&#7845;n</span>
<span class=SpellE>&#273;&#7873;</span> <span class=SpellE>phát</span> <span
class=SpellE>sinh</span> do <span class=SpellE>bên</span> <span class=SpellE>th&#7913;</span>
<span class=SpellE>ba</span> <span class=SpellE>và</span> <span class=SpellE>các</span>
<span class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>h&#7907;p</span>
<span class=SpellE>b&#7845;t</span> <span class=SpellE>kh&#7843;</span> <span
class=SpellE>kháng</span> <span class=SpellE>khác</span> (<span class=SpellE>thiên</span>
tai, <span class=SpellE>d&#7883;ch</span> <span class=SpellE>b&#7879;nh</span>,
<span class=SpellE>chi&#7871;n</span> <span class=SpellE>tranh</span>...)<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Should there be any delay in the delivery
by Party A. of the materials and accessories and if this delay either has
prevented the production to begin or has terminated the production, the
delivery time will be increased of the same delay. In case party A don't provide
enough materials to keep Party B sewing continuously as agreed in each annex,
Party A shall pay extra labor cost to party B, except for objective reasons out
of Party A control such as: delay, defect materials because of supplier;
changing information because of customers, other problem because of third party
and other force majeure cases (natural disasters, epidemics, war, etc.)<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>N&#7871;u</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>x&#7843;y</span> <span class=SpellE>ra</span> <span class=SpellE>tr&#432;&#7901;ng</span>
<span class=SpellE>h&#7907;p</span> <span class=SpellE>xu&#7845;t</span> <span
class=SpellE>thi&#7871;u</span>, <span class=SpellE>xu&#7845;t</span> <span
class=SpellE><span class=GramE>th&#7915;a</span></span><span class=GramE> ,</span>
<span class=SpellE>xu&#7845;t</span> <span class=SpellE>t&#7915;ng</span> <span
class=SpellE>ph&#7847;n</span> , <span class=SpellE>xu&#7845;t</span> <span
class=SpellE>hàng</span> <span class=SpellE>s&#7899;m</span> <span
class=SpellE>thì</span> <span class=SpellE>hai</span> <span class=SpellE>bên</span>
<span class=SpellE>ph&#7843;i</span> <span class=SpellE>thông</span> <span
class=SpellE>báo</span> <span class=SpellE>cho</span> <span class=SpellE>nhau</span>
<span class=SpellE>b&#7857;ng</span> <span class=SpellE>v&#259;n</span> <span
class=SpellE>b&#7843;n</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>th&#432;</span> <span class=SpellE>&#273;i&#7879;n</span> <span
class=SpellE>t&#7917;</span> <span class=SpellE>và</span> <span class=SpellE>th&#7889;ng</span>
<span class=SpellE>nh&#7845;t</span> <span class=SpellE>b&#7903;i</span> <span
class=SpellE>hai</span> <span class=SpellE>bên</span>.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>If there will be short shipment, over
shipment, partial shipment, early shipment the parties should have advanced
notice in writing or email agreed by two parties. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>cung</span> <span
class=SpellE>c&#7845;p</span> <span class=SpellE>nguyên</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>cho</span> <span class=SpellE>bên</span> B <span class=SpellE>theo</span>
<span class=SpellE>phi&#7871;u</span> <span class=SpellE>Xu&#7845;t</span> <span
class=SpellE>kho</span> <span class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span>
<span class=SpellE>li&#7879;u</span>. <br>
<span class=SpellE>Bên</span> B <span class=SpellE>bàn</span> <span
class=SpellE>giao</span> <span class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span>
<span class=SpellE>sau</span> <span class=SpellE>khi</span> <span class=SpellE>hoàn</span>
<span class=SpellE>thành</span> <span class=SpellE>gia</span> <span
class=SpellE>công</span> <span class=SpellE>cho</span> <span class=SpellE>bên</span>
A <span class=SpellE>theo</span> <span class=SpellE>phi&#7871;u</span> <span
class=SpellE>xu&#7845;t</span> <span class=SpellE>kho</span> <span
class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>bên</span> B.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party A delivery the material for Party B
base on Party A's the <span class=GramE>take out</span> card form. Party B
delivery the garment for Party A base on Party B's the <span class=GramE>take
out</span> card form.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>hoàn</span> <span class=SpellE>thi&#7879;n</span> <span
class=SpellE>các</span> <span class=SpellE>th&#7911;</span> <span class=SpellE>t&#7909;c</span>
<span class=SpellE>h&#7843;i</span> <span class=SpellE>qu&#7843;n</span> <span
class=SpellE>cho</span> <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>nh&#7853;p</span> <span class=SpellE>kh&#7849;u</span> <span
class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span> <span class=SpellE>và</span> <span class=SpellE>xu&#7845;t</span>
<span class=SpellE>kh&#7849;u</span> <span class=SpellE>s&#7843;n</span> <span
class=SpellE>ph&#7849;m</span> <span class=SpellE>hoàn</span> <span
class=SpellE>thi&#7879;n</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party B is responsible to complete
formalities for importing materials and for exporting finished products.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>&#272;I&#7872;U 5: NHÃN
MÁC, TÊN G&#7884;I VÀ XU&#7844;T X&#7912;<o:p></o:p></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>ARTICLE 5:
LABELING, NAME, ORIGIN OF GOODS<o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></b></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>&#273;&#7843;m</span>
<span class=SpellE>b&#7843;o</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>vi&#7879;c</span> <span class=SpellE>s&#7917;</span> <span
class=SpellE><span class=GramE>d&#7909;ng</span></span><span class=GramE> :</span>
<span class=SpellE>lô</span> <span class=SpellE>gô</span>, <span class=SpellE>ki&#7875;u</span>
<span class=SpellE>dáng</span>, <span class=SpellE>&#273;&#7873;</span> can, <span
class=SpellE>nhãn</span> <span class=SpellE>mác</span>, <span class=SpellE>xu&#7845;t</span>
<span class=SpellE>x&#7913;</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span>, <span class=SpellE>và</span> <span
class=SpellE>nhãn</span> <span class=SpellE>hi&#7879;u</span> <span
class=SpellE>xu&#7845;t</span> <span class=SpellE>hàng</span>, <span
class=SpellE>và</span> <span class=SpellE>ch&#7883;u</span> <span class=SpellE>trách</span>
<span class=SpellE>nhi&#7879;m</span> <span class=SpellE>toàn</span> <span
class=SpellE>b&#7897;</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>th&#432;&#417;ng</span> <span class=SpellE>hi&#7879;u</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>hàng</span> <span
class=SpellE>hóa</span> <span class=SpellE>thành</span> <span class=SpellE>ph&#7849;m</span>.
<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party A takes responsibilities to insure
and <span class=GramE>guarantee:</span> logo, styles, decal, label, origin of
materials and shipping mark, take full responsibility for any of <span
class=SpellE>trade marks</span> of finished products.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>&#272;I&#7872;U 6 X&#7916;
LÍ NGUYÊN PH&#7908; LI&#7878;U L&#7894;I, D&#431; TH&#7914;A HO&#7862;C
PH&#7870; TH&#7842;I<o:p></o:p></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>ARTICLE 6:
TREATMENT OF WONG, LEFTOVER OR USELESS WASTE MATERIALS<o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>chuy&#7875;n</span> <span class=SpellE>tr&#7843;</span> <span
class=SpellE>s&#7889;</span> <span class=SpellE>nguyên</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>li&#7879;u</span> <span
class=SpellE>l&#7895;i</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>dôi</span> <span class=SpellE>d&#432;</span> <span class=SpellE>sau</span>
<span class=SpellE>khi</span> <span class=SpellE>k&#7871;t</span> <span
class=SpellE>thúc</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> (<span class=SpellE>n&#7871;u</span> <span
class=SpellE>có</span>) <span class=SpellE>cho</span> <span class=SpellE>bên</span>
A<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party B must return balance of materials
accessories after finishing contract or wrong (if have) to party A. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> B <span class=SpellE>s&#7869;</span> <span
class=SpellE>không</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>phép</span> <span class=SpellE>s&#7917;</span> <span class=SpellE>d&#7909;ng</span>
<span class=SpellE>b&#7845;t</span> <span class=SpellE>c&#7913;</span> <span
class=SpellE>s&#7843;n</span> <span class=SpellE>ph&#7849;m</span> <span
class=SpellE>th&#7915;a</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>l&#7895;i</span> <span class=SpellE>nào</span> <span class=SpellE>trong</span>
<span class=SpellE>quá</span> <span class=SpellE>trình</span> <span
class=SpellE>bên</span> A <span class=SpellE>v&#7851;n</span> <span
class=SpellE>&#273;ang</span> <span class=SpellE>ti&#7871;p</span> <span
class=SpellE>t&#7909;c</span> <span class=SpellE>s&#7843;n</span> <span
class=SpellE>xu&#7845;t</span> <span class=SpellE>trong</span> <span
class=SpellE>vòng</span> 1 <span class=SpellE>n&#259;m</span> <span
class=SpellE>sau</span> <span class=SpellE>ngày</span> <span class=SpellE>xu&#7845;t</span>
<span class=SpellE>hàng</span>. <span class=SpellE>Trong</span> <span
class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>có</span> <span class=SpellE>s&#7921;</span> <span class=SpellE>chuy&#7875;n</span>
<span class=SpellE>nh&#432;&#7907;ng</span> <span class=SpellE>thành</span> <span
class=SpellE>ph&#7849;m</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>nguyên</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>li&#7879;u</span> <span class=SpellE>thì</span> <span
class=SpellE>c&#7847;n</span> <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>có</span> <span class=SpellE>s&#7921;</span> <span class=SpellE>ch&#7845;p</span>
<span class=SpellE>thu&#7853;n</span> <span class=SpellE>b&#7903;i</span> <span
class=SpellE>Bên</span> A <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>v&#259;n</span> <span class=SpellE>b&#7843;n</span>.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party B will not dispose of any excess,
defective and/or left-over garments resulting during Party A 's manufacturing
process for at least 1 year after the shipping date.<span
style='mso-spacerun:yes'>  </span><span class=GramE>In the event that</span> a
transaction of garments and materials must have Party A's approval by written
documents<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin:0in;text-indent:0in;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>&#272;I&#7872;U 7:
PH&#431;&#416;NG TH&#7912;C THANH TOÁN <o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>ARTICLE 7: PAYMENT
TERM <o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span style='font-size:12.0pt;line-height:115%;
font-family:Times New Roman,serif;mso-fareast-font-family:Times New Roman;
color:windowtext'>Sau <span class=SpellE>khi</span> <span class=SpellE>k&#7871;t</span>
<span class=SpellE>thúc</span> <span class=SpellE>m&#7895;i</span> <span
class=SpellE>ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>, <span
class=SpellE>hai</span> <span class=SpellE>bên</span> <span class=SpellE>th&#7889;ng</span>
<span class=SpellE>nh&#7845;t</span> <span class=SpellE>ký</span> <span
class=SpellE>biên</span> <span class=SpellE>b&#7843;n</span> <span
class=SpellE>thanh</span> <span class=SpellE>lý</span> <span class=SpellE>Ph&#7909;</span>
<span class=SpellE>l&#7909;c</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>nêu</span> <span
class=SpellE>rõ</span> <span class=SpellE>giá</span> <span class=SpellE>tr&#7883;</span>
<span class=SpellE>th&#7921;c</span> <span class=SpellE>t&#7871;</span> <span
class=SpellE>th&#7921;c</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>, <span
class=SpellE>các</span> <span class=SpellE>kho&#7843;n</span> chi <span
class=SpellE>phí</span> <span class=SpellE>phát</span> <span class=SpellE>sinh</span>,
<span class=SpellE>các</span> <span class=SpellE>kho&#7843;n</span> <span
class=SpellE>ph&#7841;t</span>, <span class=SpellE>b&#7893;i</span> <span
class=SpellE>th&#432;&#7901;ng</span> (<span class=SpellE>n&#7871;u</span> <span
class=SpellE>có</span>). <span class=SpellE>Bên</span> B <span class=SpellE>phát</span>
<span class=SpellE>hành</span> <span class=SpellE>hóa</span> <span
class=SpellE>&#273;&#417;n</span> GTGT <span class=SpellE>cho</span> <span
class=SpellE>bên</span> A <span class=SpellE>theo</span> <span class=SpellE>biên</span>
<span class=SpellE>b&#7843;n</span> <span class=SpellE>thanh</span> <span
class=SpellE>lý</span> <span class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span>
<span class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>&#273;ã</span> <span class=SpellE>ký</span> bao <span
class=SpellE>g&#7891;m</span> <span class=SpellE>t&#7845;t</span> <span
class=SpellE>c&#7843;</span> <span class=SpellE>các</span> <span class=SpellE>kho&#7843;n</span>
chi <span class=SpellE>phí</span> <span class=SpellE>phát</span> <span
class=SpellE>sinh</span>, <span class=SpellE>b&#7891;i</span> <span
class=SpellE>th&#432;&#7901;ng</span> (<span class=SpellE>n&#7871;u</span> <span
class=SpellE>có</span>). <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>After finishing each annex, two parties
agree singing Minutes of annex contract liquidation made clearly actual appendix
contract value, extra cost, penalty, charge back (if any). Party B issue VAT
invoice to Party A following the minutes of liquidation including all extra
cost, penalty, charge back (if any).<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:0in;line-height:115%'><span
class=SpellE><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Các</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>kho&#7843;n</span> chi <span class=SpellE>phí</span> <span
class=SpellE>phát</span> <span class=SpellE>sinh</span>, <span class=SpellE>các</span>
<span class=SpellE>kho&#7843;n</span> <span class=SpellE>ph&#7841;t</span>, <span
class=SpellE>b&#7891;i</span> <span class=SpellE>th&#432;&#7901;ng</span> <span
class=SpellE>là</span> <span class=SpellE>các</span> chi <span class=SpellE>phí</span>
<span class=SpellE>phát</span> <span class=SpellE>sinh</span> <span
class=SpellE>khi</span> <span class=SpellE>mà</span> <span class=SpellE>m&#7897;t</span>
<span class=SpellE>trong</span> <span class=SpellE>hai</span> <span
class=SpellE>bên</span> vi <span class=SpellE>ph&#7841;m</span> <span
class=SpellE>các</span> <span class=SpellE>&#273;i&#7873;u</span> <span
class=SpellE>kho&#7843;n</span> <span class=SpellE>trong</span> <span
class=SpellE>h&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>ho&#7863;c</span> <span class=SpellE>ph&#7909;</span> <span
class=SpellE>l&#7909;c</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span>, <span class=SpellE>d&#7851;n</span> <span
class=SpellE>&#273;&#7871;n</span> <span class=SpellE>bên</span> <span
class=SpellE>còn</span> <span class=SpellE>l&#7841;i</span> <span class=SpellE>phát</span>
<span class=SpellE>sinh</span> <span class=SpellE>các</span> chi <span
class=SpellE>phí</span> <span class=SpellE>ngoài</span> <span class=SpellE>h&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>&#273;&#7875;</span>
<span class=SpellE>gi&#7843;i</span> <span class=SpellE>quy&#7871;t</span> <span
class=SpellE>các</span> vi <span class=SpellE>ph&#7841;m</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>bên</span> <span class=SpellE>còn</span>
<span class=SpellE>l&#7841;i</span>, <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>là</span> <span class=SpellE>các</span> <span class=SpellE>kho&#7843;n</span>
<span class=SpellE>ph&#7841;t</span>, <span class=SpellE>yêu</span> <span
class=SpellE>c&#7847;u</span> <span class=SpellE>b&#7891;i</span> <span
class=SpellE>th&#432;&#7901;ng</span> <span class=SpellE>khi</span> <span
class=SpellE>m&#7897;t</span> <span class=SpellE>trong</span> <span
class=SpellE>hai</span> <span class=SpellE>bên</span> <span class=SpellE>gây</span>
<span class=SpellE>ra</span> <span class=SpellE>nh&#7919;ng</span> <span
class=SpellE>thi&#7879;t</span> <span class=SpellE>h&#7841;i</span> <span
class=SpellE>cho</span> <span class=SpellE>bên</span> <span class=SpellE>còn</span>
<span class=SpellE>l&#7841;i</span> <span class=SpellE>trong</span> <span
class=SpellE>th&#7901;i</span> <span class=SpellE>gian</span> <span
class=SpellE>th&#7921;c</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>h&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>. <i><o:p></o:p></i></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>After finishing each annex, two parties
agree singing Minutes of annex contract liquidation made clearly actual
appendix contract value, extra cost, penalty, charge back (if any). Party B
issue VAT invoice to Party A following the minutes of liquidation including all
extra cost, penalty, charge back (if any).<o:p></o:p></span></i></p>

<p class=MsoListParagraphCxSpFirst style='margin-top:0in;margin-right:0in;
margin-bottom:0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;
line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Bên</span></span><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'> A <span class=SpellE>có</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>thanh</span> <span class=SpellE>toán</span> <span class=SpellE>cho</span>
<span class=SpellE>bên</span> B <span class=SpellE>trong</span> <span
class=SpellE>vòng</span> 15 <span class=SpellE>ngày</span> <span class=SpellE>làm</span>
<span class=SpellE>vi&#7879;c</span> <span class=SpellE>k&#7875;</span> <span
class=SpellE>t&#7915;</span> <span class=SpellE>ngày</span> <span class=SpellE>nh&#7853;n</span>
<span class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>&#273;&#7847;y</span>
<span class=SpellE>&#273;&#7911;</span> <span class=SpellE>hóa</span> <span
class=SpellE>&#273;&#417;n</span> <span class=SpellE>giá</span> <span
class=SpellE>tr&#7883;</span> <span class=SpellE>gia</span> <span class=SpellE>t&#259;ng</span>,
<span class=SpellE>biên</span> <span class=SpellE>b&#7843;n</span> <span
class=SpellE>thanh</span> <span class=SpellE>lý</span> <span class=SpellE>H&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>và</span> <span
class=SpellE>các</span> <span class=SpellE>gi&#7845;y</span> <span
class=SpellE>t&#7901;</span> <span class=SpellE>liên</span> <span class=SpellE>quan</span>
<span class=SpellE>khác</span> <span class=SpellE>t&#7915;</span> <span
class=SpellE>bên</span> B. <span class=SpellE>N&#7871;u</span> <span
class=SpellE>ch&#7853;m</span> <span class=SpellE>tr&#7877;</span> <span
class=SpellE>trong</span> <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>thanh</span> <span class=SpellE>toán</span>, <span class=SpellE>bên</span>
A <span class=SpellE>s&#7869;</span> <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>thanh</span> <span class=SpellE>toán</span> <span class=SpellE>cho</span>
<span class=SpellE>bên</span> B <span class=SpellE>kho&#7843;n</span> <span
class=SpellE>ph&#7841;t</span> <span class=SpellE>lãi</span> <span
class=SpellE>tr&#7843;</span> <span class=SpellE>ch&#7853;m</span> 0.02%/<span
class=SpellE>ngày</span> <span class=SpellE>trên</span> <span class=SpellE>s&#7889;</span>
<span class=SpellE>ti&#7873;n</span> <span class=SpellE>ch&#7853;m</span> <span
class=SpellE>tr&#7843;</span> <span class=SpellE>ch&#7853;m</span> <span
class=SpellE>theo</span> <span class=SpellE>t&#7915;ng</span> <span
class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>.<o:p></o:p></span></p>

<p class=MsoListParagraphCxSpLast style='margin-top:0in;margin-right:0in;
margin-bottom:0in;margin-left:.5in;mso-add-space:auto;text-indent:0in;
line-height:115%'><span class=SpellE><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif;mso-fareast-font-family:Times New Roman;
color:windowtext'>Bên</span></span><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif;mso-fareast-font-family:Times New Roman;
color:windowtext'> A <span class=SpellE>xác</span> <span class=SpellE>nh&#7853;n</span>
<span class=SpellE>b&#7843;ng</span> <span class=SpellE>kê</span> <span
class=SpellE>và</span> <span class=SpellE>hóa</span> <span class=SpellE>&#273;&#417;n</span>
GTTT <span class=SpellE>nháp</span> <span class=SpellE>cho</span> <span
class=SpellE>Bên</span> B qua <span class=SpellE>th&#432;</span> <span
class=SpellE>&#273;i&#7879;n</span> <span class=SpellE>t&#7917;</span> <span
class=SpellE>trong</span> <span class=SpellE>vòng</span> 3 <span class=SpellE>ngày</span>
<span class=SpellE>làm</span> <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>k&#7875;</span> <span class=SpellE>t&#7915;</span> <span
class=SpellE>ngày</span> <span class=SpellE>m&#7903;</span> <span class=SpellE>t&#7901;</span>
<span class=SpellE>khai</span> <span class=SpellE>xu&#7845;t</span> <span
class=SpellE>kh&#7849;u</span>.<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party A shall pay party B within 15 working
days since party A receive fully requirement documents from party B including
VAT invoice, Minutes of annex contract liquidation and other documents. If
party A fails to make payment on time, party A shall make a payment of late <span
class=SpellE>penaty</span> with rate 0.02% per day on the late payment amount
as separate annex.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Party A shall confirm the quantity summary
list and draft VAT invoice via email within 3 working days from the date of
opening the export declaration<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
0in;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Ph&#432;&#417;ng</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>th&#7913;c</span> <span class=SpellE>thanh</span> <span
class=SpellE>toán</span>: <span class=SpellE>Chuy&#7875;n</span> <span
class=SpellE>kho&#7843;n</span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'>Payment method: Bank transfer<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-fareast-font-family:
Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Tên</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>th&#7909;</span> <span class=SpellE>h&#432;&#7903;ng</span></span></b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>/ Beneficiary:<span
style='mso-spacerun:yes'>  </span></span></i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>scot12</span><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><span class=SpellE><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>Tên</span></b></span><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>Ngân</span> <span class=SpellE>Hàng</span></span></b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>/ Bank detail: </span></i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
background:white;mso-bidi-font-weight:bold'>scot18</span><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><span class=SpellE><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>S&#7889;</span></i></b></span><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'> <span
class=SpellE>tài</span> <span class=SpellE>kho&#7843;n</span></span></i></b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'>/ Bank account: </span></i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
background:white;mso-bidi-font-weight:bold'>scot17</span><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin:0in;text-indent:0in;line-height:115%'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
mso-fareast-font-family:Times New Roman;color:windowtext'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;I&#7872;U
8: B&#7890;I TH&#431;&#7900;NG THI&#7878;T H&#7840;I VÀ VI PH&#7840;M H&#7906;P
&#272;&#7890;NG<o:p></o:p></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>ARTICLE
8: COMPENSATION FOR DAMAGES AND PENALTY FOR CONTRACT <o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Trong</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>m&#7895;i</span> <span class=SpellE>bên</span> <span class=SpellE>th&#7921;c</span>
<span class=SpellE>hi&#7879;n</span> <span class=SpellE>không</span> <span
class=SpellE>&#273;úng</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>không</span> <span class=SpellE>&#273;&#7847;y</span> <span
class=SpellE>&#273;&#7911;</span> <span class=SpellE>ngh&#297;a</span> <span
class=SpellE>v&#7909;</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>mình</span> <span class=SpellE>theo</span> <span class=SpellE>H&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>Nguyên</span> <span
class=SpellE>t&#7855;c</span> <span class=SpellE>này</span>, <span
class=SpellE>bên</span> <span class=SpellE>&#273;ó</span> <span class=SpellE>ph&#7843;i</span>
<span class=SpellE>ch&#7883;u</span> <span class=SpellE>trách</span> <span
class=SpellE>nhi&#7879;m</span> <span class=SpellE>b&#7891;i</span> <span
class=SpellE>th&#432;&#7901;ng</span> <span class=SpellE>cho</span> <span
class=SpellE>nh&#7919;ng</span> <span class=SpellE>t&#7893;n</span> <span
class=SpellE>th&#7845;t</span> <span class=SpellE>mà</span> <span class=SpellE>bên</span>
kia <span class=SpellE>ph&#7843;i</span> <span class=SpellE>gánh</span> <span
class=SpellE>ch&#7883;u</span> do <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>th&#7921;c</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>không</span> <span class=SpellE>&#273;úng</span> <span
class=SpellE>ngh&#297;a</span> <span class=SpellE>v&#7909;</span> <span
class=SpellE>&#273;ó</span>, bao <span class=SpellE>g&#7891;m</span> <span
class=SpellE>c&#7843;</span> <span class=SpellE>thi&#7879;t</span> <span
class=SpellE>h&#7841;i</span> <span class=SpellE>tr&#7921;c</span> <span
class=SpellE>ti&#7871;p</span> <span class=SpellE>và</span> <span class=SpellE>gián</span>
<span class=SpellE>ti&#7871;p</span> <span class=SpellE>x&#7843;y</span> <span
class=SpellE>ra.</span> <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i style='mso-bidi-font-style:
normal'><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>In
case that either Party fails to perform or not perform fully obligations
herein, such Party shall compensate for all damages incurred by the other Party
due to non-performance of such obligations, including direct and indirect
damages. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i style='mso-bidi-font-style:
normal'><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></i></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>M&#7895;i</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>bên</span> <span class=SpellE>th&#7921;c</span> <span
class=SpellE>hi&#7879;n</span> <span class=SpellE>không</span> <span
class=SpellE>&#273;úng</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>không</span> <span class=SpellE>&#273;&#7847;y</span> <span
class=SpellE>&#273;&#7911;</span> <span class=SpellE>ngh&#297;a</span> <span
class=SpellE>v&#7909;</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>mình</span> <span class=SpellE>theo</span> <span class=SpellE>H&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>Nguyên</span> <span
class=SpellE>t&#7855;c</span> <span class=SpellE>này</span> <span class=SpellE>còn</span>
<span class=SpellE>ph&#7843;i</span> <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>m&#7897;t</span> <span class=SpellE>kho&#7843;n</span> <span
class=SpellE>ti&#7873;n</span> <span class=SpellE>ph&#7841;t</span> vi <span
class=SpellE>ph&#7841;m</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>v&#7899;i</span> <span
class=SpellE>s&#7889;</span> <span class=SpellE>ti&#7873;n</span> <span
class=SpellE>t&#432;&#417;ng</span> <span class=SpellE>&#273;&#432;&#417;ng</span>
<span class=SpellE>c&#7911;a</span> <span class=SpellE>lô</span> <span
class=SpellE>hàng</span> <span class=SpellE>trên</span> <span class=SpellE>H&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>c&#7909;</span> <span
class=SpellE>th&#7875;</span> <span class=SpellE>t&#7841;i</span> <span
class=SpellE>m&#7897;t</span> <span class=SpellE>th&#7901;i</span> <span
class=SpellE>&#273;i&#7875;m</span> <span class=SpellE>c&#7909;</span> <span
class=SpellE>th&#7875;</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>kho&#7843;n</span> <span class=SpellE>ph&#7841;t</span> <span
class=SpellE>&#273;&#7871;n</span> <span class=SpellE>t&#7915;</span> <span
class=SpellE>khách</span> <span class=SpellE>hàng</span> <span class=SpellE>c&#7911;a</span>
<span class=SpellE>bên</span> <span class=SpellE>thuê</span> <span
class=SpellE>gia</span> <span class=SpellE>công</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>A Party which
fails to perform or performs inadequately obligations herein shall be also
subject to penalty of contract breach with similar amount of the specific
consignment in a specific contract in a specific moment or all penalties from
buyers. </span></i><i><span style='font-size:12.0pt;line-height:115%;
font-family:Times New Roman,serif;mso-fareast-font-family:Times New Roman'><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Bên</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>
B <span class=SpellE>có</span> <span class=SpellE>trách</span> <span
class=SpellE>nhi&#7879;m</span> <span class=SpellE>giao</span> <span
class=SpellE>hàng</span> <span class=SpellE>&#273;úng</span> <span
class=SpellE>th&#7901;i</span> <span class=SpellE>gian</span> <span
class=SpellE>và</span> <span class=SpellE>&#273;&#7883;a</span> <span
class=SpellE>&#273;i&#7875;m</span> <span class=SpellE>giao</span> <span
class=SpellE>hàng</span> <span class=SpellE>theo</span> <span class=SpellE>ch&#7881;</span>
<span class=SpellE>&#273;&#7883;nh</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>Bên</span> A. <span class=SpellE>N&#7871;u</span> <span
class=SpellE>bên</span> B <span class=SpellE>giao</span> <span class=SpellE>thành</span>
<span class=SpellE>ph&#7847;m</span> <span class=SpellE>không</span> <span
class=SpellE>&#273;úng</span> <span class=SpellE>h&#7841;n</span> <span
class=SpellE>làm</span> <span class=SpellE>&#7843;nh</span> <span class=SpellE>h&#432;&#7903;ng</span>
<span class=SpellE>&#273;&#7871;n</span> <span class=SpellE>ngày</span> <span
class=SpellE>giao</span> <span class=SpellE>hàng</span> <span class=SpellE>c&#7911;a</span>
<span class=SpellE>bên</span> A, <span class=SpellE>Bên</span> B <span
class=SpellE>ph&#7843;i</span> <span class=SpellE>ch&#7883;u</span> <span
class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span> <span
class=SpellE>v&#7873;</span> <span class=SpellE>t&#7845;t</span> <span
class=SpellE>c&#7843;</span> <span class=SpellE>nh&#7919;ng</span> <span
class=SpellE>t&#7893;n</span> <span class=SpellE>th&#7845;t</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>bên</span> A <span
class=SpellE>và</span> <span class=SpellE>các</span> chi <span class=SpellE>phí</span>
<span class=SpellE>phát</span> <span class=SpellE><span class=GramE>sinh</span></span><span
class=GramE><span style='mso-spacerun:yes'>  </span>bao</span> <span
class=SpellE>g&#7891;m</span> <span class=SpellE>nh&#432;ng</span> <span
class=SpellE>không</span> <span class=SpellE>gi&#7899;i</span> <span
class=SpellE>h&#7841;n</span> <span class=SpellE>nh&#432;</span> chi <span
class=SpellE>phí</span> <span class=SpellE>v&#7853;n</span> <span class=SpellE>chuy&#7875;n</span>
<span class=SpellE>&#273;&#432;&#7901;ng</span> <span class=SpellE>hàng</span> <span
class=SpellE>không</span>, chi <span class=SpellE>phí</span> <span
class=SpellE>ph&#7841;t</span> <span class=SpellE>ch&#7853;m</span> <span
class=SpellE>giao</span> <span class=SpellE>hàng</span> <span class=SpellE>t&#7915;</span>
<span class=SpellE>khách</span> <span class=SpellE>hàng</span>... <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Party B has the
responsibility of delivering the garments to the address designated by Party A
on time. If Party B fails to deliver the garments on time, affecting to Party
A's OTD, Party B <span class=GramE>has to</span> take responsibility for all
expenses incurred such as air-freight cost, late penalties from customer... but
not limited.<o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<h1 style='margin-right:73.55pt;text-align:justify;line-height:115%;mso-list:
none'><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;I&#7872;U
9: B&#7842;O M&#7852;T <o:p></o:p></span></h1>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><b><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>ARTICLE 9:
CONFIDENTIALITY <o:p></o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Các</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>Bên</span> <span class=SpellE>có</span> <span class=SpellE>trách</span>
<span class=SpellE>nhi&#7879;m</span> <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>gi&#7919;</span> <span class=SpellE>kín</span> <span class=SpellE>t&#7845;t</span>
<span class=SpellE>c&#7843;</span> <span class=SpellE>nh&#7919;ng</span> <span
class=SpellE>thông</span> tin <span class=SpellE>liên</span> <span
class=SpellE>quan</span> <span class=SpellE>t&#7899;i</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>và</span> <span class=SpellE>Ph&#7909;</span> <span class=SpellE>l&#7909;c</span>
<span class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>, <span
class=SpellE>thông</span> tin <span class=SpellE>khách</span> <span
class=SpellE>hàng</span> <span class=SpellE>mà</span> <span class=SpellE>mình</span>
<span class=SpellE>nh&#7853;n</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
<span class=SpellE>t&#7915;</span> <span class=SpellE>phía</span> <span
class=SpellE>bên</span> kia <span class=SpellE>trong</span> <span class=SpellE>su&#7889;t</span>
<span class=SpellE>th&#7901;i</span> <span class=SpellE>h&#7841;n</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span>.<span style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>The Parties are
obligated to keep confidential all information related to the Contract and its <span
class=GramE>Appendix,</span> customer information acquired from the other Party
during the Contract period. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraphCxSpFirst style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>M&#7895;i</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>bên</span> <span class=SpellE>không</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
<span class=SpellE>ti&#7871;t</span> <span class=SpellE>l&#7897;</span> <span
class=SpellE>cho</span> <span class=SpellE>b&#7845;t</span> <span class=SpellE>c&#7913;</span>
<span class=SpellE>Bên</span> <span class=SpellE>th&#7913;</span> <span
class=SpellE>ba</span> <span class=SpellE>nào</span> <span class=SpellE>b&#7845;t</span>
<span class=SpellE>k&#7923;</span> <span class=SpellE>thông</span> tin <span
class=SpellE>nói</span> <span class=SpellE>trên</span> <span class=SpellE>tr&#7915;</span>
<span class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>h&#7907;p</span>
<span class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>ch&#7845;p</span>
<span class=SpellE>thu&#7853;n</span> <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>v&#259;n</span> <span class=SpellE>b&#7843;n</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>Bên</span> kia <span
class=SpellE>ho&#7863;c</span> <span class=SpellE>theo</span> <span
class=SpellE>yêu</span> <span class=SpellE>c&#7847;u</span> <span class=SpellE>c&#7911;a</span>
<span class=SpellE>c&#417;</span> <span class=SpellE>quan</span> <span
class=SpellE>qu&#7843;n</span> <span class=SpellE>lý</span> <span class=SpellE>Nhà</span>
<span class=SpellE>n&#432;&#7899;c</span> <span class=SpellE>có</span> <span
class=SpellE>th&#7849;m</span> <span class=SpellE>quy&#7873;n</span>. <o:p></o:p></span></p>

<p class=MsoListParagraphCxSpLast style='margin-right:0in;mso-add-space:auto;
text-indent:0in;line-height:115%'><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><i>Each Party is not allowed to disclose to
any Third Party any aforesaid information unless a written consent is obtained
from the other Party or at the request of the State competent authorities.</i> <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>M&#7895;i</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>bên</span> <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>ti&#7871;n</span> <span class=SpellE>hành</span> <span
class=SpellE>m&#7885;i</span> <span class=SpellE>bi&#7879;n</span> <span
class=SpellE>pháp</span> <span class=SpellE>c&#7847;n</span> <span
class=SpellE>thi&#7871;t</span> <span class=SpellE>&#273;&#7875;</span> <span
class=SpellE>&#273;&#7843;m</span> <span class=SpellE>b&#7843;o</span> <span
class=SpellE>r&#7857;ng</span> <span class=SpellE>không</span> <span
class=SpellE>m&#7897;t</span> <span class=SpellE>nhân</span> <span
class=SpellE>viên</span> <span class=SpellE>nào</span> hay <span class=SpellE>b&#7845;t</span>
<span class=SpellE>c&#7913;</span> ai <span class=SpellE>thu&#7897;c</span> <span
class=SpellE>s&#7921;</span> <span class=SpellE>qu&#7843;n</span> <span
class=SpellE>lý</span> <span class=SpellE>c&#7911;a</span> <span class=SpellE>mình</span>
vi <span class=SpellE>ph&#7841;m</span> <span class=SpellE>&#273;i&#7873;u</span>
<span class=SpellE>kho&#7843;n</span> <span class=SpellE>này</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Each Party must
take any necessary measures to ensure that no employee or any subordinates
under its management violates this provision.<span style='mso-spacerun:yes'> 
</span><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraphCxSpFirst style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>&#272;i&#7873;u</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>kho&#7843;n</span> <span class=SpellE>này</span> <span
class=SpellE>v&#7851;n</span> <span class=SpellE>còn</span> <span class=SpellE>hi&#7879;u</span>
<span class=SpellE>l&#7921;c</span> <span class=SpellE>ngay</span> <span
class=SpellE>c&#7843;</span> <span class=SpellE>khi</span> <span class=SpellE>H&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>h&#7871;t</span> <span class=SpellE>hi&#7879;u</span> <span
class=SpellE>l&#7921;c</span> <span class=SpellE>và</span> <span class=SpellE>hai</span>
<span class=SpellE>bên</span> <span class=SpellE>không</span> <span
class=SpellE>còn</span> <span class=SpellE>h&#7907;p</span> <span class=SpellE>tác</span>.
<o:p></o:p></span></p>

<p class=MsoListParagraphCxSpLast style='margin-right:0in;mso-add-space:auto;
text-indent:0in;line-height:115%'><i><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'>This provision still survives even
termination and non-cooperation. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:0in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b style='mso-bidi-font-weight:
normal'><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span></span></b><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:49.5pt;margin-bottom:
.25pt;margin-left:.25in;text-indent:4.0pt;line-height:115%;tab-stops:255.8pt'><b
style='mso-bidi-font-weight:normal'><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'>&#272;I&#7872;U 10: LO&#7840;I TR&#7914;
TRÁCH NHI&#7878;M C&#7910;A M&#7894;I BÊN<o:p></o:p></span></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:234.7pt;margin-bottom:
.25pt;margin-left:.25in;text-indent:4.0pt;line-height:115%'><b
style='mso-bidi-font-weight:normal'><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'>ARTICLE 10: </span></b><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Trong</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>x&#7843;y</span> <span class=SpellE>ra</span> <span class=SpellE>s&#7921;</span>
<span class=SpellE>ki&#7879;n</span> <span class=SpellE>b&#7845;t</span> <span
class=SpellE>kh&#7843;</span> <span class=SpellE>kháng</span> <span
class=SpellE>khi&#7871;n</span> <span class=SpellE>cho</span> <span
class=SpellE>m&#7895;i</span> <span class=SpellE>bên</span> <span class=SpellE>không</span>
<span class=SpellE>th&#7875;</span> <span class=SpellE>th&#7921;c</span> <span
class=SpellE>hi&#7879;n</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>th&#7921;c</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>không</span> <span class=SpellE>&#273;&#7847;y</span> <span
class=SpellE>&#273;&#7911;</span> <span class=SpellE>quy&#7873;n</span> <span
class=SpellE>và</span> <span class=SpellE>ngh&#297;a</span> <span class=SpellE>v&#7909;</span>
<span class=SpellE>c&#7911;a</span> <span class=SpellE>mình</span> <span
class=SpellE>v&#7899;i</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>thì</span> <span class=SpellE>trách</span> <span class=SpellE>nhi&#7879;m</span>
<span class=SpellE>b&#7891;i</span> <span class=SpellE>th&#432;&#7901;ng</span>
<span class=SpellE>thi&#7879;t</span> <span class=SpellE>h&#7841;i</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>m&#7895;i</span> <span
class=SpellE>bên</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>lo&#7841;i</span> <span class=SpellE>tr&#7915;</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span>In case of Force Majeure Events, <span
class=GramE>making</span> a Party fail to perform or perform inadequately its
rights and obligations herein, the responsibility for compensating damages of
each Party shall be released. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>S&#7921;</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>ki&#7879;n</span> <span class=SpellE>b&#7845;t</span> <span
class=SpellE>kh&#7843;</span> <span class=SpellE>kháng</span> <span
class=SpellE>là</span> <span class=SpellE>các</span> <span class=SpellE>s&#7921;</span>
<span class=SpellE>ki&#7879;n</span> <span class=SpellE>x&#7843;y</span> <span
class=SpellE>ra</span> <span class=SpellE>m&#7897;t</span> <span class=SpellE>cách</span>
<span class=SpellE>khách</span> <span class=SpellE>quan</span> <span
class=SpellE>không</span> <span class=SpellE>th&#7875;</span> <span
class=SpellE>l&#432;&#7901;ng</span> <span class=SpellE>tr&#432;&#7899;c</span>
<span class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>và</span> <span
class=SpellE>không</span> <span class=SpellE>th&#7875;</span> <span
class=SpellE>kh&#7855;c</span> <span class=SpellE>ph&#7909;c</span> <span
class=SpellE>&#273;&#432;&#7907;c</span>, <span class=SpellE>m&#7863;c</span> <span
class=SpellE>dù</span> <span class=SpellE>&#273;ã</span> <span class=SpellE>áp</span>
<span class=SpellE>d&#7909;ng</span> <span class=SpellE>m&#7885;i</span> <span
class=SpellE>bi&#7879;n</span> <span class=SpellE>pháp</span> <span
class=SpellE>c&#7847;n</span> <span class=SpellE>thi&#7871;t</span> <span
class=SpellE>là</span> <span class=SpellE>kh&#7843;</span> <span class=SpellE>n&#259;ng</span>
<span class=SpellE>cho</span> <span class=SpellE>phép</span>, bao <span
class=SpellE>g&#7891;m</span> <span class=SpellE>nh&#432;ng</span> <span
class=SpellE>không</span> <span class=SpellE>gi&#7899;i</span> <span
class=SpellE>h&#7841;n</span> &#7903; <span class=SpellE>các</span> <span
class=SpellE>s&#7921;</span> <span class=SpellE>ki&#7879;n</span> <span
class=SpellE>nh&#432;</span> <span class=SpellE>thiên</span> tai, <span
class=SpellE>h&#7887;a</span> <span class=SpellE>ho&#7841;n</span>, <span
class=SpellE>l&#361;</span> <span class=SpellE>l&#7909;t</span>, <span
class=SpellE>&#273;&#7897;ng</span> <span class=SpellE>&#273;&#7845;t</span>,
tai <span class=SpellE>n&#7841;n</span>, <span class=SpellE>th&#7843;m</span> <span
class=SpellE>ho&#7841;</span>, <span class=SpellE>h&#7841;n</span> <span
class=SpellE>ch&#7871;</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>d&#7883;ch</span> <span class=SpellE>b&#7879;nh</span>, <span
class=SpellE>nhi&#7877;m</span> <span class=SpellE>h&#7841;t</span> <span
class=SpellE>nhân</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>phóng</span> <span class=SpellE>x&#7841;</span>, ... <span
class=SpellE>chi&#7871;n</span> <span class=SpellE>tranh</span>, <span
class=SpellE>n&#7897;i</span> <span class=SpellE>chi&#7871;n</span>, <span
class=SpellE>kh&#7903;i</span> <span class=SpellE>ngh&#297;a</span>, <span
class=SpellE>&#273;ình</span> <span class=SpellE>công</span> <span
class=SpellE>ho&#7863;c</span> <span class=SpellE>b&#7841;o</span> <span
class=SpellE>lo&#7841;n</span>, can <span class=SpellE>thi&#7879;p</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>C&#417;</span> <span
class=SpellE>quan</span> <span class=SpellE>Chính</span> <span class=SpellE>ph&#7911;</span>.
<o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Force Majeure
Events are events that occur objectively, unforeseeably, and irrecoverably.
although any necessary measures have been applied to the permissible extent,
including but not limited to events such as Acts of Gods, fire, flood,
earthquake, accidents, disasters, restrictions of epidemics, nuclear or
radioactive contamination, war, civil war, <span class=GramE>rise up</span>,
strike or riot, intervention of the department Government, etc. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Trong</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>x&#7843;y</span> <span class=SpellE>ra</span> <span class=SpellE>s&#7921;</span>
<span class=SpellE>ki&#7879;n</span> <span class=SpellE>b&#7845;t</span> <span
class=SpellE>kh&#7843;</span> <span class=SpellE>kháng</span>, <span
class=SpellE>m&#7895;i</span> <span class=SpellE>bên</span> <span class=SpellE>ph&#7843;i</span>
<span class=SpellE>nhanh</span> <span class=SpellE>chóng</span> <span
class=SpellE>thông</span> <span class=SpellE>báo</span> <span class=SpellE>cho</span>
<span class=SpellE>bên</span> kia <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>v&#259;n</span> <span class=SpellE>b&#7843;n</span> <span
class=SpellE>v&#7873;</span> <span class=SpellE>vi&#7879;c</span> <span
class=SpellE>không</span> <span class=SpellE>th&#7921;c</span> <span
class=SpellE>hi&#7879;n</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>ngh&#297;a</span> <span class=SpellE>v&#7909;</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>mình</span> do <span
class=SpellE>s&#7921;</span> <span class=SpellE>ki&#7879;n</span> <span
class=SpellE>b&#7845;t</span> <span class=SpellE>kh&#7843;</span> <span
class=SpellE>kháng</span>, <span class=SpellE>và</span> <span class=SpellE>s&#7869;</span>,
<span class=SpellE>trong</span> <span class=SpellE>th&#7901;i</span> <span
class=SpellE>gian</span> 15 (<span class=SpellE>m&#432;&#7901;i</span> <span
class=SpellE><span class=GramE>l&#259;m</span></span><span class=GramE> )</span>
<span class=SpellE>ngày</span> <span class=SpellE>k&#7875;</span> <span
class=SpellE>t&#7915;</span> <span class=SpellE>ngày</span> <span class=SpellE>x&#7843;y</span>
<span class=SpellE>ra</span> <span class=SpellE>s&#7921;</span> <span
class=SpellE>ki&#7879;n</span> <span class=SpellE>b&#7845;t</span> <span
class=SpellE>kh&#7843;</span> <span class=SpellE>kháng</span>, <span
class=SpellE>chuy&#7875;n</span> <span class=SpellE>tr&#7921;c</span> <span
class=SpellE>ti&#7871;p</span> <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>th&#432;</span> <span class=SpellE>b&#7843;o</span> <span
class=SpellE>&#273;&#7843;m</span> <span class=SpellE>cho</span> <span
class=SpellE>Bên</span> kia <span class=SpellE>các</span> <span class=SpellE>b&#7857;ng</span>
<span class=SpellE>ch&#7913;ng</span> <span class=SpellE>v&#7873;</span> <span
class=SpellE>vi&#7879;c</span> <span class=SpellE>x&#7843;y</span> <span
class=SpellE>ra</span> <span class=SpellE>s&#7921;</span> <span class=SpellE>ki&#7879;n</span>
<span class=SpellE>b&#7845;t</span> <span class=SpellE>kh&#7843;</span> <span
class=SpellE>kháng</span> <span class=SpellE>và</span> <span class=SpellE>kho&#7843;ng</span>
<span class=SpellE>th&#7901;i</span> <span class=SpellE>gian</span> <span
class=SpellE>x&#7843;y</span> <span class=SpellE>ra</span> <span class=SpellE>s&#7921;</span>
<span class=SpellE>ki&#7879;n</span> <span class=SpellE>b&#7845;t</span> <span
class=SpellE>kh&#7843;</span> <span class=SpellE>kháng</span> <span
class=SpellE>&#273;ó</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>In case of Force
Majeure Events, each Party must promptly notify the other Party in writing
about failure in fulfilling obligations due to Force Majeure Events and within
15 (fifteen) working days since date of Force Majeure Event, send a mail via
registered mail delivery service to the other Party regarding the <span
class=GramE>evidences</span> on Force Majeure Events and occurrence time. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Bên</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>thông</span> <span class=SpellE>báo</span> <span class=SpellE>vi&#7879;c</span>
<span class=SpellE>th&#7921;c</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>h&#7885;</span> <span
class=SpellE>tr&#7903;</span> <span class=SpellE>nên</span> <span class=SpellE>không</span>
<span class=SpellE>th&#7875;</span> <span class=SpellE>th&#7921;c</span> <span
class=SpellE>hi&#7879;n</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
do <span class=SpellE>S&#7921;</span> <span class=SpellE>ki&#7879;n</span> <span
class=SpellE>b&#7845;t</span> <span class=SpellE>kh&#7843;</span> <span
class=SpellE>kháng</span> <span class=SpellE>có</span> <span class=SpellE>trách</span>
<span class=SpellE>nhi&#7879;m</span> <span class=SpellE>ph&#7843;i</span> <span
class=SpellE>th&#7921;c</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>m&#7885;i</span> <span class=SpellE>n&#7895;</span> <span
class=SpellE>l&#7921;c</span> <span class=SpellE>&#273;&#7875;</span> <span
class=SpellE>gi&#7843;m</span> <span class=SpellE>thi&#7875;u</span> <span
class=SpellE>&#7843;nh</span> <span class=SpellE>h&#432;&#7903;ng</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>S&#7921;</span> <span
class=SpellE>ki&#7879;n</span> <span class=SpellE>b&#7845;t</span> <span
class=SpellE>kh&#7843;</span> <span class=SpellE>kháng</span> <span
class=SpellE>&#273;ó</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>The Party who
notifies that its Contract performance cannot be conducted by Force Majeure
Events is obligated to make any efforts to minimize impacts of such Force
Majeure Events. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></i></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
.05pt;margin-left:.5in;mso-add-space:auto;text-indent:-.5in;line-height:115%;
mso-list:l6 level1 lfo2'><![if !supportLists]><span style='mso-list:Ignore'>-<span
style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span style='font-size:12.0pt;line-height:115%;
font-family:Times New Roman,serif'>Khi <span class=SpellE>S&#7921;</span> <span
class=SpellE>ki&#7879;n</span> <span class=SpellE>b&#7845;t</span> <span
class=SpellE>kh&#7843;</span> <span class=SpellE>kháng</span> <span
class=SpellE>x&#7843;y</span> <span class=SpellE>ra</span>, <span class=SpellE>thì</span>
<span class=SpellE>ngh&#297;a</span> <span class=SpellE>v&#7909;</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>các</span> <span class=SpellE>Bên</span>
<span class=SpellE>t&#7841;m</span> <span class=SpellE>th&#7901;i</span> <span
class=SpellE>không</span> <span class=SpellE>th&#7921;c</span> <span
class=SpellE>hi&#7879;n</span> <span class=SpellE>và</span> <span class=SpellE>s&#7869;</span>
<span class=SpellE>ngay</span> <span class=SpellE>l&#7853;p</span> <span
class=SpellE>t&#7913;c</span> <span class=SpellE>ph&#7909;c</span> <span
class=SpellE>h&#7891;i</span> <span class=SpellE>l&#7841;i</span> <span
class=SpellE>các</span> <span class=SpellE>ngh&#297;a</span> <span
class=SpellE>v&#7909;</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>mình</span> <span class=SpellE>theo</span> <span class=SpellE>H&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>khi</span> <span
class=SpellE>ch&#7845;m</span> <span class=SpellE>d&#7913;t</span> <span
class=SpellE>S&#7921;</span> <span class=SpellE>ki&#7879;n</span> <span
class=SpellE>b&#7845;t</span> <span class=SpellE>kh&#7843;</span> <span
class=SpellE>kháng</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>khi</span> <span class=SpellE>s&#7921;</span> <span class=SpellE>ki&#7879;n</span>
<span class=SpellE>b&#7845;t</span> <span class=SpellE>kh&#7843;</span> <span
class=SpellE>kháng</span> <span class=SpellE>&#273;ó</span> <span class=SpellE>b&#7883;</span>
<span class=SpellE>lo&#7841;i</span> <span class=SpellE>b&#7887;</span>.<span
style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>When Force Majeure
Events occur, the Parties' obligations are <span class=GramE>temporarily
suspended</span> and promptly assumed when Force Majeure Events disappear, or
such Force Majeure Events are eliminated. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><b style='mso-bidi-font-weight:
normal'><span style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span></span></b><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'><o:p></o:p></span></p>

<h1 style='margin-top:0in;margin-right:73.55pt;margin-bottom:.25pt;margin-left:
0in;text-align:justify;text-indent:0in;line-height:115%;mso-list:none'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;I&#7872;U
11: S&#7916;A &#272;&#7892;I, T&#7840;M NG&#431;NG TH&#7920;C HI&#7878;N VÀ CH&#7844;M
D&#7912;T H&#7906;P &#272;&#7890;NG<o:p></o:p></span></h1>

<h1 style='margin-top:0in;margin-right:73.55pt;margin-bottom:.25pt;margin-left:
0in;text-align:justify;text-indent:0in;line-height:115%;mso-list:none'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>ARTICLE
11: AMENDMENT, SUSPENSION &amp; TERMINATION <o:p></o:p></span></i></h1>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-top:0in;margin-right:0in;margin-bottom:
.25pt;margin-left:.5pt;mso-add-space:auto;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>B&#7845;t</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>k&#7923;</span> <span class=SpellE>s&#7917;a</span> <span
class=SpellE>&#273;&#7893;i</span> <span class=SpellE>ho&#7863;c</span> <span
class=SpellE>b&#7893;</span> sung <span class=SpellE>nào</span> <span
class=SpellE>&#273;&#7889;i</span> <span class=SpellE>v&#7899;i</span> <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>Nguyên</span> <span class=SpellE>t&#7855;c</span> <span
class=SpellE>s&#7869;</span> <span class=SpellE>ch&#7881;</span> <span
class=SpellE>có</span> <span class=SpellE>hi&#7879;u</span> <span class=SpellE>l&#7921;c</span>
<span class=SpellE>khi</span> <span class=SpellE>có</span> <span class=SpellE>tho&#7843;</span>
<span class=SpellE>thu&#7853;n</span> <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>v&#259;n</span> <span class=SpellE>b&#7843;n</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>Các</span> <span class=SpellE>Bên</span>.
<span class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>ch&#7845;m</span> <span class=SpellE>d&#7913;t</span> <span
class=SpellE>trong</span> <span class=SpellE>các</span> <span class=SpellE>tr&#432;&#7901;ng</span>
<span class=SpellE>h&#7907;p</span> <span class=SpellE>sau</span>:<span
style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>Any
amendments or supplements to the Contract are only valid when it is agreed in
writing by the Parties. The Contract is terminated in following circumstances: <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:40.5pt;text-indent:.25in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:40.5pt;text-indent:0in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>1. <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>h&#7871;t</span> <span class=SpellE>h&#7841;n</span> <span
class=SpellE>và</span> <span class=SpellE>Các</span> <span class=SpellE>Bên</span>
<span class=SpellE>không</span> <span class=SpellE>gia</span> <span
class=SpellE>h&#7841;n</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span>:<span style='mso-spacerun:yes'>  </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:40.5pt;text-indent:0in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'>    </span><i>The Contract expires and the Contract is
not extended by the Parties. <o:p></o:p></i></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:40.5pt;text-indent:0in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:40.5pt;text-indent:0in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span>2. <span class=SpellE>Các</span> <span
class=SpellE>Bên</span> <span class=SpellE>th&#7887;a</span> <span
class=SpellE>thu&#7853;n</span> <span class=SpellE>ch&#7845;m</span> <span
class=SpellE>d&#7913;t</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>tr&#432;&#7899;c</span> <span
class=SpellE>th&#7901;i</span> <span class=SpellE>h&#7841;n</span>, <span
class=SpellE>trong</span> <span class=SpellE>tr&#432;&#7901;ng</span> <span
class=SpellE>h&#7907;p</span> <span class=SpellE>&#273;ó</span>, <span
class=SpellE>Các</span> <span class=SpellE>Bên</span> <span class=SpellE>s&#7869;</span>
<span class=SpellE>tho&#7843;</span> <span class=SpellE>thu&#7853;n</span> <span
class=SpellE>v&#7873;</span> <span class=SpellE>các</span> <span class=SpellE>&#273;i&#7873;u</span>
<span class=SpellE>ki&#7879;n</span> <span class=SpellE>c&#7909;</span> <span
class=SpellE>th&#7875;</span> <span class=SpellE>liên</span> <span
class=SpellE>quan</span> <span class=SpellE>t&#7899;i</span> <span
class=SpellE>vi&#7879;c</span> <span class=SpellE>ch&#7845;m</span> <span
class=SpellE>d&#7913;t</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:49.5pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'>  </span>Early termination is agreed by the Parties;
in that case, the Parties shall agree about the specific conditions of
termination. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:40.5pt;text-indent:0in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:49.5pt;text-indent:-9.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>3. <span
class=SpellE>M&#7897;t</span> <span class=SpellE>trong</span> <span
class=SpellE>Các</span> <span class=SpellE>Bên</span> <span class=SpellE>ng&#7915;ng</span>
<span class=SpellE>kinh</span> <span class=SpellE>doanh</span>, <span
class=SpellE>không</span> <span class=SpellE>có</span> <span class=SpellE>kh&#7843;</span>
<span class=SpellE>n&#259;ng</span> chi <span class=SpellE>tr&#7843;</span> <span
class=SpellE>các</span> <span class=SpellE>kho&#7843;n</span> <span
class=SpellE>n&#7907;</span> <span class=SpellE>&#273;&#7871;n</span> <span
class=SpellE>h&#7841;n</span>, <span class=SpellE>lâm</span> <span
class=SpellE>vào</span> <span class=SpellE>tình</span> <span class=SpellE>tr&#7841;ng</span>
<span class=SpellE>ho&#7863;c</span> <span class=SpellE>b&#7883;</span> <span
class=SpellE>xem</span> <span class=SpellE>là</span> <span class=SpellE>m&#7845;t</span>
<span class=SpellE>kh&#7843;</span> <span class=SpellE>n&#259;ng</span> <span
class=SpellE>thanh</span> <span class=SpellE>toán</span>, <span class=SpellE>có</span>
<span class=SpellE>quy&#7871;t</span> <span class=SpellE>&#273;&#7883;nh</span>
<span class=SpellE>gi&#7843;i</span> <span class=SpellE>th&#7875;</span>, <span
class=SpellE>phá</span> <span class=SpellE>s&#7843;n</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:49.5pt;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Either Party is
subject to business suspension, having no payment capacity for due debts,
insolvency, dissolution decision, bankruptcy. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:40.5pt;text-indent:.25in;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Trong</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>tr&#432;&#7901;ng</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>này</span> <span class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>
<span class=SpellE>s&#7869;</span> <span class=SpellE>k&#7871;t</span> <span
class=SpellE>thúc</span> <span class=SpellE>b&#7857;ng</span> <span
class=SpellE>cách</span> <span class=SpellE>th&#7913;c</span> do Hai <span
class=SpellE>Bên</span> <span class=SpellE>tho&#7843;</span> <span
class=SpellE>thu&#7853;n</span> <span class=SpellE>và</span> / <span
class=SpellE>ho&#7863;c</span> <span class=SpellE>phù</span> <span
class=SpellE>h&#7907;p</span> <span class=SpellE>v&#7899;i</span> <span
class=SpellE>các</span> <span class=SpellE>quy</span> <span class=SpellE>&#273;&#7883;nh</span>
<span class=SpellE>c&#7911;a</span> <span class=SpellE>pháp</span> <span
class=SpellE>lu&#7853;t</span> <span class=SpellE>hi&#7879;n</span> <span
class=SpellE>hành</span>; <span class=SpellE>ho&#7863;c</span> Thanh <span
class=SpellE>lý</span> <span class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>:
Khi <span class=SpellE>có</span> <span class=SpellE>nhu</span> <span
class=SpellE>c&#7847;u</span> <span class=SpellE>thanh</span> <span
class=SpellE>lý</span> <span class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span>,
<span class=SpellE>hai</span> <span class=SpellE>bên</span> <span class=SpellE>ti&#7871;n</span>
<span class=SpellE>hành</span> <span class=SpellE>&#273;&#7889;i</span> <span
class=SpellE>soát</span>, <span class=SpellE>thanh</span> <span class=SpellE>toán</span>
<span class=SpellE>hoàn</span> <span class=SpellE>thi&#7879;n</span> <span
class=SpellE>các</span> <span class=SpellE>kho&#7843;n</span> <span
class=SpellE>phí</span>. Sau <span class=SpellE>khi</span> <span class=SpellE>hai</span>
<span class=SpellE>bên</span> <span class=SpellE>hoàn</span> <span
class=SpellE>thành</span> <span class=SpellE>ngh&#297;a</span> <span
class=SpellE>v&#7909;</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>mình</span> <span class=SpellE>s&#7869;</span> <span class=SpellE>ti&#7871;n</span>
<span class=SpellE>hành</span> <span class=SpellE>ký</span> <span class=SpellE>k&#7871;t</span>
<span class=SpellE>Biên</span> <span class=SpellE>b&#7843;n</span> <span
class=SpellE>thanh</span> <span class=SpellE>lý</span> <span class=SpellE>H&#7907;p</span>
<span class=SpellE>&#273;&#7891;ng</span>.<span style='mso-spacerun:yes'> 
</span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>In this case, the
Contract shall be terminated by the ways agreed by the Parties and/or in
accordance with the applicable legal regulations; or Contract liquidation: When
it is required to liquidate the Contract, the Parties conduct reconciliation
and payment for the costs. After the obligations. are successfully completed,
the Minutes of Contract Liquidation shall be signed by the Parties. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:0in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<h1 style='margin-top:0in;margin-right:73.55pt;margin-bottom:.25pt;margin-left:
0in;text-align:justify;text-indent:0in;line-height:115%;mso-list:none'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;I&#7872;U
12: GI&#7842;I QUY&#7870;T TRANH CH&#7844;P<o:p></o:p></span></h1>

<p class=MsoNormal style='margin-left:0in;text-indent:0in;line-height:115%'><o:p>&nbsp;</o:p></p>

<h1 style='margin-top:0in;margin-right:73.55pt;margin-bottom:.25pt;margin-left:
0in;text-align:justify;text-indent:0in;line-height:115%;mso-list:none'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>ARTICLE
12: DISPUTE SETTLEMENT<o:p></o:p></span></i></h1>

<p class=MsoListParagraphCxSpFirst style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>T&#7845;t</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>c&#7843;</span> <span class=SpellE>nh&#7919;ng</span> <span
class=SpellE>phát</span> <span class=SpellE>sinh</span> <span class=SpellE>n&#7871;u</span>
<span class=SpellE>có</span> <span class=SpellE>liên</span> <span class=SpellE>quan</span>
<span class=SpellE>&#273;&#7871;n</span> <span class=SpellE>h&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>s&#7869;</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>gi&#7843;i</span> <span class=SpellE>quy&#7871;t</span> <span
class=SpellE>b&#7857;ng</span> <span class=SpellE>th&#432;&#417;ng</span> <span
class=SpellE>l&#432;&#7907;ng</span>. <span class=SpellE>N&#7871;u</span> <span
class=SpellE>không</span> <span class=SpellE>th&#432;&#417;ng</span> <span
class=SpellE>l&#432;&#7907;ng</span> <span class=SpellE>&#273;&#432;&#7907;c</span>
<span class=SpellE>thì</span> <span class=SpellE>tranh</span> <span
class=SpellE>ch&#7845;p</span> <span class=SpellE>s&#7869;</span> <span
class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>gi&#7843;i</span> <span
class=SpellE>quy&#7871;t</span> <span class=SpellE>t&#7841;i</span> <span
class=SpellE>Trung</span> <span class=SpellE>tâm</span> <span class=SpellE>tr&#7885;ng</span>
<span class=SpellE>tài</span> <span class=SpellE>qu&#7889;c</span> <span
class=SpellE>t&#7871;</span> <span class=SpellE>Vi&#7879;t</span> Nam <span
class=SpellE>bên</span> <span class=SpellE>c&#7841;nh</span> <span
class=SpellE>phòng</span> <span class=SpellE>th&#432;&#417;ng</span> <span
class=SpellE>m&#7841;i</span> <span class=SpellE>và</span> <span class=SpellE>công</span>
<span class=SpellE>nghi&#7879;p</span> <span class=SpellE>Vi&#7879;t</span> Nam
<span class=SpellE>theo</span> <span class=SpellE>Quy</span> <span
class=SpellE>t&#7855;c</span> <span class=SpellE>t&#7889;</span> <span
class=SpellE>t&#7909;ng</span> <span class=SpellE>tr&#7885;ng</span> <span
class=SpellE>tài</span> <span class=SpellE>c&#7911;a</span> <span class=SpellE>Trung</span>
<span class=SpellE>tâm</span> <span class=SpellE>này</span>. <span
class=SpellE>Quy&#7871;t</span> <span class=SpellE>&#273;&#7883;nh</span> <span
class=SpellE>c&#7911;a</span> <span class=SpellE>tr&#7885;ng</span> <span
class=SpellE>tài</span> <span class=SpellE>là</span> <span class=SpellE>quy&#7871;t</span>
<span class=SpellE>&#273;&#7883;nh</span> <span class=SpellE>cu&#7889;i</span> <span
class=SpellE>cùng</span> <span class=SpellE>và</span> <span class=SpellE>ph&#7843;i</span>
<span class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>các</span> <span
class=SpellE>bên</span> <span class=SpellE>tuân</span> <span class=SpellE>theo</span>,
<span class=SpellE>Phí</span> <span class=SpellE>tr&#7885;ng</span> <span
class=SpellE>tài</span> <span class=SpellE>s&#7869;</span> do <span
class=SpellE>bên</span> <span class=SpellE>thua</span> <span class=SpellE>ki&#7879;n</span>
<span class=SpellE>tr&#7843;</span>, <span class=SpellE>tr&#7915;</span> phi <span
class=SpellE>có</span> <span class=SpellE>s&#7921;</span> <span class=SpellE>th&#7887;a</span>
<span class=SpellE>thu&#7853;n</span> <span class=SpellE>c&#7911;a</span> <span
class=SpellE>hai</span> <span class=SpellE>bên</span>.<o:p></o:p></span></p>

<p class=MsoListParagraphCxSpLast style='margin-right:0in;mso-add-space:auto;
text-indent:0in;line-height:115%'><span style='font-size:12.0pt;line-height:
115%;font-family:Times New Roman,serif'><span style='mso-spacerun:yes'> 
</span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>All disputes, if
any, related to this Contract shall be handled by negotiation. Otherwise, such
disputes shall be put forward to Vietnam International Arbitration Center next
to VCCI for final settlement in accordance with Arbitration Rules of this
Center. The arbitration's judgment shall be final and binding the Party. The
arbitration fees shall be borne by the loser, unless otherwise agreed by the
Parties. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<h1 style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;margin-left:
.25in;text-align:justify;text-indent:4.0pt;line-height:115%;mso-list:none'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>&#272;I&#7872;U
13: &#272;I&#7872;U KHO&#7842;N QUY &#272;&#7882;NH V&#7872; HI&#7878;U L&#7920;C
VÀ GI&#7842;I QUY&#7870;T TRANH CH&#7844;P H&#7906;P &#272;&#7890;NG<o:p></o:p></span></h1>

<h1 style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;margin-left:
.25in;text-align:justify;text-indent:4.0pt;line-height:115%;mso-list:none;
tab-stops:27.0pt'><i><span style='font-size:12.0pt;line-height:115%;font-family:
Times New Roman,serif'>ARTICLE 13: PROVISIONS ON CONTRACT VALIDITY AND
DISPUTE <o:p></o:p></span></i></h1>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.25in;text-indent:4.0pt;line-height:115%'><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'> </span><o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>H&#7907;p</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>có</span> <span class=SpellE>hi&#7879;u</span> <span class=SpellE>l&#7921;c</span>
<span class=SpellE>t&#7915;</span> <span class=SpellE>ngày</span> <span
class=SpellE>ký</span> <span class=SpellE>&#273;&#7871;n</span> <span
class=SpellE>h&#7871;t</span> <span class=SpellE>ngày</span> <span
style='mso-bidi-font-style:italic'>scot23</span><o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:-.5in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-spacerun:yes'>           </span>This Contract is valid since
approval date till the end of </span></i><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif;mso-bidi-font-style:italic'>scot24</span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <o:p></o:p></span></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>Và</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>s&#7869;</span> <span class=SpellE>&#273;&#432;&#7907;c</span> <span
class=SpellE>gia</span> <span class=SpellE>h&#7841;n</span> <span class=SpellE>n&#7871;u</span>
<span class=SpellE>hai</span> <span class=SpellE>bên</span> <span class=SpellE>&#273;&#7891;ng</span>
<span class=SpellE>th&#7889;ng</span> <span class=SpellE>nh&#7845;t</span> <span
class=SpellE>h&#7907;p</span> <span class=SpellE>tác</span> <span class=SpellE>cho</span>
<span class=SpellE>nh&#7919;ng</span> <span class=SpellE>&#273;&#417;n</span> <span
class=SpellE>hàng</span> <span class=SpellE>ti&#7871;p</span> <span
class=SpellE>theo.</span> <span class=SpellE>H&#7907;p</span> <span
class=SpellE>&#273;&#7891;ng</span> <span class=SpellE>này</span> <span
class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>l&#7853;p</span> <span
class=SpellE>thành</span> 04 (<span class=SpellE>b&#7889;n</span>) <span
class=SpellE>b&#7843;n</span>, <span class=SpellE>m&#7895;i</span> <span
class=SpellE>bên</span> <span class=SpellE>gi&#7919;</span> 02 (<span
class=SpellE>hai</span>) <span class=SpellE>b&#7843;n</span> <span
class=SpellE>có</span> <span class=SpellE>giá</span> <span class=SpellE>tr&#7883;</span>
<span class=SpellE>pháp</span> <span class=SpellE>lý</span> <span class=SpellE>nh&#432;</span>
<span class=SpellE>nhau</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>Contract will be
extended if the two parties unanimously cooperate for the next orders. This
Contract is made into four (04) copies of the same legal validity, each Party
retains 02 (two) copies. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:0in;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></p>

<p class=MsoListParagraph style='margin-right:0in;mso-add-space:auto;
text-indent:-.5in;line-height:115%;mso-list:l6 level1 lfo2'><![if !supportLists]><span
style='mso-list:Ignore'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span><![endif]><span class=SpellE><span style='font-size:12.0pt;
line-height:115%;font-family:Times New Roman,serif'>N&#7871;u</span></span><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'> <span
class=SpellE>có</span> <span class=SpellE>b&#7845;t</span> <span class=SpellE>c&#7913;</span>
<span class=SpellE>tranh</span> <span class=SpellE>ch&#7845;p</span> <span
class=SpellE>nào</span> <span class=SpellE>mà</span> <span class=SpellE>hai</span>
<span class=SpellE>bên</span> <span class=SpellE>không</span> <span
class=SpellE>có</span> <span class=SpellE>th&#7875;</span> <span class=SpellE>&#273;&#432;a</span>
<span class=SpellE>ra</span> <span class=SpellE>h&#432;&#7899;ng</span> <span
class=SpellE>gi&#7843;i</span> <span class=SpellE>quy&#7871;t</span> <span
class=SpellE>cu&#7889;i</span> <span class=SpellE>cùng</span>, <span
class=SpellE>H&#7907;p</span> <span class=SpellE>&#273;&#7891;ng</span> <span
class=SpellE>&#273;&#432;&#7907;c</span> <span class=SpellE>&#273;&#432;a</span>
<span class=SpellE>tòa</span> <span class=SpellE>án</span> <span class=SpellE>c&#7845;p</span>
<span class=SpellE>cao</span> <span class=SpellE>t&#7841;i</span> <span
class=SpellE>thành</span> <span class=SpellE>ph&#7889;</span> <span
class=SpellE>bên</span> B <span class=SpellE>và</span> <span class=SpellE>d&#7921;a</span>
<span class=SpellE>vào</span> <span class=SpellE>ti&#7871;ng</span> <span
class=SpellE>Vi&#7879;t</span>. <o:p></o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:0in;margin-bottom:.25pt;
margin-left:.5in;text-indent:0in;line-height:115%'><i><span style='font-size:
12.0pt;line-height:115%;font-family:Times New Roman,serif'>If there is any
dispute that <span class=GramE>the both</span> parties cannot give a final
solution, the contract is taken to a superior court in the city of Party B and
based on Vietnamese copies. <o:p></o:p></span></i></p>

<p class=MsoNormal style='margin-left:0in;text-indent:0in;line-height:115%'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-left:0in;text-indent:0in;line-height:115%'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-left:0in;text-indent:0in;line-height:115%'><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></p>

<p class=MsoNormal style='margin-top:0in;margin-right:-22.5pt;margin-bottom:
.25pt;margin-left:.5in;text-indent:0in;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>

<table class=MsoTableGrid border=0 cellspacing=0 cellpadding=0 width=669
 style='width:501.55pt;border-collapse:collapse;border:none;mso-yfti-tbllook:
 1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:none;mso-border-insidev:
 none'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>
  <td width=291 valign=top style='width:218.05pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><span class=SpellE><b><span style='font-size:12.0pt;
  line-height:115%;font-family:Times New Roman,serif'>&#272;&#7841;i</span></b></span><b><span
  style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>
  <span class=SpellE>Di&#7879;n</span> <span class=SpellE>Bên</span> A<o:p></o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><i><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'>Party A’s representative</span></i></b><b><span
  style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p></o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><span style='font-size:12.0pt;line-height:115%;font-family:
  Times New Roman,serif;mso-bidi-font-weight:bold'>scot10<o:p></o:p></span></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><span style='font-size:12.0pt;line-height:115%;font-family:
  Times New Roman,serif;mso-fareast-font-family:Times New Roman;color:windowtext'>scot11</span><span
  style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>/
  <span style='mso-bidi-font-style:italic'>scot25<b><i><o:p></o:p></i></b></span></span></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><i><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>
  </td>
  <td width=113 valign=top style='width:85.05pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:23.45pt;text-align:center;text-indent:0in;
  line-height:115%'><b><i><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>
  </td>
  <td width=265 valign=top style='width:198.45pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><span class=SpellE><b><span style='font-size:12.0pt;
  line-height:115%;font-family:Times New Roman,serif'>&#272;&#7841;i</span></b></span><b><span
  style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'>
  <span class=SpellE>Di&#7879;n</span> <span class=SpellE>Bên</span> B<o:p></o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><i><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'>Party B’s representative</span></i></b><b><span
  style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p></o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><span style='font-size:12.0pt;line-height:115%;font-family:
  Times New Roman,serif;color:black;mso-themecolor:text1;mso-bidi-font-weight:
  bold;mso-bidi-font-style:italic'>scot20<o:p></o:p></span></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><span style='font-size:12.0pt;line-height:115%;font-family:
  Times New Roman,serif;mso-fareast-font-family:Times New Roman;color:windowtext'>scot19</span><span
  style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
  color:black;mso-themecolor:text1;mso-bidi-font-style:italic'>/ </span><span
  style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif;
  mso-bidi-font-style:italic'>scot26<i><o:p></o:p></i></span></p>
  </td>
 </tr>
</table>

<p class=MsoNormal style='margin-top:0in;margin-right:-22.5pt;margin-bottom:
.25pt;margin-left:0in;text-indent:0in;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>

<table class=MsoTableGrid border=0 cellspacing=0 cellpadding=0 width=654
 style='width:490.5pt;margin-left:26.75pt;border-collapse:collapse;border:none;
 mso-yfti-tbllook:1184;mso-padding-alt:0in 5.4pt 0in 5.4pt;mso-border-insideh:
 none;mso-border-insidev:none'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>
  <td width=327 valign=top style='width:245.25pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=left style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:left;text-indent:0in;
  line-height:115%'><b><i><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>
  </td>
  <td width=327 valign=top style='width:245.25pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><span style='mso-spacerun:yes'>       
  </span><i><o:p></o:p></i></span></b></p>
  <p class=MsoNormal align=center style='margin-top:0in;margin-right:-22.5pt;
  margin-bottom:.25pt;margin-left:0in;text-align:center;text-indent:0in;
  line-height:115%'><b><i><span style='font-size:12.0pt;line-height:115%;
  font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>
  </td>
 </tr>
</table>

<p class=MsoNormal align=center style='margin-top:0in;margin-right:0in;
margin-bottom:8.0pt;margin-left:0in;text-align:center;text-indent:0in;
line-height:107%'><b><i><span style='font-size:12.0pt;line-height:107%;
font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:-22.5pt;margin-bottom:
.25pt;margin-left:0in;text-indent:.5in;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>

<p class=MsoNormal style='margin-top:0in;margin-right:-22.5pt;margin-bottom:
.25pt;margin-left:0in;text-indent:.5in;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><o:p>&nbsp;</o:p></span></i></b></p>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 width=15
 style='width:11.1pt;margin-left:.5pt;border-collapse:collapse;mso-yfti-tbllook:
 1184;mso-padding-alt:0in 5.4pt 0in 5.4pt'>
 <tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes;
  height:15.75pt'>
  <td width=15 nowrap valign=bottom style='width:11.1pt;padding:0in 5.4pt 0in 5.4pt;
  height:15.75pt'></td>
 </tr>
</table>

<p class=MsoNormal style='margin-top:0in;margin-right:-22.5pt;margin-bottom:
.25pt;margin-left:0in;text-indent:.5in;line-height:115%'><b><i><span
style='font-size:12.0pt;line-height:115%;font-family:Times New Roman,serif'><span
style='mso-tab-count:11'>                                                                                                                                    </span><span
style='mso-spacerun:yes'>   </span><o:p></o:p></span></i></b></p>

</div>

</body>
";

            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "test",s);
            xrRichText1.Html = s;
        }
        private void PrintingSystem_AfterMarginsChange(object sender, DevExpress.XtraPrinting.MarginsChangeEventArgs e)
        {
            Convert.ToInt32(Math.Round(e.Value));
            switch (e.Side)
            {
                case DevExpress.XtraPrinting.MarginSide.Left:
                    Margins = new System.Drawing.Printing.Margins((int)e.Value, (int)Margins.Right, (int)Margins.Top, (int)Margins.Bottom);
                    CreateDocument();
                    break;
                case DevExpress.XtraPrinting.MarginSide.Right:
                    Margins = new System.Drawing.Printing.Margins((int)Margins.Left, (int)e.Value, (int)Margins.Top, (int)Margins.Bottom);
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

        private void XtraReport1_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
