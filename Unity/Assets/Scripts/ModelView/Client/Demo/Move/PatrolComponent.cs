using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Entity))]
    public class PatrolComponent : Entity, IAwake<Vector3, int>, IUpdate
    {
        public Vector3 TargetPos;

        public Vector3 InitPos;

        public int FindAngle = 0;

        public AIComponent AIComponent;

        public int ColliderLayer;

    }
}