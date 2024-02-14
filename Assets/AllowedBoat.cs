using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles -take Boat and can walk of sea
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedSea : MonoBehaviour
{
    [SerializeField] TileBase[] tileBoat = null;

    public bool Contains(TileBase tile)
    {
        return tileBoat.Contains(tile);
    }

    public TileBase[] Get() { return tileBoat; }
}
