using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PowerUp")]
public class PowerUp : ScriptableObject
{
    public bool isProjectile;
    public float quantity;
    public float attackSpeed;
    public float bulletRange;
    public float duration;
    public float damage;
}
