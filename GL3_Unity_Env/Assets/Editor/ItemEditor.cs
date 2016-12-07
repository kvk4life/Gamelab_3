using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Item item = (Item) target;
        item.itemName = EditorGUILayout.TextField("Item Name", item.itemName);
        item.icon = (Sprite) EditorGUILayout.ObjectField("Item Icon", item.icon, typeof(Sprite), true);
        item.itemType = (ItemType) EditorGUILayout.EnumPopup("Item Type", item.itemType);
        item.itemQuality = (ItemQuality)EditorGUILayout.EnumPopup("Item Quality", item.itemQuality);
        switch (item.itemType)
        {
            case ItemType.Equippable:
                item.strength = EditorGUILayout.IntField("Strength", item.strength);
                item.agility = EditorGUILayout.IntField("Agility", item.agility);
                item.stamina = EditorGUILayout.IntField("Stamina", item.stamina);
                item.intellect = EditorGUILayout.IntField("Intellect", item.intellect);
                item.equipment = (Equipment) EditorGUILayout.EnumPopup("Equipment Type", item.equipment);
                switch (item.equipment)
                {
                    case Equipment.Weapon:
                        item.weaponType = (WeaponType) EditorGUILayout.EnumPopup("Weapon Type", item.weaponType);
                        switch(item.weaponType)
                        {
                            case WeaponType.MainHand:
                                item.mainHand = (MainHand) EditorGUILayout.EnumPopup("Main Hand", item.mainHand);
                                break;
                            case WeaponType.OffHand:
                                item.offHand = (OffHand)EditorGUILayout.EnumPopup("Off Hand", item.offHand);
                                break;
                            case WeaponType.TwoHanded:
                                item.twoHanded = (TwoHanded)EditorGUILayout.EnumPopup("Two Handed", item.twoHanded);
                                break;
                        }
                        break;

                }
                break;
            case ItemType.Consumable:
                item.restoreHP = EditorGUILayout.IntField("Health Restore", item.restoreHP);
                item.restoreMana = EditorGUILayout.IntField("Mana Restore", item.restoreMana);
                break;
        }
        item.itemPrefab = (GameObject)EditorGUILayout.ObjectField("Item Prefab", item.itemPrefab, typeof(GameObject), true);
        item.flavorText = EditorGUILayout.TextField("Flavor Text", item.flavorText);
    }
}
