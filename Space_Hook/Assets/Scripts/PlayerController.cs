using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;

    //Below is public for testing in Unity, otherwise would be private
    public GameObject attatchedTo;
    public bool attatched = false;
    public float playerVelocity;

    private GameObject forcefield;
    private SoundManager sMan;

    void Start()
    {
        forcefield = LevelManger.Instance.ForceField;
        sMan = SoundManager.Instance;
        rb2d.AddForce(Vector2.right * 5f);
    }
    public void Detatch()
    {
        forcefield.SetActive(false);
        forcefield.GetComponent<ForcefieldPull>().checkRot = true;
        attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;
        attatchedTo = null;
        attatched = false;
        GetComponent<ConstantSpeed>().Speed += 3;
        sMan.PlaySound(sMan.SpeedUp);
        sMan.StopSound(sMan.SecondsoundToPlay);
    }
    void Attatched()
    {
        if (Input.GetButtonDown("Fire1")) //dettaches
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!attatchedTo.GetComponent<Collider2D>().bounds.Contains(mousePos))
            {
                Detatch();
            }
        }
    }

    void FreeFall()
    {
        this.GetComponent<ConstantSpeed>().Speed -= 0.03f;
        forcefield.GetComponent<ForcefieldPull>().howClose = 0;

    }
    void Update()
    {
        if (attatched)
        {
            Attatched();
        }
        else
        {
            FreeFall();
        }
    }
    private void FixedUpdate()
    {
        playerVelocity = rb2d.velocity.magnitude;
        sMan.ChangePitch((this.GetComponent<ConstantSpeed>().Speed / 10) - 0.5f);
    }

    public void Reset()
    {
        // When the player die reset things the player needs.
        
        GetComponent<ConstantSpeed>().Speed = GetComponent<ConstantSpeed>().StartSpeed;
        if (attatchedTo != null)
            Detatch();
        attatchedTo = null;
        rb2d.velocity = Vector2.zero;
    }
    
    public Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
    
}
