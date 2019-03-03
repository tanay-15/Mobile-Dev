using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShootButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool bPressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        bPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bPressed = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
