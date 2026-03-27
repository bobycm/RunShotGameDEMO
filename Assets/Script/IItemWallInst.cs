using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemAttributeType
{
    Health,
    Fireball, 
    Spear,
    Bullet
}

public interface IItemWallInst
{
    void Initialize(ItemWallData data, ItemAttributeType itemType, Sprite itemSprite);
}
