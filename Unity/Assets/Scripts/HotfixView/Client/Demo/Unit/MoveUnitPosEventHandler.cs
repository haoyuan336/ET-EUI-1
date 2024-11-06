using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class MoveUnitPosEventHandler : AEvent<Scene, MoveUnitPos>
    {
        protected override async ETTask Run(Scene scene, MoveUnitPos a)
        {
            Vector2 direction = a.Vector2;

            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();

            Unit unit = unitComponent.MyUnit;

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            Vector3 targetPos = gameObjectComponent.GameObject.transform.position + new Vector3(direction.x, 0, -direction.y) * 10;

            Vector3 drawPos = gameObjectComponent.GameObject.transform.position + new Vector3(direction.x, 0, -direction.y) * 4;

            int wall = LayerMask.GetMask("Wall");

            Vector3 startPos = gameObjectComponent.GameObject.transform.position;

            Vector3 origin = new Vector3(startPos.x, 0.5f, startPos.z);

            bool isHited = Physics.Raycast(origin, new Vector3(direction.x, 0, -direction.y).normalized, out RaycastHit hitInfo,
                10, wall);

            if (isHited)
            {
                targetPos = new Vector3(hitInfo.point.x, targetPos.y, hitInfo.point.z);

                drawPos = targetPos;
            }

            GlobalComponent globalComponent = scene.Root().GetComponent<GlobalComponent>();

            globalComponent.ArrowGameObject.transform.position = new Vector3(drawPos.x, 0.1f, drawPos.z);

            gameObjectComponent.Move(targetPos);

            foreach (var heroCard in gameObjectComponent.HeroCards)
            {
                if (heroCard != null && !heroCard.IsDisposed)
                {
                    AIComponent aiComponent = heroCard.GetComponent<AIComponent>();

                    if (aiComponent.GetCurrentState() != AIState.Moving)
                    {
                        continue;
                    }

                    MoveObjectComponent moveObjectComponent = heroCard.GetComponent<MoveObjectComponent>();

                    if (moveObjectComponent != null && !moveObjectComponent.IsDisposed)
                    {
                        moveObjectComponent.Move(targetPos);
                    }
                }
            }

            await ETTask.CompletedTask;
        }
    }
}