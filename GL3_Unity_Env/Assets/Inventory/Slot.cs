using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Slot : MonoBehaviour
{
    public bool occupied;
    public Sprite backGroundSlotImage;
    public Sprite occupiedSprite;
    public Image myImage;
    public GameObject itemPrefab;
    public Item item;
    public Inventory inventory;
    public GameObject tooltip;
    public PlayerStats playerstats;
    public bool ishovered;
    public EquipmentSlot head, shoulders, chest, legs, hands, boots;

    public void Start()
    {
        head = inventory.head;
        shoulders = inventory.shoulders;
        chest = inventory.chest;
        legs = inventory.legs;
        hands = inventory.hands;
        boots = inventory.boots;
        playerstats = GameObject.Find("Pig Benis01").GetComponent<PlayerStats>();
        tooltip = GameObject.Find("ToolTip");
    }

    public void Update()
    {
        if (ishovered == true)
        {
            OnPointerHover();
        }
    }

    public virtual void ShowToolTip(Transform transformSlot)
    {
        if (occupied == true)
        {
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

    public void HideToolTip()
    {
        ishovered = false;
        tooltip.transform.position = Input.mousePosition;
        tooltip.GetComponent<CanvasGroup>().alpha = 0f;
    }


    public void UpdateSlotOccupied()
    {
        myImage.color = new Color(1, 1, 1, 1);
        occupiedSprite = item.icon;
        itemPrefab = item.itemPrefab;
        myImage.sprite = occupiedSprite;
    }

    public void UpdateSlotEmpty()
    {
        myImage.color = new Color(1, 1, 1, 0);
        occupiedSprite = null;
        itemPrefab = null;
    }

    public void OnPointerHover()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            switch (item.itemType)
            {
                case ItemType.Equippable:
                    switch (item.equipment)
                    {
                        case Equipment.Head:
                            Equip(head, this, item);
                            break;
                        case Equipment.Shoulders:
                            Equip(shoulders, this, item);
                            break;
                        case Equipment.Chest:
                            Equip(chest, this, item);
                            break;
                        case Equipment.Legs:
                            Equip(legs, this, item);
                            break;
                        case Equipment.Boots:
                            Equip(boots, this, item);
                            break;
                        case Equipment.Hands:
                            Equip(hands, this, item);
                            break;
                        case Equipment.Weapon:
                            break;
                    }
                    break;
                case ItemType.Consumable:
                    playerstats.health += item.restoreHP;
                    playerstats.mana += item.restoreMana;
                    if (playerstats.health > playerstats.maxHealth)
                    {
                        playerstats.health = playerstats.maxHealth;
                    }
                    if (playerstats.mana > playerstats.maxMana)
                    {
                        playerstats.mana = playerstats.maxMana;
                    }
                    break;
                case ItemType.Reagent:
                    break;

            }
        }
    }

    public void Equip(EquipmentSlot eqSlot, Slot thisSlot, Item item)
    {
        if(eqSlot.occupied==false)
        {
            inventory.EquipItem(item, thisSlot, eqSlot);
            HideToolTip();
        }
        else if(eqSlot.occupied==true)
        {
            inventory.EquipItemSwitch(item, thisSlot, eqSlot);
            HideToolTip();
        }
    }

    public virtual void Clicked()
    {
        if (occupied == false)
        {
            if (inventory.movingSlot.occupied == true)
            {
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
            if (inventory.movingSlot.occupied == true)
            {
                inventory.TempItemIn(inventory.movingSlot.item);
                inventory.TempItemOut(this.item);
                inventory.PutItemInInventory(inventory.movingSlot.item, this);
                inventory.PutItemInMovingSlotSwitch(inventory.GetTempItem(), this, inventory.movingSlot);
            }
            else if (inventory.movingSlot.occupied == false)
            {
                inventory.PutItemInMovingSlot(inventory.movingSlot.item, this, inventory.movingSlot);
            }
        }
    }

}
