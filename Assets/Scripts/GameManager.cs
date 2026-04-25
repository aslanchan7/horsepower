using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float startGameCooldown = 3.0f;
    public bool gameStarted = false;


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
        if (startGameCooldown > 0.0f)
        {
            startGameCooldown -= Time.deltaTime;
        }
        else
        {
            gameStarted = true;
        }
    }
}
