using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.cyborgAssets.inspectorButtonPro;
using TMPro;


public class UIController : MonoBehaviour
{
	[SerializeField]
	private GameController gameController;

	[SerializeField]
	private Canvas cnvMainMenu, cnvSettings, cnvGameplay, cnvPause, cnvGameOver, cnvRevive;

	[SerializeField]
	private Image[] EquipIcons;

	private Color[] color = new Color[5];

	[SerializeField]
	private TextMeshProUGUI txtGameplaySoftCurrency;
	[SerializeField]
	private TextMeshProUGUI txtGameplayHardCurrency;
	[SerializeField]
	private TextMeshProUGUI txtMainTotalSoftCurrency;
	[SerializeField]
	private TextMeshProUGUI txtMainTotalHardCurrency;
	[SerializeField]
	private TextMeshProUGUI txtGameplayDistance;
	[SerializeField]
	private TextMeshProUGUI txtMainHighScore;

	void Start()
	{
		color[0] = EquipIcons[0].color;
		color[1] = EquipIcons[1].color;
		color[2] = EquipIcons[2].color;
		color[3] = EquipIcons[3].color;
		color[4] = EquipIcons[4].color;
		txtGameplaySoftCurrency.text = "0";
		txtGameplayHardCurrency.text = "0";
	}

	public void updateSoftCurrency(int _newValue)
	{
		txtGameplaySoftCurrency.text = _newValue.ToString();
	}

	public void updateHardCurrency(int _newValue)
	{
		txtGameplayHardCurrency.text = _newValue.ToString();
	}

	public void updateTotalCurrency(int _newSoft, int _newHard)
	{
		txtMainTotalSoftCurrency.text = _newSoft.ToString();
		txtMainTotalHardCurrency.text = _newHard.ToString();
	}

	public void updateDistance(float _newDistance)
	{
		txtGameplayDistance.text = ((int)_newDistance).ToString() + "m";
	}

	public void updateHighScore(int _newHighScore)
	{
		txtMainHighScore.text = _newHighScore.ToString() + "m";
	}

	public void resetUI()
	{
		cnvMainMenu.gameObject.SetActive(true);
		cnvSettings.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(false);
		cnvPause.gameObject.SetActive(false);
		cnvGameOver.gameObject.SetActive(false);
		cnvRevive.gameObject.SetActive(false);
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
		gameController.onGameStart();
	}

	public void btnPause()
	{
		cnvPause.gameObject.SetActive(true);
		gameController.onPause();
	}

	public void btnResume()
	{
		cnvPause.gameObject.SetActive(false);
		gameController.onResume();
	}

	public void btnQuit()
	{
		cnvRevive.gameObject.SetActive(false);
		cnvGameOver.gameObject.SetActive(false);
		cnvPause.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(false);
		cnvMainMenu.gameObject.SetActive(true);
		gameController.onGameOver();
	}

	public void promptRevive()
	{
		cnvRevive.gameObject.SetActive(true);
	}

	public void btnAcceptReviveHardCurrency()
	{
		if (gameController.onHardCurrencyReviveAttempt())
		{
			cnvRevive.gameObject.SetActive(false);
			gameController.Revive();
		}
	}

	public void btnAcceptReviveAD()
	{
		cnvRevive.gameObject.SetActive(false);
		gameController.Revive();
	}

	public void btnPlayAgain() //unused
	{
		cnvGameOver.gameObject.SetActive(false);
		cnvPause.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(true);
	}

	[ProButton]
	public void EquipAlpha(int index, bool isActive)
	{
		if (isActive)
		{
			color[index].a = 1f;
			EquipIcons[index].color = color[index];
		}
		else
		{
			color[index].a = 0.2f;
			EquipIcons[index].color = color[index];
		}
	}
}
