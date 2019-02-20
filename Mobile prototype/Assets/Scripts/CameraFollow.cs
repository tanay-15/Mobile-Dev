using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 offset;
    public GameObject ball;

    void Start()
    {
        ball = GameObject.Find("Ball");
        offset = this.transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = ball.transform.position + offset;
        transform.position = Vector3.Slerp(transform.position, position, 1.0f);
    }
}
