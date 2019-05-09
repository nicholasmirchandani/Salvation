using Pathfinding;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = levelCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = grid.WorldToCell(worldPoint);
            Tile tile = (Tile)tilemap.GetTile(position);

            if (tile != null)
            {

                tracker.SetActive(true);
                tracker.transform.position = position + new Vector3(0.5f, 0.5f, 0);
                player.GetComponent<IAstarAI>().SearchPath();
            }
            else
            {
                tracker.SetActive(false);
            }
        }
    }
}
