using System;
using ET.Client;
using UnityEngine;

namespace ET
{
    [EntitySystemOf(typeof(CameraComponent))]
    public static partial class CameraComponentSystem
    {
        [EntitySystem]
        public static void Awake(this CameraComponent self)
        {
            self.Camera = Camera.main;

            Unit unit = UnitHelper.GetMyUnit(self.Root());

            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();

            self.UnitObject = gameObjectComponent.GameObject;

            self.Camera.transform.position = self.UnitObject.transform.position + new Vector3(-8, 14, -8);
        }

        [EntitySystem]
        public static void Update(this CameraComponent self)
        {
            if (self.UnitObject != null)
            {
                Vector3 pos = self.UnitObject.transform.position + new Vector3(-8, 14, -8);

                Vector3 endPos = Vector3.Lerp(pos, self.Camera.transform.position, Time.deltaTime);
                
                // self.Camera.transform.Translate(direction.normalized * Time.deltaTime * 2);

                self.Camera.transform.position = endPos;
            }
        }
    }
}