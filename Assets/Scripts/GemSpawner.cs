using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NavMesh.SamplePosition
// https://docs.unity3d.com/ja/current/ScriptReference/AI.NavMesh.SamplePosition.html

// [Unity][C# Script] 敵キャラをNavMeshでかしこくかっこよく動かしてみよう。
// https://zenn.dev/k1togami/articles/71519622146168


public class GemSpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject gemPrefab;


    [SerializeField]
    private Gem[] gemPrefabs;


    public float spawnInterval;

    private float timer;

    [SerializeField]
    private Transform[] spawnGemTrans;

    public bool isSpawnGem;


    void Start() {
        // 宝石の生成
        //SpawnGem();

        StartCoroutine(ObserveTimer());
    }


    //void Update()
    //{
    //    SpawnTimer();


    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        RandomSpawnGem();
    //    }
    //}

    /// <summary>
    /// 時間経過による宝石の生成
    /// </summary>
    private void SpawnTimer() {
        timer += Time.deltaTime;

        if (timer >= spawnInterval) {
            timer = 0;
            SpawnGem();
        }
    }

    /// <summary>
    /// 宝石の生成
    /// </summary>
    public void SpawnGem() {

        // 生成
        //Instantiate(gemPrefab);

        Debug.Log("宝石生成");
    }


    private IEnumerator ObserveTimer() {

        while (isSpawnGem) {
            timer += Time.deltaTime;

            if (timer >= spawnInterval) {
                timer = 0;
                RandomSpawnGem();
            }

            yield return null;
        }

    }

    private void RandomSpawnGem() {
        // 生成位置をランダムに決定
        Vector3 spawnPos = new(Random.Range(spawnGemTrans[0].position.x, spawnGemTrans[1].position.x), spawnGemTrans[0].position.y, Random.Range(spawnGemTrans[0].position.z, spawnGemTrans[1].position.z));

        // 生成するジェムをランダムに決定
        int gemIndex = Random.Range(0, gemPrefabs.Length);


        // 生成
        var gem = Instantiate(gemPrefabs[gemIndex], spawnPos, gemPrefabs[gemIndex].transform.rotation);

        // SamplePosition は、第4引数に指定した範囲内の NavMesh において、第1引数について、最も近い点を検索する。見つかった場合には hit に代入される。置けない場合だけ false になる
        // navMeshHit変数は、NavMeshベイクエリアに置ける場合は、gem の positionの情報が代入される
        // NavMeshベイクエリアじゃない場合、一番近いNavMeshベイクエリアの情報が代入される
        if (NavMesh.SamplePosition(gem.transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
            // hit.position の値は、ベイクしたエリア内に置ける Position の場合には、gem の position と同じ値をそのまま代入し直す
            // そうでない場合には、一番近い NavMesh のベイクエリアの Position の値を代入する
            gem.transform.position = hit.position;

            Debug.Log("宝石の位置調整して配置");
        } else {
            Debug.Log("宝石の位置　調整なし");
        }
    }
}
