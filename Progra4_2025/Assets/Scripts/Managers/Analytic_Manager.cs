using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class Analytic_Manager : MonoBehaviour
{
    public static Analytic_Manager Instance;
    [HideInInspector] public bool isInitialized = false;

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
    public async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
        isInitialized = true;
    }
    public void SaveMyFirstCustomEvent(float MFCELindoFloat)
    {
        if (isInitialized)
        {
            Debug.Log("Funcionando Correctamente");
            MyFirstCustomEvent myFirstCustomEvent = new MyFirstCustomEvent()
            {
                MFCE_LindoFloat = MFCELindoFloat,
            };
            AnalyticsService.Instance.RecordEvent(myFirstCustomEvent);
        }
    }
    public void SaveMySecondEvent(string MSE_String, int MSE_Int, bool MSE_Bool)
    {
        if (isInitialized)
        {
            Debug.Log("Funcionando Correctamente2");
            MySecondEvent mySecondEvent = new MySecondEvent()
            {
                MSE_NewInt = MSE_Int,
                MSE_NewString = MSE_String,
                MSE_NewBool = MSE_Bool
            };
            AnalyticsService.Instance.RecordEvent(mySecondEvent);
            //AnalyticsService.Instance.Flush(); //<--- Esto es para que la informacion se mande de manera inmediata
        }
    }
    // -----------------------------
    //   ATAJOS PARA EVENTOS COMUNES
    // -----------------------------

    //public void LevelStart(int level)
    //{
    //    if (isInitialized)
    //    {
    //        Debug.Log("Funcionando Correctamente2");
    //        MySecondEvent mySecondEvent = new MySecondEvent()
    //        {
    //            MSE_NewInt = MSE_Int
    //        };
    //        AnalyticsService.Instance.RecordEvent(mySecondEvent);
    //        //AnalyticsService.Instance.Flush(); //<--- Esto es para que la informacion se mande de manera inmediata
    //    }
    //}

    //public void LevelComplete(int level, float time, int score)
    //{
    //    if (isInitialized)
    //    {
    //        Debug.Log("Funcionando Correctamente2");
    //        MySecondEvent mySecondEvent = new MySecondEvent()
    //        {
    //            MSE_NewInt = MSE_Int,
    //            MSE_NewFloat = MSE_Float,
    //            MSE_NewInt = MSE_Int,
    //        };
    //        AnalyticsService.Instance.RecordEvent(mySecondEvent);
    //        //AnalyticsService.Instance.Flush(); //<--- Esto es para que la informacion se mande de manera inmediata
    //    }
    //}

    public void PlayerDead(string died, float posX, float posY)
    {
        if (isInitialized)
        {
            Debug.Log("Analisis de muerte comprobado");
            PlayerDeadEvent mySecondEvent = new PlayerDeadEvent()
            {
                PD_StringDied = died,
                PD_FloatX = posX,
                PD_FloatY = posY
            };
            AnalyticsService.Instance.RecordEvent(mySecondEvent);
            //AnalyticsService.Instance.Flush(); //<--- Esto es para que la informacion se mande de manera inmediata
        }
    }

    public void EnemyDefeated(string enemy)
    {
        if (isInitialized)
        {
            Debug.Log("Funcionando Correctamente2");
            EnemyDefeatedEvent enemyDefeatedEvent = new EnemyDefeatedEvent()
            {
                ED_StringEnemy = enemy
            };
            AnalyticsService.Instance.RecordEvent(enemyDefeatedEvent);
            //AnalyticsService.Instance.Flush(); //<--- Esto es para que la informacion se mande de manera inmediata
        }
    }

    //public void ScorePerPlayer(int score)
    //{
    //    if (isInitialized)
    //    {
    //        Debug.Log("Funcionando Correctamente2");
    //        MySecondEvent mySecondEvent = new MySecondEvent()
    //        {
    //            MSE_NewInt = MSE_Int,
    //            MSE_NewString = MSE_String,
    //            MSE_NewBool = MSE_Bool
    //        };
    //        AnalyticsService.Instance.RecordEvent(mySecondEvent);
    //        //AnalyticsService.Instance.Flush(); //<--- Esto es para que la informacion se mande de manera inmediata
    //    }
    //}

    //public void BulletThrowing(int bullet)
    //{
    //    if (isInitialized)
    //    {
    //        Debug.Log("Funcionando Correctamente2");
    //        MySecondEvent mySecondEvent = new MySecondEvent()
    //        {
    //            MSE_NewInt = MSE_Int
    //        };
    //        AnalyticsService.Instance.RecordEvent(mySecondEvent);
    //        //AnalyticsService.Instance.Flush(); //<--- Esto es para que la informacion se mande de manera inmediata
    //    }
    //}
}
