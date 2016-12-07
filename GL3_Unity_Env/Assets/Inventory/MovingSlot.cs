using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovingSlot : Slot
{
    public void Update()
    {
        transform.position = Input.mousePosition;
    }
    public void Empty()
    {
        Image image = GetComponent<Image>();
        occupied=false;
        image.sprite=null;
        item=null;
        occupiedSprite=null;
        image.color=new Color(1,1,1,0);
    }

}
