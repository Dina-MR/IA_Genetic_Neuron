using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class ChickenMove : MonoBehaviour
{
    [SerializeField] private float _speed; // Speed of the chicken
    public float distanceTravelled; // Distance travelled by the chicken from the beginning to the end of its race
    public float timer; // How much time the chicken travelled until the end
    private Vector3 previousPosition; // Previous position. It's used to calculate the distance
    public Vector3 startingPosition; // Position at the beginning of the run

    // Directions pour le gène
    static private Dictionary<string, Vector3> _directionsDictionary = new Dictionary<string, Vector3>()
    {
        { "Left", Vector3.left },
        { "Right", Vector3.right },
        { "Forward", Vector3.forward }
    };
    private List<string> _directionsList = new List<string>(_directionsDictionary.Keys);
    private ChickenGenetic _genetics;
    public Queue<string> _path = new Queue<string>(); // Path made of multiple directions
    public int maxPathSize; // Maximum amount of (generated) directions, before the queue gets reseted
    private int _pathInitialSize; // The initial amount of directions in the path DNA

    private ChickenWinState _winState;

    private void Start()
    {
        // Initilization of race parameters
        distanceTravelled = 0f;
        timer = 0f;
        previousPosition = transform.position;
        // Access to other components (winning state and genetics)
        _winState = gameObject.GetComponent<ChickenWinState>();
        _genetics = gameObject.GetComponent<ChickenGenetic>();
        _pathInitialSize = _genetics.pathDNA.Count;
        // Update on the path, but only if the DNA has been established (only the 1st isn't concerned by this)
        if(_pathInitialSize > 0)
            SetupEstablishedDirections();
        startingPosition = gameObject.transform.position;
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

    // Moving toward the goal. The chicken can go to the left, to the right or forward
    private void Move()
    {
        float step = this._speed * Time.deltaTime;
        // Regeneration of a path if the queue containing the temporary path is empty
        if (_path.Count == 0)
            SetupRandomDirections();
        else
            gameObject.transform.position += PickNextDirection() * step;
    }

    // Random generation of directions
    public void SetupRandomDirections()
    {
        for(int i = 0; i < maxPathSize; i++)
        {
            int directionIndex = Random.Range(0, 3);
            _path.Enqueue(_directionsList[directionIndex]);
        }
    }

    // Extraction of the directions inside the DNA (doesn't apply to the 1st generation)
    public void SetupEstablishedDirections()
    {
        for (int i = 0; i < _pathInitialSize; i++)
            _path.Enqueue(_genetics.pathDNA[i]);
    }

    // Picking the first direction in the path queue (then dequeueing it)
    private Vector3 PickNextDirection()
    {
        string nextDirection = _path.Dequeue();
        // Adding the direction in the DNA, unless it's already inside it
        if(_pathInitialSize == 0 || _genetics.pathDNA.Count > _pathInitialSize)
            _genetics.pathDNA.Add(nextDirection);
        return _directionsDictionary[nextDirection];
    }

    // Calculation of the distance travelled by the chicken
    private void MeasureDistance()
    {
        distanceTravelled += Vector3.Distance(previousPosition, transform.position);
        previousPosition = transform.position;
    }
}
