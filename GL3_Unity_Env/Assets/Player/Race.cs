using UnityEngine;
using System.Collections;

public class Race : MonoBehaviour
{
    public enum RaceType
    {
        Human,
        Elf,
        Orc,
        Dwarf,
        Undead,
        Tauren,
        Pig
    }
    public RaceType race;
    public int baseHP = 100;
    public float hpMultiplier;
}
