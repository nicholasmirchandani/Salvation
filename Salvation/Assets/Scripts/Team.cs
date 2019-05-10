using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public List<Unit> units;
    public bool playerControlled;
    public bool hasTurn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Team(List<Unit> units, bool playerControlled, bool hasTurn)
    {
        this.units = units;
        this.playerControlled = playerControlled;
        this.hasTurn = hasTurn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
