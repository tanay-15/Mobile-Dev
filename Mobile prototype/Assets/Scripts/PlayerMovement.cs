using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Joystick joyStick;
    PassButton passButton;
    ShootButton shootButton;
    Rigidbody Player;
    bool passing = false;
    bool shooting = false;
    Ball ball;
    float magnitude;
    public Vector3 Direction;
    public bool ballisChild = false;
    private Vector3 warp1pos;
    private Vector3 warp2pos;
    // Start is called before the first frame update
    void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        passButton = FindObjectOfType<PassButton>();
        shootButton = FindObjectOfType<ShootButton>();
        Player = GetComponent<Rigidbody>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveVector = (Vector3.right * joyStick.Horizontal + Vector3.forward * joyStick.Vertical);
        Direction = moveVector;
        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * 8f * Time.deltaTime, Space.World);
        }
        //Player.velocity = new Vector3(joyStick.Horizontal * 10f, 0, joyStick.Vertical * 10f);
        //Player.transform.Rotate(0, joyStick.Horizontal * 0.1f,0);
        Debug.Log("Player.velocity.y " + Player.velocity.y);
        
        if(ballisChild)
        {
            
            Debug.Log("drawing ray");
            Debug.DrawRay(transform.position, Direction.normalized * 10f, Color.red);
            if (!passing && passButton.pressed)
            {
                passing = true;
                ball.transform.parent = null;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.GetComponent<Rigidbody>().velocity = Direction.normalized * 20f;
                //ball.GetComponent<Rigidbody>().velocity;
            }

            if(!shooting && shootButton.bPressed)
            {
                shooting = true;
                ball.transform.parent = null;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.GetComponent<Rigidbody>().velocity = Direction.normalized * 30f;
            }
        }
       

        if (passing && !passButton.pressed)
        {
            passing = false;
        }

        if(ballisChild)
        {
            ball.transform.SetParent(transform);
        }
    }

    public bool IhaveBall()
    {
        return transform.childCount > 2;
    }

    void OnCollisionEnter(Collision other)
    {     
        //if(other.gameObject.layer == 10)
        if (other.gameObject.tag == "Ball")
        {
           
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //ball.GetComponent<Rigidbody>().isKinematic = true;
            //ball.GetComponent<Rigidbody>().velocity = Player.velocity;
            //ball.GetComponent<Rigidbody>().AddForce(joyStick.Horizontal, Player.velocity.y, joyStick.Vertical, ForceMode.Force); 
            ball.transform.SetParent(transform);
            ballisChild = true;
        }

    }

    public void GetTackled(Vector3 _knockbackForce)
    {
        ball.transform.parent = null;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        _knockbackForce.Normalize();
        Player.AddForce(_knockbackForce * 100f);
    }



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.transform.SetParent(null);
            ballisChild = false;
        }
    }
}
