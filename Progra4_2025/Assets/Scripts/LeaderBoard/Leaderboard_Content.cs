using TMPro;
using UnityEngine;
public class Leaderboard_Content : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text_Number;
    [SerializeField] TextMeshProUGUI text_DisplayName;
    [SerializeField] TextMeshProUGUI text_Points;
    public void SetLeaderBoardDataList(LeaderBoardData leaderBoardData)
    {
        text_Number.text = (leaderBoardData.boardPos + 1).ToString();
        text_DisplayName.text = leaderBoardData.displayName;
        text_Points.text = leaderBoardData.score.ToString();
    }
}