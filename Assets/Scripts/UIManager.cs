using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;


    /// <summary>
    /// �X�R�A�̕\���X�V
    /// </summary>
    /// <param name="score"></param>
    public void UpdateDisplayScore(int score) {
        txtScore.text = score.ToString();
    }
}
