using System;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(GameObjectComponent))]
    public static partial class GameObjectComponentSystem
    {
        [EntitySystem]
        private static void Destroy(this GameObjectComponent self)
        {
            UnityEngine.Object.Destroy(self.GameObject);
        }

        [EntitySystem]
        private static void Awake(this GameObjectComponent self, GameObject gameObject)
        {
            self.GameObject = gameObject;

            self.CharacterController = self.GameObject.GetComponent<CharacterController>();

            Log.Debug($"awake CharacterController{self.CharacterController == null}");
        }

        public static void Move(this GameObjectComponent self, Vector2 direction)
        {
            Log.Debug($"x {direction.x}, {direction.y}");
            // self.CharacterController.Move(new Vector3(1, 0, 1) * Time.deltaTime);

            // self.CharacterController.

            self.MoveSpeed = new Vector3(direction.x, 0, direction.y * -1);
        }

        [EntitySystem]
        public static void Update(this GameObjectComponent self)
        {
            self.CharacterController.Move(self.MoveSpeed * Time.deltaTime);
        }
    }
}