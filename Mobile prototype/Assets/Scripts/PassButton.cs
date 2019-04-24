using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class PassButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public bool pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.green;
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //GetComponent<Image>().color = Color.blue;

        GetComponent<Image>().color = new Color(0.0235849f, 0.7081457f, 1);
        pressed = false;
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
