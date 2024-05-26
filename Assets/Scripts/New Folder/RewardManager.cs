using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RewardManager : MonoBehaviour {
    [SerializeField]
    private RewardDataSO rewardDataSO;　　　　　　　　　　　　　// RewardDataSO スクリプタブル・オブジェクトをアサインして参照するための変数

    [SerializeField]
    private TreasureBoxDataSO treasureBoxDataSO;　// JobTypeRewardRatesDataSO スクリプタブル・オブジェクトをアサインして参照するための変数

    [SerializeField]
    private TreasureType debugTreasureType; 　　　　　　　　　　　　　　　// デバッグ用


    void Update() {

        // デバッグ用。スペースキーを押す度に抽選する
        if (Input.GetKeyDown(KeyCode.Space)) {
            ResultingReward(debugTreasureType);
        }
    }

    /// <summary>
    /// 抽選処理
    /// </summary>
    public void ResultingReward(TreasureType treasureType) {

        // 難易度から褒賞決定
        RewardData rewardData = GetLotteryForRewards(treasureType);

        Debug.Log("決定した褒賞の通し番号 : " + rewardData.id);

        // TODO 獲得した褒賞データの管理、セーブの処理などがあれば追加する

    }

    /// <summary>
    /// 抽選の実処理
    /// </summary>
    private RewardData GetLotteryForRewards(TreasureType treasureType) {

        // 選択されたトレジャーボックスによる、褒賞データの希少度の合計値を算出した上で、その中からランダムな値を決定
        int randomRarityValue = Random.Range(0, treasureBoxDataSO.treasureBoxDataList[(int)treasureType].rewardRates.Sum());

        Debug.Log("今回のトレジャーボックスの希少度 : " + treasureType + " / 排出される褒賞データの希少度の合計値 : " + treasureBoxDataSO.treasureBoxDataList[(int)treasureType].rewardRates.Sum());
        Debug.Log("排出される褒賞データの希少度を決定するためのランダムな値 : " + randomRarityValue);

        // 抽選用の初期値を設定
        RarityType rarityType = RarityType.Common;
        int total = 0;

        // 決定したランダム値がどの希少度になるか確認
        for (int i = 0; i < treasureBoxDataSO.treasureBoxDataList.Count; i++) {

            // 希少度の重み付け行うために加算
            total += treasureBoxDataSO.treasureBoxDataList[(int)treasureType].rewardRates[i];
            Debug.Log("希少度を決定するためのランダムな値 : " + randomRarityValue + " <= " + " 希少度の重み付けの合計値 : " + total);

            // total の値がどの希少度に該当するかを順番に確認
            if (randomRarityValue <= total) {
                // 希少度を決定
                rarityType = (RarityType)i;
                break;
            }
        }

        Debug.Log("今回の希少度 : " + rarityType);

        // 今回対象となる希少度を持つ褒賞データだけのリストを作成
        List<RewardData> rewardDatas = new List<RewardData>(rewardDataSO.rewardDataList.Where(x => x.rarityType == rarityType).ToList());

        // 同じ希少度の褒賞の提供割合の値の合計値を算出して、ランダムな値を決定
        int randomRewardValue = UnityEngine.Random.Range(0, rewardDatas.Select(x => x.rate).ToArray().Sum());

        Debug.Log("希少度内の褒賞用のランダムな値 : " + randomRewardValue);

        total = 0;

        // 決定したランダム値がどの褒賞データになるか確認
        for (int i = 0; i < rewardDatas.Count; i++) {

            // // total の値が褒賞に該当するまで加算
            total += rewardDatas[i].rate;
            Debug.Log("希少度内の褒賞用のランダムな値 : " + randomRewardValue + " <= " + " 褒賞の重み付けの合計値 : " + total);

            if (randomRewardValue <= total) {

                // 褒賞確定
                return rewardDatas[i];
            }
        }
        return null;
    }
}
