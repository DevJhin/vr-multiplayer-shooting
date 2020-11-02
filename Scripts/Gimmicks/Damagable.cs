using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

[RequireComponent(typeof(Collider))]
public class Damagable : MonoBehaviour
{
    Enemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void ApplyDamage(int playerIndex)
    {
        Debug.Log($"Damage applied by {gameObject.name} of {enemy.gameObject.name}");
        enemy.ApplyDamage(playerIndex);
    }
}
