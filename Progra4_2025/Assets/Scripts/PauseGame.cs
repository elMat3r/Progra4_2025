using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject canvasPanelStore;
    public bool isGamePaused = false;
    [Tooltip("desactiva el ataque")]public Shooting_System shootingSystem;
    public void ResumeGame()
    {
        shootingSystem.enabled = true; //La torreta vuelve a atacar
        canvasPanelStore.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void Pause()
    {
        shootingSystem.enabled = false; //La torreta deja de atacar
        canvasPanelStore.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }
}
