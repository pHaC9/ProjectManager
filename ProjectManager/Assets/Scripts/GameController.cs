using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
//Esse script cuida do game flow, até o número de clientes chegar a zero.
{
    public static GameController controller;
    private void Awake() { controller = this; }    

    int numeroDeClientes;

    public int qtePilhas;
    public int qteLeite;
    public int qteOvos;
    public int dinheiro;

    public TextMeshProUGUI teste1Text;



    [HideInInspector]
    public bool gamepaused;
 
    private void Start()
    {
        controller.qtePilhas = 30;
        controller.qteLeite = 30;
        controller.qteOvos = 30;
        controller.dinheiro = 100;
        numeroDeClientes = 3;

        Load();
    }
    public void Load()
    {

        qtePilhas = PlayerPrefs.GetInt("qtePilhas", 10);
    }

    void Update()
    {
        //Roda o jogo até acabar os clientes
        ChecarNumeroDeClientes();

        GlobalData.qtePilhas = controller.qtePilhas;
        GlobalData.qteLeite = controller.qteLeite;
        GlobalData.qteOvos = controller.qteOvos;
        GlobalData.dinheiro = controller.dinheiro;
        teste1Text.text = "Estoque: " + qtePilhas +  " \nPreço: " + 25 + "\n Dinheiro: " + dinheiro;

        Save();
    }

    public void Save()
    {
         PlayerPrefs.SetString("dinheiro", dinheiro.ToString());
    }

    void ChecarNumeroDeClientes()
    {        
        if (numeroDeClientes > 0) //Se ainda existirem clientes:
        {
            //Continua processando o jogo
        }
        //else printa uma mensagem de fim
    }

    public void BuyProductionUpgrade1()
    {
        if (qtePilhas > 0)
        {
            qtePilhas--;
            dinheiro += 25;
            
        }
    }

}
