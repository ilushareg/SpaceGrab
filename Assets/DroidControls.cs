using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidControls : MonoBehaviour
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

        Vector2 inputStick = joystick.Direction;
        Rigidbody2D ship = this.GetComponentInParent<Rigidbody2D>();
        Transform tr = this.transform;


        float strength = Mathf.Abs(inputStick.magnitude);

        if (strength > 1.0f)
            strength = 1.0f;

        if (strength <= Mathf.Epsilon)
            return;

        ship.AddForce(inputStick * 1000.0f * Time.deltaTime);



    }
}
