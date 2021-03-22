using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Character
{
    //Finite State Machine for AI behaviour
    public enum State { Patrol, Follow, Flee };

    public State _state = State.Patrol;

    [Header("AI Settings")]
    public float _targetRange;
    public float _targetMaxRange;
    [SerializeField] private UnitCharacter target;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise base character settings
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    //Call corresponding movement function based on FSM
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
            case State.Flee:
                Flee();
                break;
            default:
                _state = State.Patrol;
                break;
        }
        
    }

    //Move to random directions and avoid colliding with the wall and look for enemies
    private void Patrol()
    {
        bool sweepResult = false;
        if (_units[0]._rigidbody.SweepTest(_units[0]._transform.forward, out RaycastHit hit, _targetRange))
        {
            if (hit.collider.TryGetComponent(out Character character))
            {
                target = character.GetTarget();
                if (character._units.Count > _units.Count || character is PlayerCharacter)
                {
                    _state = State.Flee;
                    return;
                }
                else
                {
                    _state = State.Follow;
                    return;
                }
            }
            if (hit.collider.CompareTag("Boundary"))
            {
                sweepResult = true;
            }
        }
        if (moveRotation == Vector3.zero || sweepResult)
        {
            moveRotation.y = Random.Range(0, 180);
            _transform.rotation = Quaternion.Euler(moveRotation);
        }
        _transform.position += _transform.forward * moveSpeed * Time.deltaTime;

    }

    //Follow an enemy when found and try to eat them
    private void Follow()
    {
        if (!target)
        {
            _state = State.Patrol;
            return;
        }

        Vector3 moveDirection = target._transform.position - _transform.position;
        if (moveDirection.magnitude < _targetMaxRange)
        {
            _transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        }
        else
        {
            target = null;
            _state = State.Patrol;
            return;
        }
        _transform.position += _transform.forward * moveSpeed * Time.deltaTime;
    }

    //Flee if the enemy has higher number of units or if the enemy is the player
    private void Flee()
    {
        if (!target)
        {
            _state = State.Patrol;
            return;
        }

        Vector3 fleeDirection = target._transform.position - _transform.position;
        if (fleeDirection.magnitude > _targetMaxRange)
        {
            _state = State.Patrol;
            return;
        }
        else
        {
            moveRotation = Quaternion.LookRotation(-fleeDirection).eulerAngles;
            Patrol();
        }
    }
}
