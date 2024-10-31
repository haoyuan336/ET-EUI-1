/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    public enum WindowID
    {
        WindowID_Invaild = 0,
        //----------------------ChooseServerPackage---------------------
        ChooseServerLayer = 1,
        ServerItemCell = 2,
        LoginServerLayer = 3,
        //----------------------FormationPackage---------------------
        FormationLayer = 4,
        FormationCardItemCell = 5,
        //----------------------GameUIPackage---------------------
        GameUILayer = 6,
        //----------------------HeroCardBagPackage---------------------
        HeroCardBagLayer = 7,
        HeroCardItemCell = 8,
        HeroInfoLayer = 9,
        HeroWordBarItemCell = 10,
        //----------------------LoadingPackage---------------------
        LoadingLayer = 11,
        //----------------------LoginLayerPackage---------------------
        LoginLayer = 12,
        //----------------------MainLayerPackage---------------------
        MainLayer = 13,
        AddHeroItemCell = 14,
        JoyStickLayer = 15,
        HPProgressItemCell = 16,
        FightTextLayer = 17,
        DamageTextItemCell = 18,
        GameLoseLayer = 19,
        MoveingLayer = 20,
        HeadItemBarLayer = 21,
        ItemBarItemCell = 22,
        AddExpTextItemCell = 23,
        AddMeatTextItemCell = 24,
        //----------------------RootPackage---------------------
        RootLayer = 25,
        EmptyLayer = 26,
        SpalshLayer = 27,
    }
    public class ComponentPackageMap
    {
        private static ComponentPackageMap Instance = null;
        private Dictionary<WindowID, string> Maps = new Dictionary<WindowID, string>();
        private static ComponentPackageMap GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ComponentPackageMap();
                Instance.InitData();
            }
            return Instance;
        }
        public static string Get(WindowID windowID)
        {
            return GetInstance().Maps[windowID];
        }
        private void InitData()
        {
            for (int j = 1; j < 4; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "ChooseServerPackage");
            }
            for (int j = 4; j < 6; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "FormationPackage");
            }
            for (int j = 6; j < 7; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "GameUIPackage");
            }
            for (int j = 7; j < 11; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "HeroCardBagPackage");
            }
            for (int j = 11; j < 12; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoadingPackage");
            }
            for (int j = 12; j < 13; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoginLayerPackage");
            }
            for (int j = 13; j < 25; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "MainLayerPackage");
            }
            for (int j = 25; j < 28; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "RootPackage");
            }
        }
        public static string[] GetPackNames()
        {
            return GetInstance().Maps.Values.ToArray();
        }
    }
}