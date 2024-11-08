using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(HeroCard))]
    public class FindTreeComponent : Entity, IAwake, IUpdate
    {
        public int TreeColliderLayer;

        public AIComponent AIComponent;

        public int FindAngle;

        public RaycastHit[] RaycastHits = new RaycastHit[10];

        public float MaxFindTreeDistance = 0;
    }
}