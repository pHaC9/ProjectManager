using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MaterialIcon : MonoBehaviour
{
    public CraftingMaterial material;
    public TextMeshProUGUI estoqueDoMaterial;
    public Image materialImage;
    // Start is called before the first frame update
    void Start()
    {
        materialImage.sprite = material.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        estoqueDoMaterial.text = material.estoque.ToString();
    }
}
