using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Loader loaderScript;
    Joystick joyStick;
    PassButton passButton;
    ShootButton shootButton;
    Rigidbody Player;
    public bool passing = false;
    public bool shooting = false;
    Ball ball;
    float magnitude;
    public Vector3 Direction;
    public bool ballisChild = false;
    private Vector3 warp1pos;
    private Vector3 warp2pos;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        passButton = FindObjectOfType<PassButton>();
        shootButton = FindObjectOfType<ShootButton>();
        Player = GetComponent<Rigidbody>();
        ball = FindObjectOfType<Ball>();
        anim = this.GetComponent<Animator>();
        loaderScript = FindObjectOfType<Loader>();
    }

    // Update is called once per frame
    void Update()
    {
      
          
        
        Vector3 moveVector = (Vector3.right * joyStick.Horizontal + Vector3.forward * joyStick.Vertical);

        anim.SetFloat("SpeedX", joyStick.Horizontal);
        anim.SetFloat("SpeedY", joyStick.Vertical);

        Direction = moveVector;
        if (moveVector != Vector3.zero && loaderScript.CameraFirstHalf.activeSelf)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * 8f * Time.deltaTime, Space.World);
        }
        else if (moveVector != Vector3.zero && loaderScript.CameraSecondHalf.activeSelf)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(-moveVector * 8f * Time.deltaTime, Space.World);
        }



        if (ballisChild)
        {
            var dir = ball.GetComponent<Rigidbody>().transform.position - this.transform.position;
            var dis = dir.magnitude;
            var fdir = dir / dis;
            Debug.Log("drawing ray");
            Debug.Log("Direction : " + fdir);
            Debug.DrawRay(transform.position, fdir * 10f, Color.red);
            if (!passing && passButton.pressed)
            {
                passing = true;
                ball.transform.SetParent(null);
                //ball.transform.parent = null;
                
                //ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.GetComponent<Rigidbody>().AddForce((new Vector3(fdir.x,0,fdir.z).normalized) * 20f, ForceMode.Impulse);
                //ball.GetComponent<Rigidbody>().velocity = Direction.normalized * 20f;
                //ball.GetComponent<Rigidbody>().velocity;
            }

            if(!shooting && shootButton.bPressed)
            {
                shooting = true;
                //ball.transform.parent = null;
                ball.transform.SetParent(null);
                Debug.Log("after drawing ray1");
                //ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.GetComponent<Rigidbody>().AddForce((new Vector3(fdir.x, fdir.y, fdir.z).normalized) * 40f, ForceMode.Impulse);
                //ball.GetComponent<Rigidbody>().velocity = Direction.normalized * 30f;
            }
        }
       

        if (passing && !passButton.pressed)
        {
            passing = false;
        }

        if (shooting && !shootButton.bPressed)
        {
            shooting = false;
        }

        if (ballisChild)
        {
            ball.transform.SetParent(transform);
        }
    }

    public void TriggerHandler(GameObject DribbleBox)
    {      
         //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
         //ball.GetComponent<Rigidbody>().isKinematic = true;
         //ball.GetComponent<Rigidbody>().velocity = Player.velocity;
         //ball.GetComponent<Rigidbody>().AddForce(joyStick.Horizontal, Player.velocity.y, joyStick.Vertical, ForceMode.Force); 
         ball.transform.SetParent(this.transform);
        
        //ball.transform.position = other.transform.position;

        ballisChild = true;      
    }
    //void OnCollisionEnter(Collision other)
    //{     
    //    //if(other.gameObject.layer == 10)
    //    if (other.gameObject.tag == "Ball")
    //    {
           
    //        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        //ball.GetComponent<Rigidbody>().isKinematic = true;
    //        //ball.GetComponent<Rigidbody>().velocity = Player.velocity;
    //        //ball.GetComponent<Rigidbody>().AddForce(joyStick.Horizontal, Player.velocity.y, joyStick.Vertical, ForceMode.Force); 
    //        ball.transform.SetParent(transform);
    //        ballisChild = true;
    //    }

    //}

    public void GetTackled(Vector3 _knockbackForce)
    {
        ball.transform.parent = null;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        _knockbackForce.Normalize();
        Player.AddForce(_knockbackForce * 100f);
    }



    public void TriggerHandlerExit(GameObject DribbleBox)
    {
      
            //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.transform.SetParent(null);
            ballisChild = false;
    }
}
