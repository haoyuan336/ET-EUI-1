using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class InteractionObject : MonoBehaviour
    {
        private Transform[] child;

        void Start()
        {
            this.child = new Transform[this.transform.childCount];

            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.child[i] = this.transform.GetChild(i);
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < this.child.Length; i++)
            {
                this.child[i].rotation = Camera.main.transform.rotation;
            }
        }
    }
}