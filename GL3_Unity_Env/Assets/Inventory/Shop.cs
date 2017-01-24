using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
    public Item[] itemSold;
    public int itemSoldLevel = 0;
    public GoldMng gold;
    public GameObject player;
    public float price;
    public Transform spawnPos;
    void Start()
    {
        player = GameObject.FindWithTag("Champion");
        gold = player.GetComponent<GoldMng>();
    }

    void Update()
    {
        if(Vector3.Distance(transform.position,player.transform.position)<5)
        {
            if(Input.GetButtonDown("X"))
            {
                Buy();
            }
        }
    }

    void Buy()
    {
        if(gold.curGold>=price)
        {
            //inventoryFunctionaliteit
            Instantiate(itemSold[itemSoldLevel].itemPrefab,spawnPos.transform.position,spawnPos.transform.rotation);
            gold.RemoveGold((int)price);
            PrepareForNext();
        }
    }

    void PrepareForNext()
    {
        if(itemSold.Length>itemSoldLevel)
        {
            itemSoldLevel++;
            price = Mathf.RoundToInt(price*1.2f);
        }
        else
        {
            this.enabled=false;
        }
    }
}
