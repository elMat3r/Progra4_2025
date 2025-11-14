using System;
using TMPro;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
    public static Score_Manager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            UpdateScoreUI();
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
    void SaveDataToLeaderboard(Action<string, bool> onEndSave) //Funciones en el sistema de score, asi que por ahora van a estar aqui
    {
        PlayFabLogin playfabLogin = new PlayFabLogin();
        playfabLogin.AddDataToMaxPoints(score, onEndSave);
        Debug.Log("Saving Data");
    }
    //private void OnEndSavePoints(string message, bool result)
    //{
    //    Debug.Log("Pasando Algo");
    //    LoadLeaderBoard();
    //    //Cuando termine de guardar la escena cambia a leaderboard
    //}
}
