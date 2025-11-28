using UnityEngine;

public class Analytics_Testing : MonoBehaviour
{
    public float testingFloat;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("EstoyFuncionando");
            Analytic_Manager.Instance.SaveMyFirstCustomEvent(testingFloat);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("EstoyFuncionando");
            Analytic_Manager.Instance.SaveMySecondEvent("Holi", Random.Range(0, 20), Random.Range(0, 2) == 0);
        }
    }
}
