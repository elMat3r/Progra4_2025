using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject canvasPanelStore;
    public GameObject canvasGameplayeInfo;
    public bool isGamePaused = false;
    [Tooltip("desactiva el ataque")]public Shooting_System shootingSystem;
    [Tooltip("desactiva la rotacion")]public TurretRotation turretRotation;
    public void ResumeGame()
    {
        turretRotation.enabled = true; //La rotacion vuelve a funcionar
        shootingSystem.enabled = true; //La torreta vuelve a atacar
        canvasPanelStore.SetActive(false);
        canvasGameplayeInfo.SetActive(true);
        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void Pause()
    {
        turretRotation.enabled = false; //La rotacion deja de funcionar
        shootingSystem.enabled = false; //La torreta deja de atacar
        canvasPanelStore.SetActive(true);
        canvasGameplayeInfo.SetActive(false);
        Time.timeScale = 0;
        isGamePaused = true;
    }
}
