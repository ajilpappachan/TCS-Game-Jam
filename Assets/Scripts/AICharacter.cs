using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Character
{
    enum State { Patrol, Follow, Attack };

    [SerializeField] private State _state = State.Patrol;

    [Header("AI Settings")]
    public float _targetRange;
    [SerializeField] private UnitCharacter target;

    // Start is called before the first frame update
    void Start()
    {
        moveRotation.y = Random.Range(0, 180);
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        switch(_state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Follow:
                Follow();
                break;
            case State.Attack:
                Attack();
                break;
            default:
                _state = State.Patrol;
                break;
        }
        _transform.position += _transform.forward * moveSpeed * Time.deltaTime;
    }

    private void Patrol()
    {
        bool sweepResult = false;
        if(units[0]._rigidbody.SweepTest(units[0]._transform.forward, out RaycastHit hit, _targetRange))
        {
            if(hit.collider.TryGetComponent(out Character character))
            {
                target = character.GetTarget();
                _state = State.Follow;
                return;
            }
            if(hit.collider.CompareTag("Boundary"))
            {
                sweepResult = true;
            }
        }
        if (moveRotation == Vector3.zero || sweepResult)
        {
            moveRotation.y = Random.Range(0, 180);
            _transform.rotation = Quaternion.Euler(moveRotation);
        }
        
    }

    private void Follow()
    {

    }

    private void Attack()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
