using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class ColliderAction : MonoBehaviour
    {
        public Action<GameObject> OnTriggerEnterAction;

        public Action<GameObject> OnTriggerStayAction;

        public Action<GameObject> OnTriggerExitAction;

        public void OnTriggerEnter(Collider other)
        {
            if (this.OnTriggerEnterAction != null)
            {
                this.OnTriggerEnterAction.Invoke(this.gameObject);
            }

            Log.Debug($"OnTriggerEnter {this.gameObject.name}, {other.gameObject.name}");
        }

        public void OnTriggerStay(Collider other)
        {
        }

        public void OnTriggerExit(Collider other)
        {
            Log.Debug($"OnTriggerExit {other.gameObject.name}");

            if (this.OnTriggerExitAction != null)
            {
                this.OnTriggerExitAction.Invoke(this.gameObject);
            }
        }
    }
}