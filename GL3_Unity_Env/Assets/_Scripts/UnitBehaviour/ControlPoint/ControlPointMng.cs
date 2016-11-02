using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlPointMng : MonoBehaviour {
    public GameObject controlPoint;
    private ControlPoint theControlPoint;
    public List<Transform> controlSpots = new List<Transform>();

    public void Start() {
        theControlPoint = controlPoint.GetComponent<ControlPoint>();
        controlPoint.GetComponent<ControlPoint>().Enabler(false);
    }

    public void UnpoolControlPoint() {
        int randomSpot = Random.Range(0, controlSpots.Count);
        theControlPoint.Unpool(controlSpots[randomSpot].position);
    }

    public void ControlPointSucces() {
        if (!theControlPoint.pooled) {
            BonusThePlayers();
        }
    }

    public void BonusThePlayers() {
        print("Player gets the Bonus!");
    }
}
