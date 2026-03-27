using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemTypeToSprite
{
    public ItemAttributeType itemType;
    public Sprite itemSprite;
}

public class ObjectSpawnSystem : MonoBehaviour
{
    [Header("Item Attribute Sprites")]
    public ItemTypeToSprite[] ItemAttributeSprites;

    [Header("Monster Presets")]
    public MonsterData[] Monsters;

    [Header("Item Wall Presets")]
    public GameObject ItemWallPrefab;
    public ItemWallData[] ItemWalls;

    private float MIN_X = -3;
    private float MAX_X = 3;

    void Start()
    {
        StartCoroutine(AutoSpawnMonsterRoutine());
        StartCoroutine(AutoSpawnItemRoutine());
    }

    IEnumerator AutoSpawnMonsterRoutine()
    {
        while (true)
        {
            if (Monsters != null && Monsters.Length > 0)
            {
                float randomX = Random.Range(MIN_X, MAX_X);
                
                float totalWeight = 0;
                foreach (var monster in Monsters)
                {
                    totalWeight += monster.spawnWeight;
                }

                float randomValue = Random.Range(0, totalWeight);
                float currentWeightSum = 0;
                MonsterData dataToSpawn = Monsters[0];

                foreach (var monster in Monsters)
                {
                    currentWeightSum += monster.spawnWeight;
                    if (randomValue <= currentWeightSum)
                    {
                        dataToSpawn = monster;
                        break;
                    }
                }

                Vector3 spawnPosition = new Vector3(
                    randomX,
                    transform.position.y,
                    transform.position.z
                );

                GameObject monsterobj = ObjectPool.Instance.Get(
                    dataToSpawn.monsterPrefab, 
                    spawnPosition, 
                    transform.rotation);
                
                PoolItemSource poolSource = monsterobj.GetComponent<PoolItemSource>();
                if (poolSource == null) poolSource = monsterobj.AddComponent<PoolItemSource>();
                poolSource.sourcePrefab = dataToSpawn.monsterPrefab;

                IMonsterInstance monsterInstance = monsterobj.GetComponent<IMonsterInstance>();
                if (monsterInstance != null)
                {
                    int healthBonus = 0;
                    if (GameManager.Instance != null)
                    {
                        healthBonus = (int)(GameManager.Instance.survivalTime / 30f) * 10;//health+10/30s
                    }
                    monsterInstance.Initialize(dataToSpawn, healthBonus);
                }
            }
            
            float waitTime = 1f;
            if (GameManager.Instance != null)
            {
                int extraSpawnPerSec = (int)(GameManager.Instance.survivalTime / 60f);//spawn+1/1m
                float spawnsPerSec = 1f + extraSpawnPerSec;
                waitTime = 1f / spawnsPerSec;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator AutoSpawnItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (ItemWalls != null && ItemWalls.Length > 0 && ItemWallPrefab != null)
            {
                List<ItemWallData> goodWalls = new List<ItemWallData>();
                List<ItemWallData> badWalls = new List<ItemWallData>();

                foreach (var wall in ItemWalls)
                {
                    if (wall.wallType == ItemWallType.Good)
                        goodWalls.Add(wall);
                    else if (wall.wallType == ItemWallType.Bad)
                        badWalls.Add(wall);
                }

                if (goodWalls.Count > 0 && badWalls.Count > 0)
                {
                    ItemWallData selectedGood = goodWalls[Random.Range(0, goodWalls.Count)];
                    ItemWallData selectedBad = badWalls[Random.Range(0, badWalls.Count)];

                    float[] spawnXs = new float[] { -2f, 2f };
                    
                    //0 -> -2f，1 -> 2f
                    int goodIndex = Random.Range(0, 2);
                    int badIndex = 1 - goodIndex;

                    ItemWallData[] positionedWalls = new ItemWallData[2];
                    positionedWalls[goodIndex] = selectedGood;
                    positionedWalls[badIndex] = selectedBad;

                    ItemAttributeType chosenType = ItemAttributeType.Health;
                    Sprite chosenSprite = null;

                    if (ItemAttributeSprites != null && ItemAttributeSprites.Length > 0)
                    {
                        int randomAttr = Random.Range(0, ItemAttributeSprites.Length);
                        chosenType = ItemAttributeSprites[randomAttr].itemType;
                        chosenSprite = ItemAttributeSprites[randomAttr].itemSprite;
                    }

                    ItemWall[] spawnedWalls = new ItemWall[2];

                    for (int i = 0; i < 2; i++)
                    {
                        ItemWallData dataToSpawn = positionedWalls[i];
                        float spawnX = spawnXs[i];

                        Vector3 spawnPosition = new Vector3(
                            spawnX,
                            transform.position.y,
                            transform.position.z
                        );

                        GameObject itemobj = ObjectPool.Instance.Get(
                            ItemWallPrefab, 
                            spawnPosition, 
                            transform.rotation);

                        PoolItemSource poolSource = itemobj.GetComponent<PoolItemSource>();
                        if (poolSource == null) poolSource = itemobj.AddComponent<PoolItemSource>();
                        poolSource.sourcePrefab = ItemWallPrefab;

                        IItemWallInst itemInstance = itemobj.GetComponent<IItemWallInst>();
                        if (itemInstance != null)
                        {
                            itemInstance.Initialize(dataToSpawn, chosenType, chosenSprite);
                        }

                        spawnedWalls[i] = itemobj.GetComponent<ItemWall>();
                    }

                    if (spawnedWalls[0] != null && spawnedWalls[1] != null)
                    {
                        spawnedWalls[0].partnerWall = spawnedWalls[1];
                        spawnedWalls[1].partnerWall = spawnedWalls[0];
                    }
                }
            }
        }
    }
}
