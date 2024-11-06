using UnityEngine;

namespace ET.Client
{
    public struct BindTreeObject
    {
        public MapConfig MapConfig;
    }

    [Event(SceneType.Demo)]
    public class BindTreeObjectEventHandler : AEvent<Scene, BindTreeObject>
    {
        protected override async ETTask Run(Scene scene, BindTreeObject a)
        {
            TreeComponent treeComponent = scene.GetComponent<TreeComponent>();

            if (treeComponent == null)
            {
                treeComponent = scene.AddComponent<TreeComponent>();
            }

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Tree");

            foreach (var gameObject in gameObjects)
            {
                string name = gameObject.name;

                int number = GetStringNumberHelper.GetNumber(name);

                string str = gameObject.transform.position.ToString();

                Log.Debug($"str {str}");

                long id = str.GetLongHashCode();

                Log.Debug($"id {id}");

                Tree tree = treeComponent.GetChild<Tree>(id);

                if (tree == null)
                {
                    tree = treeComponent.AddChildWithId<Tree, int>(id, number);
                }

                tree.BindObject(gameObject);
            }

            await ETTask.CompletedTask;
        }
    }
}