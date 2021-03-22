using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        _transform.rotation = Quaternion.Euler(moveRotation);
        _transform.position += _transform.forward * moveSpeed * Time.deltaTime;
    }

    public void TurnLeft()
    {
        moveRotation.y -= rotateSpeed * Time.deltaTime;
    }

    public void TurnRight()
    {
        moveRotation.y += rotateSpeed * Time.deltaTime;
    }
}
