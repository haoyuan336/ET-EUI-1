using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ET.Client
{
    public static partial class InteractivePointComponentSystem
    {
        [EntitySystem]
        public static void Awake(this InteractivePointComponent self)
        {
        }

        public static void ChangeMapScene(this InteractivePointComponent self)
        {
            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();

            List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag("InteractivePoint").ToList();

            Log.Debug($"Gameobejcts {gameObjects.Count}");

            foreach (var gameObject in gameObjects)
            {
                long number = GetStringNumberHelper.GetLong(gameObject.name);

                InteractivePoint interactivePoint = self.CreateInteractive(number);

                interactivePoint.BindObject(gameObject);
            }
        }

        public static InteractivePoint CreateInteractive(this InteractivePointComponent self, long number)
        {
            InteractivePoint interactivePoint = self.GetChild<InteractivePoint>(number);

            if (interactivePoint == null)
            {
                interactivePoint = self.AddChildWithId<InteractivePoint>(number);
            }

            return interactivePoint;
        }
    }
}