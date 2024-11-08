using UnityEngine;

namespace ET.Client
{
    public class FindEnemyComponent : Entity, IAwake<int>, IUpdate
    {
        public int ColliderLayer;

        public AIComponent AIComponent;

        public int FindAngle;

        public RaycastHit[] RaycastHits = new RaycastHit[10];

        public SkillComponent SkillComponent;

        public FightDataComponent FightDataComponent;

        public float FindEnemyDistance;
    }
    
}