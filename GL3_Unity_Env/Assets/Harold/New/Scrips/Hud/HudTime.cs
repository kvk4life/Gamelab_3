using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudTime : MonoBehaviour {

    public GameObject minutesHud, secondsHud;
    private float secondsSave;
    private int minutesSave;

    void Start() {//remove later, is for testing
        StartTime(0, 0);
    }

    public void StartTime(int minutes, float seconds) {//Start the time by giving it a int for min and float for seconds, this will most likeble 0 for both.
        StartCoroutine(GameTimer(minutes, seconds));
    }

    public void ResetTime() {//reset the time
        StopCoroutine(GameTimer(0, 0));
        StartCoroutine(GameTimer(0, 0));
    }

    public void StopTime() {//stops the time.
        StopCoroutine(GameTimer(minutesSave, secondsSave));
    }
    
    IEnumerator GameTimer(int minutes, float seconds) {

        seconds += 1 * Time.deltaTime;

        if(seconds >= 60) {
            minutes++;
            seconds = 0;
        }

        secondsSave = seconds;
        minutesSave = minutes;

        string min = null;
        string sec = null;

        min = string.Format("{0}", minutes);
        sec = string.Format("{0}", (int)seconds);

        minutesHud.GetComponent<Text>().text = min;
        secondsHud.GetComponent<Text>().text = sec;

        yield return new WaitForSeconds(0);

        StartCoroutine(GameTimer(minutes, seconds));
    }
}
