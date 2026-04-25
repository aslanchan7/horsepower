using System;
using UnityEngine;

public class EnemyHorseAI : MonoBehaviour
{
    [SerializeField] float velocity = 0.09f;
    [SerializeField] float fallChance = 0.1f;
    [SerializeField] float fallRecoveryTime = 2.0f;
    [SerializeField] float secondsPerQuery = 1.0f;
    [SerializeField] Animator animator;
    // [SerializeField] Tuple<HorseAnimations, string> horseAnimationStrings;
    private float lastFallTime;
    private bool fallen;
    private float lastQueryTime;
    void Start()
    {
    }

    void Update()
    {
        if (!GameManager.Instance.gameStarted)
        {
            animator.Play($"{transform.name}Stand");
            return;
        }

        if (Time.time - lastQueryTime > secondsPerQuery)
        {
            float random = UnityEngine.Random.Range(0.0f, 1.0f);
            if (random <= fallChance)
            {
                animator.Play($"{transform.name}FallAndGetUp");
                lastFallTime = Time.time;
                fallen = true;
            }

            lastQueryTime = Time.time;
        }

        if (!fallen)
        {
            animator.Play($"{transform.name}Run");
            transform.localPosition += Vector3.right * velocity;
        }
        else
        {
            if (Time.time - lastFallTime > fallRecoveryTime)
            {
                fallen = false;
            }
        }
    }
}

// public enum HorseAnimations
// {
//     Stand,
//     Run,
//     Fall,
//     GetUp
// }