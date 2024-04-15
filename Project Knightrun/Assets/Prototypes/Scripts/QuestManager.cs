using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> ActiveQuests = new List<Quest>();
    public GameController gameController;

    private void Start()
    {
        int LastDay = PlayerPrefs.GetInt("LastDay");
        if(LastDay != System.DateTime.Now.Day)
        {
            CreateNewQuest();
        }
    }

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void CreateNewQuest()
    {
        int objective = Random.Range(0,4);
        int rewardType = Random.Range(0,2);
        Quest newQuest = ScriptableObject.CreateInstance<Quest>();

        switch (objective)
        {
            case 0:
                newQuest.objectiveType = (Quest.ObjectiveType)objective;
                newQuest.targetAmount = Random.Range(1, 11) * 10;
                newQuest.rewardType = (Quest.RewardType)rewardType;
                if(rewardType == 0)
                {
                    newQuest.rewardAmount = Random.Range(1, 6) * 100;
                    newQuest.description = "Run for " + newQuest.targetAmount + " meters. Reward: " + newQuest.rewardAmount + " softcoins";
                }
                else
                {
                    newQuest.rewardAmount = Random.Range(1,6);
                    newQuest.description = "Run for " + newQuest.targetAmount + " meters. Reward: " + newQuest.rewardAmount + " premiumcoins";
                }
                break;

            case 1:
                newQuest.objectiveType = (Quest.ObjectiveType)objective;
                newQuest.targetAmount = Random.Range(5, 11);
                newQuest.rewardType = (Quest.RewardType)rewardType;
                if (rewardType == 0)
                {
                    newQuest.rewardAmount = Random.Range(5, 11) * 100;
                    newQuest.description = "Destroy " + newQuest.targetAmount + " obstacles. Reward: " + newQuest.rewardAmount + " softcoins";
                }
                else
                {
                    newQuest.rewardAmount = Random.Range(1, 6);
                    newQuest.description = "Destroy " + newQuest.targetAmount + " obstacles. Reward: " + newQuest.rewardAmount + " premiumcoins";
                }
                break;

            case 2:
                newQuest.objectiveType = (Quest.ObjectiveType)objective;
                newQuest.targetAmount = Random.Range(10, 31) * 5;
                newQuest.rewardType = (Quest.RewardType)1;
                newQuest.rewardAmount = Random.Range(1, 4);
                newQuest.description = "Collect " + newQuest.targetAmount + " coins. Reward: " + newQuest.rewardAmount + " premiumcoins";
                break;

            case 3:
                newQuest.objectiveType = (Quest.ObjectiveType)objective;
                newQuest.targetAmount = Random.Range(5, 11);
                newQuest.rewardType = (Quest.RewardType)rewardType;
                if (rewardType == 0)
                {
                    newQuest.rewardAmount = Random.Range(5, 11) * 100;
                    newQuest.description = "Switch mode " + newQuest.targetAmount + " times. Reward: " + newQuest.rewardAmount + " softcoins";
                }
                else
                {
                    newQuest.rewardAmount = Random.Range(1, 6);
                    newQuest.description = "Switch mode " + newQuest.targetAmount + " times. Reward: " + newQuest.rewardAmount + " premiumcoins";
                }
                break;
        }
        ActiveQuests.Add(newQuest);
    }

    public void UpdateQuestProgress(int objective)
    {
        foreach (Quest q in ActiveQuests)
        {
            if((int)q.objectiveType == objective)
            {
                q.currentAmount++;
                if (q.currentAmount >= q.targetAmount)
                {
                    if(q.rewardType == 0)
                    {
                        gameController.totalSoftCurrency += q.rewardAmount;
                    }
                    else
                    {
                        gameController.totalHardCurrency += q.rewardAmount;
                    }
                    ActiveQuests.Remove(q);
                    PlayerPrefs.SetInt("LastDay", System.DateTime.Now.Day);
                }
            }
        }
    }
}