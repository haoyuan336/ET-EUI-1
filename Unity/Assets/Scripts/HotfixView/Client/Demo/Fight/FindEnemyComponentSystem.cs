using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [EntitySystemOf(typeof(FindEnemyComponent))]
    public static partial class FindEnemyComponentSystem
    {
        [EntitySystem]
        public static void Awake(this FindEnemyComponent self, int colliderLayer)
        {
            self.ColliderLayer = colliderLayer;

            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;
        }

        [EntitySystem]
        public static void Update(this FindEnemyComponent self)
        {
            if (self.AIComponent != null)
            {
                if (self.AIComponent.GetCurrentState() == AIState.Patrol || self.AIComponent.GetCurrentState() == AIState.FindEnemy)
                {
                    if (self.FindAngle % 2 == 0)
                    {
                        // Vector3 forword = Quaternion.Euler(0, self.FindAngle, 0) * Vector3.forward;

                        GameObject gameObject = self.Parent.GetComponent<ObjectComponent>().GameObject;

                        if (gameObject == null)
                        {
                            return;
                        }

                        // Vector3 startPos = gameObject.transform.position;
                        // Vector3 sourcePos = gameObject.transform.position + gameObject.GetComponent<Collider>().bounds.size.y * 0.5f * Vector3.up;

                        Vector3 sourcePos = gameObject.transform.position + Vector3.up * 15;

                        // bool isHited = Physics.SphereCast(sourcePos, 1, forword,
                        //     out RaycastHit hit, ConstValue.FindEnemyDistance,
                        //     self.ColliderLayer);

                        bool isHited = Physics.SphereCast(sourcePos, 10, Vector3.down, out RaycastHit hit, 20, self.ColliderLayer);

                        if (isHited)
                        {
                            long entityId = FightDataHelper.GetIdByGameObjectName(hit.transform.gameObject.name);

                            FightManagerComponent fightManagerComponent = self.GetFightManagerComponent();

                            Entity entity = fightManagerComponent.GetChild<Entity>(entityId);

                            if (entity == null || entity.IsDisposed)
                            {
                                return;
                            }

                            bool isDead = FightDataHelper.GetIsDead(entity);

                            if (isDead)
                            {
                                return;
                            }

                            TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                            trackComponent.SetTrackObject(entity);

                            self.AIComponent.EnterAIState(AIState.Track);
                        }
                    }

                    self.FindAngle++;
                }
            }
        }

        public static void OnEnterStateAction(this FindEnemyComponent self, AIState aiState)
        {

            if (aiState == AIState.Patrol)
            {
                // AnimComponent animComponent = self.Parent.GetComponent<AnimComponent>();
                //
                // if (animComponent != null)
                // {
                //     animComponent.PlayAnim("idle");
                // }
            }
        }

        public static void OnOutStateAction(this FindEnemyComponent self, AIState aiState)
        {
        }

        private static FightManagerComponent GetFightManagerComponent(this FindEnemyComponent self)
        {
            FightManagerComponent fightManagerComponent = self.Parent.GetParent<FightManagerComponent>();

            return fightManagerComponent;
        }
    }
}