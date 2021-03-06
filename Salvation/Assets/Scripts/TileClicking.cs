﻿using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClicking : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera levelCamera;
    public Grid grid;
    public Tilemap tilemap;
    public GameObject player;
    public float maxDistance;

    public GameObject tracker;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Optimization: Only call onMouseOver if mouse is over tilemap
    void OnMouseOver()
    {
        if(Time.frameCount % 5 == 0 && GameManager.Instance.placingUnit)
        {
            Ray ray = levelCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = grid.WorldToCell(worldPoint);
            Tile tile = (Tile)tilemap.GetTile(position);
            GameManager.Instance.unitBeingPlaced.transform.position = position +  new Vector3(0.5f, 0.5f, 0.5f);
        }
        if (Input.GetMouseButtonDown(1))
        {

            if(player == null)
            {
                return;
            }

            Ray ray = levelCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = grid.WorldToCell(worldPoint);
            Tile tile = (Tile)tilemap.GetTile(position);

            if (tile != null && Vector3.Magnitude(position - player.transform.position) <= maxDistance)
            {

                tracker.SetActive(true);
                tracker.transform.position = position + new Vector3(0.5f, 0.5f, 0);
                player.GetComponent<IAstarAI>().SearchPath();
                player = null;
                //GameManager.Instance.canMove = false;
                //GameManager.Instance.hasMoved = true;
                //GameManager.Instance.Turn();
            }
            else
            {
                tracker.SetActive(false);
            }
        }
    }
}
