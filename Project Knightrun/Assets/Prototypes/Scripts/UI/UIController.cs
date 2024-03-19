using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.cyborgAssets.inspectorButtonPro;


public class UIController : MonoBehaviour
{
	[SerializeField]
	private Canvas cnvMainMenu, cnvSettings, cnvGameplay, cnvPause, cnvGameOver, cnvRevive;

	[SerializeField]
	private Image[] EquipIcons;

	private Color[] color = new Color[3];

	void Start()
	{
		color[0] = EquipIcons[0].color;
		color[1] = EquipIcons[1].color;
		color[2] = EquipIcons[2].color;
	}

	public void btnSettings()
	{
		cnvSettings.gameObject.SetActive(true);
		cnvMainMenu.gameObject.SetActive(false);
	}

	public void btnCloseSettings()
	{
		cnvSettings.gameObject.SetActive(false);
		cnvMainMenu.gameObject.SetActive(true);
	}

	public void btnPlay()
	{
		cnvGameplay.gameObject.SetActive(true);
		cnvMainMenu.gameObject.SetActive(false);
		//signal gamecontroller to play
	}

	public void btnPause()
	{
		cnvPause.gameObject.SetActive(true);
		//signal gamecontroller to pause
	}

	public void btnResume()
	{
		cnvPause.gameObject.SetActive(false);
		//signal gamecontroller to resume
	}

	public void btnQuit()
	{
		cnvRevive.gameObject.SetActive(false);
		cnvGameOver.gameObject.SetActive(false);
		cnvPause.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(false);
		cnvMainMenu.gameObject.SetActive(true);
		//signal gamecontroller to quit
	}

	public void btnPlayAgain()
	{
		cnvGameOver.gameObject.SetActive(false);
		cnvPause.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(true);
		//signal gamecontroller to restart
	}

	[ProPlayButton]
	public void EquipAlpha(int index)
	{
		if (EquipIcons[index].color.a != 0.2f)
		{
			color[index].a = 0.2f;
			EquipIcons[index].color = color[index];
		}
		else
		{
			color[index].a = 1f;
			EquipIcons[index].color = color[index];
		}
	}
}
