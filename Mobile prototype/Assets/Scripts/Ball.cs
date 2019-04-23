using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject startpoint;
    public GameObject selectedPlayer = null;
    PassButton passButton;
    Rigidbody BallObject;
    public bool Team1HasBall = false;
    public bool Team2HasBall = false;
    public LayerMask playerDetect;
    static float t = 1.0f;

    PlayerMovement playerMovement;

    

    float[] playerBallDistance;//= new float[5];


    //variables for testing purpose

    public GameObject Ballparent;
    void Start()
    {
        playerBallDistance = new float[6];
        playerMovement = FindObjectOfType<PlayerMovement>();
        passButton = FindObjectOfType<PassButton>();
        BallObject = GetComponent<Rigidbody>();
        checkforPlayers();
    }

    // Update is called once per frame
    void Update()
    {

        BallPossession();
        //if (playerMovement.ballisChild)
        //{
        //    Debug.Log("lets go");
        //    applyDribbling();
        //}
        if (!playerMovement.ballisChild && passButton.pressed)
        {
            checkforPlayers();
        }
       

    }

    //public void applyDribbling()
    //{
    //    //transform.position = new Vector3(transform.parent.position.x + 0.8f, 2.095f, transform.parent.position.z);
    //    //transform.position = new Vector3(playerMovement.Direction.x, 2.095f, playerMovement.Direction.z);
    //    //transform.position = Vector3.Lerp(transform.parent.position, playerMovement.Direction, 0.5f);
    //    var dir = new Vector3(playerMovement.fdir.x, 0f, playerMovement.fdir.z).normalized;
    //    Debug.Log("fdir value " + playerMovement.fdir);
    //    BallObject.AddForceAtPosition(dir * 8f, transform.position,ForceMode.Impulse);
    //    //this.transform.SetParent(null);
    //    //BallObject.velocity = new Vector3(Mathf.Lerp(0, 2, t), playerMovement.GetComponent<Rigidbody>().velocity.y, Mathf.Lerp(0, 2, t));
    //}
    GameObject checkforPlayers()
    {
        Collider[] checkPlayers = Physics.OverlapSphere(this.transform.position, 150f, playerDetect);
        float value = float.PositiveInfinity;
        int index = -1;
        for (int i = 0; i < checkPlayers.Length; i++)
        {
            //Collider hit = checkPlayers[i];
            float distance = Vector3.Distance(this.transform.position, checkPlayers[i].transform.position);
            playerBallDistance[i] = distance; 
            if(playerBallDistance[i] < value)
            {
                index = i;
                value = playerBallDistance[i];
                selectedPlayer = checkPlayers[i].gameObject;
            }

        }
        //Debug.Log(" selectedPlayer  " + selectedPlayer.name);
        return selectedPlayer;
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

        if (collision.gameObject.tag == "Team1")
        {
            Team1HasBall = true;
            Team2HasBall = false;
            BallObject.velocity = Vector3.zero;
            // this.transform.SetParent(collision.gameObject.transform);

        }

        //if(collision.gameObject.layer == 11)
        //{
        //    playerMovement.setBallPosition(this.gameObject);
        //}

        if (collision.gameObject.tag == "Team2")
        {
            Team2HasBall = true;
            Team1HasBall = false;
            BallObject.velocity = Vector3.zero;
            //this.transform.SetParent(collision.gameObject.transform);

        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Team1")
        {
            Team1HasBall = true;
            Team2HasBall = false;
            BallObject.velocity = Vector3.zero;
            //this.transform.SetParent(collision.gameObject.transform);
            //Ballparent = collision.gameObject;

        }

        if (collision.gameObject.tag == "Team2")
        {
            Team2HasBall = true;
            Team1HasBall = false;
            BallObject.velocity = Vector3.zero;
            //this.transform.SetParent(collision.gameObject.transform);
          
            //Ballparent = collision.gameObject;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Team1")
        {
       
            Team1HasBall = false;
            Team2HasBall = false;
        }

        if (collision.gameObject.tag == "Team2")
        {
        
            Team1HasBall = false;
            Team2HasBall = false;
            
        }
    }


    public void BallPossession()
    {
        if (this.gameObject.transform.parent == null)
        {
            //Debug.Log("Nobody has possession of ball");
            Team1HasBall = false;
            Team2HasBall = false;
        }

        else if (this.gameObject.transform.parent.tag == "Team1")
        {
            //Debug.Log("Team 1 has ball");
            Team1HasBall = true;
            Team2HasBall = false;
        }

        else if (this.gameObject.transform.parent.tag == "Team2")
        {
            //Debug.Log("Team 2 has ball");
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

    public GameObject ReturnSelectedPlayer()
    {
        return selectedPlayer;
    }


    public void SetSelectedPlayer(GameObject _selectedPlayer)
    {
        selectedPlayer = _selectedPlayer;
    }

   
}
