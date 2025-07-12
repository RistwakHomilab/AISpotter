using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionCard : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Elements")]
    public TMP_Text optionText;
    public Image optionImage;
    public int correctValue = 0;
    public int incorrectValue = 0;

    [Header("Background Sprites")]
    public Sprite defaultImage;
    public Sprite correctImage;
    public Sprite incorrectImage;
    public Sprite selectedImage;

    private string optionId;
    private string optionType;
    private Image bg;

    [HideInInspector] public bool isCorrect = false;
    [HideInInspector] public bool isSelected = false;

    public System.Action<OptionCard> onCardClicked;

    // Global score counter
    public static int totalScore = 0;

    void Awake()
    {
        // Initialize background
        bg = GetComponent<Image>();
        if (bg == null)
        {
            Debug.LogError("OptionCard: Missing Image component on root GameObject.");
        }
        else
        {
            bg.sprite = defaultImage;
        }

        // Display current score at start
        if (OptionCardSpawner.instance != null && OptionCardSpawner.instance.scoreText != null)
            OptionCardSpawner.instance.scoreText.text = $"vad {totalScore}";
    }

    /// <summary>
    /// Initialize the card data.
    /// </summary>
    public void SetupCard(string text, Sprite image, string id, string type)
    {
        optionText.text = text;
        optionImage.sprite = image;
        optionId = id;
        optionType = type;

        isCorrect = (type == "AI");
        isSelected = false;
        bg.sprite = defaultImage;
    }

    /// <summary>
    /// Handle user click toggling selection.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound("Click");
        isSelected = !isSelected;
        bg.sprite = isSelected ? selectedImage : defaultImage;
        onCardClicked?.Invoke(this);
    }

    /// <summary>
    /// Reveal the card's result, update sprite, and adjust score.
    /// </summary>
    public void RevealResult()
    {
        int delta = 0;

        if (isSelected && isCorrect)
        {
            bg.sprite = correctImage;
            delta = correctValue;
        }
        else if (isSelected && !isCorrect)
        {
            bg.sprite = incorrectImage;
            delta = -incorrectValue;
        }
        else
        {
            bg.sprite = defaultImage;
            delta = 0;
        }

        // Update global score
        totalScore += delta;
        if (OptionCardSpawner.instance != null && OptionCardSpawner.instance.scoreText != null)
            OptionCardSpawner.instance.scoreText.text = $"vad {totalScore}";
    }

    /// <summary>
    /// Get this option's unique identifier.
    /// </summary>
    public string GetOptionId()
    {
        return optionId;
    }

    /// <summary>
    /// Programmatically set selection state.
    /// </summary>
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        bg.sprite = isSelected ? selectedImage : defaultImage;
    }
}
