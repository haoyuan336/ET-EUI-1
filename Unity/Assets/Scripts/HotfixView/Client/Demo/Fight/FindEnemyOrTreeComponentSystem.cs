using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [EntitySystemOf(typeof(FindEnemyOrTreeComponent))]
    public static partial class FindEnemyOrTreeComponentSystem
    {
        [EntitySystem]
        public static void Awake(this FindEnemyOrTreeComponent self, int colliderLayer)
        {
            self.ColliderLayer = colliderLayer;

            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;

            self.TreeColliderLayer = LayerMask.GetMask("Tree");
        }

        [EntitySystem]
        public static void Update(this FindEnemyOrTreeComponent self)
        {
            if (self.AIComponent != null && !self.AIComponent.InSafeArea)
            {
                if (self.AIComponent.GetCurrentState() == AIState.Patrol)
                {
                    self.FindAngle++;

                    if (self.FindAngle % 2 == 0)
                    {
                        // Vector3 forword = Quaternion.Euler(0, self.FindAngle, 0) * Vector3.forward;

                        GameObject gameObject = self.Parent.GetComponent<ObjectComponent>().GameObject;

                        if (gameObject == null)
                        {
                            return;
                        }

                        Vector3 sourcePos = gameObject.transform.position + Vector3.up * 15;

                        var size = Physics.SphereCastNonAlloc(sourcePos, 10, Vector3.down, self.RaycastHits, 20, self.ColliderLayer);

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

                                break;
                            }

                            return;
                        }

                        size = Physics.SphereCastNonAlloc(sourcePos, 10, Vector3.down, self.RaycastHits, 20, self.TreeColliderLayer);

                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                RaycastHit hit = self.RaycastHits[i];

                                long entityId = hit.collider.transform.position.ToString().GetLongHashCode();

                                TreeComponent treeComponent = self.Root().GetComponent<TreeComponent>();

                                Tree tree = treeComponent.GetChild<Tree>(entityId);

                                if (tree == null || tree.IsDisposed)
                                {
                                    continue;
                                }

                                if (!tree.GetIsCanCut())
                                {
                                    continue;
                                }

                                TrackComponent trackComponent = self.Parent.GetComponent<TrackComponent>();

                                trackComponent.SetTrackObject(tree);

                                self.AIComponent.EnterAIState(AIState.TrackTree);

                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void OnEnterStateAction(this FindEnemyOrTreeComponent self, AIState aiState)
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

        public static void OnOutStateAction(this FindEnemyOrTreeComponent self, AIState aiState)
        {
        }

        private static FightManagerComponent GetFightManagerComponent(this FindEnemyOrTreeComponent self)
        {
            FightManagerComponent fightManagerComponent = self.Parent.GetParent<FightManagerComponent>();

            return fightManagerComponent;
        }
    }
}