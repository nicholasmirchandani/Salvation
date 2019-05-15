using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    
    public List<GameObject> units;
    public bool playerControlled;
    public bool hasTurn;
    public int teamSize;
    public GameObject[] teamButtons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        teamSize = units.Count;
    }

    public void SetTeamButtonsOn()
    {
        foreach(GameObject b in teamButtons)
        {
            b.SetActive(true);
        }
    }

    public void SetTeamButtonsOff()
    {
        foreach (GameObject b in teamButtons)
        {
            b.SetActive(false);
        }
    }
}
