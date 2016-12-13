using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudBars : MonoBehaviour {

    public Image imageBar;
    public float fillSpeed;


    void Start () {
        imageBar = GetComponent<Image>();
    }
	
	void Update () {
        if (Input.GetButtonDown("Jump")) {
            StartCoroutine(ChangeBar(500, 0));
        }

        if (Input.GetButtonDown("Fire1")) {
            StartCoroutine(ChangeBar(500, 276));
        }
	}

    public IEnumerator ChangeBar(float maxHealth, float currentHealth) {
        float currentFill = imageBar.fillAmount;
        float newFill = 1 / maxHealth * currentHealth;

        newFill = Mathf.Round(newFill * 100F) / 100F;

        if(newFill <= currentFill) {
            currentFill-= 0.1F * fillSpeed * Time.deltaTime;
        }
        else {
            currentFill += 0.1F * fillSpeed * Time.deltaTime;
        }

        //Mathf.Round(currentFill * 100);
        //currentFill =/ 100
        currentFill = Mathf.Round(currentFill * 100F) / 100F;
        imageBar.fillAmount = currentFill;

        yield return new WaitForSeconds(0);

        if (currentFill == newFill) {
            StopCoroutine(ChangeBar(0,0));
        }
        else {
            StartCoroutine(ChangeBar(maxHealth, currentHealth));
        }
    }
}
