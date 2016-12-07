using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipmentSlot : Slot
{
    public Equipment equipment;
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

    public override void Clicked()
    {
        if (occupied == false)
        {
            if (inventory.movingSlot.occupied == true && inventory.movingSlot.item.itemType == ItemType.Equippable && inventory.movingSlot.item.equipment == equipment)
            {
                playerstats.AddStats(inventory.movingSlot.item);
                inventory.PutItemInInventory(inventory.movingSlot.item, this);
                //put item in
            }
            else if (inventory.movingSlot.occupied == false)
            {
                //do nothing
            }
        }
        else if (occupied == true)
        {
            if (inventory.movingSlot.occupied == true && inventory.movingSlot.item.itemType == ItemType.Equippable && inventory.movingSlot.item.equipment == equipment)
            {
                playerstats.AddStats(inventory.movingSlot.item);
                inventory.TempItemIn(inventory.movingSlot.item);
                inventory.TempItemOut(this.item);
                inventory.PutItemInInventory(inventory.movingSlot.item, this);
                inventory.PutItemInMovingSlotSwitch(inventory.GetTempItem(), this, inventory.movingSlot);
                playerstats.RemoveStats(inventory.movingSlot.item);
            }
            else if (inventory.movingSlot.occupied == false)
            {
                inventory.PutItemInMovingSlot(inventory.movingSlot.item, this, inventory.movingSlot);
                playerstats.RemoveStats(inventory.movingSlot.item);
            }
        }
        

    }
}
