using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Shopmng : MonoBehaviour
{
    public List<Shop> shopList = new List<Shop>();
    public void ResetShop()
    {
        for(int i =0; i<shopList.Count; i++)
        {
            shopList[i].itemSoldLevel=0;
            if(shopList[i].enabled==false)
            {
                shopList[i].enabled=true;
            }
        }
    }
}
