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

    // Start is called before the first frame update
    void Start()
    {
        hitpoints = 100;
        damageDone = 0;
        hasDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GameManager.Instance.canAttack = true;
            GameManager.Instance.SelectUnit(gameObject);
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

    void ResetPath()
    {
        currentPath = new List<Vector2>();
    }
}
