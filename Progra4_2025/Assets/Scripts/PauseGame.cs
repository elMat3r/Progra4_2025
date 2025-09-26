using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject canvasPanelStore;
    public bool isGamePaused = false;
    [Tooltip("desactiva el ataque")]public Shooting_System shootingSystem;
    [Tooltip("desactiva la rotacion")]public TurretRotation turretRotation;
    public void ResumeGame()
    {
        turretRotation.enabled = true;
        shootingSystem.enabled = true; //La torreta vuelve a atacar
        canvasPanelStore.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void Pause()
    {
        turretRotation.enabled = false;
        shootingSystem.enabled = false; //La torreta deja de atacar
        canvasPanelStore.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }
}
