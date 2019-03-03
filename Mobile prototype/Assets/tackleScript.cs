using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tackleScript : MonoBehaviour
{
    ShootButton shootButton;
    Ball ball;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        shootButton = FindObjectOfType<ShootButton>();
        ball = FindObjectOfType<Ball>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "TackleBox" && !playerMovement.ballisChild)
        {
            Debug.Log("should be here");
            if (shootButton.bPressed)
            {
                ball.transform.SetParent(other.transform.parent);
                playerMovement.ballisChild = true;
            }
        }
    }
}
