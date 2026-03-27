using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour,IMonsterInstance
{
    private float currentspeed;
    private int currentHealth;
    public EnemyHealth EnemyHealth;

    private void Awake()
    {
        EnemyHealth = GetComponent<EnemyHealth>(); 
    }

    public void Initialize(MonsterData data, int healthBonus = 0)
    {
        this.currentspeed = data.speed;
        this.EnemyHealth.maxHealth = data.health + healthBonus;
        this.EnemyHealth.ResetHealth();
        this.EnemyHealth.SetPoolSource(data.monsterPrefab);
    }
    private void Update()
    {
        transform.position += transform.forward * currentspeed * Time.deltaTime;
    }
}
