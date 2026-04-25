using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    bool transition;
    [SerializeField] TransitionUI transitionUI;

    void Update()
    {
        if (!transition)
        {
            if (Input.anyKeyDown)
            {
                // Transition to RaceScene
                transition = true;
                transitionUI.FadeOut(1);
            }
        }
    }
}
