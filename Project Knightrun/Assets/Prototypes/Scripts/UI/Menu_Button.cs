using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_Button : MonoBehaviour
{
	private TextMeshProUGUI buttonText;

	private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
	}

	public void changeText(string newText) => buttonText.text = newText;

    private void OnButtonClick()
	{
		Debug.Log("Button pressed");
	}
}
