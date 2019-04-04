using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameManager;

    public float GameTimer;

    public GameObject CameraFirstHalf;
    public GameObject CameraSecondHalf;
    public GameObject TimeManager;

    public bool SecondHalf = false;
    public bool MatchEnd = false;
    /*
     * Note****
     * Half Time at 15 secs for testing purpose and 60 for playtesting, In- game value will be updated later on.
     */


    //Positions
    public GameObject AIAreas;
    public GameObject firstHalfPosition;
    public GameObject secondHalfPosition;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        CameraFirstHalf.SetActive(true);
        CameraSecondHalf.SetActive(false);

        GameTimer = TimeManager.GetComponent<CountDown>().GetTimer();

        AIAreas.transform.position = firstHalfPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        GameTimer = TimeManager.GetComponent<CountDown>().GetTimer();

        //First half ends
        if (GameTimer <=0.0f && !SecondHalf)
        {
            AIAreas.transform.position = secondHalfPosition.transform.position;
            AIAreas.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
            //CameraSecondHalf.SetActive(true);
            //CameraFirstHalf.SetActive(false);
            
            TimeManager.GetComponent<CountDown>().SetTimer(480f);
            SecondHalf = true;
        }

        if (SecondHalf)
        {
            Invoke("SecondHalfBegin", 3f);
        }
       
        //Match ends

        if(GameTimer <= 0.0f && MatchEnd)
        {
            Application.Quit();
        }

    }

    void SecondHalfBegin()
    {
        MatchEnd = true;
    }
}
