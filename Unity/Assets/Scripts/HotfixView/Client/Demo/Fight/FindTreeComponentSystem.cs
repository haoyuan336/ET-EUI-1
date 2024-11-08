using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;

namespace ET.Client
{
    [EntitySystemOf(typeof(FindTreeComponent))]
    public static partial class FindTreeComponentSystem
    {
        [EntitySystem]
        public static void Awake(this FindTreeComponent self)
        {
            self.AIComponent = self.Parent.GetComponent<AIComponent>();

            self.AIComponent.EnterStateAction += self.OnEnterStateAction;

            self.AIComponent.OutStateAction += self.OnOutStateAction;

            self.TreeColliderLayer = LayerMask.GetMask("Tree");

            FightDataComponent fightDataComponent = self.Parent.GetComponent<FightDataComponent>();

            self.MaxFindTreeDistance = fightDataComponent.GetValueByType(WordBarType.MaxTrackDistance);
        }

        [EntitySystem]
        public static void Update(this FindTreeComponent self)
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

                        int size = Physics.SphereCastNonAlloc(sourcePos, self.MaxFindTreeDistance, Vector3.down, self.RaycastHits, 20,
                            self.TreeColliderLayer);

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

                                TrackTreeComponent trackTreeComponent = self.Parent.GetComponent<TrackTreeComponent>();

                                trackTreeComponent.SetTrackObject(tree);

                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void OnEnterStateAction(this FindTreeComponent self, AIState aiState)
        {
            if (aiState == AIState.Patrol)
            {
            }
        }

        public static void OnOutStateAction(this FindTreeComponent self, AIState aiState)
        {
        }

        private static FightManagerComponent GetFightManagerComponent(this FindTreeComponent self)
        {
            FightManagerComponent fightManagerComponent = self.Parent.GetParent<FightManagerComponent>();

            return fightManagerComponent;
        }
    }
}