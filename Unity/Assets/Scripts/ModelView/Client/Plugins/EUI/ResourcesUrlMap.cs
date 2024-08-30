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
        //---------------LoginLayerPackage---------------
        public const string LoginLayer = "ui://9sdsckomrsyn0";
        public const string BgLayer = "ui://9sdsckoml5qe2";
        //---------------RootPackage---------------
        public const string RootLayer = "ui://9nomxs01i2d80";
        public const string EmptyLayer = "ui://9nomxs01i2d81";
    }
}