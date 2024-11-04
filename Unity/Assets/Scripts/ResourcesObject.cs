using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class ResourcesObject : MonoBehaviour
    {
        // Start is called before the first frame update

        private Transform Body;

        void Start()
        {
            this.Body = this.gameObject.transform.GetChild(0);
        }

        // Update is called once per frame
        void Update()
        {
            if (this.Body != null)
            {
                this.Body.transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}