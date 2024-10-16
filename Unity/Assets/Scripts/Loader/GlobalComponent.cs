using FairyGUI;
using UnityEngine;

namespace ET
{
    [FriendOf(typeof(GlobalComponent))]
    public static partial class GlobalComponentSystem
    {
        [EntitySystem]
        public static void Awake(this GlobalComponent self)
        {
            self.Global = GameObject.Find("/Global").transform;
            self.Unit = GameObject.Find("/Global/Unit").transform;
            // self.UI = GameObject.Find("/Global/UI").transform;
            // self.NormalRoot = GameObject.Find("/Global/UI/NormalRoot").transform;
            // self.PopUpRoot = GameObject.Find("/Global/UI/PopUpRoot").transform;
            // self.FixedRoot = GameObject.Find("/Global/UI/FixedRoot").transform;
            // self.OtherRoot = GameObject.Find("/Global/UI/OtherRoot").transform;
            self.PoolRoot = GameObject.Find("/Global/PoolRoot").transform;

            // self.Prefabs = GameObject.Find("/Global/Prefabs").GetComponent<Prefabs>();

            self.UIPanel = GameObject.Find("/Global/UIPanel").transform.GetComponent<UIPanel>();

            // self.NormalRoot = self.UIPanel.

            self.RootLayer = self.UIPanel.ui;

            // self.NormalRoot = self.RootLayer.GetChild("NormalRootLayer").asCom;
            //
            // self.PopUpRoot = self.RootLayer.GetChild("PopUpRootLayer").asCom;
            //
            // self.FixedRoot = self.RootLayer.GetChild("FixedRootLayer").asCom;
            //
            // self.OtherRoot = self.RootLayer.GetChild("OtherRootLayer").asCom;

            self.GlobalConfig = Resources.Load<GlobalConfig>("GlobalConfig");

            self.ReferenceCollector = self.Global.GetComponent<ReferenceCollector>();

            // GRoot.inst.SetContentScaleFactor(720,1280,UIContentScaler.ScreenMatchMode.MatchWidth);

            // self.UIPanel.ui.MakeFullScreen();
        }
    }

    [ComponentOf(typeof(Scene))]
    public class GlobalComponent : Entity, IAwake
    {
        public void Init()
        {
            this.RootLayer = this.UIPanel.ui;

            this.NormalRoot = this.RootLayer.GetChild("NormalRootLayer").asCom;

            this.PopUpRoot = this.RootLayer.GetChild("PopUpRootLayer").asCom;

            this.FixedRoot = this.RootLayer.GetChild("FixedRootLayer").asCom;

            this.OtherRoot = this.RootLayer.GetChild("OtherRootLayer").asCom;
        }

        public Transform Global;
        public Transform Unit { get; set; }

        // public Prefabs Prefabs;
        // public Transform UI;

        public GlobalConfig GlobalConfig { get; set; }

        // public Transform NormalRoot { get; set; }
        // public Transform PopUpRoot { get; set; }
        // public Transform FixedRoot { get; set; }
        // public Transform PoolRoot { get; set; }
        // public Transform OtherRoot { get; set; }
        //
        // public Transform UIPanel { get; set; }

        public GComponent RootLayer;

        public GComponent NormalRoot { get; set; }
        public GComponent PopUpRoot { get; set; }
        public GComponent FixedRoot { get; set; }
        public Transform PoolRoot { get; set; }
        public GComponent OtherRoot { get; set; }

        public UIPanel UIPanel { get; set; }

        public ReferenceCollector ReferenceCollector { get; set; }
    }
}