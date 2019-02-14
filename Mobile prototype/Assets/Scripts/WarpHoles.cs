using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpHoles : MonoBehaviour
{
    // Start is called before the first frame update
    private RaycastHit hitinfo;

    public GameObject currentplayer;

    public GameObject warp_partner1;
    private GameObject warp_position1;

    public GameObject warp_partner2;
    private GameObject warp_position2;

    public CapsuleCollider warp1collider;
    public CapsuleCollider warp2collider;


    void Start()
    {
        warp_position1 = warp_partner1.transform.GetChild(0).gameObject;
        warp_position2 = warp_partner2.transform.GetChild(0).gameObject;

       
    }

    // Update is called once per frame
    void Update()
    {
        #region Touchcollision
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hitinfo;
           
            if (Physics.Raycast(ray,out hitinfo))
            {
               if(hitinfo.collider.gameObject.tag == "Warphole")
                {
                    //Teleport Red Player to warp position;

                    if(hitinfo.collider.gameObject.name == warp_partner1.gameObject.name)
                    {
                        //go to warp 2

                        currentplayer.transform.position = warp_position2.transform.position;
                    }

                    else if(hitinfo.collider.gameObject.name == warp_partner2.gameObject.name)
                    {

                        //warp to warp1
                        currentplayer.transform.position = warp_position1.transform.position;
                    }
                   
                }
            }
        }
        #endregion


    }


    void MyFunction()
    {
       
    }


    public Vector3 GetPartnerPosition()
    {
        return warp_position1.transform.position;
    }

    public Vector3 GetPartnerPosition2()
    {
        return warp_position2.transform.position;
    }

   

}
