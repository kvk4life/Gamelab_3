using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public enum TalentEffect
{
    StatIncrease,
    AbilityGet,
    TotalDamageIncrease,
    Buff,
}

public enum StatToIncrease
{

}


public class Talent : MonoBehaviour
{
    public TalentTreeManager manager;
    public TalentTypes thisType;
    public TalentEffect talentEffect;
    public UnityEvent methods;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            methods.Invoke();
        }
    }


    public void YarakTalent()
    {
        print("Wajow");
    }

    public void YarakTalent2()
    {
        print("2");
    }
}
