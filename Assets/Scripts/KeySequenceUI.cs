using TMPro;
using UnityEngine;

public class KeySequenceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] player1Keys;
    [SerializeField] TextMeshProUGUI[] player2Keys;
    public void UpdateSequence(KeyCode[] player1KeySequence, KeyCode[] player2KeySequence)
    {
        for (int i = 0; i < player1KeySequence.Length; i++)
        {
            string player1KeyString = "";
            if (player1KeySequence[i] == KeyCode.Alpha1) player1KeyString = "1";
            else if (player1KeySequence[i] == KeyCode.Alpha2) player1KeyString = "2";
            else if (player1KeySequence[i] == KeyCode.Alpha3) player1KeyString = "3";
            else if (player1KeySequence[i] == KeyCode.Alpha4) player1KeyString = "4";
            player1Keys[i].text = player1KeyString;

            string player2KeyString = "";
            if (player2KeySequence[i] == KeyCode.Alpha7) player2KeyString = "7";
            else if (player2KeySequence[i] == KeyCode.Alpha8) player2KeyString = "8";
            else if (player2KeySequence[i] == KeyCode.Alpha9) player2KeyString = "9";
            else if (player2KeySequence[i] == KeyCode.Alpha0) player2KeyString = "0";
            player2Keys[i].text = player2KeyString;
        }
    }
}
