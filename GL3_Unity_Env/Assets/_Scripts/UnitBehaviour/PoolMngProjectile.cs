using UnityEngine;
using System.Collections;

public class PoolMngProjectile : MonoBehaviour {
    public GameObject[] pooledProjectiles;

    public void ActivatePool(GameObject curTarget, Transform myProjectileHolder)
    {
        for (int i = 0; i < pooledProjectiles.Length; i++)
        {
            if (pooledProjectiles[i].GetComponent<ProjectileTower>().pooled)
            {
                pooledProjectiles[i].GetComponent<ProjectileTower>().Unpool(curTarget, myProjectileHolder);
                break;
            }
        }
    }
}
