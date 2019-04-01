using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 offset;
    public GameObject ball;
    public LayerMask wallLayerMask;
    public GameObject Wall;
    float minDistance = 22.0f;
    float measuredDistance;
    Collider[] hits;
    
    void Start()
    {
        ball = GameObject.FindWithTag("Ball");
        Wall = GameObject.FindWithTag("Boundary");
        offset = this.transform.position - ball.transform.position;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = ball.transform.position + offset;
        transform.position = Vector3.Slerp(transform.position, position, 1.0f);


        hits = Physics.OverlapSphere(this.transform.position, 50f, wallLayerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            Collider hit = hits[i];         
            measuredDistance = Vector3.Distance(hit.transform.position, ball.transform.position);
            //Debug.Log("measured distance " + measuredDistance);
        }
        
        if(measuredDistance < minDistance)
        {
            
            makeTransparent();

        }
        else if(measuredDistance > minDistance && Wall.GetComponent<Renderer>().material.color.a == 0)
        {
            makeOpaque();
        }
    }

    void makeTransparent()
    {
        Color colorToAdjust = Wall.GetComponent<Renderer>().material.color;
        colorToAdjust.a = 0f;
        Wall.GetComponent<Renderer>().material.color = colorToAdjust;
        
    }

    void makeOpaque()
    {
        Color normalColor = Wall.GetComponent<Renderer>().material.color;
        normalColor.a = 255f;
        Wall.GetComponent<Renderer>().material.color = normalColor;
    }
}
