using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameManager;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
