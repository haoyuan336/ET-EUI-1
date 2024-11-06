using UnityEngine;

namespace ET.Client
{
    [ChildOf(typeof(MapManagerComponent))]
    public class MapScene : Entity, IAwake<GameObject>, IDestroy
    {
        public GameObject ColliderObject;

        public Scene Scene;

        public bool IsLoaded;
    }
}