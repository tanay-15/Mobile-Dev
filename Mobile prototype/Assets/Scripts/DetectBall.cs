using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AIMovement>().BallEnter(other);


    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<AIMovement>().BallExit(other);
    }

    private void OnTriggerStay(Collider other)
    {
        FindObjectOfType<AIMovement>().BallStay(other);
    }
}
