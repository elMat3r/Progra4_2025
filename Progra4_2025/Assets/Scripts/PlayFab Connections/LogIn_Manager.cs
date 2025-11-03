using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogIn_Manager : MonoBehaviour
{
    [Header("Clases")]
    [SerializeField] PlayFabLogin playFabLogIn;

    [Header("Variables")]
    public string user;
    public string mail;
    public string password;
    public string repeatPassword;
    public int score;
    public int lifePoints;

    [Header("Objetos")]
    [SerializeField] GameObject blockPanel;
    [SerializeField] GameObject[] panels;
    [SerializeField] TextMeshProUGUI textFeedback;
    private void Start()
    {
        SetPanel(LoginPanelType.Login);
        playFabLogIn = new PlayFabLogin();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            SavePJData();
        }
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            LoadPJData();
        }
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

    //ChangeFunctions
    public void OnChangePass(string val)
    {
        password = val;
    }
    public void OnChangeUser(string val)
    {
        user = val;
    }
    public void OnChangeRepeatPassword(string val)
    {
        repeatPassword = val;
    }
    public void OnChangeMail(string val)
    {
        mail = val;
    }

    //RecoveryFunctions
    public void RecoveryButton()
    {
        SetBlockPanel("Sending recovery email...", true);
        playFabLogIn.RecoveryAccount(mail, OnFinishAction);
    }
    public void RecoveryButtonAccess()
    {
        SetPanel(LoginPanelType.Recovery);
    }

    //LoginFunctions
    public void OnLoginButton()
    {
        SetBlockPanel("Loading...", true);
        playFabLogIn.LogInUser(mail, password, OnFinishAction);
    }

    //CreateAccountFunctions
    public void CreateAccountButton()
    {
        SetPanel(LoginPanelType.Register);
    }
    public void CreateAccountCreateButton()
    {
        if(password == repeatPassword)
        {
            SetBlockPanel("Creating...", true);
            playFabLogIn.LogInAnonymous(null);
            playFabLogIn.RegisterUser(user, mail, password, OnFinishAction);
        }
    }


    //OtherFunctions
    private void SetPanel(LoginPanelType panelType)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if(i == (int)panelType)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }
    public void BackButton()
    {
        SetPanel(LoginPanelType.Login);
    }

    //Load&SaveFunctions
    private void OnLoadData(string json, bool success)
    {
        if (success)
        {
            PJData pjData = JsonUtility.FromJson<PJData>(json);
            score = pjData.score;
            lifePoints = pjData.lifePoints;
            SetBlockPanel("Load Success!", false);
        }
        else
        {
            SetBlockPanel("Sucedio un error en la carga de datos", true);
        }
    }
    private void SavePJData()
    {
        PJData pjData = new PJData()
        {
            score = score,
            lifePoints = lifePoints,
        };
        string json = JsonUtility.ToJson(pjData);
        SetBlockPanel("Saving, please don't close the app", true);
        playFabLogIn.SaveDataInfo(json, "PJInfo", OnFinishAction);
    }
    private void LoadPJData()
    {
        SetBlockPanel("Loading, please don't close the app", true);
        playFabLogIn.LoadDataInfo("PJInfo", OnLoadData);
    }
}

public enum LoginPanelType
{
    Login,
    Register,
    Recovery
}

[System.Serializable]
public class PJData
{
    public int score;
    public int lifePoints;
}

[System.Serializable]
public class LeaderBoardData
{
    public string displayName;
    public int score;
    public int boardPos;
}
