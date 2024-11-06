using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class ColliderAction : MonoBehaviour
    {
        public Action<GameObject, GameObject> OnTriggerEnterAction;

        public Action<GameObject, GameObject> OnTriggerStayAction;

        public Action<GameObject, GameObject> OnTriggerExitAction;

        public void OnTriggerEnter(Collider other)
        {
            if (this.OnTriggerEnterAction != null)
            {
                this.OnTriggerEnterAction.Invoke(this.gameObject, other.gameObject);
            }
        }

        public void OnTriggerStay(Collider other)
        {
        }

        public void OnTriggerExit(Collider other)
        {
            if (this.OnTriggerExitAction != null)
            {
                this.OnTriggerExitAction.Invoke(this.gameObject, other.gameObject);
            }
        }
    }
}