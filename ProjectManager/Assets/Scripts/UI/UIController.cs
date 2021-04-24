using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
//Esse script gerencia os elementos de UI da cena.
{
    public static UIController controller;
    private void Awake() { controller = this; }

    [Header("DISPLAYS")]
    //ESTOQUE
    public TextMeshProUGUI pilhasText;
    public TextMeshProUGUI leiteText;
    public TextMeshProUGUI ovosText;
    public TextMeshProUGUI dinheiroText;
 

    // Start is called before the first frame update
    void Start()
    {        
        Time.timeScale = 1; //Unity tem um problema onde o timescale é ás vezes alterado pra 0 durante transições de cenas
    }

    // Update is called once per frame
    void Update()
    {
        //ATUALIZAR A PARTE DE RECURSOS DA HUD
        AtualizarHUD();        
    }

    public void AtualizarHUD()
    {
        pilhasText.text = "Estoque: " + GlobalData.qtePilhas + " \nPreço: " + 25;
        leiteText.text = "Estoque: " + GlobalData.qteLeite + " \nPreço: " + 25;
        ovosText.text = "Estoque: " + GlobalData.qteOvos + " \nPreço: " + 25;
        dinheiroText.text = "Dinheiro: " + GlobalData.dinheiro;
    }


}
