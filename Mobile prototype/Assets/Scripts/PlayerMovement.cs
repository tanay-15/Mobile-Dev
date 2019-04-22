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
    GameObject audioManager;
    public bool passing = false;
    public bool shooting = false;
    public LayerMask playerMask;
    public float movspeed;
    public float ballSpeed;
    float step;
    Ball ball;
    float magnitude;
    public Vector3 Direction,fdir;
    public bool ballisChild = false;
    private Vector3 warp1pos;
    private Vector3 warp2pos;
    private Animator anim;
    //RaycastHit hit;
    //List<float> anglePot;


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
        audioManager = GameObject.Find("AudioManager");
         //anglePot= new List<float>();
    }

    // Update is called once per frame
    void Update()
    {


         step = ballSpeed * Time.deltaTime;

        Vector3 moveVector = (Vector3.right * joyStick.Horizontal + Vector3.forward * joyStick.Vertical);

        anim.SetFloat("SpeedX", joyStick.Horizontal);
        anim.SetFloat("SpeedY", joyStick.Vertical);
        Direction = moveVector;
        
       
        
        if (moveVector != Vector3.zero && loaderScript.CameraFirstHalf.activeSelf)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * movspeed * Time.deltaTime, Space.World);
        }
        else if (moveVector != Vector3.zero && loaderScript.CameraSecondHalf.activeSelf)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(-moveVector * movspeed * Time.deltaTime, Space.World);
        }



        if (ballisChild)
        {
            var dir = ball.GetComponent<Rigidbody>().transform.position - this.transform.position;
            var dis = dir.magnitude;
            fdir = dir / dis;
            applyDribbling();
            //Debug.DrawRay(transform.position, fdir * 10f, Color.red);
            Debug.DrawRay(transform.position, Direction * 10f, Color.red);

            //float angle = Mathf.Infinity;
            //Collider[] checkforPlayers = Physics.OverlapSphere(this.transform.position, 150f, playerMask);
            //for (int i = 0; i < checkforPlayers.Length; i++)
            //{

            //    var directiontoPlayer = checkforPlayers[i].transform.position - this.transform.position;
            //    var playerAngle = Vector3.Angle(fdir, directiontoPlayer);
            //    if(playerAngle < angle)
            //    {
            //        angle = playerAngle;
            //    }


            //}
                
            
            if (!passing && passButton.pressed)
            {
                passing = true;
                ball.transform.SetParent(null);
                ball.GetComponent<Rigidbody>().AddForce((new Vector3(fdir.x,0,fdir.z).normalized) * 25f, ForceMode.Impulse);
                audioManager.GetComponent<AudioManager>().Play("Pass");
                
            }

            if(!shooting && shootButton.bPressed)
            {
                shooting = true;              
                ball.transform.SetParent(null);
                ball.GetComponent<Rigidbody>().AddForce((new Vector3(fdir.x, 0, fdir.z).normalized) * 40f, ForceMode.Impulse);
                audioManager.GetComponent<AudioManager>().Play("Pass");
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

    //public void setBallPosition(GameObject ballPostoset)
    //{
    //    Debug.Log("should happen");
    //    Transform dribblebox = this.transform.GetChild(4).transform;

    //    ballPostoset.transform.position = dribblebox.position;
    //}
    public void TriggerHandler(GameObject DribbleBox)
    {
        
        ball.transform.SetParent(transform);
       
        //Debug.Log("Dribble box at pos : " + DribbleBox.transform.position);
        //Debug.Log("ball is at pos : " + ball.transform.position);
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
        //_knockbackForce.Normalize();
        //Player.AddForce(_knockbackForce * 100f);
    }



    public void TriggerHandlerExit(GameObject DribbleBox)
    {
      
            //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.transform.SetParent(null);
            ballisChild = false;
    }

    //AIMovement when player is not selected

    public bool IsBallChild()
    {
        return ballisChild;
    }
   
}
