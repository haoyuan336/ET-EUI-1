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
        //----------------------GameUIPackage---------------------
        GameUILayer = 4,
        //----------------------LoadingPackage---------------------
        LoadingLayer = 5,
        //----------------------LoginLayerPackage---------------------
        LoginLayer = 6,
        //----------------------MainLayerPackage---------------------
        MainLayer = 7,
        AddHeroItemCell = 8,
        //----------------------RootPackage---------------------
        RootLayer = 9,
        EmptyLayer = 10,
        SpalshLayer = 11,
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
            for (int j = 4; j < 5; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "GameUIPackage");
            }
            for (int j = 5; j < 6; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoadingPackage");
            }
            for (int j = 6; j < 7; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoginLayerPackage");
            }
            for (int j = 7; j < 9; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "MainLayerPackage");
            }
            for (int j = 9; j < 12; j++)
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