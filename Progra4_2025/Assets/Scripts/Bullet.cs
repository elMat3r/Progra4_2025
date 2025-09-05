using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
