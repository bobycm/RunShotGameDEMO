using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // 契約：任何可被傷害的物體都必須提供這個方法
    void TakeDamage(int damageAmount);
}
