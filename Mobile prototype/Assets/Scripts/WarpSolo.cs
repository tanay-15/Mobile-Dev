using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSolo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetCollided;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        targetCollided = collision.gameObject;
    }

    public void OnCollisionExit(Collision collision)
    {
        targetCollided = null;
    }

    public GameObject GetCollidedObject()
    {
        return targetCollided;
    }
}
