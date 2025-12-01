using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting_System : MonoBehaviour
{
    [Header("InputSystem")]
    public InputActionAsset inputActions;
    private InputAction m_attackAction;

    [Header("Bullet")]
    public Transform spawnPoint;
    public float bulletSpd;
    public GameObject bulletPrefab;

    [Header("Analytic")]
    public int bulletThrowingCount;
    public void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    public void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        m_attackAction = InputSystem.actions.FindAction("Attack");
    }
    private void Update()
    {
        if (m_attackAction.WasPerformedThisFrame())
        {
            GunShoot();
            bulletThrowingCount++;
            Analytic_Manager.Instance.BulletThrowing(bulletThrowingCount);
        }
    }
    public void GunShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = spawnPoint.up * bulletSpd;
            rb.transform.up = spawnPoint.up;
        }
    }
    public GameObject InstantiateBullet(Vector3 pos, Vector3 dir, float spd)
    {
        GameObject newObj = Instantiate(bulletPrefab, pos, Quaternion.identity);
        newObj.GetComponent<Rigidbody2D>().linearVelocity = dir * spd;
        newObj.GetComponent<Rigidbody2D>().transform.up = pos;
        return newObj;
    }
}
