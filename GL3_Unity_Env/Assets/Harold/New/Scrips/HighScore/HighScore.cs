using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    public int[] scoreList;
    public int oldHighScore, oldHighScore2;
    public string[] stringList;
    public GameObject[] hudList;

    public void CheckHighScore(int newScore) {
        bool newHigh = false;
        bool switcher = true;
        for(int i = 0; i < scoreList.Length; i++) {
            if(newScore > scoreList[i]) {             
                if (newHigh == false) {
                    oldHighScore = scoreList[i];
                    scoreList[i] = newScore;
                    newHigh = true;
                }
                else {
                    if (switcher) {
                        oldHighScore2 = scoreList[i];
                        scoreList[i] = oldHighScore;
                        switcher = false;
                    }
                    else {
                        oldHighScore = scoreList[i];
                        scoreList[i] = oldHighScore2;
                        switcher = true;
                    }
                }
            }
        }
        SetScoreHud();
    }

    public void SetScoreHud() {
        for(int i = 0; i < scoreList.Length; i++) {
            stringList[i] = string.Format("{0}", scoreList[i]);
            hudList[i].GetComponent<Text>().text = stringList[i];
        }
    }
}
