using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float minValue = 0.5f;
    public float maxValue = 2.0f;
    public float minSpeed = 50f;
    public float maxSpeed = 150;
    Rigidbody2D rb;
    public float maxSpinSpeed = 50f;
    public GameObject bounceEffectPrefab;

    void Start()
    {
        float randomSize = Random.Range(minValue, maxValue);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        Vector2 randomDirection = Random.insideUnitCircle;
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed);

        float randomSpin = Random.Range(-maxSpeed, maxSpeed);
        rb.AddTorque(randomSpin);


    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;
        GameObject bounceEffect = Instantiate(bounceEffectPrefab, contactPoint, Quaternion.identity);
        Destroy(bounceEffect, 1f);
    }
}
