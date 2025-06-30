using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionRoundManager : MonoBehaviour
{
    [Header("Round Settings")]
    public float roundTime = 10f;
    public float waitAfterResult = 3f;

    [Header("UI References")]
    public Image timerFillImage;
    public GameObject optionContainer;
    public OptionCardSpawner spawner;

    private float timeRemaining;
    private bool roundActive = false;

    private List<OptionCard> selectedCards = new List<OptionCard>();

    void Start()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.resumeMode)
        {
            ResumeGame(); // ✅ Call this instead of StartNewRound
        }
        else
        {
            StartNewRound();
        }
    }

    void Update()
    {
        if (roundActive)
        {
            timeRemaining -= Time.deltaTime;
            timerFillImage.fillAmount = timeRemaining / roundTime;

            if (timeRemaining <= 0)
            {
                roundActive = false;
                EndRound();
            }
        }
    }

    void StartNewRound()
    {
        // Clear previous round
        foreach (Transform child in optionContainer.transform)
        {
            Destroy(child.gameObject);
        }

        selectedCards.Clear();

        // Spawn new cards
        spawner.SpawnOptions(HandleCardClick);

        timeRemaining = roundTime;
        timerFillImage.fillAmount = 1f;
        roundActive = true;
    }

    void HandleCardClick(OptionCard card)
    {
        if (card.isSelected)
        {
            if (!selectedCards.Contains(card))
                selectedCards.Add(card);
        }
        else
        {
            if (selectedCards.Contains(card))
                selectedCards.Remove(card);
        }
    }

    void EndRound()
    {
        foreach (Transform child in optionContainer.transform)
        {
            OptionCard card = child.GetComponent<OptionCard>();
            if (card != null)
                card.RevealResult();
        }

        StartCoroutine(NextRoundDelay());
    }

    IEnumerator NextRoundDelay()
    {
        yield return new WaitForSeconds(waitAfterResult);
        StartNewRound();
    }

    public void ExtractGameState(out List<string> ids, out List<bool> selections, out float timeLeft)
    {
        ids = new List<string>();
        selections = new List<bool>();

        foreach (Transform child in optionContainer.transform)
        {
            OptionCard card = child.GetComponent<OptionCard>();
            if (card != null)
            {
                ids.Add(card.GetOptionId());
                selections.Add(card.isSelected);
            }
        }

        timeLeft = timeRemaining;
    }

    public void ResumeGame()
    {
        // Clear existing cards
        foreach (Transform child in optionContainer.transform)
        {
            Destroy(child.gameObject);
        }

        selectedCards.Clear();

        // Load data from GameStateManager
        var state = GameStateManager.Instance;
        spawner.SpawnOptionsWithPreset(state.currentQuestionIDs, state.selectedStates, HandleCardClick);

        timeRemaining = state.remainingTime;
        timerFillImage.fillAmount = timeRemaining / roundTime;
        roundActive = true;
    }
}