using UnityEngine;
using System.Collections;

public enum TalentTypes
{
    Offensive,
    Defensive,
    Utility,
}

public class TalentTreeManager : MonoBehaviour
{
    public TalentTypes talentTypes;
    public int offensivePoints;
    public int defensivePoints;
    public int utilityPoints;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
