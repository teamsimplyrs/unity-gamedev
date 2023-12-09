using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class ProjectileSO : ScriptableObject
{
    [field: SerializeField]
    public Sprite ProjectileSprite { get; private set; }

    [field: SerializeField]
    public AudioClip ProjectileSound { get; private set; }

    [field: SerializeField]
    public List<ProjectileParameter> ProjectileParameters { get; private set; }
}

[Serializable]
public struct ProjectileParameter : IEquatable<ProjectileParameter>
{
    public ProjectileParameterSO projectileParameter;
    public float value;

    public bool Equals(ProjectileParameter other)
    {
        return other.projectileParameter == projectileParameter;
    }
}
