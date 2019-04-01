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
    public Vector3 Direction,fdir;
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
            fdir = dir / dis;
            applyDribbling();
            Debug.DrawRay(transform.position, fdir * 10f, Color.red);
            if (!passing && passButton.pressed)
            {
                passing = true;
                ball.transform.SetParent(null);
                ball.GetComponent<Rigidbody>().AddForce((new Vector3(fdir.x,0,fdir.z).normalized) * 20f, ForceMode.Impulse);
                
            }

            if(!shooting && shootButton.bPressed)
            {
                shooting = true;              
                ball.transform.SetParent(null);
                ball.GetComponent<Rigidbody>().AddForce((new Vector3(fdir.x, 0, fdir.z).normalized) * 40f, ForceMode.Impulse);
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

    }

    public void TriggerHandler(GameObject DribbleBox)
    {
        ball.transform.SetParent(transform);
        ballisChild = true;    
    }


    void applyDribbling()
    {
        
        //var dir = new Vector3(fdir.x, 0f, fdir.z).normalized;
        //Debug.Log("fdir value " + fdir);
        //ball.GetComponent<Rigidbody>().AddForceAtPosition(dir * 0.5f, ball.GetComponent<Rigidbody>().transform.position, ForceMode.Impulse);
        
    }
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
