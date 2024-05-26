using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreasureBoxDataSO", menuName ="Create TreasureBoxDataSO")]
public class TreasureBoxDataSO : ScriptableObject
{
    public List<TreasureBoxData> treasureBoxDataList = new();
}
