using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour
{

    public Text nameText;
    public Text staminaText;
    public Text agilityText;
    public Text intellectText;
    public Text strengthText;
    public Text flavorTextText;
    public Text itemType;
    public GameObject statParent;


    public void UpdateToolTip(ItemQuality itemQuality, string name, int stamina, int agility, int strength, int intellect, string flavorText, Item item)
    {
        switch (itemQuality)
        {
            case ItemQuality.Thrash:
                nameText.color = Color.white;
                break;
            case ItemQuality.Common:
                nameText.color = Color.green;
                break;
            case ItemQuality.Rare:
                nameText.color = Color.blue;
                break;
            case ItemQuality.Epic:
                print("Epic!");
                nameText.color = Color.magenta;
                break;
            case ItemQuality.Legendary:
                nameText.color = Color.yellow;
                break;
        }
        switch (item.itemType)
        {
            case ItemType.Equippable:
                GetComponent<Image>().fillAmount =1f;
                statParent.SetActive(true);
                staminaText.text = "+" + stamina.ToString("F0");
                agilityText.text = "+" + agility.ToString("F0");
                intellectText.text = "+" + intellect.ToString("F0");
                strengthText.text = "+" + strength.ToString("F0");
                switch (item.equipment)
                {
                    case Equipment.Weapon:
                        switch (item.weaponType)
                        {
                            case WeaponType.MainHand:
                                itemType.text = item.mainHand.ToString();
                                break;
                            case WeaponType.OffHand:
                                itemType.text = item.offHand.ToString();
                                break;
                            case WeaponType.TwoHanded:
                                itemType.text = item.twoHanded.ToString();
                                break;
                        }
                        break;
                    default:
                        itemType.text = item.equipment.ToString();
                        break;
                }
                break;
            default:
                statParent.SetActive(false);
                itemType.text = item.itemType.ToString();
                break;
        }
        nameText.text = name;
        flavorTextText.text = flavorText;

    }
}
