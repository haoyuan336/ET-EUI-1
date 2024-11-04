/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    public enum WindowID
    {
        WindowID_Invaild = 0,
        //----------------------CallLayerPackage---------------------
        CallLayer = 1,
        //----------------------ChooseServerPackage---------------------
        ChooseServerLayer = 2,
        ServerItemCell = 3,
        LoginServerLayer = 4,
        //----------------------FightTextLayerPackage---------------------
        FightTextLayer = 5,
        AddExpTextItemCell = 6,
        AddMeatTextItemCell = 7,
        DamageTextItemCell = 8,
        HPProgressItemCell = 9,
        InteractivePointItemCell = 10,
        //----------------------FormationPackage---------------------
        FormationLayer = 11,
        FormationCardItemCell = 12,
        //----------------------GameLoseLayerPackage---------------------
        GameLoseLayer = 13,
        //----------------------GameUIPackage---------------------
        GameUILayer = 14,
        //----------------------HeroCardBagPackage---------------------
        HeroCardBagLayer = 15,
        HeroCardItemCell = 16,
        HeroInfoLayer = 17,
        HeroWordBarItemCell = 18,
        //----------------------LoadingPackage---------------------
        LoadingLayer = 19,
        //----------------------LoginLayerPackage---------------------
        LoginLayer = 20,
        //----------------------MainLayerPackage---------------------
        MainLayer = 21,
        JoyStickLayer = 22,
        HeadItemBarLayer = 23,
        ItemBarItemCell = 24,
        AddHeroItemCell = 25,
        //----------------------MovingLayerPackage---------------------
        MoveingLayer = 26,
        //----------------------RootPackage---------------------
        RootLayer = 27,
        EmptyLayer = 28,
        SpalshLayer = 29,
        //----------------------TeleportPackage---------------------
        TeleportLayer = 30,
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
            for (int j = 1; j < 2; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "CallLayerPackage");
            }
            for (int j = 2; j < 5; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "ChooseServerPackage");
            }
            for (int j = 5; j < 11; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "FightTextLayerPackage");
            }
            for (int j = 11; j < 13; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "FormationPackage");
            }
            for (int j = 13; j < 14; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "GameLoseLayerPackage");
            }
            for (int j = 14; j < 15; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "GameUIPackage");
            }
            for (int j = 15; j < 19; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "HeroCardBagPackage");
            }
            for (int j = 19; j < 20; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoadingPackage");
            }
            for (int j = 20; j < 21; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoginLayerPackage");
            }
            for (int j = 21; j < 26; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "MainLayerPackage");
            }
            for (int j = 26; j < 27; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "MovingLayerPackage");
            }
            for (int j = 27; j < 30; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "RootPackage");
            }
            for (int j = 30; j < 31; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "TeleportPackage");
            }
        }
        public static string[] GetPackNames()
        {
            return GetInstance().Maps.Values.ToArray();
        }
    }
}