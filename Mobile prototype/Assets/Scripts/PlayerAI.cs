using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    // Start is called before the first frame update
    Ball ball;
    public bool Team1Possession;
    public bool Team2Possession;
    public bool IsBallChild;

    public GameObject currentPlayer;

    private Animator anim;
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsBallChild = GetComponent<PlayerMovement>().IsBallChild();
        GetBallPossession();
        AI();
    }

    void GetBallPossession()
    {
        Team1Possession = ball.GetTeam1possession();
        Team2Possession = ball.GetTeam2possession();
    }

    
    public void AI()
{
        currentPlayer = ball.GetComponent<Ball>().ReturnSelectedPlayer();

        if(currentPlayer != this.gameObject)
        {
            //As long as this player is not

            if (Team1Possession)
            {
                if (!IsBallChild)
                {
                    anim.SetBool("Run", true);
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, this.transform.position.z), 4f * Time.deltaTime);
                }
            }

            if (!Team1Possession)
            {
                if(Vector3.Distance(ball.transform.position, this.transform.position) < 12f)
                {
                    anim.SetBool("Run", true);
                    this.transform.LookAt(new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z));
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, ball.transform.position.z), 6f * Time.deltaTime);
                }
            }

            if (IsBallChild)
            {
                ball.GetComponent<Ball>().SetSelectedPlayer(this.gameObject);
            }
        }
    }
}
