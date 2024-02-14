using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles -take Goat and can walk of Mountains
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedMountains : MonoBehaviour
{
    [SerializeField] TileBase[] tileMountain = null;

    public bool Contains(TileBase tile)
    {
        return tileMountain.Contains(tile);
    }

    public TileBase[] Get() { return tileMountain; }
}
