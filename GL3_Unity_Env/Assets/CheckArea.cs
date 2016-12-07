using UnityEngine;
using System.Collections;

public class CheckArea : MonoBehaviour
{
    public bool isNearForge;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Furnace")
        {
            isNearForge=true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Furnace")
        {
            isNearForge = false;
        }
    }
}
