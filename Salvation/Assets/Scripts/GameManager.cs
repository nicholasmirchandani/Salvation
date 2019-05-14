using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int numCharUnits;
    bool playerControl;
    bool movePhase;
    bool attackPhase;
    public bool canMove;
    public bool hasMoved;
    public bool canAttack;
    public bool hasAttacked;
    public bool unitsPlaced;
    public GameObject selectedUnit;
    public GameObject ActiveUnit;
    public GameObject CharTeam;
    public GameObject AITeam;
    public Team ActiveTeam;
    public TileClicking tc;
    int maxTeamSize = 3;
    int curActive = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        movePhase = true;
        canAttack = false;
        canMove = true;
        hasMoved = false;
        playerControl = true;
        unitsPlaced = true;
        ActiveTeam = CharTeam.GetComponent<Team>();
        ActiveUnit = ActiveTeam.units[0];
        ActiveUnit.GetComponent<Unit>().hasTurn = true;
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Set the next unit in the team to their turn
    public void NextActiveUnit()
    {
        Debug.Log("Set Turn called");
        int lastInt = maxTeamSize -1; //get the index of the last unit in the team
        //check to see if the last unit in the team has turn
        //if it does switch player control so the next team can go
        //As well as set the turn to the first unit
        //bool nextTurn = false;
        //Iterate through the team and set the turn to the next unit in the team

        for(int i = curActive;i<=maxTeamSize; i++)
        {
            if (i == maxTeamSize - 1)
            {
                if (!movePhase)
                    SwitchTeam();
                else
                {
                    movePhase = false;
                    curActive = 0;
                    SwitchActiveUnit(ActiveTeam.units[curActive]);
                }
                return;
            }
            else if (ActiveTeam.units[i + 1].GetComponent<Unit>().hasDied)
            {
                continue;
            }
            else
            {
                SwitchActiveUnit(ActiveTeam.units[i + 1]);
                curActive = i + 1;
                return;
            }
        }

        /*
        foreach (GameObject u in ActiveTeam.units)
        {
            
            if (u.GetComponent<Unit>().hasDied)
            {
                continue;
            }
            else if(u.GetComponent<Unit>().Equals(ActiveUnit))
            {
                continue;
            }
            else if (u.GetComponent<Unit>().Equals(ActiveUnit))
            {
                nextTurn = true;
            }
            else if (nextTurn)
            {
                Debug.Log("Switch Unit called");
                SwitchActiveUnit(u);
                nextTurn = false;
                if (movePhase)
                {
                    canMove = true;
                }
                else
                {
                    canAttack = true;
                    hasAttacked = false;
                }
                return;
            }

        }
        */
    }

    void Attack()
    {
        if(Vector3.Magnitude(ActiveUnit.transform.position-selectedUnit.transform.position)<= ActiveUnit.GetComponent<Unit>().range && canAttack)
        {
            ActiveUnit.GetComponent<Unit>().Attack(selectedUnit.GetComponent<Unit>());
            canAttack = false;
            hasAttacked = true;
            NextActiveUnit();
        }
        
    }


    //Controls the Logic for Player Actions
    public void NextAction()
    {
        if(movePhase)
        {
            NextActiveUnit();
        }
        else if(canAttack && selectedUnit != null)
        {
            if(playerControl)
                Attack();
            else
                Attack();
        }


        if (!playerControl && movePhase && !canMove)
        {
            NextActiveUnit();
        }
        else if (!playerControl && hasAttacked)
        {
            NextActiveUnit();
        }
        


    }

    public void SwitchTeam()
    {
        switch (playerControl)
        {
            case true:
                ActiveTeam = AITeam.GetComponent<Team>();
                break;
            case false:
                ActiveTeam = CharTeam.GetComponent<Team>();
                break;
        }
        SwitchActiveUnit(ActiveTeam.units[0]);
        playerControl = !playerControl;
        movePhase = true;
        canMove = true;
    }

    public void SwitchActiveUnit(GameObject u)
    {
        ActiveUnit = u;
        if (movePhase)
        {
            tc.player = ActiveUnit;
            tc.maxDistance = ActiveUnit.GetComponent<Unit>().maxMovement;
            tc.tracker = ActiveUnit.GetComponent<TrackerRemover>().tracker;
        }
        else if (attackPhase)
        {

        }
    }

    //Controls the actions for the computer turns

    public void SelectUnit(GameObject unit)
    {
        selectedUnit = unit;
    }

    public void placeUnits()
    {
        
    }

    
}
