﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    public float speed = 30f;
    public bool clockwise = true;
    public bool rotating = true;
    public bool isMoving;
    public float movingSpeed = 100000;
    
    public Directions startDirection;
    public GameObject attatchedTo;

    public bool imAttatched;
    private SoundManager sMan;
    public Vector3 startPosition;
    public Collider2D myCol;

    // public List<GameObject> astShards;
    //  public Sprite[] shardSprites;
    // public GameObject Shard;

    private Rigidbody2D astShardsBod;
    private PlayerController player;
    private GameObject forcefield;

    public enum Directions { Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight };

    // Use this for initialization
    void Start ()
    {
        player = LevelManger.Instance.player;
        forcefield = LevelManger.Instance.ForceField;
        startPosition = transform.position;
	    sMan = SoundManager.Instance;
        //shardSprites = Resources.LoadAll<Sprite>("AstJagged");
        if(startDirection!= null && isMoving)
        {
            Vector3 startD = new Vector3(0,0,0);
            switch (startDirection)
            {
                case Directions.Up:
                    startD = new Vector3(0, 1, 0);
                    break;
                case Directions.Down:
                    startD = new Vector3(0, -1, 0);
                    break;
                case Directions.Left:
                    startD = new Vector3(-1, 0, 0);
                    break;
                case Directions.Right:
                    startD = new Vector3(1, 0, 0);
                    break;
                case Directions.UpLeft:
                    startD = new Vector3(-1, 1, 0);
                    break;
                case Directions.UpRight:
                    startD = new Vector3(1, 1, 0);
                    break;
                case Directions.DownLeft:
                    startD = new Vector3(-1, -1, 0);
                    break;
                case Directions.DownRight:
                    startD = new Vector3(1, -1, 0);
                    break;
            }
            GetComponent<Rigidbody2D>().AddForce(startD * movingSpeed);
        }
        forcefield.SetActive(false);
        imAttatched = false;
        myCol = attatchedTo.GetComponent<Collider2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            if (player.attatchedTo != null )
                if (forcefield.GetComponent<ForcefieldPull>().howClose > 0.8f && player.attatchedTo != attatchedTo)
                { forcefield.GetComponent<ForcefieldPull>().clockwise = !forcefield.GetComponent<ForcefieldPull>().clockwise; }
                else { player.Detatch(); }
            

            if (attatchedTo.tag == "Bouncy")
            {
                //Bounce();
                sMan.PlaySound(sMan.bounceCol);
                player.GetComponent<ConstantSpeed>().Speed += 2;
            }
            if (attatchedTo.tag == "Damage")
            {
                //Damage();
                sMan.PlaySound(sMan.dmgCol);
                /*while(astShards.Count<16)
                {
                    CreateShard();
                }
                */

                if (player.GetComponent<ConstantSpeed>().max - player.GetComponent<ConstantSpeed>().Speed < player.GetComponent<ConstantSpeed>().Speed)
                {
                    player.GetComponent<ConstantSpeed>().Speed = player.GetComponent<ConstantSpeed>().max - player.GetComponent<ConstantSpeed>().Speed;
                }
                else
                {
                    player.GetComponent<ConstantSpeed>().Speed = 10;
                }
              
                gameObject.SetActive(false);
            }
        }
    }

    /*void CreateShard()
    {
        float j = -0.6f, k = 0;
        
        astShards.Add(Instantiate(Shard, transform.position, transform.rotation));
        int i = astShards.Count;
        astShards[i].AddComponent<AstShard>();
        astShardsBod = astShards[i].GetComponent<Rigidbody2D>();
            if (i < 4)
            {
                astShardsBod.AddForce(Vector2.up * player.GetComponent<ConstantSpeed>().Speed / 2);
            }
            else if (i < 8)
            {
                astShardsBod.AddForce(Vector2.up * player.GetComponent<ConstantSpeed>().Speed / 3);

            }
            else if (i < 12)
            {
                astShardsBod.AddForce(Vector2.down * player.GetComponent<ConstantSpeed>().Speed / 3);
            }
            else
            {
                astShardsBod.AddForce(Vector2.down * player.GetComponent<ConstantSpeed>().Speed / 3);
            }

            if ((i == 0) || (i == 4) || (i == 8) || (i == 12))
            {
                astShardsBod.AddForce(Vector2.right * -player.GetComponent<ConstantSpeed>().Speed / 2);

            }
            else if ((i == 1) || (i == 5) || (i == 9) || (i == 13))
            {
                astShardsBod.AddForce(Vector2.right * -player.GetComponent<ConstantSpeed>().Speed / 3);

            }
            else if ((i == 2) || (i == 6) || (i == 10) || (i == 14))
            {
                astShardsBod.AddForce(Vector2.right * player.GetComponent<ConstantSpeed>().Speed / 3);

            }
            else if ((i == 3) || (i == 7) || (i == 11) || (i == 15))
            {
                astShardsBod.AddForce(Vector2.right * player.GetComponent<ConstantSpeed>().Speed / 2);

            }

            if (j > 1.2f)
            {
                j = -0.6f;
                k -= 0.6f;
            }


            astShards[i].transform.position = astShards[i].transform.position + new Vector3(j, k, 0);
            j += 0.6f;

        }*/
        void OnMouseDown()
    {
        sMan.PlaySound(sMan.Forcefield);

        if(player.attatchedTo != null) //your already attatched
        {
            forcefield.GetComponent<ForcefieldPull>().checkRot = true;
            player.attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;

            if (player.attatchedTo != attatchedTo) //if what this object is not what youre currently attatched to
            {
                player.GetComponent<ConstantSpeed>().Speed += 3f;
                player.attatchedTo = attatchedTo;
                imAttatched = true;
                forcefield.SetActive(true);
                forcefield.transform.position = transform.position;
                sMan.PlaySound(sMan.SpeedUp);

            }
            else //if youre attacthed to this object and then click it - detatches
            {
                player.Detatch();
            }
        }
        else //if in free fall - attatch
        {
            player.attatchedTo = attatchedTo;
            imAttatched = true;
            forcefield.SetActive(true);
            forcefield.transform.position = transform.position;
        }

       
    }
    // Update is called once per frame
    void Update () {
        if (rotating)
        {
            if (!clockwise)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * -speed);

            }
        }

        if(imAttatched)
        {
            player.GetComponent<ConstantSpeed>().Speed += (0.1f * forcefield.GetComponent<ForcefieldPull>().howClose/2);
            if (!player.attatched)
            {
                player.attatched = true;
                player.attatchedTo = attatchedTo;
            }
        }
    }
}
