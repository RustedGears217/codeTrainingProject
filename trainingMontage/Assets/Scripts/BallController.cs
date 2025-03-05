using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _lifeTime = 3;
    [SerializeField] private int damage = 5;
    [SerializeField] private string tagToDamage;
    public void SetDirection(Vector2 direction)
    {
        direction = direction.normalized;
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
        Invoke(nameof(Vanish), _lifeTime);
    }

    private void Vanish()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag(tagToDamage))
        {
            other.transform.GetComponent<HealthSystem>()?.Damage(damage);
        }
    }
}
