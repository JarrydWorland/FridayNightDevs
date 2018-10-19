using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    public float speed = 30f;
    public bool clockwise = true;
    public bool rotating = true;
    public bool isMoving;
    public float movingSpeed = 100000;
    public enum Directions {Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight  };
    public Directions startDirection;
    public GameObject attatchedTo;
    public GameObject forcefield;
    public GameObject player;
    public bool imAttatched;
    public GameObject SoundManager;
    private SoundManager sMan;
    public Vector3 startPosition;
    public GameObject shardPrefab;
    public GameObject shard;
    private Rigidbody2D t;
    private PlayerController playerC;
    public Collider2D myCol;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        sMan = SoundManager.GetComponent<SoundManager>();
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
        playerC = player.GetComponent<PlayerController>();
        myCol = attatchedTo.GetComponent<Collider2D>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            if (playerC.attatchedTo != null )
                if (forcefield.GetComponent<ForcefieldPull>().howClose > 0.8f && playerC.attatchedTo != attatchedTo)
                { forcefield.GetComponent<ForcefieldPull>().clockwise = !forcefield.GetComponent<ForcefieldPull>().clockwise; }
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
               
                    CreateShard();

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

    void CreateShard()
    {
        int i = 0;
        
        shard = (Instantiate(shardPrefab, transform.position, transform.rotation));

        foreach(Transform t in shard.transform)
        {
            if (i < 4)
            {
                t.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * player.GetComponent<ConstantSpeed>().Speed / 2);
            }
            else if (i < 8)
            {
                t.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * player.GetComponent<ConstantSpeed>().Speed / 3);

            }
            else if (i < 12)
            {
                t.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * player.GetComponent<ConstantSpeed>().Speed / 3);
            }
            else
            {
                t.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * player.GetComponent<ConstantSpeed>().Speed / 3);
            }

            if ((i == 0) || (i == 4) || (i == 8) || (i == 12))
            {
                t.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -player.GetComponent<ConstantSpeed>().Speed / 2);

            }
            else if ((i == 1) || (i == 5) || (i == 9) || (i == 13))
            {
                t.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -player.GetComponent<ConstantSpeed>().Speed / 3);

            }
            else if ((i == 2) || (i == 6) || (i == 10) || (i == 14))
            {
                t.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * player.GetComponent<ConstantSpeed>().Speed / 3);

            }
            else if ((i == 3) || (i == 7) || (i == 11) || (i == 15))
            {
                t.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * player.GetComponent<ConstantSpeed>().Speed / 2);

            }
            i++;
        }
    }
        void OnMouseDown()
    {
        sMan.PlaySound(sMan.Forcefield);

        if(playerC.attatchedTo != null) //your already attatched
        {
            forcefield.GetComponent<ForcefieldPull>().checkRot = true;
            playerC.attatchedTo.GetComponent<AsteroidBehavior>().imAttatched = false;

            if (playerC.attatchedTo != attatchedTo) //if what this object is not what youre currently attatched to
            {
                player.GetComponent<ConstantSpeed>().Speed += 3f;
                playerC.attatchedTo = attatchedTo;
                imAttatched = true;
                forcefield.SetActive(true);
                forcefield.transform.position = transform.position;
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
            if (!playerC.attatched)
            {
                playerC.attatched = true;
                playerC.attatchedTo = attatchedTo;
            }
        }
    }
}
