using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet", menuName = "ScriptableObjects/PlanetData", order = 1)]
public class PlanetObject : ScriptableObject
{
    public Transform transform;
    public float gravity;
}
