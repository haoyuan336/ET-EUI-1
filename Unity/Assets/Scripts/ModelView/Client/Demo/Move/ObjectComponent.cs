using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Entity))]
    public class ObjectComponent : Entity, IAwake<GameObject, Vector3>, IDestroy
    {
        public GameObject GameObject;
    }
}