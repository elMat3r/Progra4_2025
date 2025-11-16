using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
    [Header("Clases")]
    [SerializeField] Movement movement;
    [SerializeField] Shooting_System shootingSystem;
    [SerializeField] Bullet bulletDmg;
    [SerializeField] TurretRotation turretRotation;
    public TankSpriteModifier spriteModifier;
    public List<StatInfo> currentStats = new List<StatInfo>();

    [Header("PlayerText")]
    public TextMeshProUGUI tankTextName;
    public TextMeshProUGUI tankTextNameWorld;


    [Header("TankPieces")]
    public Color piece_Light;
    public TankPieceScriptable piece_Track;
    public TankPieceScriptable piece_Hull;
    public TankPieceScriptable piece_Tower;
    public TankPieceScriptable piece_Gun;
    public TankPieceScriptable piece_GunConnector;
    public TankPieceScriptable piece_Projectile;
    public ColorPicker colorPicker;

    [Header("TankStats")]
    public StatInfo stat_Spd;
    public StatInfo stat_RootSpd;
    public StatInfo stat_Attack;
    public StatInfo stat_Defense;
    public StatInfo stat_Life;
    public StatInfo stat_BulletSpd;

    [Header("PlayerSave&Load")]
    public string playerName;
    public int currentDmg;
    public int points;

    [Header("Life")]
    public int maxHealth;
    int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateControllersWithTankPieces();
        LoadData();
    }
    public void ChangeName(string name)
    {
        playerName = name;
        tankTextName.text = name;
        tankTextNameWorld.text = name;
    }
    public void OnTankPieceChange(TankPieceScriptable newPiece)
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
            default:
                break;
        }
        UpdateControllersWithTankPieces();
    }
    public void OnChangeColorTank(Color tankColor)
    {
        piece_Light = tankColor;
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
        OnTankPieceStats();
    }
    public void OnTankPieceStats()
    {
        foreach (var item in currentStats)
        {
            switch (item.type)
            {
                case StatType.Spd:
                    movement.moveSpd = item.value;
                    break;
                case StatType.RootSpd:
                    movement.rotateSpd = item.value;
                    turretRotation.rotateSpd = item.value;
                    break;
                case StatType.Attack:
                    bulletDmg.bulletDmg = item.value;
                    break;
                //case StatType.Defense:
                //    stat_Defense = newStat;
                //    break;
                //case StatType.Life:
                //    stat_Life = newStat;
                //    break;
                case StatType.BulletSpd:
                    shootingSystem.bulletSpd = item.value;
                    break;
            }
        }
    }
    public void LoadData()
    {
        LoadSaveSystem loadSave = new LoadSaveSystem();
        loadSave.LoadPlayerInfo(OnEndLoadData);
    }
    public void OnEndLoadData(PlayerDataInfo playerData)
    {
        if (playerData == null) return;
        ChangeName(playerData.playerName);
        currentDmg = playerData.currentDmg;
        points = playerData.score;

        LoadResources loadResources = new LoadResources();

        piece_Track = loadResources.GetTankPieceScriptable(TankPieceType.Track, playerData.piecesName[0]);
        piece_Hull = loadResources.GetTankPieceScriptable(TankPieceType.Hull, playerData.piecesName[1]);
        piece_Tower = loadResources.GetTankPieceScriptable(TankPieceType.Tower, playerData.piecesName[2]);
        piece_Gun = loadResources.GetTankPieceScriptable(TankPieceType.Gun, playerData.piecesName[3]);
        piece_GunConnector = loadResources.GetTankPieceScriptable(TankPieceType.GunConnector, playerData.piecesName[4]);
        piece_Projectile = loadResources.GetTankPieceScriptable(TankPieceType.Projectile, playerData.piecesName[5]);

        piece_Light = playerData.colorTank;

        spriteModifier.ChangeSprite(piece_Track.pieceType, piece_Track.pieceSprite);
        spriteModifier.ChangeSprite(piece_Hull.pieceType, piece_Hull.pieceSprite);
        spriteModifier.ChangeSprite(piece_Tower.pieceType, piece_Tower.pieceSprite);
        spriteModifier.ChangeSprite(piece_Gun.pieceType, piece_Gun.pieceSprite);
        spriteModifier.ChangeSprite(piece_GunConnector.pieceType, piece_GunConnector.pieceSprite);
        spriteModifier.ChangeSprite(piece_Projectile.pieceType, piece_Projectile.pieceSprite);
        spriteModifier.ChangeLightColor(piece_Light);

        UpdateControllersWithTankPieces();
    }
    public void SaveData()
    {
        PlayerDataInfo playerData = new PlayerDataInfo();

        playerData.playerName = playerName;
        playerData.currentDmg = currentDmg;
        playerData.score = points;

        playerData.piecesName = new List<string>();
        playerData.piecesName.Add(piece_Track.id);
        playerData.piecesName.Add(piece_Hull.id);
        playerData.piecesName.Add(piece_Tower.id);
        playerData.piecesName.Add(piece_Gun.id);
        playerData.piecesName.Add(piece_GunConnector.id);
        playerData.piecesName.Add(piece_Projectile.id);

        playerData.colorTank = piece_Light;

        LoadSaveSystem loadSave = new LoadSaveSystem();
        loadSave.SavePlayerInfo(playerData);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Debug.Log("Dead");
            Die();
        }
    }
    public void Die()
    {
        Level_Manager.Instance.OnPlayerDie();
    }
}