using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject afterBall;
    public static TeamAI instance = null;



    private void Awake()
    {
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
        
    }

    public void SetAfterBallEntity()
    {
        afterBall = null;
    }
}
