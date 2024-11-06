using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(HeroCard))]
    public class FindEnemyOrTreeComponent : Entity, IAwake<int>, IUpdate
    {
        public int ColliderLayer;

        public int TreeColliderLayer;

        public AIComponent AIComponent;

        public int FindAngle;

        public RaycastHit[] RaycastHits = new RaycastHit[10];
    }
}