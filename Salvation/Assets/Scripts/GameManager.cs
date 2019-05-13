using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int numCharUnits;
    bool playerControl;
    bool movePhase;
    public bool canMove;
    public bool canAttack;
    public bool hasAttacked;
    public bool unitsPlaced;
    public GameObject selectedUnit;
    public GameObject ActiveUnit;
    public GameObject CharTeam;
    public GameObject AITeam;
    public TileClicking tc;
    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        movePhase = true;
        canAttack = false;
        canMove = true;
        playerControl = true;
        unitsPlaced = true;
        CharTeam.GetComponent<Team>().units[0].GetComponent<Unit>().hasTurn = true;
        ActiveUnit = CharTeam.GetComponent<Team>().units[0];
    }

    // Update is called once per frame
    void Update()
    {
       Turn();
    }

    //Set the next unit in the team to their turn
    public void setTurn(Team team)
    {
        int lastInt = team.units.Count -1; //get the index of the last unit in the team
        //check to see if the last unit in the team has turn
        //if it does switch player control so the next team can go
        //As well as set the turn to the first unit
        if (team.units[2].GetComponent<Unit>().hasTurn && movePhase)
        {
            team.units[0].GetComponent<Unit>().hasTurn = true;
            ActiveUnit = team.units[0];
            movePhase = !movePhase;
            return;
        }
        else if(team.units[2].GetComponent<Unit>().hasTurn)
        {
            playerControl = !playerControl;
        }
        bool nextTurn = false;
        //Iterate through the team and set the turn to the next unit in the team
        foreach (GameObject u in team.units)
        {
            if (nextTurn)
            {
                u.GetComponent<Unit>().hasTurn = true;
                ActiveUnit = u;
                canMove = true;
                nextTurn = false;
                hasAttacked = false;
                return;
            }
            if (u.GetComponent<Unit>().hasDied)
            {
                continue;
            }
            else if(!u.GetComponent<Unit>().hasTurn)
            {
                continue;
            }
            else if (u.GetComponent<Unit>().hasTurn)
            {
                nextTurn = true;
                u.GetComponent<Unit>().hasTurn = !u.GetComponent<Unit>().hasTurn;
            }

            
        }
    }

    void Attack()
    {
        if(Vector3.Magnitude(ActiveUnit.transform.position-selectedUnit.transform.position)<= ActiveUnit.GetComponent<Unit>().range)
        {
            ActiveUnit.GetComponent<Unit>().Attack(selectedUnit.GetComponent<Unit>());
        }
        
    }


    //Controls the Logic for Player Actions
    void Turn()
    {
        if(movePhase)
        {
            tc.player = ActiveUnit;
            tc.maxDistance = ActiveUnit.GetComponent<Unit>().maxMovement;
        }
        if(canAttack)
        {
            Attack();
            hasAttacked = true;
        }
        
        
        if(playerControl && movePhase && !canMove)
        {
            setTurn(CharTeam.GetComponent<Team>());
        }
        else if(playerControl && hasAttacked)
        {
            setTurn(CharTeam.GetComponent<Team>());
        }
        else
        {
            setTurn(AITeam.GetComponent<Team>());
        }



    }

    //Controls the actions for the computer turns
    void ComTurn()
    {
        if (movePhase)
        {
            
        }
        else
        {
            Attack();
        }

        setTurn(AITeam.GetComponent<Team>());
    }

    public void SelectUnit(GameObject unit)
    {
        selectedUnit = unit;
    }

    public void placeUnits()
    {
        
    }

    
}
