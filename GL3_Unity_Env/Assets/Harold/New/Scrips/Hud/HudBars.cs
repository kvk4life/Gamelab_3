using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudBars : MonoBehaviour {

    public Image imageBar;
    public float fillSpeed;


    void Start () {
        imageBar = GetComponent<Image>();// place the class on the image you want to be the bar and it will asign itself.
    }
	
	void Update () {// remove later, no update needed
        if (Input.GetButtonDown("Jump")) {//remove later, was for testing.
            StartCoroutine(ChangeBar(500, 0));
        }

        if (Input.GetButtonDown("Fire1")) {
            StartCoroutine(ChangeBar(500, 276));
        }
	}

    public void StartBar(float maxAmount, float CurrentAmount) {//when you want to activate the bar changer start it by giving a value of the max amount (max health, max mana ect.) and the currentAmount ( current health, current mana ect.)
        StopCoroutine(ChangeBar(0, 0));
        StartCoroutine(ChangeBar(maxAmount, CurrentAmount));
    }

    public IEnumerator ChangeBar(float maxAmount, float currentAmount) {
        float currentFill = imageBar.fillAmount;
        float newFill = 1 / maxAmount * currentAmount;

        newFill = Mathf.Round(newFill * 100F) / 100F;

        if(newFill <= currentFill) {
            currentFill-= 0.1F * fillSpeed * Time.deltaTime;
        }
        else {
            currentFill += 0.1F * fillSpeed * Time.deltaTime;
        }

        currentFill = Mathf.Round(currentFill * 100F) / 100F;
        imageBar.fillAmount = currentFill;

        yield return new WaitForSeconds(0);

        if (currentFill == newFill) {
            StopCoroutine(ChangeBar(0,0));
        }
        else {
            StartCoroutine(ChangeBar(maxAmount, currentAmount));
        }
    }
}
