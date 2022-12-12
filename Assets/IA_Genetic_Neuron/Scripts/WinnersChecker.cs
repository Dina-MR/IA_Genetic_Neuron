using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnersChecker : MonoBehaviour
{
    public int winnersCount; // nombre de poulets gagnants
    public List<GameObject> winners; // liste des poulets gagnants

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
