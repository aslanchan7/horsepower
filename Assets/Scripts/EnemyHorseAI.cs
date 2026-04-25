using UnityEngine;

public class EnemyHorseAI : MonoBehaviour
{
    [SerializeField] float velocity = 0.09f;
    [SerializeField] float fallChance = 0.1f;
    [SerializeField] float fallRecoveryTime = 2.0f;
    [SerializeField] float secondsPerQuery = 1.0f;
    private float lastFallTime;
    private bool fallen;
    private float lastQueryTime;
    void Start()
    {
    }

    void Update()
    {
        if (Time.time - lastQueryTime > secondsPerQuery)
        {
            float random = Random.Range(0.0f, 1.0f);
            if (random <= fallChance)
            {
                lastFallTime = Time.time;
                fallen = true;
            }

            lastQueryTime = Time.time;
        }

        if (!fallen)
        {
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
