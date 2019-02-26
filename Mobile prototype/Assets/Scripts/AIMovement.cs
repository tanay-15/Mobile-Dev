using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ball;
    public GameObject goalPost;

    public enum PlayerRole
    {
        Default,
        Forward,
        Midfielder,
        Defender,
        Goalkeeper
    }

    public PlayerRole role;


    public float MoveSpeed;
    private bool HasBall = false;
    private bool MoveTowardsBall = false;

    public Collider[] nearbyColliders;


    void Start()
    {
        ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }


    void Shoot()
    {
        if (HasBall)
        {
            var heading =  goalPost.transform.position - this.transform.position;

            var distance = heading.magnitude;

              var direction = heading / distance;

                Debug.DrawRay(this.transform.position, direction * 20f, Color.blue);

                ball.transform.parent = null;
                ball.GetComponent<Rigidbody>().isKinematic = false;

                ball.GetComponent<Rigidbody>().velocity = direction.normalized * 30f;
                HasBall = false;
            

           
        }
    }

    void Movement()
    {
        if (MoveTowardsBall)
        {
            if (!HasBall)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, ball.transform.position, MoveSpeed * Time.deltaTime);
            }
        }
        
       
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
        
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.transform.SetParent(transform);
            HasBall = true;
            MoveTowardsBall = false;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            MoveTowardsBall = true;
        }

     

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            MoveTowardsBall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            MoveTowardsBall = false;
        }
    }



    //////           Behaviours               
    
    void ForwardBehaviour()
    {


    }

    void MidBehaviour()
    {

    }

    void DefenderBeahviour()
    {

    }

    void GoalieBehaviour()
    {

    }

    void AIPassing()
    {

    }
}
