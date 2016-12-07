using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Smelting : MonoBehaviour
{
    public Item[] requiredItems;
    public Item returnItem;
    public Slot req1, req2, returnSlot;
    public Image progressBar;
    public GameObject backGroundBar;
    public Inventory inventory;
    public CheckArea area;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smelt()
    {
        if (area.isNearForge)
        {
            StartCoroutine("StartSmelt");
        }
    }

    public bool CheckForNull()
    {
        if(req1.item==null || req2.item==null)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

    IEnumerator StartSmelt()
    {
        if(CheckForNull() && req1.item.itemName==requiredItems[0].itemName && req2.item.itemName == requiredItems[1].itemName && returnSlot.occupied==false)
        {
            backGroundBar.SetActive(true);
            while (progressBar.fillAmount < 1)
            {
                progressBar.fillAmount += 0.01f;
                yield return new WaitForSeconds(0.03f);
            }
            backGroundBar.SetActive(false);
            progressBar.fillAmount = 0f;
            inventory.PutItemInInventory(returnItem, returnSlot);
            req1.occupied=false;
            req1.item=null;
            req1.UpdateSlotEmpty();
            req2.occupied = false;
            req2.item = null;
            req2.UpdateSlotEmpty();

        }

    }

}
