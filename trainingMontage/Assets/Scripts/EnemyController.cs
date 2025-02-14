using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _direction = Vector2.right;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(PatrolCoroutine());
    }

    private void FixedUpdate()
    {
        //keep resetting the velocity to the
        //direction * speed even if nudged
        if (GameManager.Instance.State == GameState.Playing)
        {
            _rigidbody.velocity = _direction;
        }
         else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    //IEnumerator return type for coroutine
    //that can yield for time and come back
    IEnumerator PatrolCoroutine()
    {
        //change the direction every second
        while (true)
        {
            _direction = new Vector2(1, -1);
            yield return new WaitForSeconds(1);
            _direction = new Vector2(-1, 1);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnEnable()
    {
        GameManager.OnAfterStateChanged += HandleGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnAfterStateChanged -= HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState state)
    {
        if (state == GameState.Starting)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }

        if (state == GameState.Playing)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }
}
