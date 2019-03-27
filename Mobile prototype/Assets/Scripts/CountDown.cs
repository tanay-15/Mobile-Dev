using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField]Text uiText;
    [SerializeField] float mainTimer;
    float timer;
    bool canCount = true;
    bool doOnce = false;
    int seconds, minutes;
    string timerString;
    // Start is called before the first frame update
    void Start()
    {
        timer = mainTimer;
        
    }

    // Update is called once per frame
    void Update()
    {
        seconds = (int)(timer % 60);
        minutes = (int)(timer / 60) % 60;
        if (timer >= 0.0 && canCount)
        {
            timer -= Time.deltaTime * 5;
            timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
            uiText.text = timerString;
            //uiText.text = timer.ToString("F");
        }
        
        else if (timer <=0.0 && !doOnce)
        {
            canCount = false;
            doOnce = true;
            timer = 0.0f;
        }
            

             
    }

    public void SetTimer(float _timer)
    {
        timer = _timer;
    }

    public float GetTimer()
    {
        return timer;
    }
}
