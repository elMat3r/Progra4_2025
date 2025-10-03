using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Events;

public class PlayFabLogin : MonoBehaviour
{
    private Action<string, bool> OnFinishActionEvent;
    private void OnLoginSuccess(LoginResult result)
    {
        OnFinishActionEvent?.Invoke("Success", true);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
        OnFinishActionEvent?.Invoke("Success", true);
        //Debug.LogError(error.ErrorMessage.ToString()); //Manda un mensaje de error
    }
    //Funciones de registro
    public void RegisterUser(string mail, string pass, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new RegisterPlayFabUserRequest
        {
            Email = mail,
            Password = pass,
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
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLogInSuccess, OnError);
    }
    private void OnLogInSuccess(LoginResult result)
    {
        Debug.Log("Log in succesful !");
        OnFinishActionEvent?.Invoke("Success", true);
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
            CustomId = "ApoloLeyton",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess /*action event*/, OnLoginFailure /*funcion si es que no funciona*/);
    }
}