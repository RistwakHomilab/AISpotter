using UnityEngine;
using TMPro;

public class AboutTextManager : MonoBehaviour
{
    public TMP_Text aboutTextField;

    void Start()
    {
        if (aboutTextField != null)
        {
            aboutTextField.text =
                "<b>ABOUT THE GAME</b>\n" +
                "AI Spotter is an educational game that helps players recognize how Artificial Intelligence is used in everyday life.\n\n" +

                "<b>PURPOSE</b>\n" +
                "- Understand the presence of AI in common apps and devices.\n" +
                "- Learn how to distinguish between AI-powered and non-AI technologies.\n" +
                "- Encourage curiosity about technology in daily life.\n\n" +

                "<b>HOW TO PLAY</b>\n" +
                "- A scene will be shown with multiple images (apps, gadgets, tools, etc.).\n" +
                "- A question or prompt will be given.\n" +
                "- Select all images you think are powered by AI.\n" +
                "- You can select as many images as you want — even all of them if you believe they are correct.\n" +
                "- You have a limited time to make your selections.\n" +
                "- Once time runs out, your answers will be automatically submitted.\n\n" +

                "<b>AFTER SUBMISSION</b>\n" +
                "- You will see which of your selected images were correct.\n" +
                "- Learn from the results and improve your understanding of AI.\n\n" +

                "<b>SCORE</b>\n" +
                "- Correct answer: +10 points\n" +
                "- Incorrect answer: -4 points\n\n" +

                "Enjoy the game, think critically, and discover how AI is shaping the world around you.";
        }
    }
}
