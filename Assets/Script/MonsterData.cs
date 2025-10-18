using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterData", menuName = "Custom/Monster Data")]
public class MonsterData : MonoBehaviour
{
    public string MonsterName = "test";

    [Header("Prefab")]
    public GameObject monsterPrefab;

    [Header("Speed")]
    public float speed = 20f;

    [Header("Damage")]
    public int damage = 10;

    [Header("Life")]
    public int life = 10;
}
