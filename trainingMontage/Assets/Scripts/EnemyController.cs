using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _patrolTargetPosition;
    public Color uranium = new Color();
    private WaypointPath _waypointPath;

    [SerializeField] private float patrolDelay = 1;
    [SerializeField] private float patrolSpeed = 3;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _waypointPath = GetComponentInChildren<WaypointPath>();
    }

    private IEnumerator Start()
    {
        if (_waypointPath)
        {
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();
        }
        else
        {
            StartCoroutine(PatrolCoroutine());
        }
    }

    private void FixedUpdate()
    {
        if (!_waypointPath)
        {
            return;
        }

        var dir = _patrolTargetPosition - (Vector2)transform.position;

        if (dir.magnitude <= 0.1)
        {
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();

            dir = _patrolTargetPosition - (Vector2)transform.position;
        }

        //keep resetting the velocity to the
        //direction * speed even if nudged
        if (GameManager.Instance.State == GameState.Playing)
        {
            _rigidbody.velocity = dir.normalized * patrolSpeed;
        }
         else
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }

    IEnumerator PatrolCoroutine()
    {
        //change the direction every second
        while (true)
        {
            _patrolTargetPosition = new Vector2(1, -1);
            yield return new WaitForSeconds(1);
            _patrolTargetPosition = new Vector2(-1, 1);
            yield return new WaitForSeconds(1);
        }
    }

    //IEnumerator return type for coroutine
    //that can yield for time and come back



    //private void OnEnable()
    //{
    //    GameManager.OnAfterStateChanged += HandleGameStateChange;
    //}

    //private void OnDisable()
    //{
    //    GameManager.OnAfterStateChanged -= HandleGameStateChange;
    //}

    //private void HandleGameStateChange(GameState state)
    //{
    //    if (state == GameState.Starting)
    //    {
    //        GetComponent<SpriteRenderer>().color = Color.grey;
    //    }

    //    if (state == GameState.Playing)
    //    {
    //        GetComponent<SpriteRenderer>().color = Color.magenta;
    //    }
    //}

    private void OnEnable()
    {
        GameManager.OnAfterStateChanged += HandleGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnAfterStateChanged += HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState state)
    {
        if (state == GameState.Starting)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (state == GameState.Playing)
        {
            GetComponent<SpriteRenderer>().color = uranium;
        }
    }
}
