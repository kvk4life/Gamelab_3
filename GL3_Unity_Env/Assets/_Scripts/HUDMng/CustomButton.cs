using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomButton : MonoBehaviour {
    public enum ButtonType{
        StartButton,
        SwitchHUDButton,
        ExitGameButton
    }
    public ButtonType myButtonType;
    [HideInInspector]
    public GameObject hUDMng;
    public GameObject hUDToSwap;
    [HideInInspector]
    public HUDController hUDControl;
    public Image activeMarker;
    public HUDController.ButtonDelegate buttonDel;
    private bool alreadyDecidedTheDel;

    public void Awake() {
        if (!alreadyDecidedTheDel) {
            DecideTheDel();
            alreadyDecidedTheDel = true;
        }
    }

    public void DecideTheDel() {
        switch (myButtonType) {
            case ButtonType.StartButton:
                buttonDel = ButtonStart;
                break;
            case ButtonType.SwitchHUDButton:
                buttonDel = ButtonSwapHUD;
                break;
            case ButtonType.ExitGameButton:
                buttonDel = ButtonExitGame;
                break;
        }
    }

    public void Unselect() {
        activeMarker.GetComponent<CanvasGroup>().alpha = 0;
        EmptyDelegate();
    }

    public void Selected() {
        activeMarker.GetComponent<CanvasGroup>().alpha = 1;
        FillDelegate();
    }

    public void EmptyDelegate() {
        hUDControl.buttonDel = null;
    }

    public void FillDelegate() {
        hUDControl.buttonDel = buttonDel;
    }

    public void ButtonSwapHUD() {
        hUDMng.GetComponent<HUDMng>().SwapHUD(hUDToSwap);
        print("I'm the option button!");
    }

    public void ButtonStart() {
        SceneManager.LoadScene(1);
        print("I'm the Start button!");
    }

    public void ButtonExitGame() {
        Application.Quit();
        print("I'm the Exit Game Button!");
    }
}
