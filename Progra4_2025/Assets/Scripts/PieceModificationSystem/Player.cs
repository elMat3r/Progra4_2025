using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Movement movement;
    public List<StatInfo> currentStats = new List<StatInfo>();

    [Header("TankPieces")]
    public Color piece_Light;
    public TankPieceScriptable piece_Track;
    public TankPieceScriptable piece_Hull;
    public TankPieceScriptable piece_Tower;
    public TankPieceScriptable piece_Gun;
    public TankPieceScriptable piece_GunConnector;
    public TankPieceScriptable piece_Projectile;

    private void Start()
    {
        UpdateControllersWithTankPieces();
    }
    public void OnTankPieceChange(TankPieceScriptable newPiece)
    {
        Debug.Log("Tank piece changed" + newPiece.pieceType);
        Debug.Log("Tank piece changed" + newPiece.id);
    }

    public void UpdateControllersWithTankPieces()
    {
        List<StatInfo> statsInfo = new List<StatInfo>();

        foreach (var item in piece_Track.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value; 
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in piece_Hull.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in piece_Tower.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in piece_Gun.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in piece_GunConnector.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        foreach (var item in piece_Projectile.statInfo)
        {
            StatInfo currentStat = statsInfo.Find(x => x.type == item.type);
            if (currentStat != null)
            {
                currentStat.value += item.value;
            }
            else
            {
                StatInfo newInfo = new StatInfo();
                newInfo.type = item.type;
                newInfo.value = item.value;
                statsInfo.Add(newInfo);
            }
        }
        currentStats = statsInfo;
    }

    public void OnTankOieceChange(TankPieceScriptable newPiece)
    {
        switch (newPiece.pieceType)
        {
            case TankPieceType.Light:
                Debug.Log("Aura Farming");
                break;
            case TankPieceType.Track:
                piece_Track = newPiece;
                break;
            case TankPieceType.Hull:
                piece_Hull = newPiece;
                break;
            case TankPieceType.Tower:
                piece_Tower = newPiece;
                break;
            case TankPieceType.Gun:
                piece_Gun = newPiece;
                break;
            case TankPieceType.GunConnector:
                piece_GunConnector = newPiece;
                break;
            case TankPieceType.Projectile:
                piece_Projectile = newPiece;
                break;
        }
    }
}