using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData")]
public class EntitiesData : ScriptableObject
{
    public float health;
    public float speed;
    public float damage;
}
