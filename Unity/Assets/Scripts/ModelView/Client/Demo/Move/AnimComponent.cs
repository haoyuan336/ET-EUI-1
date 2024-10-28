using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    public class AnimComponent: Entity, IAwake, IDestroy
    {
        public SkeletonAnimation SkeletonAnimation;
        
        public ETTask AnimTask;
    }
}