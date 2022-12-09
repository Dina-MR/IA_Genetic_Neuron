using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMove : MonoBehaviour
{
    [SerializeField] private float _speed; // vitesse du poulet
    public float distanceTravelled; // distance totale parcourue
    public float timer; // temps total parcouru
    private Vector3 previousPosition; // position précédente du poulet, utilisée pour le calcul de distance

    private void Start()
    {
        // Initialisation des paramètres de course
        distanceTravelled = 0f;
        timer = 0f;
        previousPosition = transform.position;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        Move();
        MeasureDistance();
    }

    // Mouvement simple en avant
    private void Move()
    {
        float step = this._speed * Time.deltaTime;
        gameObject.transform.position += Vector3.forward * step;
    }

    private void MeasureDistance()
    {
        distanceTravelled += Vector3.Distance(previousPosition, transform.position);
        previousPosition = transform.position;
    }
}
