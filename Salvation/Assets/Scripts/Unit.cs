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
    public int range;
    public bool selected;

    // Start is called before the first frame update
    void Start()
    {
        damageDone = 0;
        hasDied = false;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
        selected = false;
    }

    public void Attack(Unit u)
    {
        this.damageDone += this.damage;
        u.hitpoints -= this.damage;
    }

    void Die()
    {
        hasDied = true;
    }

    void ResetPath()
    {
        currentPath = new List<Vector2>();
    }
}
