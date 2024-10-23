using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class RotationToCamera : MonoBehaviour
    {
        void Start()
        {
            Transform body = this.gameObject.transform.GetChild(0);

            body.rotation = Camera.main.transform.rotation;
        }
    }
}