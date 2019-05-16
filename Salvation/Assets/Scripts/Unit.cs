using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool playerControlled;
    public int damageDone;
    public bool hasDied;
    public bool hasTurn;
    public int maxMovement;
    public int damage;
    public int hitpoints;
    public bool moving;
    public bool prevMoving;
    public int range;
    public bool delayCall;
    public string name;
    bool prevSpaceDown;
    public GameObject attackCircle;
    public GameObject moveCircle;
    public int id;
    public int attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        attackCircle.SetActive(false);
        moveCircle.SetActive(false);
        damageDone = 0;
        attackDamage = 0;
        hasDied = false;
        prevMoving = false;
        moving = false;
        delayCall = false;
        prevSpaceDown = false;
        GetComponentInChildren<Animator>().SetBool("isMoving", false);
    }

    // Update is called once per frame
    void Update()
    {
            moving = !gameObject.GetComponent<IAstarAI>().reachedEndOfPath; //If the unit is moving

        if (!moving && prevMoving && GameManager.Instance.movePhase)
            {
                GameManager.Instance.NextAction();
            }
            prevMoving = moving;
    }

    public void OnMouseOver()
    {
        if(hasDied)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //PLace unit
            if (GameManager.Instance.placingUnit)
            {
                GameManager.Instance.ActiveTeam.GetComponent<Team>().units.Add(GameManager.Instance.unitBeingPlaced);
                GameManager.Instance.unitBeingPlaced = null;
                GameManager.Instance.placingUnit = false;
                GameManager.Instance.unitPlaced = true;
                GameManager.Instance.totalUnits++;
                if(GameManager.Instance.totalUnits==6)
                {
                    GameManager.Instance.setTeamPhase = false;
                    GameManager.Instance.PlaceUnitsControl();
                    return;
                }
                GameManager.Instance.PlaceUnitsControl();
                Debug.Log("PLACE");
            }    
            else
            {
                //select unit
                GameManager.Instance.SelectUnit(gameObject);
            }
            return;
        }
        if (Input.GetMouseButtonDown(1) && GameManager.Instance.attackPhase)
        {
            //attack unit
            GameManager.Instance.TargetUnit(gameObject);
            GameManager.Instance.canAttack = true;
            return;

        }
    }

    public void Attack(Unit u)
    {
        attackDamage = Random.Range(0, damage + 1);
        this.damageDone += this.attackDamage;
        u.hitpoints -= this.attackDamage;
        if(attackDamage == 0)
        {
            GameManager.Instance.AttackText.text = this.name + " missed "  + u.name;
        }
        else
        {
            GameManager.Instance.AttackText.text = this.name + " deals " + this.attackDamage + " damage to " + u.name;
            GameManager.Instance.source.PlayOneShot(GameManager.Instance.attackSound, 1.0f);
        }
        if(u.hitpoints<=0)
        {
            u.Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        hasDied = true;
        attackCircle.SetActive(false);
        moveCircle.SetActive(false);
        GameManager.Instance.source.PlayOneShot(GameManager.Instance.deathSound, 1.0f);
        GameManager.Instance.CheckWin();
    }

    

    /*
    void ResetPath()
    {
        currentPath = new List<Vector2>();
    }
    */
}
