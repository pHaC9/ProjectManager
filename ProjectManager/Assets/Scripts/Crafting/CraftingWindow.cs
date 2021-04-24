using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingWindow : MonoBehaviour
{
    public CraftingRecipe recipe;

    public Image material1Image;
    public TextMeshProUGUI mat1Text;
    int quantidadeMat1;
    int estoqueMat1;

    public Image material2Image;
    public TextMeshProUGUI mat2Text;
    int quantidadeMat2;
    int estoqueMat2;

    public Image itemImage;
    int estoqueItem;
    public TextMeshProUGUI estoqueItemText;
    int quantidadeResultadoItem;
    public TextMeshProUGUI tempoParaCraftarItemText;
    float tempoParaCraftarItem;

    // Start is called before the first frame update
    void Start()
    {
        //Setup do item que irá ser craftado
        itemImage.sprite = recipe.resultado[0].item.sprite;        
        quantidadeResultadoItem = recipe.resultado[0].quantidade;

        //Setup do material #1
        material1Image.sprite = recipe.materiais[0].item.sprite;
        quantidadeMat1 = recipe.materiais[0].quantidade;

        //Setup do material #2
        material2Image.sprite = recipe.materiais[1].item.sprite;
        quantidadeMat2 = recipe.materiais[1].quantidade;

        tempoParaCraftarItem = recipe.segundosParaCraftar;

        tempoParaCraftarItemText.text = tempoParaCraftarItem.ToString() + "s";
    }

    private void Update()
    {
        estoqueItem = recipe.resultado[0].item.estoque;
        estoqueItemText.text = "No estoque: " + estoqueItem;
        estoqueMat1 = recipe.materiais[0].item.estoque;
        mat1Text.text = "x" + quantidadeMat1.ToString() + "(" + estoqueMat1 + ")";
        estoqueMat2 = recipe.materiais[1].item.estoque;
        mat2Text.text = "x" + quantidadeMat2.ToString() + "(" + estoqueMat2 + ")";
    }


    public GameObject craftingInfo;
    public GameObject craftingTimer;
    public Image craftingTimerBar;

    public void CraftItem()
    {
        if (estoqueMat1 >= quantidadeMat1 && estoqueMat2 >= quantidadeMat2)
        {
            StartCoroutine("Crafting");
        }
    }

    IEnumerator Crafting()
    {
        recipe.materiais[0].item.estoque -= quantidadeMat1;
        recipe.materiais[1].item.estoque -= quantidadeMat2;
        craftingInfo.SetActive(false);
        craftingTimer.SetActive(true);
        while (craftingTimerBar.fillAmount < 1)
        {
            craftingTimerBar.fillAmount += Mathf.Clamp(1.0f / tempoParaCraftarItem * Time.deltaTime, 0, 1);
            yield return null;
        }
        craftingTimerBar.fillAmount = 0;
        craftingTimer.SetActive(false);
        craftingInfo.SetActive(true);
        recipe.resultado[0].item.estoque += quantidadeResultadoItem;
        yield return null;
    }
}
