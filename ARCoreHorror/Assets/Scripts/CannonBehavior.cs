using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CannonBehavior : MonoBehaviour
{
    public float ForceMagnitude = 200f;
    public float CannonBallSize = 0.1f;
    public GameObject ProjectilePrefab;
    public AudioSource ShootSound;
    public GameObject GazeCursor;

    private int MaxProjectilesInScene = 50;
    private readonly List<GameObject> _projectilesShot;

    public CannonBehavior()
    {
        _projectilesShot = new List<GameObject>();
    }

    public void Shoot(Ray ray)
    {
        //    ShootSound.Play();

        var projectile = Instantiate(ProjectilePrefab);
        var rigidBody = projectile.GetComponent<Rigidbody>();

        var forward = ray.direction;
        forward = Quaternion.AngleAxis(-10, transform.right) * forward;
        rigidBody.position = ray.origin;
        rigidBody.AddForce(forward * ForceMagnitude);

        if (_projectilesShot.Count > MaxProjectilesInScene)
        {
            Destroy(_projectilesShot[0]);
            _projectilesShot.RemoveAt(0);
        }
        _projectilesShot.Add(projectile);
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