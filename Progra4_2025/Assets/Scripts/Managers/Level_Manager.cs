using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager Instance;
    [SerializeField] Score_Manager scoreManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    public void OnPlayerDie()
    {
        scoreManager.SaveDataToLeaderBoard(OnEndSave);
    }
    private void OnEndSave(string arg1, bool arg2)
    {
        if (true)
        {
            SceneManager.LoadScene("Scene_LeaderBoard");
        }
    }
    public void AddPoints(int amount)
    {
        scoreManager.AddPoints(amount);
    }
}
