using TMPro;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float fadeOutTime;

    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
        countdownText.text = "3";
    }

    void Update()
    {
        if (GameManager.Instance.startGameCooldown <= 0.0f)
        {
            countdownText.text = "GO!";
            FadeOut();
        }
        else if (GameManager.Instance.startGameCooldown <= 1.0f)
        {
            countdownText.text = "1";
        }
        else if (GameManager.Instance.startGameCooldown <= 2.0f)
        {
            countdownText.text = "2";
        }
    }

    void FadeOut()
    {
        GetComponent<CanvasGroup>().LeanAlpha(0f, fadeOutTime).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
