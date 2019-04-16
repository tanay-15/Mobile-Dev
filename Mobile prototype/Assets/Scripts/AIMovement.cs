using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Ball ball;
    public GameObject goalPost;

    public TeamAI teamCo;
    public bool WaitAtPosition = false;
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
    public bool MoveTowardsBall = false;

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
       // LeaveControlOfBallDetection();

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
        teamCo.SetAfterBallEntity();
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

    void Pass()
    {
        teamCo.SetAfterBallEntity();
        TransformLookInstant(teamMate.transform.position);
       //this.transform.LookAt(teamMate.transform.position);
        Vector3 _passlocation = teamMate.transform.position;
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
        //if (other.gameObject.tag == "Ball")
        //{
        
        //    ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    //ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //    ball.GetComponent<Rigidbody>().isKinematic = true;
        //    ball.transform.SetParent(transform);
        //    HasBall = true;
        //    MoveTowardsBall = false;
        //}

        if(other.gameObject.tag == "Ball")
        {
            WaitAtPosition = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //if(collision.gameObject.tag == "Ball")
        //{
        //    HasBall = true;
        //}
    }

    public void TriggerHandler(GameObject DribbleBox)
    {
        ball.transform.SetParent(transform);
  
       
       HasBall = true;
    }



    public void TriggerHandlerExit(GameObject DribbleBox)
    {

        //ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.transform.SetParent(null);
        HasBall = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.tag == "Ball")
        //{
          
        //    ball.GetComponent<Rigidbody>().isKinematic = false;
        //   ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //    ball.transform.SetParent(null);
        //    HasBall = false;
        //}
    }

    public void ManualBallDetection()
    {
        if (teamCo.afterBall == null)
        {
           
            if (Vector3.Distance(this.transform.position, ball.transform.position) < 60f)
            {
                MoveTowardsBall = true;

                teamCo.afterBall = this.gameObject;

            }
        }

        if(teamCo.afterBall == this.gameObject)
        {
            MoveTowardsBall = true;
        }

        else
        {
            MoveTowardsBall = false;
        }
       
    }

    public void LeaveControlOfBallDetection()
    {
        if(teamCo.afterBall == this.gameObject)
        {
            if (!HasBall)
            {
                teamCo.afterBall = null;
            }
        }
    }

    public void BallEnter(Collider other)
    {
        //if(other.gameObject.tag == "Ball")
        //{
        //    MoveTowardsBall = true;
        //}

     

    }

    public void BallStay(Collider other)
    {
        //if(other.gameObject.tag == "Ball")
        //{
        //    MoveTowardsBall = true;
        //}
    }

    public void BallExit(Collider other)
    {
        //if(other.gameObject.tag == "Ball")
        //{
        //    MoveTowardsBall = false;
        //}
    }


    public bool DoesPlayerHaveBall()
    {
        return HasBall;
    }

    //////           Behaviours               
    
    void ForwardBehaviour()
    {
        if (!WaitAtPosition)
        {
            if (Team2Possession)
            {
                //Then forward move to forward area for better shot
                this.transform.LookAt(AttackingPosition.transform.position);
                anim.SetBool("Run", true);
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(AttackingPosition.transform.position.x, this.transform.position.y, AttackingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
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
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(DefendingPosition.transform.position.x, this.transform.position.y, DefendingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
                if (Vector3.Distance(this.transform.position, DefendingPosition.transform.position) < 2f)
                {
                    anim.SetBool("Run", false);
                }
            }

            if (!Team1Possession && !Team2Possession && teamCo.afterBall != this.gameObject)
            {
                //Right now hold ground
                if (MoveTowardsBall)
                {
                    TransformLooks(ball.transform.position);
                   // this.transform.LookAt(ball.transform.position);
                    anim.SetBool("Run", true);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), MoveSpeed * Time.deltaTime);

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

            if (teamCo.afterBall == this.gameObject)
            {
                if (!HasBall)
                {
                    TransformLooks(ball.transform.position);
                    //this.transform.LookAt(ball.transform.position);
                    anim.SetBool("Run", true);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), MoveSpeed * Time.deltaTime);

                }

            }





           

        }

        //Everyone will shoot when in certain range

        if (HasBall)
        {
            this.transform.LookAt(goalPost.transform.position);
            anim.SetBool("Run", true);
            this.transform.position = Vector3.MoveTowards(this.transform.position, goalPost.transform.position, (MoveSpeed + 1f) * Time.deltaTime);

            if (Vector3.Distance(goalPost.transform.position, this.transform.position) < 24f)
            {
                Shoot();
            }
        }

    }

   

    void DefenderBeahviour()
    {
        //Defender behaviour
        // will not move much

        //if the player comes in radius, will tackle him and take ball from him
        if (!WaitAtPosition)
        {
            if (teamCo.afterBall == this.gameObject)
            {
                TransformLooks(ball.transform.position);
               // this.transform.LookAt(ball.transform.position);
                anim.SetBool("Run", true);
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), MoveSpeed * Time.deltaTime);

            }

            if (Team1Possession)
            {
                if (MoveTowardsBall)
                {
                    TransformLooks(ball.transform.position);
                    //this.transform.LookAt(ball.transform.position);
                    anim.SetBool("Run", true);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), MoveSpeed * Time.deltaTime);

                    if (Vector3.Distance(this.transform.position, ball.transform.position) < 3f)
                    {

                        ball.transform.parent.gameObject.GetComponent<PlayerMovement>().GetTackled(new Vector3(0f, 5f, -4f));
                        //ball.transform.SetParent(this.gameObject.transform);
                    }
                }
            }

            if (!Team1Possession && !Team2Possession && teamCo.afterBall != this.gameObject)
            {
                //No one has the ball

                if (MoveTowardsBall)
                {
                    TransformLooks(ball.transform.position);
                   //this.transform.LookAt(ball.transform.position);
                    anim.SetBool("Run", true);

                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), MoveSpeed * Time.deltaTime);
                }

                else
                {
                    TransformLooks(DefendingPosition.transform.position);
                    //this.transform.LookAt(DefendingPosition.transform.position);
                    anim.SetBool("Run", true);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(DefendingPosition.transform.position.x, this.transform.position.y, DefendingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
                    Debug.Log("Taking defensive position");
                    if (Vector3.Distance(this.transform.position, DefendingPosition.transform.position) < 2f)
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
                    this.transform.LookAt(goalPost.transform.position);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, goalPost.transform.position, MoveSpeed * Time.deltaTime);
                    teamMate.GetComponent<AIMovement>().StopIdle();
                    Invoke("Pass", 3f);

                }

                else
                {
                    TransformLooks(AttackingPosition.transform.position);
                    //this.transform.LookAt(AttackingPosition.transform.position);
                    anim.SetBool("Run", true);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(AttackingPosition.transform.position.x, this.transform.position.y, AttackingPosition.transform.position.z), MoveSpeed * Time.deltaTime);
                    if (Vector3.Distance(this.transform.position, AttackingPosition.transform.position) < 2f)
                    {
                        anim.SetBool("Run", false);
                    }
                }
            }

        }

    }

    void GoalieBehaviour()
    {
        //Just kick the ball
        
        if (HasBall){
            TransformLooks(ball.transform.position);
            //this.transform.LookAt(ball.transform.position);
            Invoke("GoalieKick", 2f);
        }

        else
        {
            //TransformLooks(AttackingPosition.transform.position);
            if(Vector3.Distance(this.transform.position,ball.transform.position) < 4f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, 0, ball.transform.position.z), 3f * Time.deltaTime);
            }
            this.transform.LookAt(goalPost.transform.position);
        }

    }

    Vector3 CalculateLaunchVelocity(Vector3 _AttackingPosition)
    {
        float displacementY = _AttackingPosition.y - ball.transform.position.y;

        Vector3 displacementXZ = new Vector3(_AttackingPosition.x - ball.transform.position.x, 0, _AttackingPosition.z - ball.transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * h* gravity);
        Vector3 velocityXZ = displacementXZ/(Mathf.Sqrt(-2*h/gravity) + 1.7785f/* Mathf.Sqrt(2*(displacementY-h))/gravity*/);

     
      
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
            ball.GetComponent<Rigidbody>().velocity = CalculateLaunchVelocity(teamMate.transform.position);


            HasBall = false;

            teamCo.SetAfterBallEntity();
        }
   
    }

  
    public void StopIdle()
    {
        //to get the ball

        MoveTowardsBall = false;
        anim.SetBool("Run", false);
        WaitAtPosition = true;
    }

  void TransformLooks(Vector3 _targetLocation)
    {
        Vector3 targetLoc = new Vector3(_targetLocation.x, _targetLocation.y, _targetLocation.z) - this.transform.position;

        Quaternion LookRot = Quaternion.LookRotation(targetLoc, Vector3.up);

        this.transform.rotation = Quaternion.Slerp(transform.rotation, LookRot, Time.deltaTime * 3.0f);
    }

    void TransformLookInstant(Vector3 _targetLocation)
    {
       Vector3 curRot = this.transform.eulerAngles;
        Vector3 targetLoc = new Vector3(Mathf.Clamp(_targetLocation.x,0,24f), _targetLocation.y, _targetLocation.z) - this.transform.position;

        Quaternion LookRot = Quaternion.LookRotation(targetLoc, Vector3.up);
        
        this.transform.rotation = Quaternion.Slerp(transform.rotation, LookRot, Time.deltaTime * 50.0f);
    }
}
