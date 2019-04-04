using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Ball ball;
    public GameObject goalPost;

    private Animator anim;

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

    public float h = 20;
    public float gravity = -18;
 

    


    void Start()
    {
        anim = this.GetComponent<Animator>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
     
        GetBallPossession();
        ManualBallDetection();

        if (role == PlayerRole.Forward)
        {
            ForwardBehaviour();
        }

        if(role == PlayerRole.Defender)
        {
            DefenderBeahviour();
        }

        if(role == PlayerRole.Goalkeeper)
        {
            
            GoalieBehaviour();
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

            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            ball.transform.SetParent(null);
            //ball.GetComponent<Rigidbody>().isKinematic = false;

            ball.GetComponent<Rigidbody>().velocity = direction.normalized * 50f;
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
           
          
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            ball.transform.SetParent(null);
            //ball.GetComponent<Rigidbody>().isKinematic = false;

            ball.GetComponent<Rigidbody>().velocity = direction.normalized * 30f;
            HasBall = false;
        }
    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
        
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            ball.transform.SetParent(transform);
            HasBall = true;
            MoveTowardsBall = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            HasBall = true;
        }
    }



    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
          
            ball.GetComponent<Rigidbody>().isKinematic = false;
           ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            ball.transform.SetParent(null);
            HasBall = false;
        }
    }

    public void ManualBallDetection()
    {
        if(Vector3.Distance(this.transform.position,ball.transform.position) < 20f)
        {
            MoveTowardsBall = true;
        }
    }

    public void BallEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            MoveTowardsBall = true;
        }

     

    }

    public void BallStay(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            MoveTowardsBall = true;
        }
    }

    public void BallExit(Collider other)
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
            this.transform.LookAt(AttackingPosition.transform.position);
            anim.SetBool("Run", true);
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(AttackingPosition.transform.position.x,this.transform.position.y,AttackingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, AttackingPosition.transform.position) < 2f)
            {
                anim.SetBool("Run", false);
            }

        }

        if (Team1Possession && !MoveTowardsBall)
        {
            //Then move to midfield area to defend and counterattack
            this.transform.LookAt(DefendingPosition.transform.position);
            anim.SetBool("Run", true);
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(DefendingPosition.transform.position.x,this.transform.position.y,DefendingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, DefendingPosition.transform.position) < 2f)
            {
                anim.SetBool("Run", false);
            }
        }

        if(!Team1Possession && !Team2Possession)
        {
            //Right now hold ground
            if (MoveTowardsBall)
            {
                this.transform.LookAt(ball.transform.position);
                anim.SetBool("Run", true);
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x,this.transform.position.y,ball.transform.position.z), MoveSpeed * Time.deltaTime);

            }

            else
            {

                this.transform.LookAt(DefendingPosition.transform.position);
                anim.SetBool("Run", true);
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(DefendingPosition.transform.position.x, this.transform.position.y, DefendingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, DefendingPosition.transform.position) < 2f)
                {
                    anim.SetBool("Run", false);
                }

            }
        }

       

        //Everyone will shoot when in certain range

        if (HasBall)
        {
            this.transform.LookAt(goalPost.transform.position);
            anim.SetBool("Run", true);
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
                this.transform.LookAt(ball.transform.position);
                anim.SetBool("Run", true);
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), MoveSpeed * Time.deltaTime);

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
                this.transform.LookAt(ball.transform.position);
                anim.SetBool("Run", true);
                Debug.Log("Found ball");
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), MoveSpeed * Time.deltaTime);
            }

            else
            {
                this.transform.LookAt(DefendingPosition.transform.position);
                anim.SetBool("Run", true);
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(DefendingPosition.transform.position.x, this.transform.position.y, DefendingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
                Debug.Log("Taking defensive position");
                if(Vector3.Distance(this.transform.position,DefendingPosition.transform.position) < 2f)
                {
                    anim.SetBool("Run", false);
                }
            }
        }

        if (Team2Possession)
        {
            //Team has ball

            if (HasBall)
            {
                //if this character has ball
                Debug.Log("Passing ball");
                Pass(teamMate.transform.position);
                
            }

            else
            {
                this.transform.LookAt(AttackingPosition.transform.position);
                anim.SetBool("Run", true);
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(AttackingPosition.transform.position.x, this.transform.position.y, AttackingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, AttackingPosition.transform.position) < 2f)
                {
                    anim.SetBool("Run", false);
                }
            }
        }

    }

    void GoalieBehaviour()
    {
        //Just kick the ball

        if (HasBall){
            GoalieKick();
        }

    }

    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = AttackingPosition.transform.position.y - ball.transform.position.y;

        Vector3 displacementXZ = new Vector3(AttackingPosition.transform.position.x - ball.transform.position.x, 0, AttackingPosition.transform.position.z - ball.transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * h* gravity);
        Vector3 velocityXZ = displacementXZ/(Mathf.Sqrt(-2*h/gravity) + 1.7785f/* Mathf.Sqrt(2*(displacementY-h))/gravity*/);

        //Debug.Log(Mathf.Sqrt(-2 * h / gravity));
        //Debug.Log(Mathf.Sqrt(2 * (displacementY - h)) / gravity);
        //Debug.Log(displacementY);
        //Debug.Log(velocityXZ);
      
        return velocityXZ + velocityY; 
    }

    void GoalieKick()
    {
        if (HasBall)
        {
           


            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            ball.transform.SetParent(null);

            Physics.gravity = Vector3.up * gravity;
            ball.GetComponent<Rigidbody>().velocity = CalculateLaunchVelocity();


            HasBall = false;
        }
   
    }

  

    void AIPassing()
    {

    }
}
