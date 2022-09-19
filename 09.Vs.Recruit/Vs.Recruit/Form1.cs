using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs.HRM;
using Vs.Recruit.UAC;

namespace Vs.Recruit
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            //TinhSoTuanCuaTHang();
            Commons.Modules.iUngVien = -1;
            ucPhongVan uac = new ucPhongVan();
            this.Controls.Add(uac);
            uac.Dock = DockStyle.Fill;
            //LoadAA();
        }
        private void LoadAA()
        {
            string SSSSSS = @"<body lang=EN-US style='word-wrap:break-word'>

<div class=WordSection1>

<table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0
 style='border-collapse:collapse'>
 <tr>
  <td width=327 valign=top style='width:245.5pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-bottom:0in;text-align:center;
  line-height:115%'><span lang=VI style='font-family:Times New Roman,serif'>CÔNG
  TY C</span><span style='font-family:Times New Roman,serif'>&#7892; </span><span
  lang=VI style='font-family:Times New Roman,serif'>P</span><span
  style='font-family:Times New Roman,serif'>H&#7846;N</span><span lang=VI
  style='font-family:Times New Roman,serif'> MAY DUY MINH</span></p>
  </td>
  <td width=327 valign=top style='width:245.55pt;padding:0in 5.4pt 0in 5.4pt'>
  <p class=MsoNormal align=center style='margin-bottom:0in;text-align:center;
  line-height:115%'><span lang=VI style='font-family:Times New Roman,serif'>C&#7897;ng
  hòa xã h&#7897;i ch&#7911; ngh&#297;a Vi&#7879;t Nam</span></p>
  <p class=MsoNormal align=center style='margin-bottom:0in;text-align:center;
  line-height:115%'><b><span lang=VI style='font-family:Times New Roman,serif'>&#272;&#7897;c
  l&#7853;p – T&#7921; do – H&#7841;nh phúc</span></b></p>
  <p class=MsoNormal align=center style='margin-bottom:0in;text-align:center;
  line-height:115%'><span lang=VI style='font-family:Times New Roman,serif'>===========</span></p>
  </td>
 </tr>
</table>

<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center;
line-height:115%'><b><span style='font-size:14.0pt;line-height:115%;font-family:
Times New Roman,serif'>THÔNG BÁO TUY&#7874;N D&#7908;NG LAO &#272;&#7896;NG
N&#258;M 2022</span></b></p>

<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center;
line-height:115%'><b><span style='font-size:1.0pt;line-height:115%;font-family:
Times New Roman,serif'>&nbsp;</span></b></p>

<p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
margin-left:0in;text-align:justify;line-height:115%'><span style='font-family:
Times New Roman,serif'>Công ty C&#7893; ph&#7847;n may Duy Minh – Thành viên T&#7853;p
&#273;oàn Thiên Nam Th&#7883;nh V&#432;&#7907;ng – chuyên s&#7843;n xu&#7845;t
áo s&#417;mi nam và qu&#7847;n âu cao c&#7845;p, có &#273;&#7883;a ch&#7881; t&#7841;i</span><span
style='font-family:Times New Roman,serif'> </span><span style='font-family:
Times New Roman,serif'>&#273;&#432;&#7901;ng &#272;T488, thôn Thái Lãng, xã
Tr&#7921;c N&#7897;i, huy&#7879;n Tr&#7921;c Ninh (cách phà &#272;&#7841;i N&#7897;i
và UBND xã Tr&#7921;c N&#7897;i 500 mét).</span></p>

<p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
style='font-family:Times New Roman,serif'>C</span><span style='font-family:
Times New Roman,serif'>&#7847;n tuy&#7875;n 300 công nhân may có tay ngh&#7873;</span><span
lang=VI style='font-family:Times New Roman,serif'> và </span><span
style='font-family:Times New Roman,serif'>c&#7843; lao &#273;&#7897;ng</span><span
lang=VI style='font-family:Times New Roman,serif'> nam, n&#7919;</span><span
style='font-family:Times New Roman,serif'> ch&#432;a bi&#7871;t ngh&#7873;
may &#273;&#7875; &#273;ào t&#7841;o. Trong th&#7901;i gian h&#7885;c ngh&#7873;,
ng&#432;&#7901;i lao &#273;&#7897;ng v&#7851;n &#273;&#432;&#7907;c h&#432;&#7903;ng
l&#432;&#417;ng và các ch&#7871; &#273;&#7897; phúc l&#7907;i.</span></p>

<table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 width=673
 style='width:505.1pt;margin-left:9.9pt;border-collapse:collapse;border:none'>
 <tr style='height:15.4pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:15.4pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><b><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>STT</span></b></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border:solid windowtext 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt;height:15.4pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><b><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>V&#7883;
  trí tuy&#7875;n d&#7909;ng</span></b></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border:solid windowtext 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt;height:15.4pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><b><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>S&#7889;
  l&#432;&#7907;ng</span></b></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border:solid windowtext 1.0pt;
  border-left:none;padding:0in 5.4pt 0in 5.4pt;height:15.4pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><b><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Yêu
  c&#7847;u</span></b></p>
  </td>
 </tr>
 <tr style='height:9.7pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>1</span></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Công
  nhân may</span></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>300</span></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>K&#7875;
  c&#7843; ng&#432;&#7901;i ch&#432;a có tay ngh&#7873; vào &#273;ào t&#7841;o</span></p>
  </td>
 </tr>
 <tr style='height:9.7pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>2</span></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Công
  nhân may m&#7851;u</span></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>10</span></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Có
  th&#7875; may hoàn thi&#7879;n s&#7843;n ph&#7849;m áo ho&#7863;c qu&#7847;n</span></p>
  </td>
 </tr>
 <tr style='height:9.9pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:9.9pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>3</span></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.9pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Công
  nhân ki&#7875;m hàng</span></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.9pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>05</span></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.9pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Có
  k/n ki&#7875;m hàng áo ho&#7863;c qu&#7847;n</span></p>
  </td>
 </tr>
 <tr style='height:9.7pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>4</span></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Công
  nhân hoàn thi&#7879;n</span></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>10</span></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Nhanh
  nh&#7865;n, kh&#7887;e m&#7841;nh</span></p>
  </td>
 </tr>
 <tr style='height:9.7pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>5</span></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Công
  nhân c&#7855;t</span></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>02</span></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>S&#7917;
  d&#7909;ng thành th&#7841;o máy c&#7855;t</span></p>
  </td>
 </tr>
 <tr style='height:9.7pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>6</span></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Nhân
  viên k&#7871; ho&#7841;ch khách hàng</span></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>02</span></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.7pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Có
  ít nh&#7845;t 2 n&#259;m k/n Merchandiser</span></p>
  </td>
 </tr>
 <tr style='height:9.9pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:9.9pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>7</span></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.9pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Nhân
  viên xu&#7845;t nh&#7853;p kh&#7849;u</span></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.9pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>01</span></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:9.9pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Có
  ít nh&#7845;t 2 n&#259;m k/n làm XNK</span></p>
  </td>
 </tr>
 <tr style='height:1.0pt'>
  <td width=31 valign=top style='width:23.6pt;border:solid windowtext 1.0pt;
  border-top:none;padding:0in 5.4pt 0in 5.4pt;height:1.0pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>8</span></p>
  </td>
  <td width=239 valign=top style='width:179.5pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:1.0pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Nhân
  viên IT</span></p>
  </td>
  <td width=78 valign=top style='width:58.45pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:1.0pt'>
  <p class=MsoNormal align=center style='margin-top:6.0pt;margin-right:0in;
  margin-bottom:6.0pt;margin-left:0in;text-align:center;line-height:115%'><span
  lang=VI style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>01</span></p>
  </td>
  <td width=325 valign=top style='width:243.55pt;border-top:none;border-left:
  none;border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;
  padding:0in 5.4pt 0in 5.4pt;height:1.0pt'>
  <p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:
  6.0pt;margin-left:0in;text-align:justify;line-height:115%'><span lang=VI
  style='font-size:10.0pt;line-height:115%;font-family:Times New Roman,serif'>Chuyên
  ngành CNTT, hi&#7875;u bi&#7871;t v&#7873; ph&#7847;n m&#7873;m</span></p>
  </td>
 </tr>
</table>

<p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
margin-left:0in;text-align:justify;line-height:115%'><b><u><span
style='font-family:Times New Roman,serif'>Th&#7901;i gian làm vi&#7879;c</span></u></b><b><span
style='font-family:Times New Roman,serif'>:</span></b></p>

<ul style='margin-top:0in' type=disc>
 <li class=MsoNormal style='margin-bottom:0in;text-align:justify;line-height:
     115%'><span lang=VI style='font-family:Times New Roman,serif'>T</span><span
     style='font-family:Times New Roman,serif'>&#7915; 7h30 – 17h36 t&#7915;
     Th&#7913; 2 &#273;&#7871;n Th&#7913; 6</span></li>
 <li class=MsoNormal style='margin-bottom:0in;text-align:justify;line-height:
     115%'><span style='font-family:Times New Roman,serif'>Th&#7913; 7 và Ch&#7911;
     nh&#7853;t là ngày ngh&#7881; hàng tu&#7847;n.</span><span
     style='font-family:Times New Roman,serif'> <span lang=VI>&#272;i làm T</span></span><span
     style='font-family:Times New Roman,serif'>h&#7913; </span><span lang=VI
     style='font-family:Times New Roman,serif'>7 h&#432;&#7903;ng
     l&#432;&#417;ng 200%. Không làm Ch&#7911; nh&#7853;t.</span></li>
</ul>

<p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
margin-left:0in;text-align:justify;line-height:115%'><b><u><span
style='font-family:Times New Roman,serif'>Chính sách &#273;ãi ng&#7897;</span></u></b><b><span
style='font-family:Times New Roman,serif'>:</span></b></p>

<p class=MsoListParagraphCxSpFirst style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>L&#432;&#417;ng s&#7843;n
ph&#7849;m không gi&#7899;i h&#7841;n. Thu nh&#7853;p công nhân may bình quân t&#7915;
6</span><span style='font-family:Times New Roman,serif'> </span><span
style='font-family:Times New Roman,serif'>– </span><span lang=VI
style='font-family:Times New Roman,serif'>12 </span><span style='font-family:
Times New Roman,serif'>tri&#7879;u &#273;&#7891;ng/tháng, lao &#273;&#7897;ng
tay ngh&#7873; t&#7889;t &#273;&#7841;t m&#7913;c thu nh&#7853;p cao h&#417;n.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang=VI style='font-family:Times New Roman,serif'>L&#432;&#417;ng
c&#417; b&#7843;n <b>4.100.000</b> &#273;&#7891;ng</span><span
style='font-family:Times New Roman,serif'>; </span><span lang=VI
style='font-family:Times New Roman,serif'>t&#259;ng</span><span
style='font-family:Times New Roman,serif'> &#273;&#417;n giá<b> 12,5%</b> so
v&#7899;i </span><span lang=VI style='font-family:Times New Roman,serif'>n&#259;m
2021.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>Th&#432;&#7903;ng </span><span
lang=VI style='font-family:Times New Roman,serif'>n&#259;ng su&#7845;t cá
nhân A,B,C t&#7915; <b>400.000</b></span><b><span style='font-family:Times New Roman,serif'>&#273;</span></b><b><span
lang=VI style='font-family:Times New Roman,serif'> – 1.200</span></b><b><span
style='font-family:Times New Roman,serif'>.000</span></b><b><span lang=VI
style='font-family:Times New Roman,serif'>&#273;</span></b><span
style='font-family:Times New Roman,serif'>/ tháng.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang=VI style='font-family:Times New Roman,serif'>Th&#432;&#7903;ng
ngày khuy&#7871;n khích s&#7843;n l&#432;&#7907;ng hàng ra chuy&#7873;n t&#7915;
<b>500.000</b></span><b><span style='font-family:Times New Roman,serif'>&#273;</span></b><b><span
lang=VI style='font-family:Times New Roman,serif'> –</span></b><b><span
lang=VI style='font-family:Times New Roman,serif'> </span></b><b><span
lang=VI style='font-family:Times New Roman,serif'>2 tri&#7879;u &#273;</span></b><b><span
style='font-family:Times New Roman,serif'>&#7891;ng</span></b><span lang=VI
style='font-family:Times New Roman,serif'>/ngày</span><span style='font-family:
Times New Roman,serif'>.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>Th&#432;&#7903;ng chuyên
c&#7847;n       : <b>700.000 &#273;&#7891;ng</b>/ tháng.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>H&#7895; tr&#7907;
x&#259;ng xe              : <b>150.000 &#273;&#7891;ng</b>/ tháng</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>H&#7895; tr&#7907; ti&#7873;n
&#273;ò phà         : <b>200.000 &#273;&#7891;ng</b>/ tháng</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang=VI style='font-family:Times New Roman,serif'>H&#7895; tr&#7907;</span><span
lang=VI style='font-family:Times New Roman,serif'> </span><span
style='font-family:Times New Roman,serif'>&#259;n tr&#432;a t&#7841;i công ty</span><span
style='font-family:Times New Roman,serif'> </span><b><span style='font-family:
Times New Roman,serif'>20.000 &#273;&#7891;ng</span></b><span
style='font-family:Times New Roman,serif'>/su&#7845;t</span><span lang=VI
style='font-family:Times New Roman,serif'>;</span><b><span lang=VI
style='font-family:Times New Roman,serif'> </span></b><span style='font-family:
Times New Roman,serif'>1 l&#7847;n 1 tu&#7847;n<b> </b></span><span lang=VI
style='font-family:Times New Roman,serif'>b</span><span style='font-family:
Times New Roman,serif'>&#7893; sung </span><span lang=VI style='font-family:
Times New Roman,serif'>su&#7845;t &#259;n </span><span style='font-family:
Times New Roman,serif'>dinh d&#432;&#7905;ng <b>30.000 &#273;&#7891;ng</b>/su&#7845;t</span><span
lang=VI style='font-family:Times New Roman,serif'>.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>Th&#432;&#7903;ng T&#7871;t,
th&#432;&#7903;ng tháng l&#432;&#417;ng 13 theo t&#7893;ng thu nh&#7853;p.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>&#272;óng b&#7843;o hi&#7875;m
&#273;&#7847;y &#273;&#7911; theo quy &#273;&#7883;nh, n&#7889;i s&#7893; b&#7843;o
hi&#7875;m cho NL&#272;.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span lang=VI style='font-family:Times New Roman,serif'>NL&#272;
&#273;&#432;&#7907;c n</span><span style='font-family:Times New Roman,serif'>gh&#7881;
14 ngày phép/n&#259;m và các ngày l&#7877; t&#7871;t h&#432;&#7903;ng nguyên
l&#432;&#417;ng.</span><span style='font-family:Times New Roman,serif'> </span><span
style='font-family:Times New Roman,serif'>Lao &#273;&#7897;ng n&#7919; mang
thai và nuôi con nh&#7887; d&#432;&#7899;i 12 tháng </span><span lang=VI
style='font-family:Times New Roman,serif'>tu&#7893;i </span><span
style='font-family:Times New Roman,serif'>&#273;&#432;&#7907;c gi&#7843;m gi&#7901;
làm 60 phút</span><span lang=VI style='font-family:Times New Roman,serif'>/
ngày.</span></p>

<p class=MsoListParagraphCxSpMiddle style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>Môi tr&#432;&#7901;ng
làm vi&#7879;c s&#7841;ch s&#7869;, </span><span lang=VI style='font-family:
Times New Roman,serif'>máy móc hi&#7879;n &#273;&#7841;i, nhà x&#432;&#7903;ng
l&#7855;p </span><span style='font-family:Times New Roman,serif'>&#273;i&#7873;u
hòa nhi&#7879;t &#273;&#7897;.     </span></p>

<p class=MsoListParagraphCxSpLast style='margin-top:6.0pt;margin-right:0in;
margin-bottom:6.0pt;margin-left:.5in;text-align:justify;text-indent:-.25in;
line-height:115%'>-<span style='font:7.0pt Times New Roman'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span><span style='font-family:Times New Roman,serif'>&#272;&#7863;c bi&#7879;t
công ty có <b>xe &#273;&#432;a &#273;ón công nhân</b> hàng ngày; NL&#272; <b>không
ph&#7843;i n&#7897;p gi&#7845;y khám s&#7913;c kh&#7887;e</b> &#273;&#7847;u
vào khi &#273;i làm.                                                                                                                                       </span></p>

<p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
margin-left:0in;text-align:justify;text-indent:.25in;line-height:115%'><span
style='font-family:Times New Roman,serif'>Công ty c&#7893; ph&#7847;n may Duy
Minh tuy&#7875;n d&#7909;ng liên t&#7909;c vào t&#7845;t c&#7843; các ngày
trong tu&#7847;n, k&#7875; c&#7843; th&#7913; 7 và Ch&#7911; nh&#7853;t. </span></p>

<p class=MsoNormal style='margin-top:6.0pt;margin-right:0in;margin-bottom:6.0pt;
margin-left:0in;text-align:justify;text-indent:.25in;line-height:115%'><b><span
style='font-family:Times New Roman,serif'>&#272;T liên h&#7879;: 0228.6556.777
- 081.687.6335 – 0392.528.265</span></b></p>

<p class=MsoNormal style='margin-bottom:0in;text-align:justify;line-height:
115%'><span style='font-family:Times New Roman,serif'>&nbsp;</span></p>

</div>

</body>




";
            richEditControl1.Document.InsertHtmlText(richEditControl1.Document.CaretPosition, SSSSSS);
        }
        private void TinhSoTuanCuaTHang()
        {
            try
            {
                //CultureInfo _culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                //CultureInfo _uiculture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();
                //_culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
                //_uiculture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
                //System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
                //System.Threading.Thread.CurrentThread.CurrentUICulture = _uiculture;

                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Tuan", typeof(Int32));
                dt.Columns.Add("TNgay", typeof(DateTime));
                dt.Columns.Add("DNgay", typeof(DateTime));

                DateTime TN, DN;
                //lấy ngày bắc đầu và ngày kết thúc của tháng
                TN = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
                DN = TN.AddMonths(1).AddDays(-1);
                //kiểm tra ngày bắc đầu có phải thứ 2 không

                for (int i = 1; i <= 4; i++)
                {
                    if (i == 1)
                    {
                        if (TN.DayOfWeek == DayOfWeek.Monday)
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7));
                            TN = TN.AddDays(8);
                            continue;
                        }
                        else
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7 + (7 - (int)TN.DayOfWeek)));
                            TN = TN.AddDays(8 + (7 - (int)TN.DayOfWeek));
                            continue;
                        }
                    }
                    if (i == 2 || i == 3)
                    {
                        dt.Rows.Add(i, TN, TN.AddDays(6));
                        TN = TN.AddDays(7);
                        continue;
                    }
                    if (i == 4)
                    {
                        dt.Rows.Add(i, TN, DN);
                        break;
                    }
                }

                DataTable dtap = dt;

            }
            catch
            {
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string s = richEditControl1.HtmlText;
        }
    }
}
