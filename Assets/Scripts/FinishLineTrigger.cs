using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public int playerPosition = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HorseController horseController))
        {
            horseController.HorseState = HorseState.Standing;
            Leaderboard.Instance.ShowEndScreen();
        }
        else if (other.TryGetComponent(out EnemyHorseAI enemyHorse))
        {
            playerPosition++;
        }
    }
}
