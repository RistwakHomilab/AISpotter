using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionCard : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text optionText;
    public Image optionImage;

    private string optionId;
    private string optionType;
    private Image bg;

    public Sprite defaultImage;
    public Sprite correctImage;
    public Sprite incorrectImage;
    public Sprite selectedImage;

    [HideInInspector] public bool isCorrect = false;
    [HideInInspector] public bool isSelected = false;

    public System.Action<OptionCard> onCardClicked; // Callback to manager

    void Awake()
    {
        // Try to find the background image component
        bg = GetComponent<Image>();

        if (bg == null)
        {
            Debug.LogError("❌ OptionCard: Missing Image component on root GameObject! Please assign manually.");
        }
        else
        {
            bg.sprite = defaultImage;
        }
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        bg.sprite = isSelected ? selectedImage : defaultImage;

        onCardClicked?.Invoke(this);
    }

    public void RevealResult()
    {
        if (isSelected && isCorrect)
            bg.sprite = correctImage;
        else if (isSelected && !isCorrect)
            bg.sprite = incorrectImage;
        else
            bg.sprite = defaultImage;
    }

    public string GetOptionId()
    {
        return optionId;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        bg.sprite = selected ? selectedImage : defaultImage;
    }
}
