using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    [SerializeField] GameObject _ballPrefab;
    private PlayerInput _input;
    private Vector2 _facingVector = Vector2.right;
    private bool _isRecoiling = false;

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




        if (_input.actions["Pause"].WasPressedThisFrame())
        {
            GameManager.Instance.TogglePause();
        }

        //^^^
        //If these two if statements are swapped in order, then the game cannot unpause
        //vvv

        if (GameManager.Instance.State != GameState.Playing)
        {
            return;
        }

        if (_input.actions["Fire"].WasPressedThisFrame())
        {
            var ball = Instantiate(_ballPrefab, transform.position, Quaternion.identity);

            ball.GetComponent<BallController>()?.SetDirection(_facingVector);
            ball.GetComponent<Rigidbody2D>().velocity = _facingVector.normalized * 10f;
        }

        
    }

    private void FixedUpdate()
    {
        if (_isRecoiling)
        {
            return;
        }

        if (GameManager.Instance.State != GameState.Playing)
        {
            return;
        }

        //set direction to the Move action's Vector2 value
        var dir = _input.actions["Move"].ReadValue<Vector2>();

        //change the velocity to match the Move (every physics update)
        _rigidbody.velocity = dir * 5;

        if (dir.magnitude > 0.5)
        {
            _facingVector = _rigidbody.velocity;
        }
    }

    public void Recoil(Vector2 directionVector)
    {
        _rigidbody.AddForce(directionVector, ForceMode2D.Impulse);
        _isRecoiling = true;
        Invoke(nameof(StopRecoiling), .3f);

    }

    private void StopRecoiling()
    {
        _isRecoiling = false;
    }
}
