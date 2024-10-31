/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using System.Collections.Generic;
using System.Reflection;
namespace ET
{
    public class ResourcesUrlMap
    {
        private Dictionary<string, string> Maps = new Dictionary<string, string>();
        private static ResourcesUrlMap Instance = null;
        private static ResourcesUrlMap GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ResourcesUrlMap();
                Instance.InitData();
            }
            return Instance;
        }
        public static string Get(string itemName)
        {
            return GetInstance().Maps[itemName];
        }
        private void InitData()
        {
            Type type = typeof (ResourcesUrlMap);
            FieldInfo[] fieldInfos = type.GetFields();
            foreach (var propertyInfo in fieldInfos)
            {
                string name = propertyInfo.Name;
                string value = Convert.ToString(propertyInfo.GetValue(this));
                this.Maps.Add(name, value);
            }
        }
        //---------------ChooseServerPackage---------------
        public const string ChooseServerLayer = "ui://8qgfwpa8jh1l0";
        public const string ServerItemCell = "ui://8qgfwpa8jh1l1";
        public const string LoginServerLayer = "ui://8qgfwpa8jh1l2";
        //---------------FormationPackage---------------
        public const string FormationLayer = "ui://1rgdxzmjiygc0";
        public const string FormationCardItemCell = "ui://1rgdxzmjiygc2";
        //---------------GameUIPackage---------------
        public const string GameUILayer = "ui://24bwjhhnk7it0";
        //---------------HeroCardBagPackage---------------
        public const string HeroCardBagLayer = "ui://1o0rv0gziygc0";
        public const string HeroCardItemCell = "ui://1o0rv0gziygc1";
        public const string HeroInfoLayer = "ui://1o0rv0gz4yhj2";
        public const string HeroWordBarItemCell = "ui://1o0rv0gzjdez3s";
        //---------------LoadingPackage---------------
        public const string LoadingLayer = "ui://8l9768khjh1l0";
        //---------------LoginLayerPackage---------------
        public const string BgButton = "ui://9sdsckomk7it3";
        public const string LoginLayer = "ui://9sdsckomrsyn0";
        public const string LoginButton = "ui://9sdsckomigda4";
        //---------------MainLayerPackage---------------
        public const string MainLayer = "ui://7sxmdg4qckxh0";
        public const string AddHeroItemCell = "ui://7sxmdg4qckxh2";
        public const string JoyStickLayer = "ui://7sxmdg4qiygc3";
        public const string HPProgress = "ui://7sxmdg4qp1ff4";
        public const string HPProgressItemCell = "ui://7sxmdg4q4yhj6";
        public const string FightTextLayer = "ui://7sxmdg4qsdia7";
        public const string DamageTextItemCell = "ui://7sxmdg4qsdia8";
        public const string GameLoseLayer = "ui://7sxmdg4qsc409";
        public const string MoveingLayer = "ui://7sxmdg4qsc40a";
        public const string HeadItemBarLayer = "ui://7sxmdg4qjdezb";
        public const string ItemBarItemCell = "ui://7sxmdg4qjdezc";
        public const string AddExpTextItemCell = "ui://7sxmdg4qjdezd";
        public const string ItemBarProgress = "ui://7sxmdg4qjdezg";
        public const string AddMeatTextItemCell = "ui://7sxmdg4qjdezh";
        //---------------RootPackage---------------
        public const string RootLayer = "ui://9nomxs01i2d80";
        public const string EmptyLayer = "ui://9nomxs01i2d81";
        public const string Add2 = "ui://9nomxs01k7it2";
        public const string lvbtnlv = "ui://9nomxs01k7it3";
        public const string lvbtnlight = "ui://9nomxs01k7it4";
        public const string listbiaoti = "ui://9nomxs01k7it5";
        public const string linefenge = "ui://9nomxs01k7it6";
        public const string kuang_skillck = "ui://9nomxs01k7it7";
        public const string jindufenge = "ui://9nomxs01k7it8";
        public const string jiantou1 = "ui://9nomxs01k7it9";
        public const string jiantou = "ui://9nomxs01k7ita";
        public const string jdtbg = "ui://9nomxs01k7itb";
        public const string iconxuanze = "ui://9nomxs01k7itc";
        public const string icon_add = "ui://9nomxs01k7itd";
        public const string heroStar3 = "ui://9nomxs01k7ite";
        public const string namebg_input = "ui://9nomxs01k7itf";
        public const string heroStar2 = "ui://9nomxs01k7itg";
        public const string gwsp_bg = "ui://9nomxs01k7ith";
        public const string guankatxt = "ui://9nomxs01k7iti";
        public const string fengexian = "ui://9nomxs01k7itj";
        public const string equipunlock = "ui://9nomxs01k7itk";
        public const string duigou = "ui://9nomxs01k7itl";
        public const string czjdt = "ui://9nomxs01k7itm";
        public const string czjdbg = "ui://9nomxs01k7itn";
        public const string ComboBoxBg_zhuoyue = "ui://9nomxs01k7ito";
        public const string ComboBoxBg_xiyou = "ui://9nomxs01k7itp";
        public const string ComboBoxBg_tangjin = "ui://9nomxs01k7itq";
        public const string ComboBoxBg_jingliang = "ui://9nomxs01k7itr";
        public const string ComboBoxBg_chuanqi = "ui://9nomxs01k7its";
        public const string heroStar1 = "ui://9nomxs01k7itt";
        public const string ComboBoxBg_buxiu = "ui://9nomxs01k7itu";
        public const string picds = "ui://9nomxs01k7itv";
        public const string qiehuanarrow_left = "ui://9nomxs01k7itw";
        public const string zt_01 = "ui://9nomxs01k7itx";
        public const string zkjt = "ui://9nomxs01k7ity";
        public const string zdsltxt = "ui://9nomxs01k7itz";
        public const string yxjdt = "ui://9nomxs01k7it10";
        public const string yishouqing = "ui://9nomxs01k7it11";
        public const string yilingqu_lv = "ui://9nomxs01k7it12";
        public const string yilingqu = "ui://9nomxs01k7it13";
        public const string xuanzhongkuangsmall = "ui://9nomxs01k7it14";
        public const string xuanze = "ui://9nomxs01k7it15";
        public const string xinxifg02 = "ui://9nomxs01k7it16";
        public const string xinxifg01 = "ui://9nomxs01k7it17";
        public const string xinxibgpic = "ui://9nomxs01k7it18";
        public const string qiehuanarrow = "ui://9nomxs01k7it19";
        public const string wytips = "ui://9nomxs01k7it1a";
        public const string unlockzb = "ui://9nomxs01k7it1b";
        public const string txtzhuangshi = "ui://9nomxs01k7it1c";
        public const string tujian_kabei = "ui://9nomxs01k7it1d";
        public const string touxiangWK = "ui://9nomxs01k7it1e";
        public const string touxiangnl_1 = "ui://9nomxs01k7it1f";
        public const string tips_xinxi = "ui://9nomxs01k7it1g";
        public const string tips_txt_bg = "ui://9nomxs01k7it1h";
        public const string tips_ew = "ui://9nomxs01k7it1i";
        public const string tgdh = "ui://9nomxs01k7it1j";
        public const string sjarrow02 = "ui://9nomxs01k7it1k";
        public const string silllock = "ui://9nomxs01k7it1l";
        public const string shenqingshuru = "ui://9nomxs01k7it1m";
        public const string vipgmbg = "ui://9nomxs01k7it1n";
        public const string zt_02 = "ui://9nomxs01k7it1o";
        public const string btnymnfx02 = "ui://9nomxs01k7it1p";
        public const string btnfenlan02lh2 = "ui://9nomxs01k7it1q";
        public const string bgshuxingnew = "ui://9nomxs01k7it1r";
        public const string bgpicgx1 = "ui://9nomxs01k7it1s";
        public const string bgpic0001 = "ui://9nomxs01k7it1t";
        public const string bgnametxt = "ui://9nomxs01k7it1u";
        public const string bgmubiaokuang = "ui://9nomxs01k7it1v";
        public const string bglist01 = "ui://9nomxs01k7it1w";
        public const string bgjsxx = "ui://9nomxs01k7it1x";
        public const string bgitem = "ui://9nomxs01k7it1y";
        public const string bggxhsd_1 = "ui://9nomxs01k7it1z";
        public const string bggxhsd = "ui://9nomxs01k7it20";
        public const string bgfwq = "ui://9nomxs01k7it21";
        public const string bg_zhanli = "ui://9nomxs01k7it22";
        public const string bgskill = "ui://9nomxs01k7it23";
        public const string bg_xuanze = "ui://9nomxs01k7it24";
        public const string bg_name = "ui://9nomxs01k7it25";
        public const string bg_jdtdb = "ui://9nomxs01k7it26";
        public const string bg_itemshengxing = "ui://9nomxs01k7it27";
        public const string bg_itemd = "ui://9nomxs01k7it28";
        public const string bg_heroname_input = "ui://9nomxs01k7it29";
        public const string bg_ggtxt001 = "ui://9nomxs01k7it2a";
        public const string bg_ggdibanlan = "ui://9nomxs01k7it2b";
        public const string bg_ggbiaoti = "ui://9nomxs01k7it2c";
        public const string Arrowxia = "ui://9nomxs01k7it2d";
        public const string Arrowshang = "ui://9nomxs01k7it2e";
        public const string arrow_zhankai = "ui://9nomxs01k7it2f";
        public const string arrow_huadong = "ui://9nomxs01k7it2g";
        public const string bg_txt = "ui://9nomxs01k7it2h";
        public const string btnymnfx01 = "ui://9nomxs01k7it2i";
        public const string bgsxlb = "ui://9nomxs01k7it2j";
        public const string bgtanban = "ui://9nomxs01k7it2k";
        public const string btnfenlan01lh2 = "ui://9nomxs01k7it2l";
        public const string btn_tiaozhan = "ui://9nomxs01k7it2m";
        public const string btn_tiaoguo = "ui://9nomxs01k7it2n";
        public const string btn_sub_light = "ui://9nomxs01k7it2o";
        public const string btn_Sub = "ui://9nomxs01k7it2p";
        public const string btn_sousuo = "ui://9nomxs01k7it2q";
        public const string btn_shuaxin = "ui://9nomxs01k7it2r";
        public const string btn_quxiao = "ui://9nomxs01k7it2s";
        public const string btn_jlfl02 = "ui://9nomxs01k7it2t";
        public const string btn_jlfl01 = "ui://9nomxs01k7it2u";
        public const string btn_huoyuezhi = "ui://9nomxs01k7it2v";
        public const string btn_gg03 = "ui://9nomxs01k7it2w";
        public const string bgsxpic = "ui://9nomxs01k7it2x";
        public const string btn_gg003 = "ui://9nomxs01k7it2y";
        public const string btn_gg002 = "ui://9nomxs01k7it2z";
        public const string btn_gg01hong = "ui://9nomxs01k7it30";
        public const string btn_gg01 = "ui://9nomxs01k7it31";
        public const string btn_fenxuanGem02 = "ui://9nomxs01k7it32";
        public const string btn_fenxuanGem01 = "ui://9nomxs01k7it33";
        public const string btn_bianji = "ui://9nomxs01k7it34";
        public const string btn_back = "ui://9nomxs01k7it35";
        public const string btn_add_light = "ui://9nomxs01k7it36";
        public const string btn_add = "ui://9nomxs01k7it37";
        public const string bossbloodline = "ui://9nomxs01k7it38";
        public const string bossbgline = "ui://9nomxs01k7it39";
        public const string bgvipgm = "ui://9nomxs01k7it3a";
        public const string btn_gg02 = "ui://9nomxs01k7it3b";
        public const string zt_03 = "ui://9nomxs01k7it3c";
        public const string SpalshLayer = "ui://9nomxs01jh1l0";
        public const string NButton = "ui://9nomxs01jh1l3d";
        public const string AButton = "ui://9nomxs01jh1l3e";
        public const string CloseButton = "ui://9nomxs01iygc3f";
        public const string WindowBox = "ui://9nomxs01iygc3h";
        public const string BButton = "ui://9nomxs01sc403i";
    }
}