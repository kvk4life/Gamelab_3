using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorMessages : MonoBehaviour
{
    public Text errorText;
    public void ErrorMessage(string errorMessage)
    {
        errorText=GameObject.Find("ErrorMessage").GetComponent<Text>();
        errorText.text = errorMessage;
    }
}
