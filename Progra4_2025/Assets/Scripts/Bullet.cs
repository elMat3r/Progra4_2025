using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDmg;
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
