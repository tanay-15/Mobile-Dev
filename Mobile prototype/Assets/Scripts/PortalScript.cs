using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    PlayerMovement playerMovement;
    public List<GameObject> totalPortals;
    List<int> numberPot;
    Color[] colors = new Color[3];
    public float timer = 0.0f;
    int randomnumberIndex, randomnumberIndex1, chosenNumber;
    // Start is called before the first frame update
    void Start()
    {
        colors[0] = Color.cyan;
        colors[1] = Color.red;
        colors[2] = Color.green;

        totalPortals = new List<GameObject>();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            totalPortals.Add(transform.GetChild(i).gameObject);
          
        }
        numberPot = new List<int>();

        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;
        if (timer >= 5.0f)
        {
            for (int j = 0; j <= 5; j++)
            {
                numberPot.Add(j);
            }
            for (int i = 0; i < colors.Length; i++)
            {
                generateRandomness();
                totalPortals[chosenNumber].gameObject.GetComponent<Renderer>().material.color = colors[i];
                generateRandomness();
                totalPortals[chosenNumber].gameObject.GetComponent<Renderer>().material.color = colors[i];

            }
            timer = 0;
        }

    }

    void generateRandomness()
    {
        
        randomnumberIndex = Random.Range(0, numberPot.Count);
        chosenNumber = numberPot[randomnumberIndex];
        numberPot.RemoveAt(randomnumberIndex);
    }

    public void handleTeleportation(GameObject portalCollider, GameObject objColliding)
    {
        for(int i = 0; i < totalPortals.Count; i++)
        {
            if(portalCollider.GetComponent<Renderer>().material.color == totalPortals[i].gameObject.GetComponent<Renderer>().material.color)
            {
                objColliding.transform.position = totalPortals[i].gameObject.transform.position + playerMovement.Direction.normalized * 2; 
            }
        }
    }
}
