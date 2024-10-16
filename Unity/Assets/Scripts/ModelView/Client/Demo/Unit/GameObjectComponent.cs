using Cinemachine;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectComponent : Entity, IAwake<GameObject>, IDestroy, IUpdate
    {
        private GameObject gameObject;

        public CharacterController CharacterController;

        public int Speed = Animator.StringToHash("speed");

        public Vector3 MoveSpeed;

        public Vector3 Rotation;

        public GameObject GameObject
        {
            get
            {
                return this.gameObject;
            }
            set
            {
                this.gameObject = value;
                this.Transform = value.transform;
            }
        }

        public Transform Transform { get; private set; }

        public Camera Camera;

        public float TargetPower = 0;

        public float CurrentPower = 0;
    }
}