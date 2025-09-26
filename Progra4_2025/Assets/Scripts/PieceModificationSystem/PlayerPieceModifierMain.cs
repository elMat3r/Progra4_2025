using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class PlayerPieceModifierMain : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] PanelPieceSelection panelPieceSelection;
    [SerializeField] PanelPartTypeSelection panelPieceTypeSelection;
    [SerializeField] TankSpriteModifier tankSpriteModifier;
    [SerializeField] ColorPicker colorPicker;
    
    [Header("Pieces Info")]
    [SerializeField] List<TankPieceScriptable> tpiece_GunConnector;
    [SerializeField] List<TankPieceScriptable> tpiece_Guns;
    [SerializeField] List<TankPieceScriptable> tpiece_Hulls;
    [SerializeField] List<TankPieceScriptable> tpiece_Towers;
    [SerializeField] List<TankPieceScriptable> tpiece_Tracks;
    [SerializeField] List<TankPieceScriptable> tpiece_Projectiles;

    [Header("Texts")]
    [SerializeField] TMP_InputField inputField;

    public UnityEvent<TankPieceScriptable> OnTankPieceChangeEvent;
    public UnityEvent<Color> OnTankPieceChangeColorEvent;
    public UnityEvent<string> OnInputFieldChangeEvent;
    private void Start()
    {
        panelPieceTypeSelection.OnButtonSelectPartType(TankPieceType.Hull);
    }
    private void Awake()
    {
        inputField.onValueChanged.AddListener(OnInputFieldNameChange);
    }
    public void OnPieceTypeSelected(TankPieceType pieceType)
    {
        if(pieceType == TankPieceType.Light)
        {
            panelPieceSelection.EnablePanel(false);
            colorPicker.EnablePanel(true);
        }
        else
        {
            panelPieceSelection.EnablePanel(true);
            colorPicker.EnablePanel(false);
            panelPieceSelection.SetPanelSelection(GetPiecesByType(pieceType));
        }
    }

    public void OnPieceSelected(TankPieceType pieceType, string id)
    {
        ModifyPiece(GetScriptableTankPiece(pieceType, id));
    }

    public void OnChangeColor(Color color)
    {
        tankSpriteModifier.ChangeLightColor(color);
        OnTankPieceChangeColorEvent?.Invoke(color);
    }

    void ModifyPiece(TankPieceScriptable tankPiece)
    {
        panelPieceTypeSelection.SetButtonSelectPartType(tankPiece.pieceType, tankPiece.pieceSprite);
        tankSpriteModifier.ChangeSprite(tankPiece.pieceType, tankPiece.pieceSprite);
        OnTankPieceChangeEvent?.Invoke(tankPiece);
    }
    
    public void OnInputFieldNameChange(string inputField)
    {
        OnInputFieldChangeEvent?.Invoke(inputField);
    }
    private List<TankPieceScriptable> GetPiecesByType(TankPieceType pieceType)
    {
        List<TankPieceScriptable> selectedPieces = new List<TankPieceScriptable>();
        switch (pieceType)
        {
            case TankPieceType.Light:
                break;
            case TankPieceType.Track:
                selectedPieces = tpiece_Tracks;
                break;
            case TankPieceType.Hull:
                selectedPieces = tpiece_Hulls;
                break;
            case TankPieceType.Tower:
                selectedPieces = tpiece_Towers;
                break;
            case TankPieceType.Gun:
                selectedPieces = tpiece_Guns;
                break;
            case TankPieceType.GunConnector:
                selectedPieces = tpiece_GunConnector;
                break;
            case TankPieceType.Projectile:
                selectedPieces = tpiece_Projectiles;
                break;
        }
        return selectedPieces;
    }

    private TankPieceScriptable GetScriptableTankPiece(TankPieceType pieceType, string pieceID)
    {
        return GetPiecesByType(pieceType).Find(x => x.id == pieceID);
    }
}
