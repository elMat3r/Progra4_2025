using UnityEngine;

public class LogIn_Manager : MonoBehaviour
{
    [Header("Clases")]
    [SerializeField] PlayFabLogin playFabLogIn;

    [Header("Variables")]
    public string mail;
    public string password;

    [Header("Objetos")]
    [SerializeField] GameObject blockPanel;
    [SerializeField] TMPro.TextMeshProUGUI textFeedback;

    private void Start()
    {
        SetBlockPanel("Loading...", true);
        playFabLogIn.LogInUser(mail, password, OnFinishAction);
    }
    void SetBlockPanel(string message, bool enable)
    {
        textFeedback.text = message;
        blockPanel.SetActive(enable);
    }
    private void OnFinishAction(string message, bool result)
    {
        if(result == true)
        {
            SetBlockPanel(message, false);
        }
        else
        {
            SetBlockPanel(message, true);
        }
    }
}
