using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnersChecker : MonoBehaviour
{
    public int winnersCount; // Amount of winning chicken (those who reach the goal)
    public List<GameObject> winners; // List of winners

    private UIManager _uiManager; // UI

    private void Start()
    {
        winnersCount = 0;
        winners = new List<GameObject>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Chicken"))
        {
            other.gameObject.GetComponent<ChickenWinState>().hasReachedGoal = true;
            winnersCount ++;
            winners.Add(other.gameObject);
            _uiManager.SetWinnersCountUI();
        }
    }
}
