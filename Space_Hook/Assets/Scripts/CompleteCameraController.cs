using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{

    private PlayerController playerC; 
    private ForcefieldPull forcefieldC;
    public float transitionSpeed;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        playerC = LevelManger.Instance.player;
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - playerC.transform.position;
        forcefieldC = LevelManger.Instance.ForceField.GetComponent<ForcefieldPull>();

    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.

        if (playerC.attatched )//&& forcefieldC.howClose >= 0.8f)
        {
            transform.position += new Vector3((playerC.attatchedTo.transform.position.x-transform.position.x)*transitionSpeed,(playerC.attatchedTo.transform.position.y - transform.position.y) * transitionSpeed, 0f );
        }
        else
        {
            transform.position += new Vector3((playerC.transform.position.x-transform.position.x)*transitionSpeed*2,(playerC.transform.position.y - transform.position.y) * transitionSpeed*2, 0f );
        }

    }
}
