using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpHoles : MonoBehaviour
{
    // Start is called before the first frame update
    private RaycastHit hitinfo;


    public GameObject warp_partner1;
    private GameObject warp_position1;

    public GameObject warp_partner2;
    private GameObject warp_position2;

    public CapsuleCollider warp1collider;
    public CapsuleCollider warp2collider;

    public bool CollidedWithWarp1;
    public bool CollidedWithWarp2;

    public GameObject target1;
    public GameObject target2;


    void Start()
    {
        warp_position1 = warp_partner1.transform.GetChild(0).gameObject;
        warp_position2 = warp_partner2.transform.GetChild(0).gameObject;

       
    }

    // Update is called once per frame
    void Update()
    {
        #region Touchcollision
        /*
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
        */
        #endregion


        MyFunction();

    }


    void MyFunction()
    {
        SetTarget();
        SetTarget2();
        if (target1)
        {
            Warp1To2();
        }

        if (target2)
        {
            Warp2To1();
        }
    }

    void Warp1To2()
    {
        target1.transform.position = warp_position2.transform.position;
    }

    void Warp2To1()
    {
        target2.transform.position = warp_position1.transform.position;
    }

    public Vector3 GetPartnerPosition()
    {
        return warp_position1.transform.position;
    }

    public Vector3 GetPartnerPosition2()
    {
        return warp_position2.transform.position;
    }

    private void SetTarget()
    {
        target1 = warp_partner1.GetComponent<WarpSolo>().GetCollidedObject();
    }

    private void SetTarget2()
    {
        target2 = warp_partner2.GetComponent<WarpSolo>().GetCollidedObject();
    }

}
