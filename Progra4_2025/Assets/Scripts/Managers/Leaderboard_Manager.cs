using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Leaderboard_Manager : MonoBehaviour
{
    [SerializeField] Leaderboard_Content[] leaderBoardContent;
    [SerializeField] CanvasAnimation canvasAnimation;
    public int score;
    private void Start()
    {
        StartCoroutine(LoadLeaderBoardCorrutine());
    }
    IEnumerator LoadLeaderBoardCorrutine()
    {
        yield return canvasAnimation.AnimPanelCoroutine(true);
        yield return canvasAnimation.ShowPointsCoroutine(score);
        LoadLeaderBoard();
        yield return null;
    }
    public void LoadLeaderBoard()
    {
        //Debug.Log("Loading Data");
        PlayFabLogin playfabLogin = new PlayFabLogin();
        playfabLogin.GetDataFromMaxPoints(SetContent);
    }
    void SetContent(List<LeaderBoardData> leaderBoardData)
    {
        //Debug.Log("Setting Content");
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
}