using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float targetHeight = 2f;
    [SerializeField] private float floatForce = 10f;
    [SerializeField] private float moveForce = 2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float difference = targetHeight - transform.position.y;

        rb.AddForce(Vector3.up * difference * floatForce);

        Vector3 randomMovement = new Vector3(
            Mathf.Sin(Time.time),
            0,
            Mathf.Cos(Time.time)
        );

        rb.AddForce(randomMovement * moveForce);
    }
}