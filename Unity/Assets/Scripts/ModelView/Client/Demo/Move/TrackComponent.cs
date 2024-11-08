using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    public class TrackComponent : Entity, IAwake
    {
        public AIComponent AIComponent;

        // public Entity TargetEntity;

        public List<Entity> TargetEntities;
    }
}