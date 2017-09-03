using UnityEngine;

public class ShrinkCollisionBehaviour : MonoBehaviour
{
    public float Shrink = 0.05f;
    public Vector3 Torque = new Vector3(0, 0, 0);
    public bool ShouldFreeze;

    private Rigidbody _rigidBody;
    private Quaternion _initialLocalRot;

    void Start()
    {
        _initialLocalRot = transform.localRotation;
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.AddTorque(Torque);
    }

    void OnCollisionEnter(Collision coll)
    {
        transform.parent = coll.transform;
        coll.transform.localScale = coll.transform.localScale * (1.0f - Shrink);
        if (ShouldFreeze)
        {
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            //transform.localRotation = _initialLocalRot;
        }
    }
}