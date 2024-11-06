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
            if (self.AIComponent != null && !self.AIComponent.InSafeArea)
            {
                if (self.AIComponent.GetCurrentState() == AIState.Patrol)
                {
                    if (self.FindAngle % 2 == 0)
                    {
                        // Vector3 forword = Quaternion.Euler(0, self.FindAngle, 0) * Vector3.forward;

                        GameObject gameObject = self.Parent.GetComponent<ObjectComponent>().GameObject;

                        if (gameObject == null)
                        {
                            return;
                        }

                        Vector3 sourcePos = gameObject.transform.position + Vector3.up * 15;

                        // bool isHited = Physics.SphereCast(sourcePos, 10, Vector3.down, out RaycastHit hit, 20, self.ColliderLayer);

                        int size = Physics.SphereCastNonAlloc(sourcePos, 10, Vector3.down, self.RaycastHits, 20, self.ColliderLayer);

                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                RaycastHit hit = self.RaycastHits[i];

                                long entityId = FightDataHelper.GetIdByGameObjectName(hit.transform.gameObject.name);

                                FightManagerComponent fightManagerComponent = self.GetFightManagerComponent();

                                Entity entity = fightManagerComponent.GetChild<Entity>(entityId);

                                if (entity == null || entity.IsDisposed)
                                {
                                    continue;
                                }

                                bool canAttack = FightDataHelper.GetCanAttack(entity);

                                if (!canAttack)
                                {
                                    continue;
                                }

                                TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                                trackComponent.SetTrackObject(entity);

                                self.AIComponent.EnterAIState(AIState.Track);
                            }
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