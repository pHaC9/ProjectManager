using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Item Craftável")]
public class Item : ScriptableObject
{
    public enum TiposDeItens
    {        
        KitDePrimeirosSocorros,
        Default
    }
    public TiposDeItens tipoDoItem;
    public Sprite sprite;
    public int estoque;
    public int valor;
}

