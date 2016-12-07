using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour
{
    public RaycastHit hit;
    public MovingSlot movingSlot;
    public GameObject inventory;
    public Inventory inventoryClass;
    public UIManager ui;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position,transform.forward,Color.red);
        if(Input.GetButtonDown("Use"))
        {
            if(Physics.Raycast(transform.position,transform.forward,out hit, 5f))
            {
                if(hit.transform.tag=="Item")
                {
                    GetItem(hit.transform.GetComponent<Item>());
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    void GetItem(Item item)
    {
        movingSlot.item = item;
        movingSlot.occupied=true;
        movingSlot.UpdateSlotOccupied();
    }
}
