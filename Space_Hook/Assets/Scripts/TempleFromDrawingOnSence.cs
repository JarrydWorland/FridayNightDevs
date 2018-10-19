using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
// make sure you use #if UNITY_EDITOR otherwise when you want to build there there will be problems
#if UNITY_EDITOR
using UnityEditor;
#endif

class TempleFromDrawingOnSence : MonoBehaviour
    {

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Handles.Label(this.transform.position, "test String");
        }
#endif
}
