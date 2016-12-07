using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    public List<GameObject> slotList = new List<GameObject>();
	public GameObject slotPrefab;
    public GameObject slotParent;
	public int rows;
	public int coloms;
	public float offSet;
    public MovingSlot movingSlot;
    private Item tempItemIn;
    private Slot tempSlotIn;
    private Sprite tempSpriteIn;
    private Item tempItemOut;
    private Slot tempSlotOut;
    private Sprite tempSpriteOut;
    public UIManager uiManager;
    public EquipmentSlot head, shoulders, chest, legs, hands, boots;
    public PlayerStats playerstats;
    public Mining mine;
    // Use this for initialization
    void Start () 
	{   
	    GenerateInventory();
        uiManager.inventorySwitch=false;
        uiManager.ToggleInventory();
	}

    void Update()
    {

    }

    public void TempItemIn(Item item)
    {
        tempItemIn = item;
        tempSpriteIn = tempItemIn.icon;

    }

    public void TempItemOut(Item item)
    {
        tempItemOut = item;
        tempSpriteOut = tempItemOut.icon;
    }

    public Item GetTempItem()
    {
        return tempItemOut;
    }

    public void PutItemInInventory(Item item, Slot slot)
    {
        slot.item=item;
        slot.UpdateSlotOccupied();
        slot.occupied=true;
        movingSlot.Empty();
    }

    public void EquipItem(Item item, Slot slot, EquipmentSlot eqSlot)
    {
        eqSlot.item=item;
        eqSlot.UpdateSlotOccupied();
        eqSlot.occupied=true;
        playerstats.AddStats(item);
        slot.item = null;
        slot.UpdateSlotEmpty();
        slot.occupied = false;
    }

    public void EquipItemSwitch(Item item, Slot slot, EquipmentSlot eqSlot)
    {
        playerstats.AddStats(item);
        playerstats.RemoveStats(eqSlot.item);
        TempItemOut(eqSlot.item);
        eqSlot.item = item;
        eqSlot.UpdateSlotOccupied();
        eqSlot.occupied = true;
        slot.item = tempItemOut;
        slot.UpdateSlotOccupied();
        slot.occupied = true;
    }

    public void PutItemInMovingSlotSwitch(Item item, Slot slot, MovingSlot movingSlot)
    {
        movingSlot.item = item;
        movingSlot.UpdateSlotOccupied();
        movingSlot.occupied = true;
    }

    public void PutItemInMovingSlot(Item item, Slot slot, MovingSlot movingSlot)
    {
        movingSlot.item=slot.item;
        movingSlot.UpdateSlotOccupied();
        movingSlot.occupied=true;
        slot.item = null;
        slot.UpdateSlotEmpty();
        slot.occupied = false;
    }
    public Slot GetOpenSlot()
    {
        for(int i =0; i<slotList.Count; i++)
        {
            if(slotList[i].GetComponent<Slot>().occupied==false)
            {
                return slotList[i].GetComponent<Slot>();
                break;
            }
        }
        return null;
    }

    public bool CheckIfFull()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].GetComponent<Slot>().occupied == false)
            {
                return false;
                break;
            }
        }
        return true;
    }

    public virtual void GenerateInventory()
	{
		Vector3 currOffSet = new Vector3(0,0,0);
		for(int i = 0; i<coloms; i++)
		{
			for(int i2 = 0; i2<rows; i2++)
			{
				GameObject spawnedSlot = (GameObject) Instantiate(slotPrefab,transform.position+ currOffSet, transform.rotation);
                Slot spawnedSlotClass = spawnedSlot.GetComponent<Slot>();
                spawnedSlot.transform.SetParent(slotParent.transform);
                spawnedSlot.GetComponent<Slot>().myImage.color=new Color(1,1,1,0);
                spawnedSlot.GetComponent<Slot>().inventory=this;
                slotList.Add(spawnedSlot);
                currOffSet += new Vector3(offSet,0,0);
			}
            currOffSet += new Vector3(-offSet*rows,-offSet,0);
		}

        
	}
}
