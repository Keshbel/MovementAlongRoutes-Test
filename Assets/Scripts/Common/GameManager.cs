using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool IsRestarting;

    public void EndGame(int countGoal)
    {
        var countPoints = countGoal * 50;
        string score = String.Concat("Score = ", countPoints); 
        string result = "";

        switch (countPoints)
        {
            case 0:
                result = "Try again...";
                break;
            case 50:
                result = "You almost won...";
                break;
            case 100:
                result = "You won!";
                break;
        }

        AllSingleton.Instance.gameOver.SetText(score, result);
        AllSingleton.Instance.gameOver.panel.SetActive(true);
    }

    public void Restart()
    {
        IsRestarting = true;
        SceneManager.LoadSceneAsync(0);
    }
}
