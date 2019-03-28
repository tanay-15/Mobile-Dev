using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PortalScript portalScript;
    // Start is called before the first frame update
    void Start()
    {
        portalScript = GetComponentInParent<PortalScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.tag == "Team1" || other.gameObject.tag =="Team2" || other.gameObject.tag =="Ball")
        {
            portalScript.handleTeleportation(this.gameObject , other.gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Team1" || other.gameObject.tag == "Team2" || other.gameObject.tag == "Ball")
        {
            portalScript.handleTeleportation(this.gameObject, other.gameObject);
        }
    }
}
