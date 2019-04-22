using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    ShootButton shootButton;
    Ball ball;
    PlayerMovement playerMovement;
    AIMovement aiMovement;
    // Start is called before the first frame update
    void Start()
    {
        shootButton = FindObjectOfType<ShootButton>();
        ball = FindObjectOfType<Ball>();
        if(this.transform.parent.gameObject.tag =="Team1")
        {
            playerMovement = GetComponentInParent<PlayerMovement>();
        }
        else if (this.transform.parent.gameObject.tag == "Team2")
        {
            aiMovement = GetComponentInParent<AIMovement>();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            if (this.transform.parent.gameObject.tag == "Team1")
            {
                playerMovement.TriggerHandler(gameObject);
            }
            if(this.transform.parent.gameObject.tag == "Team2")
            {
 
                aiMovement.TriggerHandler(gameObject);
            }
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (this.transform.parent.gameObject.tag == "Team1")
            {
                playerMovement.TriggerHandler(gameObject);
            }
            if (this.transform.parent.gameObject.tag == "Team2")
            {

                aiMovement.TriggerHandler(gameObject);
            }
        }
        if (this.transform.parent.gameObject.tag == "Team1")
        {
            if (other.gameObject.tag == "TackleBox" && !playerMovement.ballisChild)
            {

                if (shootButton.bPressed)
                {
                    ball.transform.SetParent(null);
                    ball.transform.SetParent(other.transform.parent);
                    playerMovement.ballisChild = true;
                }

            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (this.transform.parent.gameObject.tag == "Team1")
            {
                playerMovement.TriggerHandlerExit(gameObject);
            }
            if (this.transform.parent.gameObject.tag == "Team2")
            {
                Debug.Log("In here AI exit");
                aiMovement.TriggerHandlerExit(gameObject);
            }
        }
    }
}
