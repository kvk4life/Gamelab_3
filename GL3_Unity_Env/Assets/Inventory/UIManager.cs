using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public FirstPersonController fps;
    public Inventory inventory;
    public GameObject slots;
    public bool inventorySwitch;
    public GameObject characterPanel;
    public bool characterPanelSwitch;
    public bool smeltPanelSwitch;
    public GameObject smeltPanel;
    public GameObject blackSmithPanel;
    void Start()
    {
        characterPanelSwitch = !characterPanelSwitch;
        characterPanel.SetActive(characterPanelSwitch);
        smeltPanelSwitch=!smeltPanelSwitch;
        smeltPanel.SetActive(smeltPanelSwitch);
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            print("cancere ");
            inventorySwitch=!inventorySwitch;
            ToggleInventory();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            CharacterToggle();      
        }

    }


    public void CharacterToggle()
    {
        characterPanelSwitch = !characterPanelSwitch;
        characterPanel.SetActive(characterPanelSwitch);
    }

    public void ToggleFPS(bool switchBool)
    {
        //fps.enabled=!switchBool;
    }




    public void ToggleInventory()
    {
        if(inventorySwitch==true)
        {
            slots.gameObject.SetActive(true);
        }
        else if(inventorySwitch==false)
        {
            Slot slot = new Slot();
            slot.tooltip = GameObject.Find("ToolTip");
            slot.HideToolTip();
            if(inventory.movingSlot.occupied==true)
            {
                Slot tempSlot;
                bool hasFound = false;
                for(int i = 0; i<inventory.slotList.Count && hasFound==false; i++)
                {
                    if(inventory.slotList[i].GetComponent<Slot>().occupied==false)
                    {
                        tempSlot = inventory.slotList[i].GetComponent<Slot>();
                        hasFound=true;
                        inventory.PutItemInInventory(inventory.movingSlot.item, tempSlot);
                    }
                }
            }
            slots.gameObject.SetActive(false);
        }
    }
}
