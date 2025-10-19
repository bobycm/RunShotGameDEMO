using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Custom/Monster Data")]
public class MonsterData : ScriptableObject
{
    public string MonsterName = "";

    [Header("Prefab")]
    public GameObject monsterPrefab;

    [Header("Speed")]
    public float speed = 20f;

    [Header("Damage")]
    public int damage = 10;

    [Header("Health")]
    public int health = 10;
}
