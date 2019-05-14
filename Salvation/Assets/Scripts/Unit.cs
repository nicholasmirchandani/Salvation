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

    // Start is called before the first frame update
    void Start()
    {
        hitpoints = 100;
        damageDone = 0;
        hasDied = false;
        prevMoving = false;
        moving = false;
        delayCall = false;
    }

    // Update is called once per frame
    void Update()
    {
            moving = !gameObject.GetComponent<IAstarAI>().reachedEndOfPath; //If the unit is moving
            
            if (!moving && prevMoving)
            {
                if(delayCall)
                {
                    GameManager.Instance.NextAction();
                }
                else
                {
                    delayCall = true;
                }
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
            //select unit
            GameManager.Instance.SelectUnit(gameObject);
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            //select unit
            if(GameManager.Instance.selectedUnit !=null)
                GameManager.Instance.canAttack = true;
            return;

        }
    }

    public void Attack(Unit u)
    {
        this.damageDone += this.damage;
        u.hitpoints -= this.damage;
        if(hitpoints<=0)
        {
            Die();
        }
    }

    void Die()
    {
        hasDied = true;
    }

    

    /*
    void ResetPath()
    {
        currentPath = new List<Vector2>();
    }
    */
}
