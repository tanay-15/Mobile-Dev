using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Joystick joyStick;
    PassButton passButton;
    Rigidbody Player;
    bool passing;
    Ball ball;

    private Vector3 warp1pos;
    private Vector3 warp2pos;
    // Start is called before the first frame update
    void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        passButton = FindObjectOfType<PassButton>();
        Player = GetComponent<Rigidbody>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Player.velocity = new Vector3(joyStick.Horizontal * 10f, Player.velocity.y, joyStick.Vertical * 10f);
        
        if(IhaveBall())
        {
            Vector3 direction = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical);
            Debug.DrawRay(transform.position, direction * 10f, Color.red);
            if (!passing && passButton.pressed)
            {
                passing = true;
                ball.transform.parent = null;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                Debug.Log("before applying force");
                ball.GetComponent<Rigidbody>().velocity = direction.normalized * 20f;
                //ball.GetComponent<Rigidbody>().velocity;


            }
        }
       

        if (passing && !passButton.pressed)
        {
            passing = false;
        }
    }

    private bool IhaveBall()
    {
        return transform.childCount > 0;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("colliding");
       
    //    if(ball != null)
    //    {
           
    //    }
    //}

    void OnCollisionEnter(Collision other)
    {
        
        //if(other.gameObject.layer == 10)
        if (other.gameObject.tag == "Ball")
        {
            Debug.Log("colliding");
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.transform.SetParent(transform);
        }

     /*   if(other.gameObject.tag == "Warphole")
        {
            if(other.gameObject.name == "WarpHole")
            {
                Debug.Log(other.gameObject.name);
                warp2pos = other.gameObject.transform.parent.GetComponent<WarpHoles>().GetPartnerPosition2();
                this.transform.position = warp2pos;
                
            }

            if(other.gameObject.name == "WarpHole1")
            {
                Debug.Log(other.gameObject.name);
                warp1pos = other.gameObject.transform.parent.GetComponent<WarpHoles>().GetPartnerPosition();
                this.transform.position = warp1pos;
            }

        }*/
    }
}
