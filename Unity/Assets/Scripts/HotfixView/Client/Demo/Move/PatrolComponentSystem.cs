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
            if (self.AIComponent.GetCurrentState() == AIState.Patrol)
            {
                ObjectComponent objectComponent = self.Parent.GetComponent<ObjectComponent>();

                Vector3 currentPos = objectComponent.GameObject.transform.position;

                float distance = (self.TargetPos - currentPos).magnitude;

                if (distance < 0.5f)
                {
                    self.MoveToRandomPos();
                }

                // if (self.FindAngle % 5 == 0)
                // {
                //     Vector3 forword = Quaternion.Euler(0, self.FindAngle, 0) * Vector3.forward;
                //
                //     GameObject gameObject = objectComponent.GameObject;
                //
                //     // Vector3 startPos = gameObject.transform.position;
                //     Vector3 sourcePos = gameObject.transform.position + gameObject.GetComponent<Collider>().bounds.size.y * 0.5f * Vector3.up;
                //
                //     bool isHited = Physics.SphereCast(sourcePos, 3, forword,
                //         out RaycastHit hit, ConstValue.FindEnemyDistance,
                //         self.ColliderLayer);
                //
                //     self.FindAngle %= 360;
                //
                //     if (isHited)
                //     {
                //         long entityId = FightDataHelper.GetIdByGameObjectName(hit.transform.gameObject.name);
                //
                //         FightManagerComponent fightManagerComponent = self.GetFightManagerComponent(self.Parent);
                //
                //         bool isDead = FightDataHelper.GetIsDead(fightManagerComponent, entityId);
                //
                //         if (isDead)
                //         {
                //             return;
                //         }
                //
                //         Log.Debug($"hit {hit.transform.name}");
                //         TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();
                //
                //         trackComponent.SetTrackObject(hit.transform.gameObject);
                //
                //         self.AIComponent.EnterAIState(AIState.Track);
                //     }
                // }

                self.FindAngle++;
            }
        }

        private static FightManagerComponent GetFightManagerComponent(this PatrolComponent self, Entity entity)
        {
            FightManagerComponent fightManagerComponent = entity.GetParent<FightManagerComponent>();

            return fightManagerComponent;
        }

        private static void OnEnterStateAction(this PatrolComponent self, AIState aiState)
        {
            Log.Debug($"on enter stata action {aiState}");
            if (aiState == AIState.Patrol)
            {
                AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();

                animComponent.PlayAnim("move", true).Coroutine();

                self.MoveToRandomPos();
            }
        }

        private static void OnOutStateAction(this PatrolComponent self, AIState outState)
        {
            Log.Debug($"on out state action {outState}");
            if (outState == AIState.Patrol)
            {
                AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();

                animComponent.PlayAnim("idle", true).Coroutine();
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