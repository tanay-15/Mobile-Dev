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
    void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        IsBallChild = GetComponent<PlayerMovement>().IsBallChild();
        GetBallPossession();
        AIMovement();
    }

    void GetBallPossession()
    {
        Team1Possession = ball.GetTeam1possession();
        Team2Possession = ball.GetTeam2possession();
    }

    public void AIMovement()
    {
        if (Team1Possession)
        {
            if (!IsBallChild)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(ball.transform.position.x, this.transform.position.y, this.transform.position.z), 4f * Time.deltaTime);
            }
        }
    }
}
