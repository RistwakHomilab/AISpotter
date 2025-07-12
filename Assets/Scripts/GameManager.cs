using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject mainMenuPanel;
    public GameObject exitPanel; // Assign in inspector
    public GameObject comingSoonPanel;

    bool isComingSoonActive = false;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        loadingPanel.SetActive(false);
        exitPanel.SetActive(false); // Hide exit panel initially
    }

    void Update()
    {
        if(comingSoonPanel.activeSelf)
            isComingSoonActive = false;
        if (Input.GetKeyDown(KeyCode.Escape)) // Android back button
        {
            if (!exitPanel.activeSelf)
            {
                exitPanel.SetActive(true);
                comingSoonPanel.SetActive(false);
            }
        }

        if (exitPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
    }

    public void OnExitYes()
    {
        SoundManager.Instance.PlaySound("Click");
        Application.Quit();
        Debug.Log("Game closed.");
    }

    public void OnExitNo()
    {
        SoundManager.Instance.PlaySound("Click");
        if(isComingSoonActive)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            exitPanel.SetActive(false);
        }
        Time.timeScale = 1f; // Resume game if exit panel is closed
    }

    public void OnNavigateBackToMainMenu()
    {
        SoundManager.Instance.PlaySound("Click");
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
