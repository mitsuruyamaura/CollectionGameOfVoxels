using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RewardManager : MonoBehaviour {
    [SerializeField]
    private RewardDataSO rewardDataSO;�@�@�@�@�@�@�@�@�@�@�@�@�@// RewardDataSO �X�N���v�^�u���E�I�u�W�F�N�g���A�T�C�����ĎQ�Ƃ��邽�߂̕ϐ�

    [SerializeField]
    private TreasureBoxDataSO treasureBoxDataSO;�@// JobTypeRewardRatesDataSO �X�N���v�^�u���E�I�u�W�F�N�g���A�T�C�����ĎQ�Ƃ��邽�߂̕ϐ�

    [SerializeField]
    private TreasureType debugTreasureType; �@�@�@�@�@�@�@�@�@�@�@�@�@�@�@// �f�o�b�O�p


    void Update() {

        // �f�o�b�O�p�B�X�y�[�X�L�[�������x�ɒ��I����
        if (Input.GetKeyDown(KeyCode.Space)) {
            ResultingReward(debugTreasureType);
        }
    }

    /// <summary>
    /// ���I����
    /// </summary>
    public void ResultingReward(TreasureType treasureType) {

        // ��Փx����J�܌���
        RewardData rewardData = GetLotteryForRewards(treasureType);

        Debug.Log("���肵���J�܂̒ʂ��ԍ� : " + rewardData.id);

        // TODO �l�������J�܃f�[�^�̊Ǘ��A�Z�[�u�̏����Ȃǂ�����Βǉ�����

    }

    /// <summary>
    /// ���I�̎�����
    /// </summary>
    private RewardData GetLotteryForRewards(TreasureType treasureType) {

        // �I�����ꂽ�g���W���[�{�b�N�X�ɂ��A�J�܃f�[�^�̊󏭓x�̍��v�l���Z�o������ŁA���̒����烉���_���Ȓl������
        int randomRarityValue = Random.Range(0, treasureBoxDataSO.treasureBoxDataList[(int)treasureType].rewardRates.Sum());

        Debug.Log("����̃g���W���[�{�b�N�X�̊󏭓x : " + treasureType + " / �r�o�����J�܃f�[�^�̊󏭓x�̍��v�l : " + treasureBoxDataSO.treasureBoxDataList[(int)treasureType].rewardRates.Sum());
        Debug.Log("�r�o�����J�܃f�[�^�̊󏭓x�����肷�邽�߂̃����_���Ȓl : " + randomRarityValue);

        // ���I�p�̏����l��ݒ�
        RarityType rarityType = RarityType.Common;
        int total = 0;

        // ���肵�������_���l���ǂ̊󏭓x�ɂȂ邩�m�F
        for (int i = 0; i < treasureBoxDataSO.treasureBoxDataList.Count; i++) {

            // �󏭓x�̏d�ݕt���s�����߂ɉ��Z
            total += treasureBoxDataSO.treasureBoxDataList[(int)treasureType].rewardRates[i];
            Debug.Log("�󏭓x�����肷�邽�߂̃����_���Ȓl : " + randomRarityValue + " <= " + " �󏭓x�̏d�ݕt���̍��v�l : " + total);

            // total �̒l���ǂ̊󏭓x�ɊY�����邩�����ԂɊm�F
            if (randomRarityValue <= total) {
                // �󏭓x������
                rarityType = (RarityType)i;
                break;
            }
        }

        Debug.Log("����̊󏭓x : " + rarityType);

        // ����ΏۂƂȂ�󏭓x�����J�܃f�[�^�����̃��X�g���쐬
        List<RewardData> rewardDatas = new List<RewardData>(rewardDataSO.rewardDataList.Where(x => x.rarityType == rarityType).ToList());

        // �����󏭓x�̖J�܂̒񋟊����̒l�̍��v�l���Z�o���āA�����_���Ȓl������
        int randomRewardValue = UnityEngine.Random.Range(0, rewardDatas.Select(x => x.rate).ToArray().Sum());

        Debug.Log("�󏭓x���̖J�ܗp�̃����_���Ȓl : " + randomRewardValue);

        total = 0;

        // ���肵�������_���l���ǂ̖J�܃f�[�^�ɂȂ邩�m�F
        for (int i = 0; i < rewardDatas.Count; i++) {

            // // total �̒l���J�܂ɊY������܂ŉ��Z
            total += rewardDatas[i].rate;
            Debug.Log("�󏭓x���̖J�ܗp�̃����_���Ȓl : " + randomRewardValue + " <= " + " �J�܂̏d�ݕt���̍��v�l : " + total);

            if (randomRewardValue <= total) {

                // �J�܊m��
                return rewardDatas[i];
            }
        }
        return null;
    }
}
