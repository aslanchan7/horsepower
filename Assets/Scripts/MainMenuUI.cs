using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    bool transition;
    [SerializeField] TransitionUI transitionUI;
    [SerializeField] AudioSource transitionSFX;
    [SerializeField] AudioSource titleMusic;

    void Start()
    {
        titleMusic.Play();
    }
    void Update()
    {
        if (!transition)
        {
            if (Input.anyKeyDown)
            {

                // Transition to RaceScene
                titleMusic.volume = 0.3f;
                transitionSFX.Play();
                transition = true;
                transitionUI.FadeOut(1);
            }
        }
    }
}
