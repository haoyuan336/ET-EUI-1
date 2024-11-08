using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    public class AttackComponent : Entity, IAwake
    {
        public AIComponent AIComponent;

        public Skill CurrentCastSkill = null;

        public List<Entity> TargetEntities = new List<Entity>();
    }
}