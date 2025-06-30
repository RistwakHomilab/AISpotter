using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject mainMenuPanel;
    public GameObject exitPanel; // Assign in inspector

    void Start()
    {
        mainMenuPanel.SetActive(true);
        loadingPanel.SetActive(false);
        exitPanel.SetActive(false); // Hide exit panel initially
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Android back button
        {
            if (!exitPanel.activeSelf)
                exitPanel.SetActive(true);
        }

        if (exitPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
    }

    public void OnExitYes()
    {
        Application.Quit();
        Debug.Log("Game closed.");
    }

    public void OnExitNo()
    {
        exitPanel.SetActive(false);
        Time.timeScale = 1f; // Resume game if exit panel is closed
    }

    public void OnNavigateBackToMainMenu()
    {
        GameStateManager.Instance.SaveCurrentState();

        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        StartCoroutine(LoadMainMenuAfterDelay());
    }

    private IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
