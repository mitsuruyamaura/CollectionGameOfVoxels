using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NavMesh.SamplePosition
// https://docs.unity3d.com/ja/current/ScriptReference/AI.NavMesh.SamplePosition.html

// [Unity][C# Script] �G�L������NavMesh�ł��������������悭�������Ă݂悤�B
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
        // ��΂̐���
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
    /// ���Ԍo�߂ɂ���΂̐���
    /// </summary>
    private void SpawnTimer() {
        timer += Time.deltaTime;

        if (timer >= spawnInterval) {
            timer = 0;
            SpawnGem();
        }
    }

    /// <summary>
    /// ��΂̐���
    /// </summary>
    public void SpawnGem() {

        // ����
        //Instantiate(gemPrefab);

        Debug.Log("��ΐ���");
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
        // �����ʒu�������_���Ɍ���
        Vector3 spawnPos = new(Random.Range(spawnGemTrans[0].position.x, spawnGemTrans[1].position.x), spawnGemTrans[0].position.y, Random.Range(spawnGemTrans[0].position.z, spawnGemTrans[1].position.z));

        // ��������W�F���������_���Ɍ���
        int gemIndex = Random.Range(0, gemPrefabs.Length);


        // ����
        var gem = Instantiate(gemPrefabs[gemIndex], spawnPos, gemPrefabs[gemIndex].transform.rotation);

        // SamplePosition �́A��4�����Ɏw�肵���͈͓��� NavMesh �ɂ����āA��1�����ɂ��āA�ł��߂��_����������B���������ꍇ�ɂ� hit �ɑ�������B�u���Ȃ��ꍇ���� false �ɂȂ�
        // navMeshHit�ϐ��́ANavMesh�x�C�N�G���A�ɒu����ꍇ�́Agem �� position�̏�񂪑�������
        // NavMesh�x�C�N�G���A����Ȃ��ꍇ�A��ԋ߂�NavMesh�x�C�N�G���A�̏�񂪑�������
        if (NavMesh.SamplePosition(gem.transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
            // hit.position �̒l�́A�x�C�N�����G���A���ɒu���� Position �̏ꍇ�ɂ́Agem �� position �Ɠ����l�����̂܂ܑ��������
            // �����łȂ��ꍇ�ɂ́A��ԋ߂� NavMesh �̃x�C�N�G���A�� Position �̒l��������
            gem.transform.position = hit.position;

            Debug.Log("��΂̈ʒu�������Ĕz�u");
        } else {
            Debug.Log("��΂̈ʒu�@�����Ȃ�");
        }
    }
}
