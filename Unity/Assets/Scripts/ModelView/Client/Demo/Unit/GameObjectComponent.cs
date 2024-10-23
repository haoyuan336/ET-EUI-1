using System.Collections.Generic;
using Cinemachine;
using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectComponent : Entity, IAwake, IDestroy, IUpdate
    {
        private GameObject gameObject;

        // public GameObject ArrowGameObject;

        public CharacterController CharacterController;

        public int Speed = Animator.StringToHash("speed");

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

        // public Camera Camera;

        // public float TargetPower = 0;

        public float CurrentPower = 0;
        
        // public Vector3 OldPos { get; set; }

        // public Queue<Vector3> PathPos = new Queue<Vector3>();

        // public SkeletonAnimation Animation = null;

        public Vector3 TargetPos;
    }
}