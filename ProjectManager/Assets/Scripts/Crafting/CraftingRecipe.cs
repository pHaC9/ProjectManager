using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct QuantidadeMaterial
{
    public CraftingMaterial item;
    [Range(1, 999)]
    public int quantidade;
}

[Serializable]
public struct QuantidadeItem
{
    public Item item;
    [Range(1, 999)]
    public int quantidade;
}

[CreateAssetMenu(menuName = "Receita de Crafting")]
public class CraftingRecipe : ScriptableObject
{
    public List<QuantidadeMaterial> materiais;
    public List<QuantidadeItem> resultado;
    public float segundosParaCraftar;
}
