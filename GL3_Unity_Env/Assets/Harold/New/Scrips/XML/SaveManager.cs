using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour {

	public StuffToSave stuffToSave;
    public HighScore highScoreClass;

    void Start() {
        highScoreClass = GameObject.FindGameObjectWithTag("Player").GetComponent<HighScore>();
    }

    public void LoadPress() {
        XMLManager xmlManager = new XMLManager();
        stuffToSave = xmlManager.Load();
        Loader();
    }

    public void SavePress() {
        for (int i = 0; i < stuffToSave.scoreList.Length; i++) {
            stuffToSave.scoreList[i] = highScoreClass.scoreList[i];
        }
    }

    void Loader() {
        for (int i = 0; i < stuffToSave.scoreList.Length; i++) {
            highScoreClass.scoreList[i] = stuffToSave.scoreList[i];
        }
    }
}
