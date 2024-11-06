using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    public class Tree : Entity, IAwake<int>
    {
        public int ConfigId;

        public TreeConfig TreeConfig => TreeConfigCategory.Instance.Get(this.ConfigId);

        public GameObject TreeObject;

        public SkeletonAnimation SkeletonAnimation;
        
        public bool IsLive = true;
        
        public int HP = 0;
    }
}