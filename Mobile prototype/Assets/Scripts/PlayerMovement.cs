using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Joystick joyStick;
    PassButton passButton;
    Rigidbody Player;
    bool passing;
    
    // Start is called before the first frame update
    void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        passButton = FindObjectOfType<PassButton>();
        Player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Player.velocity = new Vector3(joyStick.Horizontal * 10f, Player.velocity.y, joyStick.Vertical * 10f);
        

        if(!passing && passButton.pressed)
        {
            passing = true;
        }

        if (passing && !passButton.pressed)
        {
            passing = false;
        }
    }

    private bool IhaveBall()
    {
        return transform.childCount > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if(ball != null)
        {
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.transform.SetParent(transform);
        }
    }
}
