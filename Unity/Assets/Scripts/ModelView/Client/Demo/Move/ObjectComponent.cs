using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Entity))]
    public class ObjectComponent : Entity, IAwake<GameObject, Vector3>, IDestroy, IUpdate
    {
        public GameObject Prefab;

        public GameObject GameObject;

        public UnityEngine.AI.NavMeshAgent NavMeshAgent;

        public GameObject Body;

        public Vector3 InitPos;

        public SkeletonAnimation SkeletonAnimation;
    }
}