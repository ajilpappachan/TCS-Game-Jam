using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacter : MonoBehaviour
{
    [Header("GameObject Data")]
    [HideInInspector] public Transform _transform;
    [HideInInspector] public GameObject _gameobject;
    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector] public Character _parent;
    [HideInInspector] public int level = 0;

    [Header("Mesh Data")]
    [HideInInspector] public MeshRenderer _renderer;

    private void Awake()
    {
        _transform = transform;
        _gameobject = gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
        _parent = GetComponentInParent<Character>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Eat(UnitCharacter unit)
    {
        _parent.Eat(this, unit);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boundary"))
        {
            _parent.DestroyUnit(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out UnitCharacter unit))
        {
            if (unit._parent == _parent) return;
            if(_parent is AICharacter)
            {
                AICharacter parent = (AICharacter)_parent;
                if (parent._state == AICharacter.State.Follow)
                {
                    Eat(unit);
                    parent._state = AICharacter.State.Patrol;
                }
            }
            else
            {
                Eat(unit);
            }
        }
    }
}
