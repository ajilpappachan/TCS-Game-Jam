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

    [Header("Mesh Data")]
    [HideInInspector] public MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _gameobject = gameObject;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
        _parent = GetComponentInParent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boundary"))
        {
            _parent.DestroyUnit(this);
        }
    }
}
