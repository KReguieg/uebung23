using UnityEngine;


public class LaunchRocket : MonoBehaviour
{
    [SerializeField] private Rigidbody rocketRigidBody = null;
    [SerializeField] private float rocketForce = 0f;
    [SerializeField] private ForceMode rocketForceMode = ForceMode.Force;

    [SerializeField] private float explosionForce = 0f;
    [SerializeField] private float explosionRadius = 0f;


    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rocketRigidBody.AddForce(this.rocketForce * transform.up, rocketForceMode);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
            this.Explode();
    }

    private void Explode()
    {
        // Maybe instantiate explosion effect here

        // Get all affected objects in the radius
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, this.explosionRadius);

        // Apply Force to all objects in radius
        foreach (Collider affectedObject in affectedObjects)
        {
            var rb = affectedObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(this.explosionForce, transform.position, this.explosionRadius);
        }

        // Destroy the rocket
        Destroy(gameObject);
    }
}
