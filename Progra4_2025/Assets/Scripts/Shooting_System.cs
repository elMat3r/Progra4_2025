using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting_System : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction m_attackAction;


    public Transform spawnPoint;
    public float bulletSpd;
    public GameObject bulletPrefab;

    public int maxQuantity;
    private int index;
    public Transform poolingParent;
    private List<GameObject> poolingList = new List<GameObject>();

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
        Shoot();
    }
    public void Shoot()
    {
        if (poolingList.Count >= maxQuantity)
        {
            poolingList[index].gameObject.SetActive(true);
            poolingList[index].transform.position = spawnPoint.position;
            poolingList[index].GetComponent<Rigidbody2D>().linearVelocity = spawnPoint.up * bulletSpd;
            index++;
            if (index >= maxQuantity)
            {
                index = 0;
            }
        }
        else
        {
            poolingList.Add(InstantiateBullet(spawnPoint.position, spawnPoint.up, bulletSpd));
        }
    }
    public GameObject InstantiateBullet(Vector3 pos, Vector3 dir, float spd)
    {
        GameObject newObj = Instantiate(bulletPrefab, pos, Quaternion.identity, poolingParent);
        newObj.GetComponent<Rigidbody2D>().linearVelocity = dir * spd;
        return newObj;
    }
}
