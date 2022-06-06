using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSetup : MonoBehaviour, IPlanet
{
    [SerializeField] PlanetObject data;

    // Start is called before the first frame update
    void Start()
    {
        data.transform = transform;
    }

    public PlanetObject OnPlanet()
    {
        return data;
    }
}
