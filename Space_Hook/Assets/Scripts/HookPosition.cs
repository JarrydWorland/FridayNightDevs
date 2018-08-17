using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPosition : MonoBehaviour
{
    public GameObject hookShot;
    public GameObject playerChar;
    private Vector2 hookDirection;
    private Vector3 mousePosition;
    public float angle;
    // Use this for initialization
    void Start()
    {
        transform.position = playerChar.transform.position + new Vector3(0, 0.9f, 0);
        hookShot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hookDirection = new Vector2(mousePosition.x - playerChar.transform.position.x, mousePosition.y - playerChar.transform.position.y).normalized;
        Vector2 currentHookDirection = new Vector2(transform.position.x - playerChar.transform.position.x, transform.position.y - playerChar.transform.position.y);
        angle = Vector2.SignedAngle(currentHookDirection, hookDirection);

        transform.RotateAround(playerChar.transform.position, transform.forward, angle);

        if(Input.GetMouseButton(0))
        {
            hookShot.SetActive(true);
        }
    }
}
