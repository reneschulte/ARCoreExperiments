using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkCollisionBehaviour : MonoBehaviour
{
    public float Shrink = 0.05f;
    public Vector3 Torque = new Vector3(0, 0, 0);

    void Start()
    {
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddTorque(Torque);
    }

    void OnCollisionEnter(Collision coll)
    {
        coll.transform.localScale = coll.transform.localScale * (1.0f - Shrink);
    }
}
