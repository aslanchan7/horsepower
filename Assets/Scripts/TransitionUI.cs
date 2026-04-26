using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] CanvasGroup transitionOverlay;
    [SerializeField] float transitionTime = 1.0f;

    void Start()
    {
        transitionOverlay.alpha = 1f;
        transitionOverlay.LeanAlpha(0f, transitionTime);
    }

    public void FadeOut(int sceneIndex)
    {
        transitionOverlay.alpha = 0f;
        transitionOverlay.LeanAlpha(1f, transitionTime).setOnComplete(() =>
        {
            if (sceneIndex != -1)
            {
                SceneManager.LoadScene(sceneIndex);
            }
        });
    }
}
