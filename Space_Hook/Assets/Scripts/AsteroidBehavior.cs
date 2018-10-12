using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    public float speed = 30f;
    public bool clockwise = true;
    public bool rotating = true;
    public GameObject attatchedTo;
    public GameObject forcfield;
    public GameObject player;
    public bool imAttatched;
    public GameObject SoundManager;
    private SoundManager sMan;
    public bool isMoving;
   // public List<GameObject> astShards;
  //  public Sprite[] shardSprites;
   // public GameObject Shard;
    private Rigidbody2D astShardsBod;
    private PlayerController playerC;
    public Collider2D myCol;

	// Use this for initialization
	void Start () {
        sMan = SoundManager.GetComponent<SoundManager>();
        //shardSprites = Resources.LoadAll<Sprite>("AstJagged");

        forcfield.SetActive(false);
        imAttatched = false;
        playerC = player.GetComponent<PlayerController>();
        myCol = attatchedTo.GetComponent<Collider2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            if (playerC.attatchedTo != null)
                if (forcfield.GetComponent<ForcefieldPull>().howClose > 0.8f)
                { forcfield.GetComponent<ForcefieldPull>().clockwise = !forcfield.GetComponent<ForcefieldPull>().clockwise; }
                else { playerC.Detatch(); }
            

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

        if(playerC.attatchedTo != null) //your already attatched
        {
            forcfield.GetComponent<ForcefieldPull>().checkRot = true;
            playerC.attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;

            if (playerC.attatchedTo != attatchedTo) //if what this object is not what youre currently attatched to
            {
                player.GetComponent<ConstantSpeed>().Speed += 3f;
                playerC.attatchedTo = attatchedTo;
                imAttatched = true;
                forcfield.SetActive(true);
                forcfield.transform.position = transform.position;
                sMan.PlaySound(sMan.SpeedUp);

            }
            else //if youre attacthed to this object and then click it - detatches
            {
                playerC.Detatch();
            }
        }
        else //if in free fall - attatch
        {
            playerC.attatchedTo = attatchedTo;
            imAttatched = true;
            forcfield.SetActive(true);
            forcfield.transform.position = transform.position;
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
            player.GetComponent<ConstantSpeed>().Speed += (0.1f * forcfield.GetComponent<ForcefieldPull>().howClose/2);
            if (!playerC.attatched)
            {
                playerC.attatched = true;
                playerC.attatchedTo = attatchedTo;
            }
        }
    }
}
