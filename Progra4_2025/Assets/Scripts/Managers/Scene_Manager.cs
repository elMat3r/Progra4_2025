using UnityEngine;
public class Scene_Manager : MonoBehaviour
{
    public void Gameplay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_TankMod");
    }
}