using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour {

	  public StuffToSave stuffToSave;
   // public GameObject player;
  //  public GameObject upgradeJect;

    void Start() {
       // boostScript = player.GetComponent<BoosterScript>();
       // goldScript = player.GetComponent<GoldMaker>();
      //  upgradeScript = upgradeJect.GetComponent<Upgrades>();
        //hudScript = player.GetComponent<HudScript>();
    }

    public void LoadPress() {
        XMLManager xmlManager = new XMLManager();
        stuffToSave = xmlManager.Load();
        Loader();
    }

    public void SavePress() {
      //  StuffToSave stuffToSave = new StuffToSave();
       // stuffToSave.boost = (int)boostScript.rocketBoostStart;
    }

    void Loader() {
      //  boostScript.rocketBoostStart = (float)stuffToSave.boost;
    }
}
