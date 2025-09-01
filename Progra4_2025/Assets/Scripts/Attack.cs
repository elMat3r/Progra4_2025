using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxQuantity;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform poolingParent;
    [SerializeField] private float bulletSpd;

    private int index;
    List<GameObject> poolingList = new List<GameObject>();
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
            poolingList.Add(InstantiateBullet(spawnPoint.position, spawnPoint.forward, bulletSpd));
        }
    }
    public GameObject InstantiateBullet(Vector3 pos, Vector3 dir, float spd)
    {
        GameObject newObj = Instantiate(prefab, pos, Quaternion.identity, poolingParent);
        newObj.GetComponent<Rigidbody2D>().linearVelocity = dir * spd;
        return newObj;
    }
}
