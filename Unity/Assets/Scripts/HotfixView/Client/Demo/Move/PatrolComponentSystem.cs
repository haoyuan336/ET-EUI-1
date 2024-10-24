using UnityEngine;
using Random = System.Random;

namespace ET.Client
{
    [EntitySystemOf(typeof(PatrolComponent))]
    public static partial class PatrolComponentSystem
    {
        [EntitySystem]
        public static void Awake(this PatrolComponent self, Vector3 initPos, int colliderLayer)
        {
            self.InitPos = initPos;

            self.TargetPos = self.InitPos;

            self.ColliderLayer = colliderLayer;

            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }

        [EntitySystem]
        public static void Update(this PatrolComponent self)
        {
            if (self.AIComponent.CurrentAIState == AIState.Patrol)
            {
                ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

                Vector3 currentPos = objectComponent.GameObject.transform.position;

                float distance = (self.TargetPos - currentPos).magnitude;

                if (distance < 0.5f)
                {
                    self.MoveToRandomPos();
                }

                if (self.FindAngle % 10 == 0)
                {
                    Vector3 forword = Quaternion.Euler(0, self.FindAngle, 0) * objectComponent.GameObject.transform.forward + objectComponent.GameObject.GetComponent<SphereCollider>().center;

                    bool isHited = Physics.SphereCast(objectComponent.GameObject.transform.position, 1, forword, out RaycastHit hit, 10.0f,
                        self.ColliderLayer);

                    // bool isHited = Physics.SphereCast(objectComponent.GameObject.transform.position, forword, out RaycastHit hit, 10.0f,
                    // self.ColliderLayer);

                    Log.Debug($"is hited {isHited}");

                    if (isHited)
                    {
                        Log.Debug($"hit {hit.transform.name}");
                        TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                        trackComponent.SetTrackObject(hit.transform.gameObject);

                        self.AIComponent.EnterAIState(AIState.Track);
                    }
                }

                self.FindAngle++;
            }
        }

        private static void OnEnterStateAction(this PatrolComponent self, AIState aiState)
        {
            Log.Debug($"on enter stata action {aiState}");
            if (aiState == AIState.Patrol)
            {
                MoveObjectComponent moveComponent = self.Parent.GetComponent<MoveObjectComponent>();

                Log.Debug($"move component {moveComponent == null}");
                moveComponent.StartMove();

                self.MoveToRandomPos();
            }
        }

        private static void OnOutStateAction(this PatrolComponent self, AIState outState)
        {
            Log.Debug($"on out state action {outState}");
            if (outState == AIState.Patrol)
            {
                MoveObjectComponent moveObjectComponent = self.Parent.GetComponent<MoveObjectComponent>();

                moveObjectComponent.MoveEnd();
            }
        }

        private static void MoveToRandomPos(this PatrolComponent self)
        {
            self.TargetPos = Quaternion.Euler(0, RandomGenerator.RandomNumber(0, 360), 0) * Vector3.forward *
                    (4 + RandomGenerator.RandFloat01() * 4) + self.InitPos;

            MoveObjectComponent moveComponent = self.Parent.GetComponent<MoveObjectComponent>();

            Log.Debug($"move component {moveComponent == null}");

            moveComponent.Move(self.TargetPos);
        }
    }
}