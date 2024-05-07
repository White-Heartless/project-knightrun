using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
	[SerializeField]
	private GameController gameController;

	[SerializeField]
	private AudioController audioController;

	[SerializeField]
	private Canvas cnvMainMenu, cnvSettings, cnvGameplay, cnvPause, cnvGameOver, cnvRevive, cnvEquipMenu, cnvShopMenu, cnvConfirm;

	[SerializeField]
	private Button[] priceTags;

    [SerializeField]
    private Button[] armorStash;

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
	[SerializeField]
	private TextMeshProUGUI txtCurrentQuest;
	[SerializeField]
	private TextMeshProUGUI txtTarget;
	[SerializeField]
	private TextMeshProUGUI txtObjective;
	[SerializeField]
	private TextMeshProUGUI txtEquipCount;
	[SerializeField]
	private TextMeshProUGUI txtDistance;
	[SerializeField]
	private TextMeshProUGUI txtOverHighScore;
	[SerializeField]
	private TextMeshProUGUI txtReviveHardCurrency;
	[SerializeField]
	private TextMeshProUGUI txtArmorName;
	[SerializeField]
	private TextMeshProUGUI txtArmorUses;
	[SerializeField]
	private TextMeshProUGUI txtOverSoftCurrency, txtOverHardCurrency;
	[SerializeField]
	private string url;

	[SerializeField]
    bool isAudioPlaying;

	[SerializeField]
	private GameObject[] checkBox;


    void Start()
	{
		audioController = FindObjectOfType<AudioController>();
		color[0] = EquipIcons[0].color;
		color[1] = EquipIcons[1].color;
		color[2] = EquipIcons[2].color;
		color[3] = EquipIcons[3].color;
		color[4] = EquipIcons[4].color;
		txtGameplaySoftCurrency.text = "0";
		txtGameplayHardCurrency.text = "0";
		cnvMainMenu.gameObject.SetActive(true);
		isAudioPlaying = true;
		CheckBoxOFF();
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
		txtOverSoftCurrency.text = _newSoft.ToString();
		txtOverHardCurrency.text = _newHard.ToString();
	}

	public void updateEquipCount(int _newEquipCount)
	{
		_newEquipCount = gameController.equipmentCounts;
        txtEquipCount.text = _newEquipCount.ToString();
    }


    public void updateDistance(float _newDistance)
	{
		txtGameplayDistance.text = ((int)_newDistance).ToString() + "m";
		txtDistance.text = ((int)_newDistance).ToString() + "m";
	}

	public void updateHighScore(int _newHighScore)
	{
		txtMainHighScore.text = _newHighScore.ToString() + "m";
		txtOverHighScore.text = _newHighScore.ToString() + "m";
	}

	public void updateQuestProgress(int _newAmount)
	{
		txtCurrentQuest.text = _newAmount.ToString();
	}

	public void setQuest(int _targetAmount, int _questType)
	{
		txtTarget.text = _targetAmount.ToString();
        switch (_questType)
        {
			case 0:
				txtObjective.text = "Run";
				break;

			case 1:
				txtObjective.text = "Destroy Obstacles";
				break;

			case 2:
				txtObjective.text = "Collect Coins";
				break;

			case 3:
				txtObjective.text = "Switch Mode";
				break;
		}
	}

	public void resetUI()
	{
		cnvMainMenu.gameObject.SetActive(true);
		cnvSettings.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(false);
		cnvPause.gameObject.SetActive(false);
		cnvGameOver.gameObject.SetActive(false);
		cnvRevive.gameObject.SetActive(false);
		cnvConfirm.gameObject.SetActive(false);
	}

	public void btnSettings()
	{
		cnvSettings.gameObject.SetActive(true);
		cnvMainMenu.gameObject.SetActive(false);
        audioController.AudioTapPlay();
    }

	public void btnCloseSettings()
	{
		cnvSettings.gameObject.SetActive(false);
		cnvMainMenu.gameObject.SetActive(true);
        audioController.AudioTapPlay();
    }

	public void btnAudio()
	{
        audioController.AudioTapPlay();
        isAudioPlaying = !isAudioPlaying;

		if (!isAudioPlaying)
		{
			audioController.MuteAll();
			CheckBoxON();
		}
		else
		{
            audioController.UnmuteAll();
            CheckBoxOFF();
        }
    }

	public void btnCredits()
	{
        Application.OpenURL(url);
    }

	public void btnPlay()
	{
        audioController.AudioTapPlay();
        cnvGameplay.gameObject.SetActive(true);
		cnvMainMenu.gameObject.SetActive(false);
		gameController.onGameStart();
	}

	public void btnPause()
	{
        audioController.AudioTapPlay();
        cnvPause.gameObject.SetActive(true);
        cnvConfirm.gameObject.SetActive(false);
        gameController.onPause();
	}

	public void btnResume()
	{
        audioController.AudioTapPlay();
        cnvPause.gameObject.SetActive(false);
		cnvConfirm.gameObject.SetActive(false);
		gameController.onResume();
	}

	public void btnEquipMenuEnter()
	{
		audioController.AudioArmorStashPlay();
        cnvMainMenu.gameObject.SetActive(false);
		cnvEquipMenu.gameObject.SetActive(true);
		//gameController.onEquipMenuEnter();
	}

	public void btnEquipMenuExit()
	{
        audioController.AudioTapPlay();
        cnvEquipMenu.gameObject.SetActive(false);
		cnvMainMenu.gameObject.SetActive(true);
		//gameController.onEquipMenuExit();
	}

    public void btnShopMenuEnter()
    {
		audioController.AudioChestOpenPlay();
        cnvShopMenu.gameObject.SetActive(true);
		cnvMainMenu.gameObject.SetActive(false);
    }

	public void btnSpendCurrency(int index)
	{
		gameController.SpendSoftCurrency(index);
		audioController.AudioCashPlay();
	}
	public void btnSpendCurrencyArmor(int index)
	{
		armorStash[index].interactable = true;
	}

	public void btnShopMenuExit()
	{
        audioController.AudioTapPlay();
        cnvMainMenu.gameObject.SetActive(true);
		cnvShopMenu.gameObject.SetActive(false);
	}

    public void btnPlayer(int i)
	{
		audioController.AudioSwooshPlay();
        gameController.PlayerSwap(i);
	}

	public void btnArmorName(string _armorName)
	{
        txtArmorName.text = _armorName;
    }

	public void btnUses(int i)
	{
        txtArmorUses.text = i + " USES";
	}

    public void btnQuit()
	{
		gameController.CurrencySum();
		gameController.CurrencyUpdate();
		cnvRevive.gameObject.SetActive(false);
		cnvGameOver.gameObject.SetActive(true);
		cnvPause.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(false);
		cnvMainMenu.gameObject.SetActive(false);
        cnvConfirm.gameObject.SetActive(false);
    }

	public void btnQuitConfimation()
	{
        cnvConfirm.gameObject.SetActive(true);
        cnvRevive.gameObject.SetActive(false);
        cnvGameOver.gameObject.SetActive(false);
        cnvPause.gameObject.SetActive(false);
        cnvGameplay.gameObject.SetActive(false);
        cnvMainMenu.gameObject.SetActive(false);
    }

	public void btnGameOver()
	{
        cnvRevive.gameObject.SetActive(false);
		cnvGameOver.gameObject.SetActive(false);
		cnvPause.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(false);
		cnvMainMenu.gameObject.SetActive(true);
        cnvConfirm.gameObject.SetActive(false);
        gameController.onGameOver();
    }

	public void btnPlayAgain()
	{
        cnvRevive.gameObject.SetActive(false);
        cnvGameOver.gameObject.SetActive(false);
        cnvPause.gameObject.SetActive(false);
        cnvGameplay.gameObject.SetActive(true);
        cnvMainMenu.gameObject.SetActive(false);
        cnvConfirm.gameObject.SetActive(false);
        gameController.onGameOver();
		gameController.onGameStart();
    }

	public void promptRevive()
	{
        txtReviveHardCurrency.text = gameController.totalHardCurrency.ToString();
        cnvRevive.gameObject.SetActive(true);
		cnvGameplay.gameObject.SetActive(false);
	}

	public void btnAcceptReviveHardCurrency()
	{
		if (gameController.onHardCurrencyReviveAttempt())
		{
            cnvRevive.gameObject.SetActive(false);
			cnvGameplay.gameObject.SetActive(true);
			gameController.Revive();
		}
	}

	public void btnAcceptReviveAD()
	{
        cnvRevive.gameObject.SetActive(false);
		cnvGameplay.gameObject.SetActive(true);
		gameController.Revive();
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

	public void PriceTagsOFF()
	{
        foreach (var priceTag in priceTags)
        {
            priceTag.interactable = false;
        }
    }

	public void PriceTagsON(int index)
	{	
		priceTags[index].interactable = true;
	}

	private void CheckBoxOFF()
	{
        for (int i = 0; i < checkBox.Length; i++)
        {
            checkBox[i].SetActive(false);
        }
    }

	private void CheckBoxON()
	{
		for (int i = 0; i < checkBox.Length; i++)
		{
			checkBox[i].SetActive(true);
		}
	}
}
