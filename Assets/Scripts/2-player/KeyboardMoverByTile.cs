using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
//   [SerializeField] TileBase[] allowedTiles = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] AllowedTiles alloweSea = null;
    [SerializeField] AllowedTiles alloweMountains = null;
    [Tooltip("The TileBase that allow to cut")][SerializeField] AllowedTiles allowedCut=null;
    [Tooltip("The TileBase that will appear after you cut")][SerializeField] TileBase afterCut=null;

    private bool touchingBoat= false;
    private bool touchingGoat = false;
    private bool touchingMineCraft = false;



    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);

        if (allowedTiles.Contains(tileOnNewPosition)) {
            transform.position = newPosition;
        }
        if (alloweSea.Contains(tileOnNewPosition) && touchingBoat)
        {
            transform.position = newPosition;
        }
        if (alloweMountains.Contains(tileOnNewPosition) && touchingGoat)
        {
            transform.position = newPosition;
        }
        else {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }

        if(touchingMineCraft) 
        { 

            if (Input.GetKeyDown(KeyCode.X))
            {
               

                if (allowedCut.Contains(tileOnNewPosition))
                {
                    tilemap.SetTile(tilemap.WorldToCell(newPosition), afterCut);
                }

            }
        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag+ "otherotherotherother");

        if (other.CompareTag("Boat"))
        {

            touchingBoat = true;
            Destroy(other.gameObject); // Destroy the boat object upon collision
        }
        else if (other.CompareTag("Goat"))
        {

            touchingGoat = true;
            Destroy(other.gameObject); // Destroy the boat object upon collision
        }
        else if (other.CompareTag("MineCraft"))
        {

            touchingMineCraft = true;
            Destroy(other.gameObject); // Destroy the boat object upon collision
        }

    }
}
