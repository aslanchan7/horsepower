using System;
using System.Collections;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public static HorseController Instance;
    public HorseState HorseState;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float fallenHorseCountdown;
    [SerializeField] private AudioClip horseFallClip;
    public float InitialVelocity = 0.1f;
    public float CurrentVelocity;
    [SerializeField] Animator animator;
    private float startHorseCountdown;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        CurrentVelocity = InitialVelocity;
    }

    void Update()
    {
        if (HorseState == HorseState.Fallen)
        {
            if (Time.time - startHorseCountdown > fallenHorseCountdown)
            {
                animator.Play("PlayerHorseGetUp");
                StartCoroutine(WaitForAnimEnd("PlayerHorseGetUp"));
                AudioManager.instance.HorseGetUp();
            }
        }
        else if (HorseState == HorseState.Running)
        {
            animator.Play("PlayerHorseRun");
            transform.localPosition += Vector3.right * CurrentVelocity;
        }
        else if (HorseState == HorseState.Standing)
        {
            animator.Play("PlayerHorseStand");
        }
    }

    public void HorseFall()
    {
        
        ChangeState(HorseState.Fallen);
        animator.Play("PlayerHorseFall");
        startHorseCountdown = Time.time;
        CurrentVelocity = InitialVelocity;
        AudioManager.instance.HorseFall();
    }

    IEnumerator WaitForAnimEnd(string stateName)
    {
        // Wait until we're actually in the state
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            yield return null;

        // Wait until the animation finishes
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        ChangeState(HorseState.Standing);
    }

    private void ChangeState(HorseState state)
    {
        HorseState = state;
    }
}

public enum HorseState
{
    Running,
    Fallen,
    Standing
}
