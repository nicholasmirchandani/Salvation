using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Team charTeam;
    Team comTeam;
    bool playerControl;
    bool movePhase;
    int selectedUnit;
    

    // Start is called before the first frame update
    void Start()
    {
        movePhase = true;
        charTeam = new Team();
        comTeam = new Team();
        playerControl = true;
        selectedUnit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set the next unit in the team to their turn
    void setTurn(Team team)
    {
        int lastInt = team.units.Count -1; //get the index of the last unit in the team
        //check to see if the last unit in the team has turn
        //if it does switch player control so the next team can go
        //As well as set the turn to the first unit
        if (team.units[lastInt].hasTurn)
        {
            team.units[0].hasTurn = true;
            selectedUnit = 0;
            playerControl = !playerControl;
            return;
        }
        bool nextTurn = false;
        //Iterate through the team and set the turn to the next unit in the team
        foreach (Unit u in team.units)
        {
            if(u.hasDied)
            {
                continue;
            }
            else if (u.hasTurn)
            {
                nextTurn = true;
                u.hasTurn = !u.hasTurn;
            }
            else if(nextTurn)
            {
                selectedUnit++;
                u.hasTurn = true;
                nextTurn = false;
            }
        }
    }

    void Attack(Team attackTeam, Team defendTeam)
    {
        foreach(Unit u in defendTeam.units)
        {
            u.OnMouseOver();
            if (u.Selected)
            {
                attackTeam.units[selectedUnit].Attack(u);
            }
        }
    }

    void MoveUnit()
    {

    }

    void ComMoveUnit()
    {

    }

    //Controls the Logic for Player Actions
    void PlayerTurn()
    {
        if(movePhase)
        {
            MoveUnit();
        }
        else
        {
            Attack(charTeam, comTeam);
        }

        setTurn(charTeam);
    }

    //Controls the actions for the computer turns
    void ComTurn()
    {
        if (movePhase)
        {
            ComMoveUnit();
        }
        else
        {
            Attack(comTeam, charTeam);
        }

        setTurn(comTeam);
    }

    private void OnMouseOver()
    {
        //Left Click
        if(Input.GetMouseButtonDown(0))
        {

        }
        //Right Click
        if (Input.GetMouseButtonDown(1))
        {

        }
    }
}
