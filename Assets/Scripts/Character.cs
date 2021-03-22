using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("GameObject Data")]
    [HideInInspector] public Transform _transform;
    [HideInInspector] public GameObject _gameobject;
    [HideInInspector] public List<UnitCharacter> _units;

    [Header("Mesh Data")]
    [SerializeField] private Material[] _unitMaterials;

    [Header("Movement Data")]
    [Range(0, 100)] public float moveSpeed;
    [Range(0, 100)] public float rotateSpeed;
    public Vector3 moveRotation;

    private void Awake()
    {
        _transform = transform;
        _gameobject = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Initialise Base character Settings
    protected void InitCharacter()
    {
        moveRotation = Vector3.zero;
        _units = new List<UnitCharacter>();
        foreach (UnitCharacter unit in GetComponentsInChildren<UnitCharacter>())
        {
            _units.Add(unit);
            UpdateMaterial(unit);
        }
    }

    //Get a unit from this character
    public UnitCharacter GetTarget()
    {
        return _units[0];
    }

    //Change the material of a unit based on it's level
    protected void UpdateMaterial(UnitCharacter unit)
    {
        unit._renderer.sharedMaterial = _unitMaterials[unit.level];
    }

    //Destroy a unit and check if the game is won or lost
    public void DestroyUnit(UnitCharacter unit)
    {
        _units.Remove(unit);
        Destroy(unit._gameobject);
        if (_units.Count == 0 && _gameobject)
        {
            Destroy(_gameobject);
            FindObjectOfType<GameController>().CheckWin(this);
        }
    } 
    
    //Eat a unit and multiply and cause a chain reaction
    public void Eat(UnitCharacter unit, UnitCharacter target)
    {
        if (!target) return;

        unit.level++;

        if(ChainReact(unit))
        {
            UpdateMaterial(unit);
            UnitCharacter newUnit = Instantiate(unit._gameobject, _transform).GetComponent<UnitCharacter>();
            _units.Add(newUnit);
            newUnit.level = unit.level;
            UpdateMaterial(newUnit);
        }

        target._parent.DestroyUnit(target);
    }

    //Update the levels of all the units of the character having lower level than the unit that ate another unit
    private bool ChainReact(UnitCharacter unit)
    {
        if(unit.level > _unitMaterials.Length - 1)
        {
            DestroyUnit(unit);
            return false;
        }
        else
        {
            foreach(UnitCharacter uni in _units)
            {
                if(uni.level < unit.level - 1)
                {
                    uni.level++;
                    UpdateMaterial(uni);
                }
            }
            return true;
        }
    }
}