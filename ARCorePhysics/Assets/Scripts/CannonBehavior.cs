using System;
using UnityEngine;
using System.Collections;
using System.Linq;


public class CannonBehavior : MonoBehaviour
{
    public float ForceMagnitude = 200f;
    public float CannonBallSize = 0.1f;

    public GameObject GazeCursor;
    public Material CannonMaterial;
    public AudioSource ShootSound;
    public AudioClip CollisionClip;

    public void Shoot()
    {
        //    ShootSound.Play();

        var eyeball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        eyeball.transform.localScale = new Vector3(CannonBallSize, CannonBallSize, CannonBallSize);
        eyeball.GetComponent<Renderer>().material = CannonMaterial;

        var rigidBody = eyeball.AddComponent<Rigidbody>();
        rigidBody.mass = 0.5f;
        rigidBody.position = transform.position;
        var forward = transform.forward;
        forward = Quaternion.AngleAxis(-10, transform.right) * forward;
        rigidBody.AddForce(forward * ForceMagnitude);

        eyeball.AddComponent<AudioCollisionBehaviour>().SoundSoftCrash = CollisionClip;
    }

    void Update()
    {
        if (GazeCursor == null) return;

        var raycastHits = Physics.RaycastAll(transform.position, transform.forward);
        var firstHit = raycastHits.OrderBy(r => r.distance).FirstOrDefault();

        GazeCursor.transform.position = firstHit.point;
        GazeCursor.transform.forward = firstHit.normal;
    }
}
