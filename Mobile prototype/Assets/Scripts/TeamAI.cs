
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject afterBall;
    public static TeamAI instance = null;

    public GameObject[] AITeam;
    public GameObject nearestAI;

    Ball ball;



    private void Awake()
    {

        ball = FindObjectOfType<Ball>();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else if(instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetermineClosestToBall();
    }

    public void SetAfterBallEntity()
    {
        afterBall = null;
    }

    public void DetermineClosestToBall()
    {
        float minDistance = 100;
        foreach(GameObject go in AITeam)
        {
            if(Vector3.Distance(go.transform.position,ball.transform.position) < minDistance)
            {
                minDistance = Vector3.Distance(go.transform.position, ball.transform.position);
                nearestAI = go.gameObject;
            }
        }

        if(nearestAI != null)
        {
            afterBall = nearestAI;
        }
       
    }
}
