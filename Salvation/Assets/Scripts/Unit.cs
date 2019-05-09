using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool playerControlled;
    public List<Vector2> currentPath;
    public int damageDone;
    public bool hasDied;
    public bool hasTurn;
    public int maxMovement;
    public int damage;
    public int hitpoints;
    public int range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //select unit
        }
    }

    void MoveTile(int direction)
    {
        switch (direction)
        {
            case 1:
                //move north
                break;
            case 2:
                //move east
                break;
            case 3:
                //move south
                break;
            case 4:
                //move west
                break;
        }
    }

    void MoveAlongPath()
    {
        //move along currentPath using MoveTile
    }

    void Attack(Unit u)
    {
        this.damageDone += this.damage;
        u.hitpoints -= this.damage;
    }

    void Die()
    {
        hasDied = true;
        //remove from map/play animation
    }

    void ResetPath()
    {
        currentPath = new List<Vector2>();
    }
}
