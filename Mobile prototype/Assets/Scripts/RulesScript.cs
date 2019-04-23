using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject startpoint;
    Ball ball;
    void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.transform.position = startpoint.transform.position;
        }
    }
}
