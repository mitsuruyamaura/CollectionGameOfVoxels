using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardDataSO", menuName = "Create RewardDataSO")]
public class RewardDataSO : ScriptableObject
{
    public List<RewardData> rewardDataList = new();
}
