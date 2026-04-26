using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class EndScreenUI : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TransitionUI transitionUI;
    [SerializeField] Sprite[] endScreenSprites;
    bool isAnimating = true;

    public void ShowEndScreen(int playerPosition)
    {
        if (playerPosition == 1)
        {
            background.sprite = endScreenSprites[0];
        }
        else if (playerPosition == 2)
        {
            background.sprite = endScreenSprites[1];
        }
        else if (playerPosition == 3)
        {
            background.sprite = endScreenSprites[2];
        }
        else if (playerPosition == 4)
        {
            background.sprite = endScreenSprites[3];
        }
        else if (playerPosition == 5)
        {
            background.sprite = endScreenSprites[3];
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
