using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject panel;
    
    public TMP_Text textScore;
    public TMP_Text textResult;

    public void SetText(string score, string result)
    {
        textScore.text = score;
        textResult.text = result;
    }
}
