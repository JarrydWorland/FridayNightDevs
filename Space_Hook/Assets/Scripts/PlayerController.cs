using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector2 hookDirection;
    public float distanceOfHook = 0.9f;
    private int rotationSpeed = 100;
    private float movementSpeed = 0.1f;
    public bool manualMovement = false;
    public float angle;
    public GameObject hook;
    private Vector3 mousePosition;
    private Vector3 changeInPlayerPos;
    private Vector3 initialPlayerPos;

    // Use this for initialization
    void Start () {
        hook.transform.position = transform.position + new Vector3(0,0.9f,0);
    }
	
	// Update is called once per frame
	void Update () {

        initialPlayerPos = transform.position;
        
        HookFaceMouse();
        
        if (manualMovement)
        {
            MovePlayer();
            changeInPlayerPos = transform.position;
        }
        HookTransform();
    }

    void MovePlayer()
    {
        if (Input.GetKey("w"))
        {
            transform.position += Vector3.up * movementSpeed;
        }
        if (Input.GetKey("s"))
        {
            transform.position += Vector3.down * movementSpeed;
        }
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left * movementSpeed;
        }
        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * movementSpeed;
        }
    }

    void HookTransform()
    {
        hook.transform.position += (changeInPlayerPos - initialPlayerPos);
    }

    void HookFaceMouse()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        hookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
        Vector2 currentHookDirection = new Vector2(hook.transform.position.x - transform.position.x, hook.transform.position.y - transform.position.y);
        angle = Vector2.SignedAngle(currentHookDirection, hookDirection);
        
        hook.transform.RotateAround(transform.position, transform.forward, angle);
    }
}
