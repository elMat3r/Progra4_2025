using UnityEngine;
using UnityEngine.Events;

public class ColorPicker : MonoBehaviour
{
    public GameObject panelSelectColor;
    public FlexibleColorPicker flexibleColorPicker;
    public UnityEvent<Color> OnColorChengeEvent;

    public void OnColorChange(Color color)
    {
        OnColorChengeEvent.Invoke(color);
    }
    public void SetStartColor(Color color)
    {
        flexibleColorPicker.SetColor(color);
    }
    public void EnablePanel(bool isEnabled)
    {
        panelSelectColor.SetActive(isEnabled);
    }
}
