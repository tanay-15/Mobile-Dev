using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ShootButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool bPressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.green;
        bPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //GetComponent<Image>().color = Color.white;
        GetComponent<Image>().color = new Color(1, 0.08018869f, 0.08018869f);
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
