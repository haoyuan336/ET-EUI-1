using UnityEngine;

namespace ET.Client
{
    public class AttackComponent : Entity, IAwake, IUpdate
    {
        public GameObject AttackObject;

        public AIComponent AIComponent;

        public Skill CurrentCastSkill = null;
    }
}