using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBall : MonoBehaviour
{
    // Start is called before the first frame update

    AIMovement aiMovement;
    void Start()
    {
        aiMovement = GetComponentInParent<AIMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AIMovement>().BallEnter(other);

        if(other.gameObject.tag == "Ball")
        {
            aiMovement.MoveTowardsBall = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<AIMovement>().BallExit(other);


        if (other.gameObject.tag == "Ball")
        {
            aiMovement.MoveTowardsBall = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        FindObjectOfType<AIMovement>().BallStay(other);

        if (other.gameObject.tag == "Ball")
        {
            aiMovement.MoveTowardsBall = true;
        }
    }
}
