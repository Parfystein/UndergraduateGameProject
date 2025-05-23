using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1"); 
    }

    public void OpenStats()
    {
        Debug.Log("Stats screen not implemented yet.");
    }

    public void QuitGame()
    {
        Application.Quit();

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
