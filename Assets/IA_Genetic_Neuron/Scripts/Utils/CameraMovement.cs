using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _endPositionZ; // Arr�t de la cam�ra apr�s avoir d�pass� une certaine valeur sur l'axe z

    private void Start()
    {
    }

    void Update()
    {
        MoveCamera();
        if(transform.position.z >= _endPositionZ)
            enabled = false;
    }

    // Mouvement de la cam�ra
    private void MoveCamera()
    {
        float step = _speed * Time.deltaTime;
        transform.position += Vector3.forward * step;
    }
}
