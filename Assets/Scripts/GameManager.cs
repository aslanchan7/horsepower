using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float startGameCooldown = 5.0f;
    public bool gameStarted = false;
    private bool countdownSFXStarted = false;


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
        if (startGameCooldown <= 3.05f && countdownSFXStarted == false)
        {
            Debug.Log("start SFX");
            countdownSFXStarted = true;
            AudioManager.instance.StartRace();
        }
        if (startGameCooldown > 0.0f)
        {
            startGameCooldown -= Time.deltaTime;
        }
        else
        {
            gameStarted = true;
            AudioManager.instance.raceStarted = true;
        }
    }
}
