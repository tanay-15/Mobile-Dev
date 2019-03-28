using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            playerMovement.TriggerHandler(gameObject);
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            playerMovement.TriggerHandler(gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            playerMovement.TriggerHandlerExit(gameObject);
        }
    }
}
