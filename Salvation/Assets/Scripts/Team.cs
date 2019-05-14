using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    
    public List<GameObject> units;
    public bool playerControlled;
    public bool hasTurn;
    public int teamSize;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        teamSize = units.Count;
    }
}
