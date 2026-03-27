using UnityEngine;

public enum ItemWallType
{
    Good,
    Bad
}

[CreateAssetMenu(fileName = "NewItemWallData", menuName = "Custom/ItemWall Data")]
public class ItemWallData : ScriptableObject
{
    public string WallName = "";

    [Header("Wall Type")]
    public ItemWallType wallType = ItemWallType.Good;

    [Header("Material")]
    public Material WallMaterial;

    [Header("Value")]
    public float value = 1f;

}