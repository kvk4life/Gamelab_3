using UnityEngine;
using System.Collections;

public class WeaponSlot : Slot
{
    public bool offHand;
    public WeaponSlot offHandObj;
    public WeaponSlot mainHandObj;
    // Use this for initialization
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        //playerstats = GameObject.Find("Player").GetComponent<PlayerStats>();
        tooltip = GameObject.Find("ToolTip");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void ShowToolTip(Transform transformSlot)
    {
        if (occupied == true)
        {
            if(mainHandObj.item.weaponType == WeaponType.TwoHanded && offHand==true)
            {
                return;
            }
            ishovered = true;
            tooltip.transform.position = transformSlot.position + new Vector3(0, -50f, 100f);
            tooltip.GetComponent<CanvasGroup>().alpha = 1f;
            tooltip.GetComponent<Tooltip>().UpdateToolTip(item.itemQuality, item.itemName, item.stamina, item.agility, item.strength, item.intellect, item.flavorText, item);
        }
        else
        {
            HideToolTip();
        }

    }

    public override void Clicked()
    {
        if (occupied == false)
        {
            if (inventory.movingSlot.occupied == true && inventory.movingSlot.item.equipment == Equipment.Weapon)
            {
                if (offHand == false)
                {
                    switch (inventory.movingSlot.item.weaponType)
                    {
                        case WeaponType.MainHand:
                            playerstats.AddStats(inventory.movingSlot.item);
                            inventory.PutItemInInventory(inventory.movingSlot.item, this);
                            break;
                        case WeaponType.TwoHanded:
                            playerstats.AddStats(inventory.movingSlot.item);
                            inventory.PutItemInInventory(inventory.movingSlot.item, this);
                            if(offHandObj.occupied==true)
                            {
                                playerstats.RemoveStats(offHandObj.item);
                                inventory.PutItemInMovingSlot(offHandObj.item,offHandObj,inventory.movingSlot);
                            }
                            offHandObj.occupied = true;
                            break;
                        case WeaponType.OffHand:
                            break;
                    }
                }
                else if (offHand == true)
                {
                    if (inventory.movingSlot.item.weaponType == WeaponType.OffHand)
                    {
                        playerstats.AddStats(inventory.movingSlot.item);
                        inventory.PutItemInInventory(inventory.movingSlot.item, this);
                    }
                }

                //put item in
            }
            else if (inventory.movingSlot.occupied == false)
            {
                //do nothing
            }
        }
        else if (occupied == true)
        {
            if(mainHandObj.occupied ==  true && mainHandObj.item.weaponType == WeaponType.TwoHanded && offHand == true)
            {
                return;
            }
            if (inventory.movingSlot.occupied == true && inventory.movingSlot.item.itemType == ItemType.Equippable)
            {
                if (mainHandObj.occupied == true && mainHandObj.item.weaponType == WeaponType.MainHand && offHandObj.occupied == true)
                {
                    ErrorMessages error = new ErrorMessages();
                    error.ErrorMessage("Need Both hands for this Weapon");
                    return;
                }
                playerstats.AddStats(inventory.movingSlot.item);
                inventory.TempItemIn(inventory.movingSlot.item);
                inventory.TempItemOut(this.item);
                inventory.PutItemInInventory(inventory.movingSlot.item, this);
                inventory.PutItemInMovingSlotSwitch(inventory.GetTempItem(), this, inventory.movingSlot);
                playerstats.RemoveStats(inventory.movingSlot.item);
            }
            else if (inventory.movingSlot.occupied == false)
            {
                if (item.weaponType == WeaponType.TwoHanded)
                {
                    offHandObj.occupied = false;
                }
                inventory.PutItemInMovingSlot(inventory.movingSlot.item, this, inventory.movingSlot);
                playerstats.RemoveStats(inventory.movingSlot.item);
            }
        }


    }
}
