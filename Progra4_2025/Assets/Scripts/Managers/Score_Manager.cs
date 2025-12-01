using System;
using TMPro;
using UnityEngine;
public class Score_Manager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public void AddPoints(int amount)
    {
        score += amount;
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos:" + score;
        }
    }
    public void SaveDataToLeaderBoard(Action<string, bool> onEndSave)//esto va en mi sistema de score
    {
        //Debug.Log("Saving");
        PlayFabLogin playFabManager = new PlayFabLogin();
        playFabManager.AddDataToMaxPoints(score, onEndSave);
        Analytic_Manager.Instance.ScorePerPlayer(score);
    }
}