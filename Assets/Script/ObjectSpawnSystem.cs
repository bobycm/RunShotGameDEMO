using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnSystem : MonoBehaviour
{
    [Header("生成怪物清單")]
    public MonsterData[] Monsters;
    private float MIN_X = -3;
    private float MAX_X = 3;

    void Start()
    {
        StartCoroutine(AutoSpawnMonsterRoutine());
    }
    IEnumerator AutoSpawnMonsterRoutine()
    {
        while (true)
        {
            float randomX = Random.Range(MIN_X, MAX_X);
            int randomMonster = Random.Range(0, Monsters.Length);
            MonsterData dataToSpawn = Monsters[randomMonster];

            Vector3 spawnPosition = new Vector3(
                randomX,
                transform.position.y,
                transform.position.z
            );

            GameObject monsterobj = Instantiate(
                dataToSpawn.monsterPrefab, 
                spawnPosition, 
                transform.rotation);
            IMonsterInstance monsterInstance = monsterobj.GetComponent<IMonsterInstance>();
            if (monsterInstance != null)
            {
                monsterInstance.Initialize(dataToSpawn);
            }

            yield return new WaitForSeconds(1);
        }
    }
}
