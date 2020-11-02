using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    //TODO build a more structured connection
    public static ITargetable CurrentTarget;
    //interfaces don't serialize, so need class reference
    //[SerializeField] Creature _objectToTarget = null;

    Camera _camera = null;
    RaycastHit _hitInfo;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GetNewMouseHit();
            SetTarget();
        }
    }

    void GetNewMouseHit()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out _hitInfo, Mathf.Infinity))
        {
            Debug.Log("Ray hit: " + _hitInfo.transform.name);
        }
    }

    void SetTarget()
    {
        ITargetable possibleTarget =
            _hitInfo.transform.GetComponent<ITargetable>();
        if(possibleTarget != null)
        {
            Debug.Log("New target acquired!");
            CurrentTarget = possibleTarget;
            possibleTarget.Target();
        }
    }
}
