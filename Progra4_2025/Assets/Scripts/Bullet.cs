using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float dmg;
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
