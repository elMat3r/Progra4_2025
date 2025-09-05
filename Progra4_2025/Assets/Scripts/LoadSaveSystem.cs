using UnityEngine;
[System.Serializable]
public class LoadSaveSystem
{
    string playerInfoDataKey = "PlayerInfo"; //Nombre del archivo que estamos guardando
    //public string infoName; //Nombre de la info que estamos guardando y cargando
    public PlayerDataInfo LoadPlayerInfo()
    {
        string json = PlayerPrefs.GetString(playerInfoDataKey);
        
        PlayerDataInfo loadData = JsonUtility.FromJson<PlayerDataInfo>(json);
        return loadData;
    }
    public void SavePlayerInfo(PlayerDataInfo dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);
        PlayerPrefs.SetString(playerInfoDataKey, json);
        Debug.Log("Save Success");
    }
}
