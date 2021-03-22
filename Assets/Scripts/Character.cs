using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("GameObject Data")]
    [HideInInspector] public Transform _transform;
    [HideInInspector] public GameObject _gameobject;
    [HideInInspector] public List<UnitCharacter> units;

    [Header("Mesh Data")]
    [SerializeField] protected Material _startMaterial;
    [SerializeField] protected Material _level1Material;
    [SerializeField] protected Material _level2Material;
    [SerializeField] protected Material _level3Material;

    [Header("Movement Data")]
    public float moveSpeed;
    public float rotateSpeed;
    public Vector3 moveRotation;

    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void InitCharacter()
    {
        _transform = transform;
        _gameobject = gameObject;
        units = new List<UnitCharacter>();
        foreach (UnitCharacter unit in GetComponentsInChildren<UnitCharacter>())
        {
            units.Add(unit);
            UpdateMaterial(unit._renderer, _startMaterial);
        }
    }

    public UnitCharacter GetTarget()
    {
        return units[0];
    }

    protected void UpdateMaterial(MeshRenderer _renderer, Material mat)
    {
        _renderer.sharedMaterial = mat;
    }

    public void DestroyUnit(UnitCharacter unit)
    {
        units.Remove(unit);
        Destroy(unit._gameobject);
        if (units.Count == 0)
        {
            Debug.Log("Kill Character");
            Destroy(_gameobject);
        }
    }    
}
