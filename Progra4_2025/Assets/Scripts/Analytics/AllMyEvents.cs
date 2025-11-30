using Unity.Services.Analytics;
//Cambiar el nombre de la clase al mismo que el del evento
public class MyFirstCustomEvent : Event
{
    //Al constructor le ponemos el mismo nombre que la clase
    public MyFirstCustomEvent() : base("myFirstCustomEvent") //en "base" se usa el mismo nombre del evento creado en el dashboard
    {
    }

    //Aca se va a poner las mismas variables que tiene nuestro evento en el dashboard
    public float MFCE_LindoFloat { set { SetParameter("MFCE_LindoFloat", value); } }
}
public class MySecondEvent : Event
{
    public MySecondEvent() : base("mySecondEvent")
    {
    }

    public int MSE_NewInt { set { SetParameter("MSE_NewInt", value); } }
    public string MSE_NewString { set { SetParameter("MSE_NewString", value); } }
    public bool MSE_NewBool { set { SetParameter("MSE_NewBool", value); } }
}

public class EnemyDefeatedEvent : Event
{
    public EnemyDefeatedEvent() : base("enemyDefeated")
    {
    }

    public string ED_StringEnemy { set { SetParameter("ED_StringEnemy", value); } }
}

public class PlayerDeadEvent : Event
{
    public PlayerDeadEvent() : base("playerDead")
    {
    }

    public string PD_StringDied { set { SetParameter("PD_StringDied", value); } }
    public float PD_FloatX { set { SetParameter("PD_FloatX", value); } }
    public float PD_FloatY { set { SetParameter("PD_FloatY", value); } }
}