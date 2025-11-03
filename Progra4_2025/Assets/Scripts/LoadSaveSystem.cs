using System;
using UnityEngine;
[System.Serializable]
public class LoadSaveSystem
{
    string playerInfoDataKey = "PlayerInfo"; //Nombre del archivo que estamos guardando
    public PlayerDataInfo LoadPlayerInfo(Action<PlayerDataInfo> onEndLoadData)
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
            }
        });
        return loadData;
    }
    public void SavePlayerInfo(PlayerDataInfo dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);
        PlayerPrefs.SetString(playerInfoDataKey, json);
        Debug.Log("Save Success");
    }
}
