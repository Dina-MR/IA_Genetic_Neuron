using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class ChickenMove : MonoBehaviour
{
    [SerializeField] private float _speed; // vitesse du poulet
    public float distanceTravelled; // distance totale parcourue
    public float timer; // temps total parcouru
    private Vector3 previousPosition; // position précédente du poulet, utilisée pour le calcul de distance

    // Directions pour le gène
    static private Dictionary<string, Vector3> _directionsDictionary = new Dictionary<string, Vector3>()
    {
        { "Left", Vector3.left },
        { "Right", Vector3.right },
        { "Forward", Vector3.forward }
    };
    private List<string> _directionsList = new List<string>(_directionsDictionary.Keys);
    private ChickenGenetic _genetics;
    public Queue<string> _path = new Queue<string>(); // chemin obtenu à partir d'un ensemble de directions
    public int maxPathSize; // Maximum amount of (generated) directions, before the queue gets reseted
    private int _pathInitialSize; // The initial amount of directions in the path DNA

    private ChickenWinState _winState;

    private void Start()
    {
        // Initialisation des paramètres de course
        distanceTravelled = 0f;
        timer = 0f;
        previousPosition = transform.position;
        // Accès au vérificateur de victoire
        _winState = gameObject.GetComponent<ChickenWinState>();
        _genetics = gameObject.GetComponent<ChickenGenetic>();
        _pathInitialSize = _genetics.pathDNA.Count;
        // Mise à jour du chemin si l'ADN est établi
        if(_pathInitialSize > 0)
            SetupEstablishedDirections();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if(!_winState.hasReachedGoal && _winState.isAlive)
        {
            timer += Time.deltaTime;
            Move();
            MeasureDistance();
        }
    }

    // Mouvement simple en avant
    private void Move()
    {
        float step = this._speed * Time.deltaTime;
        // Réinitialisation du chemin lorsque qu'il n'y a plus de directions à prendre
        if (_path.Count == 0)
            SetupRandomDirections();
        else
            gameObject.transform.position += PickNextDirection() * step;
    }

    // Génération alétoire de directions
    public void SetupRandomDirections()
    {
        for(int i = 0; i < maxPathSize; i++)
        {
            int directionIndex = Random.Range(0, 3);
            _path.Enqueue(_directionsList[directionIndex]);
        }
    }

    // Récupération des directions de l'ADN (à partir de la 2ème génération)
    public void SetupEstablishedDirections()
    {
        for (int i = 0; i < _pathInitialSize; i++)
            _path.Enqueue(_genetics.pathDNA[i]);
    }

    // Application de la direction en tête de file chez le poulet
    private Vector3 PickNextDirection()
    {
        string nextDirection = _path.Dequeue();
        // Adding the direction in the DNA, unless it's already inside it
        if(_pathInitialSize == 0 || _genetics.pathDNA.Count > _pathInitialSize)
            _genetics.pathDNA.Add(nextDirection);
        return _directionsDictionary[nextDirection];
    }

    // Calcul de la distance parcourue
    private void MeasureDistance()
    {
        distanceTravelled += Vector3.Distance(previousPosition, transform.position);
        previousPosition = transform.position;
    }
}
