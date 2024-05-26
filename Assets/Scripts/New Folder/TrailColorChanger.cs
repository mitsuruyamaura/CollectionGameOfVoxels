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
        // ボールの生成
        GenerateBall(count);
    }

    /// <summary>
    /// ボールの生成
    /// </summary>
    public void GenerateBall(int count) {

        for (int i = 0; i < count; i++) {
            // ランダムな位置にボールを生成
            GameObject ball = Instantiate(ballPrefab, new(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);

            // ボールの子オブジェクトのパーティクルを取得し、そのパーティクルから Trails モジュールを取得
            ParticleSystem.TrailModule trail = ball.transform.GetChild(0).GetComponent<ParticleSystem>().trails;

            // パーティクルのトレイルの色をランダムな色に変更
            //SetRandomTrailColor(trail);

            // パーティクルのトレイルの色をランダムなグラディエント配色にする
            SetRandomGradientTrailColors(trail);
        }
    }

    /// <summary>
    /// パーティクルのトレイルの色をランダムな配色にする
    /// </summary>
    /// <param name="particles"></param>
    private void SetRandomTrailColor(ParticleSystem.TrailModule trail) {
        trail.colorOverLifetime = colorList[Random.Range(0, colorList.Count)];
    }

    /// <summary>
    /// パーティクルのトレイルの色をランダムなグラディエント配色にする
    /// </summary>
    /// <param name="trail"></param>
    private void SetRandomGradientTrailColors(ParticleSystem.TrailModule trail) {
        trail.colorOverLifetime = new ParticleSystem.MinMaxGradient(colorList[Random.Range(0, colorList.Count)], colorList[Random.Range(0, colorList.Count)]);
    }
}
