using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rigidbody;

    void Die()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        transform.position = new Vector2(3, -2);
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.right * 5f;

        Invoke(nameof(Die), 12);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
