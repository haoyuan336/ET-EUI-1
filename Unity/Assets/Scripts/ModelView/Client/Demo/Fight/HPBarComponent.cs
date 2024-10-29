using FairyGUI;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    public class HPBarComponent : Entity, IAwake<GameObject>, IUpdate, IDestroy
    {
        public FGUIHPProgressItemCellComponent CellComponent = null;

        public long OutTime;

        public GameObject GameObject;

        public Controller ColorState;

        public SkeletonAnimation SkeletonAnimation;

        public Bone Bone;
    }
}