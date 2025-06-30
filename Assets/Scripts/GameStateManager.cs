using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public bool HasPreviousSession { get; private set; }
    public bool isResuming { get; set; } = false;
    public string LastSceneName { get; private set; }

    [Header("Persistent State")]
    public List<string> currentQuestionIDs = new List<string>();
    public List<bool> selectedStates = new List<bool>();
    public float remainingTime;
    public bool resumeMode = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveCurrentState()
    {
        // Hook this into your QuestionRoundManager
        QuestionRoundManager round = FindObjectOfType<QuestionRoundManager>();
        if (round != null)
        {
            round.ExtractGameState(out currentQuestionIDs, out selectedStates, out remainingTime);
            resumeMode = true;
            HasPreviousSession = true;
        }
    }

    public void ClearState()
    {
        currentQuestionIDs.Clear();
        selectedStates.Clear();
        remainingTime = 0f;
        resumeMode = false;
    }
}
