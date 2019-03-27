using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameManager;

    public float GameTimer;

    public GameObject CameraFirstHalf;
    public GameObject CameraSecondHalf;
    public GameObject TimeManager;

    /*
     * Note****
     * Half Time at 15 secs for testing purpose and 60 for playtesting, In- game value will be updated later on.
     */

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        CameraFirstHalf.SetActive(true);
        CameraSecondHalf.SetActive(false);

        GameTimer = TimeManager.GetComponent<CountDown>().GetTimer();
    }

    // Update is called once per frame
    void Update()
    {


        GameTimer = TimeManager.GetComponent<CountDown>().GetTimer();

        if (GameTimer <=0.0f)
        {
            CameraSecondHalf.SetActive(true);
            CameraFirstHalf.SetActive(false);

            TimeManager.GetComponent<CountDown>().SetTimer(300f);
        }

        //GameTimer = GameTimer + Time.deltaTime;



    }
}
