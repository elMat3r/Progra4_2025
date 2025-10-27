using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public Transform[] wayPoints;
    public float spd;
    private int currentWaypoint;

    private void Update()
    {
        if(transform.position != wayPoints[currentWaypoint].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypoint].position, spd * Time.fixedDeltaTime);
        }
        else
        {
            currentWaypoint++;
        }
    }
}
