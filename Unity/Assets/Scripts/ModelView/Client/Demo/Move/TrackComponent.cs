using UnityEngine;

namespace ET.Client
{
    public class TrackComponent : Entity, IAwake, IUpdate
    {
        public GameObject TrackGameObject;

        public AIComponent AIComponent;
    }
}