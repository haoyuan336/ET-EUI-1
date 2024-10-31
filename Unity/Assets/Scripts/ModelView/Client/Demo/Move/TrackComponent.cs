using UnityEngine;

namespace ET.Client
{
    public class TrackComponent : Entity, IAwake, IUpdate
    {
        public AIComponent AIComponent;

        public Entity TargetEntity;
    }
}