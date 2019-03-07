﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update

  Ball ball;
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

    public GameObject AttackingPosition;
    public GameObject DefendingPosition;


    public float MoveSpeed;
    public bool HasBall = false;
    [SerializeField]
    private bool MoveTowardsBall = false;

    public bool Team2Possession;
    public bool Team1Possession;


    public GameObject teamMate;
 

    


    void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        //Shoot();
        GetBallPossession();


        if (role == PlayerRole.Forward)
        {
            ForwardBehaviour();
        }

        if(role == PlayerRole.Defender)
        {
            DefenderBeahviour();
        }
    }

    void GetBallPossession()
    {
        Team1Possession = ball.GetTeam1possession();
        Team2Possession = ball.GetTeam2possession();
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

    void Pass(Vector3 _passlocation)
    {
        if (HasBall)
        {
            var heading = _passlocation - this.transform.position;

            var distance = heading.magnitude;
            var direction = heading / distance;
            ball.transform.parent = null;
            ball.GetComponent<Rigidbody>().isKinematic = false;

            ball.GetComponent<Rigidbody>().velocity = direction.normalized * 10f;
            HasBall = false;
        }
    }

    void Movement()
    {
        if (MoveTowardsBall && !Team2Possession)
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


    public bool DoesPlayerHaveBall()
    {
        return HasBall;
    }

    //////           Behaviours               
    
    void ForwardBehaviour()
    {
        if (Team2Possession)
        {
            //Then forward move to forward area for better shot
            this.transform.position = Vector3.MoveTowards(this.transform.position, AttackingPosition.transform.position, MoveSpeed * Time.deltaTime);
            
        }

        if (Team1Possession && !MoveTowardsBall)
        {
            //Then move to midfield area to defend and counterattack

            this.transform.position = Vector3.MoveTowards(this.transform.position, DefendingPosition.transform.position, MoveSpeed * Time.deltaTime);
        }

        if(!Team1Possession && !Team2Possession)
        {
            //Right now hold ground
            if (MoveTowardsBall)
            {

                this.transform.position = Vector3.MoveTowards(this.transform.position, ball.transform.position, MoveSpeed * Time.deltaTime);

            }

            else
            {

         
            this.transform.position = Vector3.MoveTowards(this.transform.position, DefendingPosition.transform.position, MoveSpeed * Time.deltaTime);

            }
        }

       

        //Everyone will shoot when in certain range

        if (HasBall)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, goalPost.transform.position, (MoveSpeed + 1f) * Time.deltaTime);

            if(Vector3.Distance(goalPost.transform.position,this.transform.position) < 24f)
            {
                Shoot();
            }
        }
        
    }

    void MidBehaviour()
    {

    }

    void DefenderBeahviour()
    {
        //Defender behaviour
        // will not move much

        //if the player comes in radius, will tackle him and take ball from him

        if (Team1Possession)
        {
            if (MoveTowardsBall)
            {
               
                this.transform.position = Vector3.MoveTowards(this.transform.position, ball.transform.position, MoveSpeed * Time.deltaTime);

                if(Vector3.Distance(this.transform.position,ball.transform.position) < 3f)
                {
                    
                    ball.transform.parent.gameObject.GetComponent<PlayerMovement>().GetTackled(new Vector3(0f, 5f, -4f));
                    //ball.transform.SetParent(this.gameObject.transform);
                }
            }
        }

        if (!Team1Possession && !Team2Possession)
        {
            //No one has the ball

            if (MoveTowardsBall)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, ball.transform.position, MoveSpeed * Time.deltaTime);
            }

            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, DefendingPosition.transform.position, MoveSpeed * Time.deltaTime);
            }
        }

        if (Team2Possession)
        {
            //Team has ball

            if (HasBall)
            {
                //if this character has ball
                Pass(teamMate.transform.position);
                
            }

            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, AttackingPosition.transform.position, MoveSpeed * Time.deltaTime);
            }
        }

    }

    void GoalieBehaviour()
    {

    }

    void AIPassing()
    {

    }
}
