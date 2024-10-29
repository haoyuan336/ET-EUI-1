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

            self.GameObject = self.Parent.GetComponent<ObjectComponent>().GameObject;
        }

        [EntitySystem]
        public static void Update(this FindEnemyComponent self)
        {
            if (self.AIComponent != null)
            {
                if (self.AIComponent.GetCurrentState() == AIState.Patrol)
                {
                    if (self.FindAngle % 2 == 0)
                    {
                        Vector3 forword = Quaternion.Euler(0, self.FindAngle, 0) * Vector3.forward;

                        GameObject gameObject = self.GameObject;

                        // Vector3 startPos = gameObject.transform.position;
                        Vector3 sourcePos = gameObject.transform.position + gameObject.GetComponent<Collider>().bounds.size.y * 0.5f * Vector3.up;

                        bool isHited = Physics.SphereCast(sourcePos, 1, forword,
                            out RaycastHit hit, ConstValue.FindEnemyDistance,
                            self.ColliderLayer);

                        if (isHited)
                        {
                            long entityId = FightDataHelper.GetIdByGameObjectName(hit.transform.gameObject.name);

                            FightManagerComponent fightManagerComponent = self.GetFightManagerComponent();

                            bool isDead = FightDataHelper.GetIsDead(fightManagerComponent, entityId);

                            if (isDead)
                            {
                                return;
                            }

                            TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                            trackComponent.SetTrackObject(hit.transform.gameObject);

                            self.AIComponent.EnterAIState(AIState.Track);
                        }
                    }

                    self.FindAngle++;
                }
            }
        }

        public static void OnEnterStateAction(this FindEnemyComponent self, AIState aiState)
        {
            Log.Debug($"find enemy coponent {aiState}");
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