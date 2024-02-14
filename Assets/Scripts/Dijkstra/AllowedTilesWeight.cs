using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AllowedTilesWeight : MonoBehaviour
{

    [SerializeField] private Dictionary<TileBase, int> dic;
    [SerializeField] private TileBase[] allowedTiles;
    [SerializeField] private int[] weight;


    private void Awake()
    {

        dic = new Dictionary<TileBase, int>();

        for (int i = 0; i < allowedTiles.Length && i < weight.Length; i++)
        {
            dic.Add(allowedTiles[i], weight[i]);
        }
    }


    public bool Contain(TileBase tile)
    {
        return allowedTiles.Contains(tile);
    }

    public TileBase[] Get() { return allowedTiles; }

    public int GetWeight(TileBase tile) { return dic[tile]; }


}