using UnityEngine;
using System.Collections;

public class BaseSpel : MonoBehaviour
{
    public float damage;
    public float projectileSpeed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
}
