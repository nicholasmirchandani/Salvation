using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    bool playerControlled;
    List<Vector2> currentPath;
    int damageDone;
    bool hasDied;
    bool hasTurn;
    int maxMovement;
    int damage;
    int hitpoints;
    int range;

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
