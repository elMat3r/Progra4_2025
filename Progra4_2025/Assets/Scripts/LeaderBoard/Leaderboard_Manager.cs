using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Leaderboard_Manager : MonoBehaviour
{
    [SerializeField] Leaderboard_Content[] leaderBoardContent;
    public int score;

    private void Start()
    {
        SaveDataToLeaderboard();
    }
    public void LoadLeaderBoard()
    {
        Debug.Log("Loading Data");
        PlayFabLogin playfabLogin = new PlayFabLogin();
        playfabLogin.GetDataFromMaxPoints(SetContent);
    }
    void SetContent(List<LeaderBoardData> leaderBoardData)
    {
        Debug.Log("Setting Content");
        for(int i = 0;  i < leaderBoardContent.Length; i++)
        {
            if(i < leaderBoardData.Count)
            {
                leaderBoardContent[i].gameObject.SetActive(true);
                leaderBoardContent[i].SetLeaderBoardDataList(leaderBoardData[i]);
            }
            else
            {
                leaderBoardContent[i].gameObject.SetActive(false);
            }
        }
    }
    void SaveDataToLeaderboard() //Funciones en el sistema de score, asi que por ahora van a estar aqui
    {
        PlayFabLogin playfabLogin = new PlayFabLogin();
        playfabLogin.AddDataToMaxPoints(score, OnEndSavePoints);
        Debug.Log("Saving Data");
    }
    private void OnEndSavePoints(string message, bool result)
    {
        Debug.Log("Pasando Algo");
        LoadLeaderBoard();
        //Cuando termine de guardar la escena cambia a leaderboard
    }
    //Funciones en el sistema de score, asi que por ahora van a estar aqui
}
