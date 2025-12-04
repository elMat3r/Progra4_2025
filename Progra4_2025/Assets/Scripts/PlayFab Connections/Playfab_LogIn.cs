using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayFabLogin
{
    LoadSaveSystem loadSaveSystem;
    private Action<string, bool> OnFinishActionEvent;
    private Action<List<LeaderBoardData>> OnFinishLeaderBoardEvent;
    private void OnLoginSuccess(LoginResult result)
    {
        OnFinishActionEvent?.Invoke("Success", true);
        Debug.Log("Excelente! Creacion exitosa :D");
    }
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
        OnFinishActionEvent?.Invoke("Success", true);
        //Debug.LogError(error.ErrorMessage.ToString()); //Manda un mensaje de error
    }
    //------------------------Funciones de registro--------------------------
    public void RegisterUser(string userName, string mail, string pass, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new RegisterPlayFabUserRequest
        {
            Username = userName,
            Email = mail,
            Password = pass,
            DisplayName = userName,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterUserResult, OnError);
    }
    private void OnRegisterUserResult(RegisterPlayFabUserResult result)
    {
        Debug.LogWarning("Hurra ! Correct registration :D");
        OnFinishActionEvent?.Invoke("Success", true);
    }
    private void OnError(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
        Debug.LogError("Failure");
        Debug.LogError(error.GenerateErrorReport());
        OnFinishActionEvent?.Invoke(error.GenerateErrorReport(), false);
    }
    public void LogInUser(string mail, string pass, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new LoginWithEmailAddressRequest
        {
            Email = mail,
            Password = pass
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLogInResult, OnError);
    }
    private void OnLogInResult(LoginResult result)
    {
        Debug.Log("Log in succesful !");
        OnFinishActionEvent?.Invoke("Success", true);
        SceneManager.LoadScene(1);
    }
    public void LogInAnonymous(Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "1FED84";
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = System.Guid.NewGuid().ToString(),
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess /*action event*/, OnLoginFailure /*funcion si es que no funciona*/);
    }
    //-----------------------Recovery Account----------------------
    private void OnRequestSuccess(SendAccountRecoveryEmailResult result)
    {
        OnFinishActionEvent?.Invoke("Recovery email sended", true);
        Debug.Log("Recovery email sended");
    }
    public void RecoveryAccount(string email, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = email,
            TitleId = PlayFabSettings.staticSettings.TitleId,
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRequestSuccess, OnError);
    }

    //----------------------Load&Save------------------------
    public void OnDataSave(UpdateUserDataResult result)
    {
        OnFinishActionEvent = null;
        OnFinishActionEvent?.Invoke("Success", true);
        //Debug.Log("Success");
    }
    public void LoadDataInfo(string dataKey, Action<string, bool> onFinishLoad)
    {
        var request = new GetUserDataRequest();
        PlayFabClientAPI.GetUserData(request, result =>
        {
            if (result.Data != null && result.Data.ContainsKey(dataKey))
            {
                string data = result.Data[dataKey].Value;
                onFinishLoad?.Invoke(data, true);
            }
            else
            {
                onFinishLoad?.Invoke(default, false);
            }
        }, OnError);
    }
    public void SaveDataInfo(string data, string dataKey, Action<string, bool> onFinishLoad)
    {
        OnFinishActionEvent = onFinishLoad;
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {dataKey, data },
            },
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSave, OnError);
    }
    public void GetDataFromMaxPoints(Action<List<LeaderBoardData>> leaderBoard)
    {
        OnFinishLeaderBoardEvent = leaderBoard;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "MaxPoints",   // nombre de tu leaderboard
            StartPosition = 0,             // posición inicial (0 = primer lugar)
            MaxResultsCount = 10           // cantidad máxima de resultados
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardLoad, OnError);
    }
    public void AddDataToMaxPoints(int score, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<PlayFab.ClientModels.StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "MaxPoints", // nombre de tu leaderboard
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnStatisticsResult, OnError);
    }
    private void OnLeaderBoardLoad(GetLeaderboardResult result)
    {
        List<LeaderBoardData> dataList = new List<LeaderBoardData>();
        foreach (var item in result.Leaderboard)
        {
            LeaderBoardData newData = new LeaderBoardData()
            {
                displayName = item.DisplayName,
                score = item.StatValue,
                boardPos = item.Position
            };
            dataList.Add(newData);
        }
        OnFinishLeaderBoardEvent?.Invoke(dataList);
    }
    public void SetDisplayName(string displayName, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnEndRequestDisplayName, OnError);
    }
    private void OnEndRequestDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        //Debug.Log("Success");
        OnFinishActionEvent?.Invoke("Success", true);
        OnFinishActionEvent = null;
    }
    private void OnStatisticsResult(UpdatePlayerStatisticsResult result)
    {
        //Debug.Log("Success");
        OnFinishActionEvent?.Invoke("Success", true);
        OnFinishActionEvent = null;
    }
}