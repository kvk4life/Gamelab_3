using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using System;

public class Chest : Inventory
{
    public GameObject openButton,closeButton;
    public GameObject player;
    public Transform inventorySpot;
    public Animator anim;
    public bool isOpen;
    public Item[] chestStartingItems;
    public FirstPersonController fps;
    public AudioSource audio;
    public AudioClip openChest, creaking, closeChest;
    // Use this for initialization
    void Start()
    {
        fps=player.GetComponent<FirstPersonController>();
        audio=player.GetComponent<AudioSource>();
        GenerateInventory();
        for(int i = 0; i<chestStartingItems.Length; i++)
        {
            PutItemInInventory(chestStartingItems[i], slotList[i].GetComponent<Slot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 3 && isOpen==true)
        { 
            CloseInventory();
        }
        if(Input.GetButtonDown("Cancel") && isOpen)
        {
            CloseInventory();
        }
    }

    public void OpenInventory()
    {
        if(uiManager.inventorySwitch==false)
        {
            uiManager.inventorySwitch=!uiManager.inventorySwitch;
            uiManager.ToggleInventory();
        }
        fps.enabled=false;
        isOpen=true;
        anim.SetTrigger("Open");
        slotParent.transform.SetParent(inventorySpot);
        slotParent.transform.position=inventorySpot.position;
        StartCoroutine("ChestCoroutine");
    }

    public void CloseInventory()
    {
        if (uiManager.inventorySwitch == true)
        {
            uiManager.inventorySwitch = !uiManager.inventorySwitch;
            uiManager.ToggleInventory();
        }
        fps.enabled=true;
        isOpen=false;
        anim.SetTrigger("Close");
        slotParent.transform.SetParent(this.gameObject.transform);
        StartCoroutine("ChestCoroutine");
    }

    IEnumerator ChestCoroutine()
    {
        if (isOpen)
        {
            audio.PlayOneShot(openChest);
            yield return new WaitForSeconds(0.33f);
            audio.PlayOneShot(creaking);
        }
        else if (!isOpen)
        {
            yield return new WaitForSeconds(0.25f);
            audio.PlayOneShot(creaking);
            yield return new WaitForSeconds(creaking.length);
            audio.PlayOneShot(closeChest);
        }
    }

    public override void GenerateInventory()
    {
        Vector3 currOffSet = new Vector3(0, 0, 0);
        for (int i = 0; i < coloms; i++)
        {
            for (int i2 = 0; i2 < rows; i2++)
            {
                GameObject spawnedSlot = (GameObject)Instantiate(slotPrefab, transform.position + currOffSet, slotPrefab.transform.rotation);
                Slot spawnedSlotClass = spawnedSlot.GetComponent<Slot>();
                spawnedSlotClass.head = head;
                spawnedSlotClass.shoulders = shoulders;
                spawnedSlotClass.chest = chest;
                spawnedSlotClass.legs = legs;
                spawnedSlotClass.hands = hands;
                spawnedSlotClass.boots = boots;
                spawnedSlot.transform.SetParent(slotParent.transform);
                spawnedSlot.GetComponent<Slot>().myImage.color = new Color(1, 1, 1, 0);
                spawnedSlot.GetComponent<Slot>().inventory = this;
                slotList.Add(spawnedSlot);
                currOffSet += new Vector3(offSet, 0, 0);
            }
            currOffSet += new Vector3(-offSet * rows, -offSet, 0);
        }
    }
}
