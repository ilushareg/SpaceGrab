using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{

    public ParticleSystem engine = null;

    public FloatingJoystick joystick = null;
    public FixedButton buttonHook = null;
    public FixedButton buttonEngine = null;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputStick = Vector2.zero;
        if (joystick)
        {
            inputStick = joystick.Direction;
        }

        UpdateControls(inputStick);

        //if()
        UpdateParticles(inputStick.magnitude);
    }

    void UpdateControls(Vector2 inputStick)
    {
        Rigidbody2D ship = this.GetComponentInParent<Rigidbody2D>();
        Transform tr = this.transform;


        float strength = Mathf.Abs(inputStick.magnitude);

        if (strength > 1.0f)
            strength = 1.0f;

        if (strength <= Mathf.Epsilon)
            return;

        float rot_threshold = 0.3f; //If vector smaller than threshold it will just rotate.

        float thrust = Mathf.Abs(inputStick.magnitude);

        float x = inputStick.x;
        float y = inputStick.y;

        Quaternion rotation = Quaternion.LookRotation(new Vector3(y, -x, 0), new Vector3(0, 0, 1));
        tr.rotation = rotation;


        //if (strength < rot_threshold)
        //{
        //    tr.rotation.SetLookRotation(Vector3(inputStick.x, inputStick.y, 0));
        //}
        //else
        //{
        //    ship.AddForce(tr.forward * inputStick.magnitude * 100);
        //}


    }

    void UpdateParticles(float strength)
    {
        if (engine == null)
            return;
        strength = Mathf.Abs(strength);

        if (strength > 1.0f)
            strength = 1.0f;

        ParticleSystem.EmissionModule m = engine.emission;

        m.rateOverTime = 100.0f*strength;
    }

}
