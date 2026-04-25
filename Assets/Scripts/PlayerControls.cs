using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    public Queue<KeyCode> player1KeySequence;
    public Queue<KeyCode> player2KeySequence;
    List<KeyCode> player1MoveKeys = new();
    List<KeyCode> player2MoveKeys = new();
    float waitForPlayer1;
    float waitForPlayer2;
    bool player1Hit = false;
    bool player2Hit = false;
    public int upcomingKeySequenceCount = 4;
    bool skipThisInput;
    private float lastSuccessfulInputTime;
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float timeBeforeStanding = 1.5f;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    public int consecutiveSuccessfulInput = 0;

    void Awake()
    {
        player1MoveKeys.Add(KeyCode.Alpha1);
        player1MoveKeys.Add(KeyCode.Alpha2);
        player1MoveKeys.Add(KeyCode.Alpha3);
        player1MoveKeys.Add(KeyCode.Alpha4);

        player2MoveKeys.Add(KeyCode.Alpha7);
        player2MoveKeys.Add(KeyCode.Alpha8);
        player2MoveKeys.Add(KeyCode.Alpha9);
        player2MoveKeys.Add(KeyCode.Alpha0);

        player1KeySequence = new(upcomingKeySequenceCount);
        player2KeySequence = new(upcomingKeySequenceCount);

        waitForPlayer1 = 0.0f;
        waitForPlayer2 = 0.0f;
    }

    void Start()
    {
        GenerateKeySeqQueue();
    }

    void Update()
    {
        // Fill keySequenceQueue if not full
        GenerateKeySeqQueue();

        // TODO: Move this into a different script
        // Right now, this prints the queue onto the UI
        PrintArray();

        // Disable inputs if horse is fallen
        if (HorseController.Instance.HorseState == HorseState.Fallen)
            return;

        // Disable inputs if game has not started
        if (!GameManager.Instance.gameStarted)
            return;

        // Take input & logic
        CheckPlayer1Input();
        CheckPlayer2Input();

        ManageInput();
    }

    void PrintArray()
    {
        KeyCode[] arr1 = player1KeySequence.ToArray();
        KeyCode[] arr2 = player2KeySequence.ToArray();
        text1.text = $"Player 1: {arr1[0]}, {arr1[1]}, {arr1[2]}, {arr1[3]}";
        text2.text = $"Player 2: {arr2[0]}, {arr2[1]}, {arr2[2]}, {arr2[3]}";
    }

    void GenerateKeySeqQueue()
    {
        while (player1KeySequence.Count != upcomingKeySequenceCount)
        {
            int rand = Random.Range(0, 4);
            KeyCode randomKey = player1MoveKeys[rand];
            player1KeySequence.Enqueue(randomKey);
        }

        while (player2KeySequence.Count != upcomingKeySequenceCount)
        {
            int rand = Random.Range(0, 4);
            KeyCode randomKey = player2MoveKeys[rand];
            player2KeySequence.Enqueue(randomKey);
        }
    }

    void CheckPlayer1Input()
    {
        if (player1Hit == true)
        {
            waitForPlayer2 += Time.deltaTime;
        }

        KeyCode player1NextKey = player1KeySequence.Peek();
        if (Input.GetKeyDown(player1MoveKeys[0]))
        {
            if (player1NextKey != player1MoveKeys[0])
            {
                skipThisInput = true;
                return;
            }

            player1Hit = true;
        }
        else if (Input.GetKeyDown(player1MoveKeys[1]))
        {
            if (player1NextKey != player1MoveKeys[1])
            {
                skipThisInput = true;
                return;
            }

            player1Hit = true;
        }
        else if (Input.GetKeyDown(player1MoveKeys[2]))
        {
            if (player1NextKey != player1MoveKeys[2])
            {
                skipThisInput = true;
                return;
            }

            player1Hit = true;
        }
        else if (Input.GetKeyDown(player1MoveKeys[3]))
        {
            if (player1NextKey != player1MoveKeys[3])
            {
                skipThisInput = true;
                return;
            }

            player1Hit = true;
        }
    }

    void CheckPlayer2Input()
    {
        if (player2Hit == true)
        {
            waitForPlayer1 += Time.deltaTime;
        }

        KeyCode player2NextKey = player2KeySequence.Peek();
        if (Input.GetKeyDown(player2MoveKeys[0]))
        {
            if (player2NextKey != player2MoveKeys[0])
            {
                skipThisInput = true;
                return;
            }

            player2Hit = true;
        }
        else if (Input.GetKeyDown(player2MoveKeys[1]))
        {
            if (player2NextKey != player2MoveKeys[1])
            {
                skipThisInput = true;
                return;
            }

            player2Hit = true;
        }
        else if (Input.GetKeyDown(player2MoveKeys[2]))
        {
            if (player2NextKey != player2MoveKeys[2])
            {
                skipThisInput = true;
                return;
            }

            player2Hit = true;
        }
        else if (Input.GetKeyDown(player2MoveKeys[3]))
        {
            if (player2NextKey != player2MoveKeys[3])
            {
                skipThisInput = true;
                return;
            }

            player2Hit = true;
        }
    }

    void ManageInput()
    {
        if (skipThisInput)
        {
            player1KeySequence.Dequeue();
            player2KeySequence.Dequeue();

            ResetPlayerInputs();

            skipThisInput = false;
            consecutiveSuccessfulInput = 0;
            HorseController.Instance.HorseFall();
        }
        else if (player1Hit && waitForPlayer2 > coyoteTime)
        {
            player1KeySequence.Dequeue();
            player2KeySequence.Dequeue();

            ResetPlayerInputs();

            consecutiveSuccessfulInput = 0;
            HorseController.Instance.HorseFall();
        }
        else if (player2Hit && waitForPlayer1 > coyoteTime)
        {
            player1KeySequence.Dequeue();
            player2KeySequence.Dequeue();

            ResetPlayerInputs();

            consecutiveSuccessfulInput = 0;
            HorseController.Instance.HorseFall();
        }
        else if (player1Hit && player2Hit)
        {
            player1KeySequence.Dequeue();
            player2KeySequence.Dequeue();

            ResetPlayerInputs();

            lastSuccessfulInputTime = Time.time;
            consecutiveSuccessfulInput++;
            HorseController.Instance.CurrentVelocity = HorseController.Instance.InitialVelocity + (consecutiveSuccessfulInput * 0.005f);
            HorseController.Instance.HorseState = HorseState.Running;
        }
        else if (Time.time - lastSuccessfulInputTime > timeBeforeStanding)
        {
            HorseController.Instance.HorseState = HorseState.Standing;
            HorseController.Instance.CurrentVelocity = HorseController.Instance.InitialVelocity;

            consecutiveSuccessfulInput = 0;
        }
    }

    void ResetPlayerInputs()
    {
        player1Hit = false;
        player2Hit = false;
        waitForPlayer1 = 0.0f;
        waitForPlayer2 = 0.0f;
    }
}
