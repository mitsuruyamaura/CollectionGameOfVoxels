using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColorChanger : MonoBehaviour
{
    [SerializeField]
    private List<Color> colorList = new();

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private int count = 3;


    void Start() {
        // �{�[���̐���
        GenerateBall(count);
    }

    /// <summary>
    /// �{�[���̐���
    /// </summary>
    public void GenerateBall(int count) {

        for (int i = 0; i < count; i++) {
            // �����_���Ȉʒu�Ƀ{�[���𐶐�
            GameObject ball = Instantiate(ballPrefab, new(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);

            // �{�[���̎q�I�u�W�F�N�g�̃p�[�e�B�N�����擾���A���̃p�[�e�B�N������ Trails ���W���[�����擾
            ParticleSystem.TrailModule trail = ball.transform.GetChild(0).GetComponent<ParticleSystem>().trails;

            // �p�[�e�B�N���̃g���C���̐F�������_���ȐF�ɕύX
            //SetRandomTrailColor(trail);

            // �p�[�e�B�N���̃g���C���̐F�������_���ȃO���f�B�G���g�z�F�ɂ���
            SetRandomGradientTrailColors(trail);
        }
    }

    /// <summary>
    /// �p�[�e�B�N���̃g���C���̐F�������_���Ȕz�F�ɂ���
    /// </summary>
    /// <param name="particles"></param>
    private void SetRandomTrailColor(ParticleSystem.TrailModule trail) {
        trail.colorOverLifetime = colorList[Random.Range(0, colorList.Count)];
    }

    /// <summary>
    /// �p�[�e�B�N���̃g���C���̐F�������_���ȃO���f�B�G���g�z�F�ɂ���
    /// </summary>
    /// <param name="trail"></param>
    private void SetRandomGradientTrailColors(ParticleSystem.TrailModule trail) {
        trail.colorOverLifetime = new ParticleSystem.MinMaxGradient(colorList[Random.Range(0, colorList.Count)], colorList[Random.Range(0, colorList.Count)]);
    }
}
