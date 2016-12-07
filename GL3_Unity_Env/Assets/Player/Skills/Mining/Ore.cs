using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ore : MonoBehaviour
{
    public Image timeToMine;
    public Item containedItem;
    // Use this for initialization
    void Start()
    {
        containedItem=GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
