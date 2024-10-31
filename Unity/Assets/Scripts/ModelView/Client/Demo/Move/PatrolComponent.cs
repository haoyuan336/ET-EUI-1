using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Entity))]
    public class PatrolComponent : Entity, IAwake<Vector3>, IUpdate
    {
        public Vector3 TargetPos;

        public Vector3 InitPos;

        public AIComponent AIComponent;

    }
}