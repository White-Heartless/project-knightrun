using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject
{
    public string description;

    public enum ObjectiveType
    {
        runFor,
        destroyObstacle,
        collectCoins,
        switchMode
    }
    public ObjectiveType objectiveType;
    public int targetAmount;
    public int currentAmount = 0;

    public enum RewardType
    {
        softCoin,
        hardCoin
    }
    public RewardType rewardType;
    public int rewardAmount;

}