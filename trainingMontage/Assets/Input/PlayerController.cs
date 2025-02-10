using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    public GameObject Ball;
    private PlayerInput _input;

    void Die()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        transform.position = new Vector2(3, -2);
        _rigidbody = GetComponent<Rigidbody2D>();
        //_rigidbody.velocity = Vector2.right * 5f;

        //Invoke(nameof(Die), 12);

        _input = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {


      if (_input.actions["Fire"].WasPressedThisFrame())
        {
            var ball = Instantiate(Ball, transform.position, Quaternion.identity);

            ball.GetComponent<Rigidbody2D>().velocity = Vector2.left * 10f;
        }
    }

    private void FixedUpdate()
    {
        //set direction to the Move action's Vector2 value
        var dir = _input.actions["Move"].ReadValue<Vector2>();

        //change the velocity to match the Move (every physics update)
        _rigidbody.velocity = dir * 5;
    }
}
