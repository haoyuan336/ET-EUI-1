using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace ET.Client
{
  

    [ComponentOf(typeof(Entity))]
    public class MoveObjectComponent : Entity, IAwake, IDestroy, IUpdate
    {
        public SkeletonAnimation SkeletonAnimation;

        public Vector3 LocalScale;

        public Vector3 TargetPos;

        public NavMeshAgent NavMeshAgent;

        public Transform Body;
    }
}