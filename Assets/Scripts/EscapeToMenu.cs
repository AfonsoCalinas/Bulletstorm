using UnityEngine;
using Photon.Pun;  // Make sure you include this if you're using Photon
using UnityEngine.SceneManagement;

public class EscapeToMenu : MonoBehaviourPunCallbacks
{
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LeaveGame();
        }
    }

    // Function to handle leaving the game and returning to the main menu
    public void LeaveGame()
    {
        // Disconnect the player from the room
        PhotonNetwork.LeaveRoom();
    }

    // This is called after successfully leaving the room
    public override void OnLeftRoom()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenuDiff");
    }
}
