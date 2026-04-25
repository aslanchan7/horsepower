using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class EndScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerPositionText;
    [SerializeField] TransitionUI transitionUI;
    bool isAnimating = true;

    public void ShowEndScreen(int playerPosition)
    {
        if (playerPosition == 1)
        {
            playerPositionText.text = "1st";
        }
        else if (playerPosition == 2)
        {
            playerPositionText.text = "2nd";
        }
        else if (playerPosition == 3)
        {
            playerPositionText.text = "3rd";
        }
        else if (playerPosition == 4)
        {
            playerPositionText.text = "4th";
        }
        else if (playerPosition == 5)
        {
            playerPositionText.text = "5th";
        }

        GetComponent<CanvasGroup>().alpha = 0f;
        isAnimating = true;
        GetComponent<CanvasGroup>().LeanAlpha(1f, 0.5f).setOnComplete(() =>
        {
            isAnimating = false;
        });
    }

    void Update()
    {
        if (Input.anyKeyDown && !isAnimating)
        {
            transitionUI.FadeOut(0);
        }
    }
}
