using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    
    [SerializeField] private GameObject cam;
    private List<PlayerMovement> players = new List<PlayerMovement>();

    private void FixedUpdate()
    {
        if(players.Exists(p => p.canControl)) 
            cam.SetActive(true);
        else 
            cam.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
            if(!players.Contains(player)) players.Add(player);
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerMovement player = other.GetComponentInParent<PlayerMovement>();
            players.Remove(player);
        }    
    }

}
