using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : Race
{
    public int health;
    public int maxHealth;
    public int mana;
    public int maxMana;
    public int spellPower;
    public int attackPower;
    public float attackSpeed;
    public float critChance;

    public int strength;
    public int agility;
    public int intellect;
    public int stamina;

    public Text strengthTxt;
    public Text agilityTxt;
    public Text intellectTxt;
    public Text staminaTxt;
    public Text hpText;
    public Text maxHpText;
    public Text raceClassAndLevel;

    public int level;
    public void Start()
    {
        UpdatePlayerStats();
    }

    public void UpdatePlayerStats()
    {
        strengthTxt.text = strength.ToString("F0");
        agilityTxt.text = agility.ToString("F0");
        intellectTxt.text = intellect.ToString("F0");
        staminaTxt.text = stamina.ToString("F0");
        raceClassAndLevel.text = ("Level " + level.ToString("F0") + " " + race.ToString());
        maxHealth = Mathf.RoundToInt(baseHP+(stamina*(1+(level/100f)))*hpMultiplier);
        hpText.text = health.ToString();
        maxHpText.text = maxHealth.ToString();
    }

    public void AddStats(Item item)
    {
        strength+=item.strength;
        agility+=item.agility;
        intellect+=item.intellect;
        stamina+=item.stamina;
        UpdatePlayerStats();
    }

    public void RemoveStats(Item item)
    {
        strength -= item.strength;
        agility -= item.agility;
        intellect -= item.intellect;
        stamina -= item.stamina;
        UpdatePlayerStats();
    }
}
