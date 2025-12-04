using System;
using UnityEngine;
[System.Serializable]
public class LoadSaveSystem
{
    string playerInfoDataKey = "PlayerInfo"; //Nombre del archivo que estamos guardando
    public void LoadPlayerInfo(Action<PlayerDataInfo> onEndLoadData)
    {
        string json = PlayerPrefs.GetString(playerInfoDataKey);
        PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
        PlayFabLogin playFab = new PlayFabLogin();
        playFab.LoadDataInfo(playerInfoDataKey, (data, result) =>
        {
            if (result == true)
            {
                json = data;
                PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
                onEndLoadData(loadData);
                //Debug.Log("Load Success");
            }
        });
    }
    public void SavePlayerInfo(PlayerDataInfo dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);
        PlayerPrefs.SetString(playerInfoDataKey, json);
        PlayFabLogin playFabManager = new PlayFabLogin();
        playFabManager.SaveDataInfo(json, playerInfoDataKey, OnFinishSave);
        //Debug.Log("Save Success");
    }
    private void OnFinishSave(string arg1, bool arg2)
    {
        
    }
}