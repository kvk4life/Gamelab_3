using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerStats playerStats = (PlayerStats) target;
        DrawDefaultInspector();
        switch(playerStats.race)
        {
            case Race.RaceType.Human:
                playerStats.hpMultiplier=1;
                break;
            case Race.RaceType.Orc:
                playerStats.hpMultiplier = 1.05f;
                break;
            case Race.RaceType.Dwarf:
                playerStats.hpMultiplier = 1.1f;
                break;
            case Race.RaceType.Tauren:
                playerStats.hpMultiplier = 1.1f;
                break;
            case Race.RaceType.Elf:
                playerStats.hpMultiplier = 0.95f;
                break;
            case Race.RaceType.Undead:
                playerStats.hpMultiplier = 0.9f;
                break;
        }
    }
}
