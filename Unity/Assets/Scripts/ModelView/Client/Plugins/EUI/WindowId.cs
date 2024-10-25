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
        //----------------------LoadingPackage---------------------
        LoadingLayer = 9,
        //----------------------LoginLayerPackage---------------------
        LoginLayer = 10,
        //----------------------MainLayerPackage---------------------
        MainLayer = 11,
        AddHeroItemCell = 12,
        JoyStickLayer = 13,
        HPProgressItemCell = 14,
        //----------------------RootPackage---------------------
        RootLayer = 15,
        EmptyLayer = 16,
        SpalshLayer = 17,
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
            for (int j = 7; j < 9; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "HeroCardBagPackage");
            }
            for (int j = 9; j < 10; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoadingPackage");
            }
            for (int j = 10; j < 11; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoginLayerPackage");
            }
            for (int j = 11; j < 15; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "MainLayerPackage");
            }
            for (int j = 15; j < 18; j++)
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