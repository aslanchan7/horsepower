using System.Collections;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public static HorseController Instance;
    public HorseState HorseState;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float fallenHorseCountdown;
    [SerializeField] float velocity;
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

    }

    void Update()
    {
        if (HorseState == HorseState.Fallen)
        {
            if (Time.time - startHorseCountdown > fallenHorseCountdown)
            {
                HorseState = HorseState.Standing;
            }
        }
        else if (HorseState == HorseState.Running)
        {
            sprite.color = Color.green;
            transform.localPosition += Vector3.right * velocity;
        }
        else if (HorseState == HorseState.Standing)
        {
            sprite.color = Color.white;
        }
    }

    public void HorseFall()
    {
        HorseState = HorseState.Fallen;
        sprite.color = Color.red;
        startHorseCountdown = Time.time;
    }
}

public enum HorseState
{
    Running,
    Fallen,
    Standing
}
