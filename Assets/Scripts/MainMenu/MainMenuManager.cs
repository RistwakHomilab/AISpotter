using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject splashUI;
    public GameObject homeUI;
    public GameObject aboutUI;
    public TMP_Text startButtonText;
    public float splashDuration = 2.5f;
    public float fadeDuration = 1f;

    private CanvasGroup splashCanvasGroup;

    void Start()
    {
        loadingPanel.SetActive(false);
        homeUI.SetActive(false);
        splashUI.SetActive(true);
        aboutUI.SetActive(false);
        if (GameStateManager.Instance != null && GameStateManager.Instance.HasPreviousSession)
        {
            startButtonText.text = "çkjEHk";
            splashUI.SetActive(false);
            homeUI.SetActive(true);
        }
        else
        {
            startButtonText.text = "'kq:";
        }

        splashCanvasGroup = splashUI.GetComponent<CanvasGroup>();
        StartCoroutine(HandleSplash());
    }

    IEnumerator HandleSplash()
    {
        yield return new WaitForSeconds(splashDuration);

        yield return StartCoroutine(FadeOutSplash());

        splashUI.SetActive(false);
        homeUI.SetActive(true);
    }

    IEnumerator FadeOutSplash()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            splashCanvasGroup.alpha = alpha;
            yield return null;
        }

        splashCanvasGroup.alpha = 0f;
    }

    public void OnPlayClicked()
    {
        SoundManager.Instance.PlaySound("Click");
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("Mobile Game");
    }

    public void OnExitClicked()
    {
        SoundManager.Instance.PlaySound("Click");
        Application.Quit();
    }

    public void OnAboutClicked()
    {
        SoundManager.Instance.PlaySound("Click");
        homeUI.SetActive(false);
        aboutUI.SetActive(true);
    }

    public void OnBackClicked()
    {
        SoundManager.Instance.PlaySound("Click");
        aboutUI.SetActive(false);
        homeUI.SetActive(true);
    }
}
