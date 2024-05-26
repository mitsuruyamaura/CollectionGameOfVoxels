/// <summary>
/// トレジャーボックスのデータ
/// </summary>
[System.Serializable]
public class TreasureBoxData
{
    public TreasureType treasureType;
    public int[] rewardRates;          // 褒賞の提供割合[0] = Common, [1] = Uncommon, [2] = Rare
}
