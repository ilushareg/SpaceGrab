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
        UpdateControls(Vector2.left);
        transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0), new Vector3(0, 0, 1));
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

        float rot_threshold = 0.2f; //If vector smaller than threshold it will just rotate.

        float thrust = Mathf.Abs(inputStick.magnitude);


        float x = inputStick.x;
        float y = inputStick.y;

        Vector3 new_forward = new Vector3(x, y, 0);

        Quaternion desired_rotation = Quaternion.LookRotation(inputStick, new Vector3(0, 0, 1));

        Quaternion q = Quaternion.Slerp(tr.rotation, desired_rotation, Mathf.Clamp(Time.deltaTime * 10.0f, 0.0f, 1.0f));

        tr.rotation = q;

        if(thrust > rot_threshold)
        { 
            ship.AddForce(tr.forward * thrust * 10);
        }


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
