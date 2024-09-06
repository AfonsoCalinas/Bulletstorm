using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This method will be called when the Play button is clicked.
    public void PlayGame()
    {
        // Load the Lobby scene. Replace "Lobby" with the actual scene name.
        SceneManager.LoadScene("CJLobby");
    }

    // This method will be called when the Options button is clicked.
    public void OpenOptions()
    {
        // Load an options scene or open an options panel
        // SceneManager.LoadScene("Options"); // If you have a separate scene
        // Alternatively, enable an options UI panel if it's in the same scene.
    }

    // This method will be called when the Exit button is clicked.
    public void QuitGame()
    {
        // Quits the game. Note: This will only work in a built application, not in the editor.
        Application.Quit();

        // This is useful during development to simulate quitting.
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
