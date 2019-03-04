using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject startpoint;
    PassButton passButton;

    public bool Team1HasBall = false;
    public bool Team2HasBall = false;
    public LayerMask playerDetect;
    PlayerMovement playerMovement;

    float[] playerBallDistance;//= new float[5];
    void Start()
    {
        playerBallDistance = new float[10];
        playerMovement = FindObjectOfType<PlayerMovement>();
        passButton = FindObjectOfType<PassButton>();
    }

    // Update is called once per frame
    void Update()
    {
        checkforPlayers();
        //BallPossession();
        
    }

    void checkforPlayers()
    {
        Collider[] checkPlayers = Physics.OverlapSphere(this.transform.position, 10f, playerDetect);
        float value = float.PositiveInfinity;
        int index = -1;
        for (int i = 0; i < checkPlayers.Length; i++)
        {
            Collider hit = checkPlayers[i];
            float distance = Vector3.Distance(this.transform.position, hit.transform.position);
            playerBallDistance[i] = distance; 
            if(playerBallDistance[i] < value)
            {
                index = i;
                value = playerBallDistance[i];
            }

        }

        if(!playerMovement.ballisChild && passButton.pressed)
        {
            /*highlight selected player by index(smallest distance from the ball) above */
        }
        Debug.Log("Index value  " + index);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Team1GoalPost")
        {
            this.transform.position = startpoint.transform.position;
        }

        if (collision.gameObject.tag == "Team2GoalPost")
        {
            this.transform.position = startpoint.transform.position;
        }
    }


    public void BallPossession()
    {
        if(this.gameObject.transform.parent == null)
        {
            Debug.Log("Nobody has possession of ball");
            Team1HasBall = false;
            Team2HasBall = false;
        }

         if(this.gameObject.transform.parent.tag == "Team1")
        {
            Team1HasBall = true;
            Team2HasBall = false;
        }

        if(this.gameObject.transform.parent.tag == "Team2")
        {
            Team1HasBall = false;
            Team2HasBall = true;
        }
    }

    public bool GetTeam1possession()
    {
        return Team1HasBall;
    }

    public bool GetTeam2possession()
    {
        return Team2HasBall;
    }

   
}
