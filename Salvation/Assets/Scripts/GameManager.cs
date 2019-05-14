using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int numCharUnits;
    bool playerControl;
    public bool movePhase;
    public bool attackPhase;
    public bool canMove;
    public bool hasMoved;
    public bool canAttack;
    public bool hasAttacked;
    public bool unitsPlaced;
    public GameObject selectedUnit;
    public GameObject ActiveUnit;
    public GameObject CharTeam;
    public GameObject AITeam;
    public GameObject Canvas;
    public Team ActiveTeam;
    public TileClicking tc;
    public GameObject MoveCircle;
    public Text TurnText;
    public Text PhaseText;
    public Text ActiveUnitText;
    public Text SelectedUnitText;
    int activeTeamSize;
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
        selectedUnit = null;
        ActiveTeam = CharTeam.GetComponent<Team>();
        ActiveUnit = ActiveTeam.units[0];
        ActiveUnit.GetComponent<Unit>().hasTurn = true;
        ActiveUnit.GetComponent<Unit>().attackCircle.SetActive(true);
        ActiveUnit.GetComponent<Unit>().moveCircle.SetActive(true);
        activeTeamSize = ActiveTeam.teamSize;
        TurnText.text = "Player 1 Turn";
        PhaseText.text = "Move Phase";
        SelectedUnitText.text = "";
        setActiveUnitText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextAction();
        }
        activeTeamSize = ActiveTeam.teamSize;
    }

    //Set the next unit in the team to their turn
    public void NextActiveUnit()
    {
        int lastInt = activeTeamSize -1; //get the index of the last unit in the team
        //check to see if the last unit in the team has turn
        //if it does switch player control so the next team can go
        //As well as set the turn to the first unit
        //Iterate through the team and set the turn to the next unit in the team

       

        for (int i = curActive;i<=activeTeamSize; i++)
        {
            if (i == activeTeamSize - 1)
            {
                if (attackPhase)
                {
                    PhaseText.text = "Move Phase";
                    SwitchTeam();
                }
                else
                {
                    movePhase = false;
                    attackPhase = true;
                    PhaseText.text = "Attack Phase";
                    curActive = 0;
                    SwitchActiveUnit(ActiveTeam.units[curActive]);
                    StartCoroutine("Attack");
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

  
    }

    IEnumerator Attack()
    {
        Debug.Log("Enters Attack");
        while(!canAttack || selectedUnit == null)
        {
            if(!attackPhase)
            {
                yield return null;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Exit Attack");
                yield return null;
            }
            yield return new WaitForSeconds(0.02f);
        }
        Debug.Log("Attacking " + selectedUnit);
        if(Vector3.Magnitude(ActiveUnit.transform.position-selectedUnit.transform.position)<= ActiveUnit.GetComponent<Unit>().range && canAttack)
        {
            ActiveUnit.GetComponent<Unit>().Attack(selectedUnit.GetComponent<Unit>());
            setSelectedUnitText();
            canAttack = false;
            hasAttacked = true;
        }
        canAttack = false;

        //End of attack, now go to next aaction and next selected unit
        NextAction();
        yield return null;
    }


    //Controls the Logic for Player Actions
    public void NextAction()
    {
        Debug.Log("NEXT");
        if(movePhase)
        {
            NextActiveUnit();
        }
        else if(attackPhase)
        {
            NextActiveUnit();
            if (playerControl)
                StartCoroutine("Attack");
            else
                StartCoroutine("Attack");
        }
    }

    public void SwitchTeam()
    {
        switch (playerControl)
        {
            case true:
                ActiveTeam = AITeam.GetComponent<Team>();
                TurnText.text = "Player 2 Turn";
                break;
            case false:
                ActiveTeam = CharTeam.GetComponent<Team>();
                TurnText.text = "Player 1 Turn";
                break;
        }
        playerControl = !playerControl;
        movePhase = true;
        attackPhase = false;
        canMove = true;
        curActive = 0;
        SwitchActiveUnit(ActiveTeam.units[curActive]);
    }

    public void SwitchActiveUnit(GameObject u)
    {
        if (u.GetComponent<Unit>().hasDied)
        {
            NextActiveUnit();
            return;
        }
        ActiveUnit.GetComponent<Unit>().attackCircle.SetActive(false);
        ActiveUnit.GetComponent<Unit>().moveCircle.SetActive(false);
        ActiveUnit = u;
        ActiveUnit.GetComponent<Unit>().attackCircle.SetActive(true);
        ActiveUnit.GetComponent<Unit>().moveCircle.SetActive(true);
        setActiveUnitText();
        if (movePhase)
        {
            tc.player = ActiveUnit;
            tc.maxDistance = ActiveUnit.GetComponent<Unit>().maxMovement;
            tc.tracker = ActiveUnit.GetComponent<TrackerRemover>().tracker;
        }
        else if (attackPhase)
        {
            //TODO: Add attack phase setup code as needed for active unit
        }
    }

    public void SelectUnit(GameObject unit)
    {
        if(selectedUnit != null)
        {
            selectedUnit.GetComponent<Unit>().attackCircle.SetActive(false);
            selectedUnit.GetComponent<Unit>().moveCircle.SetActive(false);
        }
        selectedUnit = unit;
        selectedUnit.GetComponent<Unit>().attackCircle.SetActive(true);
        selectedUnit.GetComponent<Unit>().moveCircle.SetActive(true);
        setSelectedUnitText();
    }

    public void CheckWin()
    {
        bool hasWon = true;

        foreach (GameObject u in CharTeam.GetComponent<Team>().units)
        {
            if (u.GetComponent<Unit>().hasDied == false)
            {
                hasWon = false;
            }
        }

        if(hasWon)
        {
            SceneManager.LoadScene(0);
        }


        hasWon = true;

        foreach (GameObject u in AITeam.GetComponent<Team>().units)
        {
            if (u.GetComponent<Unit>().hasDied == false)
            {
                hasWon = false;
            }
        }

        if (hasWon)
        {
            SceneManager.LoadScene(0);
        }

    }

    public void setSelectedUnitText()
    {
        SelectedUnitText.text = "Selected Unit: \n";
        SelectedUnitText.text += "Team: " + selectedUnit.transform.parent.gameObject.name + "\n";
        SelectedUnitText.text += "Name: " + selectedUnit.GetComponent<Unit>().name + "\n";
        SelectedUnitText.text += "Health: " + selectedUnit.GetComponent<Unit>().hitpoints + "\n";
        SelectedUnitText.text += "Movement: " + selectedUnit.GetComponent<Unit>().maxMovement + "\n";
        SelectedUnitText.text += "Range: " + selectedUnit.GetComponent<Unit>().range + "\n";
        SelectedUnitText.text += "Damage: " + selectedUnit.GetComponent<Unit>().damage + "\n";
        SelectedUnitText.text += "Damage Dealt: " + selectedUnit.GetComponent<Unit>().damageDone + "\n";
        DisplayCircles(selectedUnit);
    }

    public void setActiveUnitText()
    {
        ActiveUnitText.text = "Active Unit: \n";
        ActiveUnitText.text += "Team: " + ActiveUnit.transform.parent.gameObject.name + "\n";
        ActiveUnitText.text += "Name: " + ActiveUnit.GetComponent<Unit>().name + "\n";
        ActiveUnitText.text += "Health: " + ActiveUnit.GetComponent<Unit>().hitpoints + "\n";
        ActiveUnitText.text += "Movement: " + ActiveUnit.GetComponent<Unit>().maxMovement + "\n";
        ActiveUnitText.text += "Range: " + ActiveUnit.GetComponent<Unit>().range + "\n";
        ActiveUnitText.text += "Damage: " + ActiveUnit.GetComponent<Unit>().damage + "\n";
        ActiveUnitText.text += "Damage Dealt: " + ActiveUnit.GetComponent<Unit>().damageDone + "\n";
        DisplayCircles(ActiveUnit);
    }

    public void DisplayCircles(GameObject u)
    {
        Unit unit = u.GetComponent<Unit>();
        float moveScale = unit.maxMovement * 10;
        float attackScale = unit.range * 10;
        unit.moveCircle.transform.localScale = new Vector3(moveScale, moveScale);
        unit.attackCircle.transform.localScale = new Vector3(attackScale, attackScale);
    }


}
