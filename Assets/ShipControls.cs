using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{

    public ParticleSystem engine = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        UpdateParticles();
    }

    void UpdateParticles()
    {
        if (engine == null)
            return;

        ParticleSystem.EmissionModule m = engine.emission;
        m.rateOverTime = 100.0f;
    }

}
