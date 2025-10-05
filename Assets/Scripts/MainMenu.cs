using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Name of the scene to load when Play is pressed.")]
    public string gameSceneName;

    [Header("UI Panels")]
    [Tooltip("Credits panel UI.")]
    public GameObject creditsPanel;

    /// <summary>
    /// Called when Play button is clicked
    /// </summary>
    public void PlayGame()
    {
        Debug.Log("Play button pressed!");
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogWarning("Game scene name is empty! Assign it in the Inspector.");
        }
    }

    /// <summary>
    /// Called when Credits button is clicked
    /// </summary>
    public void ShowCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }

    /// <summary>
    /// Called when Back button in credits is clicked
    /// </summary>
    public void HideCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }

    /// <summary>
    /// Called when Quit button is clicked
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit button pressed!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Editor
#else
        Application.Quit(); // Quits the build
#endif
    }
}
