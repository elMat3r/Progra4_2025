using PlayFab.EventsModels;
using PlayFab;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Analytic_Manager : MonoBehaviour
{
    public static Analytic_Manager Instance;

    // Tamaño máximo antes de enviar telemetría
    [SerializeField] private int maxBufferSize = 10;

    // Tiempo entre autosend
    [SerializeField] private float autoFlushInterval = 10f;

    private List<EventContents> buffer = new List<EventContents>();
    private float timer = 0f;
    bool isFlushing = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isFlushing) return;
        timer += Time.deltaTime;
        if (timer >= autoFlushInterval)
        {
            Flush();
        }
    }

    // -----------------------------
    //   API PÚBLICA PARA ENVIAR EVENTOS
    // -----------------------------

    public void LogEvent(string eventName, Dictionary<string, object> data = null, string eventNamespace = "custom")
    {
        if (data == null) data = new Dictionary<string, object>();

        var evt = new EventContents
        {
            Name = eventName,
            EventNamespace = "com.playfab.events.custom",
            Payload = data,
            OriginalTimestamp = DateTime.UtcNow
        };

        buffer.Add(evt);

        if (buffer.Count >= maxBufferSize)
            Flush();
    }

    // -----------------------------
    //   FLUSH (ENVÍA LOS EVENTOS A PLAYFAB)
    // -----------------------------
    public void Flush()
    {
        if (buffer.Count == 0) return;

        isFlushing = true;
        var request = new WriteEventsRequest
        {
            Events = new List<EventContents>(buffer)
        };

        PlayFabEventsAPI.WriteEvents(request,
            result =>
            {
                isFlushing = false;
                buffer.Clear();
                timer = 0f;
                Debug.Log($"Telemetry enviado ({result.AssignedEventIds.Count} eventos)");
            },
            error =>
            {
                isFlushing = false;
                Debug.LogWarning("Error enviando Telemetry, se reintentará automáticamente. " + error.ErrorMessage);
                // No limpiamos el buffer, se reenvía en el próximo Flush
            }
        );
    }

    // -----------------------------
    //   ATAJOS PARA EVENTOS COMUNES
    // -----------------------------

    public void LevelStart(int level)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("level", level);
        LogEvent("level_start", data, "gameplay");
    }

    public void LevelComplete(int level, float time, int score)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("level", level);
        data.Add("time", time);
        data.Add("score", score);
        LogEvent("level_complete", data, "gameplay");
    }

    public void PlayerDead(string reason/*, float time, float posX, float posY*/)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("reason", reason);
        //data.Add("timeAlive", time);
        //data.Add("deadPos", posX);
        //data.Add("deadPos", posY);
        LogEvent("player_dead", data, "gameplay");

        //que es lo mismo que esto
        //LogEvent("player_died", new Dictionary<string, object> {{ "reason", reason }}, "gameplay");
    }

    public void EnemyDefeated(int enemies)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("enemy", enemies);
        LogEvent("enemy_deafeated", data, "gameplay");
    }

    public void ScorePerPlayer(int score)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("scorePlayer", score);
        LogEvent("score_player", data, "leaderBoard");
    }

    public void BulletThrowing(int bullet)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("playerBullet", bullet);
        LogEvent("bullet_throwing", data, "player");
        Flush();
    }
}
