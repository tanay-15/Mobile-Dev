using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPost : MonoBehaviour
{
    // Start is called before the first frame update
    

    public Text scoreText;
    private int score;
    private int score2;


    [SerializeField]
    private GameObject gameBall;

    public GameObject[] teamMembers;
  

    void Start()
    {
        gameBall = GameObject.Find("Ball");
        score = GameManager.instance.Team1Score;
        score2 = GameManager.instance.Team2Score;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "Team1GoalPost")
        {
            scoreText.text = "Team 1 Score is " + GameManager.instance.Team1Score;
        }

        else if(this.gameObject.tag == "Team2GoalPost")
        {
            scoreText.text = "Team 2 Score is " + GameManager.instance.Team2Score;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            if(this.gameObject.tag == "Team1GoalPost")
            {
                score = GameManager.instance.Team1Score++;
                //Elimination();
            }

            else if(this.gameObject.tag == "Team2GoalPost")
            {
                score2 = GameManager.instance.Team2Score++;
                //Elimination();
            }
        }
    }


    public void Elimination()
    {
        if(teamMembers.Length > 0)
        {
            int selectedElement = Random.Range(0, teamMembers.Length);
           
            Destroy(teamMembers[selectedElement]);
        }
    }

  
}
