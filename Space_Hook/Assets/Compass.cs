using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Compass : MonoBehaviour
{
    private SpriteRenderer CompassImage;

    // Use this for initialization
    void Start()
    {
        CompassImage = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManger.Instance.HomeBase == null)
        {
            Debug.LogError("NO Homebase SET IN LEVELMANGER");
        }
        else
        {
            var dis = LevelManger.Instance.HomeBase.position - transform.position;
            var angle = Mathf.Atan2(dis.y, dis.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // Quaternion rotation = Quaternion.LookRotation(LevelManger.Instance.HomeBase.position, Vector3.forward);
            transform.rotation = rotation;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.DrawLine(transform.position, LevelManger.Instance.HomeBase.position);
    }
#endif
}
