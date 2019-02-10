using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance = null;
    public int Team1Score = 0;
    public int Team2Score = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);         /// when we load a new scene all the objects in other scene would be destroyed. We use this to avoid destroying game manager
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

       
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
