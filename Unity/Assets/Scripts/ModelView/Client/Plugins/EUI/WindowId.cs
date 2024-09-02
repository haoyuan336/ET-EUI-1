/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System;
using System.Collections.Generic;
using System.Linq;
namespace ET.Client
{
    public enum WindowID
    {
        WindowID_Invaild = 0,
        //----------------------GameUIPackage---------------------
        GameUILayer = 1,
        //----------------------LoginLayerPackage---------------------
        LoginLayer = 2,
        //----------------------RootPackage---------------------
        RootLayer = 3,
        EmptyLayer = 4,
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
                 this.Maps.Add(windowID, "GameUIPackage");
            }
            for (int j = 2; j < 3; j++)
            {
                WindowID windowID = (WindowID)j;
                 this.Maps.Add(windowID, "LoginLayerPackage");
            }
            for (int j = 3; j < 5; j++)
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