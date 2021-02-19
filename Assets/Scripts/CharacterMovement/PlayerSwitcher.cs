using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{

    [SerializeField] PlayerMovement characterQ;
    [SerializeField] PlayerMovement characterW;
    [SerializeField] PlayerMovement characterE;
    [SerializeField] PlayerMovement characterR;

    [SerializeField] Cinemachine.CinemachineBrain cameraController;

    private PlayerMovement currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        SetAllPlayersNoControl();

        currentPlayer = characterQ;
        currentPlayer.canControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraController.ActiveVirtualCamera != null && cameraController.ActiveVirtualCamera.Follow != currentPlayer.transform) 
        {
            cameraController.ActiveVirtualCamera.Follow = currentPlayer.transform;
        }

        char player = ' ';
        if(Input.GetKeyDown(KeyCode.Q)) player = 'Q';
        else if(Input.GetKeyDown(KeyCode.W)) player = 'W';
        else if(Input.GetKeyDown(KeyCode.E)) player = 'E';
        else if(Input.GetKeyDown(KeyCode.R)) player = 'R';
        
        if(player != ' ') SwitchPlayer(player);
    }

    private void SwitchPlayer(char player)
    {
        PlayerMovement newPlayer = null;
        switch(player)
        {
            case 'Q':
                newPlayer = characterQ;
                break;
            case 'W':
                newPlayer = characterW;
                break;
            case 'E':
                newPlayer = characterE;
                break;
            case 'R':
                newPlayer = characterR;
                break;
        }
        
        if(newPlayer == null)           return;
        if(newPlayer == currentPlayer)  return;

        SetNewPlayer(newPlayer);
    }

    private void SetNewPlayer(PlayerMovement newPlayer)
    {
        SetAllPlayersNoControl();

        currentPlayer = newPlayer;
        currentPlayer.canControl = true;
    }

    private PlayerMovement GetFirstAvailablePlayer()
    {
        if(characterQ) return characterQ;
        else if(characterW) return characterW;
        else if(characterE) return characterE;
        else if(characterR) return characterR;

        throw new System.NullReferenceException("No Player has a working!");
    }

    private void SetAllPlayersNoControl()
    {
        if(characterQ) characterQ.canControl = false;
        if(characterW) characterW.canControl = false;
        if(characterE) characterE.canControl = false;
        if(characterR) characterR.canControl = false;
    }
}
