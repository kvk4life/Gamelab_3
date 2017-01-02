using UnityEngine;
using System.Collections;

public class BarbellCtrl : MonoBehaviour {
    public GameObject barbellParent; //Dit is de bone waar de Barbell aanvast moet komen
    public Transform pickUpHand; //Hier wordt de hoogte (position.y) gebruikt om de hoogte van de barbell te bepalen

    public void ObtainBarbell(GameObject barbellBone) {
        barbellParent = barbellBone;
        Transform barbellBoneTrans = barbellBone.transform;
        transform.SetParent(barbellBoneTrans);
        transform.position = barbellBoneTrans.position;
        transform.rotation = barbellBoneTrans.rotation;
    }
}
