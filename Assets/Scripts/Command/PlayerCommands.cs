﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    [SerializeField] BoardSpawner _boardSpawner = null;

    Camera _camera = null;
    RaycastHit _hitInfo;

    CommandInvoker _commandInvoker = new CommandInvoker();

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        //Spawn command!

        if (Input.GetMouseButtonDown(0))
        {
            GetNewMouseHit();
            SpawnToken();
        }

        if (Input.GetMouseButtonDown(1))
        {
            GetNewMouseHit();
            BuffToken();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }
    }

    void GetNewMouseHit()
    {
        //spawn token at mouse position

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out _hitInfo, Mathf.Infinity))
        {
            Debug.Log("Ray hit: " + _hitInfo.transform.name);
        }
    }

    void SpawnToken()
    {
        //create the command
        ICommand spawnTokenCommand =
            new SpawnTokenCommand(_boardSpawner, _hitInfo.point);
        //perform the command!
        _commandInvoker.ExecuteCommand(spawnTokenCommand);
    }

    public void Undo()
    {
        _commandInvoker.UndoCommand();
    }

    public void BuffToken()
    {
        //note, this search only works if the Collider and IBuffable components are attached to the same GameObject.

        IBuffable buffableUnit =
            _hitInfo.transform.GetComponent<IBuffable>();
        //if we have the token, command it to buff
        if(buffableUnit != null)
        {
            ICommand buffCommand = new BuffCommand(buffableUnit);
            _commandInvoker.ExecuteCommand(buffCommand);
        }
    }
}
