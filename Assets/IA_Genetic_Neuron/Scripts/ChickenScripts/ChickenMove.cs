using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMove : MonoBehaviour
{
    [SerializeField] private float _speed; // vitesse du poulet

    private void Update()
    {
        Move();
    }

    // Mouvement simple en avant
    private void Move()
    {
        float step = this._speed * Time.deltaTime;
        gameObject.transform.position += Vector3.forward * step;
    }
}
