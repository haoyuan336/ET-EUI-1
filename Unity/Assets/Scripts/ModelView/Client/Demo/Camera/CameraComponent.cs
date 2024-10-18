using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class CameraComponent : Entity, IAwake, IUpdate
    {
        public GameObject UnitObject;

        public Camera Camera;

        public float CurrentPower;

    }
}