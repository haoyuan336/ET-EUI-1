using UnityEngine;

namespace ET.Client
{
    public class TrackTreeComponent : Entity, IAwake
    {
        public AIComponent AIComponent;

        public Tree Tree;

        public float FindEnemyDistance;
    }
}