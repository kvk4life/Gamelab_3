using UnityEngine;
using System.Collections;

public enum ItemQuality { Thrash, Common, Rare, Epic, Legendary };
public enum Equipment { Head, Shoulders, Hands, Chest, Legs, Boots, Weapon };
public enum MainHand { Sword, Axe, Dagger };
public enum OffHand { Shield, Trinket };
public enum TwoHanded { TwoHandedSword, TwoHandedAxe };
public enum WeaponType { MainHand, OffHand, TwoHanded};
public enum ItemType { Equippable, Consumable, Reagent };


public class Item : MonoBehaviour
{
    public string itemName;
    public ItemQuality itemQuality;
    public WeaponType weaponType;
    public Equipment equipment;
    public MainHand mainHand;
    public OffHand offHand;
    public TwoHanded twoHanded;
    public ItemType itemType;
    public Sprite icon;
    public GameObject itemPrefab;
    public string flavorText;

    [Header("Stats")]
    public int strength;
    public int agility;
    public int intellect;
    public int stamina;

    [Header("Consumeable Effect")]
    public int restoreHP;
    public int restoreMana;
}
