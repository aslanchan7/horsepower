using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public int playerPosition = 1;
    [SerializeField] Canvas endScreenUI;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out HorseController horseController))
        {
            horseController.HorseState = HorseState.Running;
            endScreenUI.gameObject.SetActive(true);
            endScreenUI.GetComponent<EndScreenUI>().ShowEndScreen(playerPosition);
            GameManager.Instance.gameStarted = false;
        }
        else if (other.TryGetComponent(out EnemyHorseAI enemyHorse))
        {
            playerPosition++;
        }
    }
}
