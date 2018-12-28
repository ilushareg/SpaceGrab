using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject from_obj = null;
    public GameObject to_obj = null;

    // Start is called before the first frame update
    void Start()
    {
        if(from_obj == null || to_obj == null)
        { return; }
        //Connect objects with number of joints
        Transform tr1 = from_obj.transform;
        Transform tr2 = to_obj.transform;

        int num_joints = 10;
        Vector3 pos = tr1.position;
        Vector3 dpos = (tr2.position - tr1.position) / num_joints;

        //first joint is tricky it has 2 components
        HingeJoint2D joint = from_obj.GetComponentInChildren<HingeJoint2D>();

        if(joint == null) { Debug.Log("from_obj missing HingeJoint2D"); }


        //create chain cell from prefab and link to next one
        for(int i=0; i< num_joints; ++i)
        {
            GameObject newJoint = (GameObject)Object.Instantiate(Resources.Load("Chain"));
            newJoint.transform.position = pos;
            Rigidbody2D body2D = newJoint.GetComponentInChildren<Rigidbody2D>();
            joint.connectedBody = body2D;

            joint = newJoint.GetComponentInChildren<HingeJoint2D>();
            pos += dpos;
        }

        Rigidbody2D body_last = to_obj.GetComponentInChildren<Rigidbody2D>();
        joint.connectedBody = body_last;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
