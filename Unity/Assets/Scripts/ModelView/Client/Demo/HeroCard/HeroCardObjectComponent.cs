using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace ET.Client
{
    public enum AIType
    {
        MoveToUnit = 1,
        Moveing = 2,
        FindEnemy = 3,
        Attacking = 4
    }

    [ComponentOf(typeof(HeroCard))]
    public class HeroCardObjectComponent : Entity, IAwake<Unit, int>, IDestroy, IUpdate
    {
        public GameObject GameObject;

        public CharacterController CharacterController;

        public float TargetPower = 0;

        public float CurrentPower = 0;

        public int Speed = Animator.StringToHash("speed");

        public GameObject UnitObject = null;

        public GameObject MoveTarget = null;

        public AIType AIType = AIType.Moveing;

        public int Index;

        public ETTask MoveToUnitTask;

        public Vector3 BeforFramePos = Vector3.zero;

        // public Animator Animator;

        public int CheckColliderNumber = 0;

        public List<Vector3> ColliderPos = new List<Vector3>();

        public Unit Unit;

        public SkeletonAnimation Animation;

        public Vector3 LocalScale;

        public Vector3 TargetPos;

    }
}