using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int explosionDamage;
    public float explosionRadius;
    public float explosionForce;
    public bool isExplosionWithMe;

    [SerializeField]
    GameObject explosionParticle;



    private void Start()
    {
        Destroy(gameObject, 20f);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }




    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            if (colliders[i].TryGetComponent(out MovementObject movementObject))
                movementObject.OnDamage(explosionDamage, true);
        }
        Instantiate(explosionParticle, transform.position, transform.rotation);

        Destroy(gameObject);

    }
}
