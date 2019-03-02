using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject startpoint;

    public bool Team1HasBall = false;
    public bool Team2HasBall = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        BallPossession();
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
