using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {
    public delegate void ButtonDelegate();
    public ButtonDelegate buttonDel;
    public GameObject[] buttonArray;
    public int selectedButton;
    public int startSelectedButton;

    public void Start() {
        foreach (GameObject buttonsInArray in buttonArray) {
            buttonsInArray.GetComponent<CustomButton>().hUDControl = this;
        }
        selectedButton = startSelectedButton;
        buttonArray[selectedButton].GetComponent<CustomButton>().Selected();
    }

    public void Update() {
        SwitchButton();
        ActivateSelectedButton();
    }

    public void SwitchButton() {
        if (Input.GetButtonDown("Horizontal")) {
            buttonArray[selectedButton].GetComponent<CustomButton>().Unselect();
            if (Input.GetAxis("Horizontal") > 0) {
                selectedButton++;
                if (selectedButton > buttonArray.Length-1) {
                    selectedButton = 0;
                }
            }
            else {
                selectedButton--;
                if (selectedButton < 0) {
                    selectedButton = buttonArray.Length-1;
                }
            }
            buttonArray[selectedButton].GetComponent<CustomButton>().Selected();
        }
    }

    public void ActivateSelectedButton() {
        if (Input.GetButtonDown("Jump")) {
            buttonDel();
        }
    }
}
