using UnityEngine;
using TMPro;

public class AboutTextManager : MonoBehaviour
{
    public TMP_Text aboutTextField;

    void Start()
    {
        if (aboutTextField != null)
        {
            aboutTextField.text =
                "<b>[ksy ds ckjs esa</b>\n" +
                ",vkbZ Li‚Vj ,d 'kSf{kd xse gS tks f[kykfM+;ksa dks jkst+ejkZ dh ft+anxh esa —f=e cqf)eÙkk ds mi;ksx dks igpkuus esa enn djrk gSAn\n\n" +

                "<b>mís';</b>\n" +
                "- lkekU; ,sIl vkSj midj.kksa esa ,vkbZ dh mifLFkfr dks le>saA\n" +
                "- ,vkbZ&lapkfyr vkSj xSj&,vkbZ rduhdksa ds chp varj djuk lh[ksaA\n" +
                "- nSfud thou esa çkS|ksfxdh ds çfr ftKklk dks çksRlkfgr djsaA\n\n" +

                "<b>[ksyus dk rjhdk</b>\n" +
                "- ,d –'; fn[kkbZ nsxk ftlesa dbZ Nfo;k¡ ¼,sIl] xStsV~l] midj.k vkfn½ gksaxhA\n" +
                "- ,d ç'u ;k ladsr fn;k tk,xkA\n" +
                "- mu lHkh Nfo;ksa dk p;u djsa ftUgsa vki lksprs gSa fd os ,vkbZ }kjk lapkfyr gSaA\n" +
                "- vki ftruh pkgsa mruh Nfo;k¡ pqu ldrs gSa — ;fn vki ekurs gSa fd os lHkh lgh gSa rks lHkh pqu ldrs gSaA\n" +
                "- vkids ikl p;u ds fy, lhfer le; gSA\n" +
                "- tc le; lekIr gks tk,xk] vkids mÙkj Lopkfyr :i ls lcfeV gks tk,axsA\n\n" +

                "<b>lcfe'ku ds ckn</b>\n" +
                "- vki ns[ksaxs fd vkius fdu Nfo;ksa dk p;u fd;k vkSj os fdruh lgh FkhaA\n" +
                "- ifj.kkeksa ls lh[ksa vkSj ,vkbZ dh viuh le> esa lqèkkj djsaA\n\n" +

                "<b>vad</b>\n" +
                "- lgh mÙkj: $10 vad\n" +
                "- xyr mÙkj: &4 vad\n\n" +

                "xse dk vkuan ysa] lksp&fopkj ls fu.kZ; ysa] vkSj tkusa fd ,vkbZ vkids vklikl dh nqfu;k dks dSls vkdkj ns jgk gSA";
        }
    }
}
