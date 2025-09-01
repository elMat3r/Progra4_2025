using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    public string keyName; //Nombre del archivo que estamos guardando
    //public string infoName; //Nombre de la info que estamos guardando y cargando

    public Monsito monsito;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveData();
            Debug.Log("Guardado completado");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetData();
            Debug.Log("Carga completado");
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(monsito);
        
        PlayerPrefs.SetString(keyName, data);
    }
    public void GetData()
    {
        string data = PlayerPrefs.GetString(keyName, "Null");
        if(data != "Null")
        {
            monsito = JsonUtility.FromJson<Monsito>(data);
        }
    }
}
