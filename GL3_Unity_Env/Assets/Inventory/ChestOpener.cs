using UnityEngine;
using System.Collections;

public class ChestOpener : MonoBehaviour
{
    public RaycastHit hit;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Physics.Raycast(transform.position,transform.forward, out hit, 3f))
        {
            print(hit.transform.name);
            if(hit.transform.tag=="Chest")
            {
                Chest hitChest = (Chest) hit.transform.GetComponent<Chest>();
                if(hitChest.isOpen==false)
                {
                    hitChest.OpenInventory();
                }
            }

        }
    }
}
