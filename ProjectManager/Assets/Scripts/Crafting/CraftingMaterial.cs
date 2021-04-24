using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Estoque de Material")]
public class CraftingMaterial : ScriptableObject
{
    public enum TiposDeMaterial
    {
        Ferro,
        Água,
        Tecido
    }
    public Sprite sprite;
    public TiposDeMaterial tipoDoItem;
    public int estoque;
    public int custoParaComprarPacote;
    public int quantidadeComprado;
}
