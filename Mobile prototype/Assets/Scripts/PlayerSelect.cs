using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    Ball ball;
    public List<GameObject> playersinTeam;


// Start is called before the first frame update
void Start()
    {
        ball = FindObjectOfType<Ball>();
        playersinTeam = new List<GameObject>(transform.childCount);
        Debug.Log("children : " + transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<PlayerMovement>().enabled = false;
            playersinTeam.Add(transform.GetChild(i).gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("selectedPlayer in playerselect " + ball.selectedPlayer.name);
        for (int i = 0; i < playersinTeam.Count; i++)
        {
            if (playersinTeam[i].name == ball.selectedPlayer.name)
            {
                playersinTeam[i].gameObject.GetComponent<PlayerMovement>().enabled = true;
            }

            else if (playersinTeam[i].name != ball.selectedPlayer.name)
            {
                playersinTeam[i].gameObject.GetComponent<PlayerMovement>().enabled = false;
            }
        }
    }
}
