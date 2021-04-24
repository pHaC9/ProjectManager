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

    public GameObject cliente;
    int numeroDeClientes;
    private float clienteSpawnTimer;
    float intervaloSpawnCliente;

    public GameObject spawner;
    public GameObject prateleiraPoint1;// Esses são os pontos de referência
    public GameObject prateleiraPoint2;// pra quando o cliente fica
    public GameObject prateleiraPoint3;// parado na frente das prateleiras
    public GameObject caixaPoint; // Esse é o ponto na frente do caixa, onde o cliente para pra ser atendido

    //[HideInInspector]
    public GameObject clienteDoPoint1;// Pra saber se existe um cliente no prateleiraPoint1
    //[HideInInspector]
    public GameObject clienteDoPoint2;// Pra saber se existe um cliente no prateleiraPoint2
    //[HideInInspector]
    public GameObject clienteDoPoint3;// Pra saber se existe um cliente no prateleiraPoint3

    [HideInInspector]
    public GameObject clienteNoCaixa;// Pra saber se existe um cliente no começo da fila/no caixa

    public List<GameObject> fila;

    public int qtePilhas;
    public int qteLeite;
    public int qteOvos;
    public int dinheiro;


    [HideInInspector]
    public bool gamepaused;
 
    private void Start()
    {
        fila = new List<GameObject>();
        fila.Capacity = 1;

        controller.qtePilhas = 30;
        controller.qteLeite = 30;
        controller.qteOvos = 30;
        controller.dinheiro = 0;
        numeroDeClientes = 3;

        Load();

        intervaloSpawnCliente = Random.Range(3f, 5f);
    }
    public void Load()
    {
        qtePilhas = PlayerPrefs.GetInt("qtePilhas", 50);
    }

    void Update()
    {
        //Spawna clientes caso existirem menos de 3 clientes
        SpawnarClientes();

        GlobalData.qtePilhas = controller.qtePilhas;
        GlobalData.qteLeite = controller.qteLeite;
        GlobalData.qteOvos = controller.qteOvos;
        GlobalData.dinheiro = controller.dinheiro;
        Save();
        FilaManager();
    }

    public void Save()
    {
         PlayerPrefs.SetString("dinheiro", dinheiro.ToString());
    }

    void SpawnarClientes()
    {
        numeroDeClientes = GameObject.FindGameObjectsWithTag("Cliente").Length;
        if (numeroDeClientes < 3) //Se existirem menos de 3 clientes:
        {
            clienteSpawnTimer += Time.deltaTime;
            if (clienteSpawnTimer >= intervaloSpawnCliente)
            {
                Instantiate(cliente, new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z), Quaternion.Euler(0, -45, 0));
                clienteSpawnTimer = 0f;
                intervaloSpawnCliente = Random.Range(3f, 6f);                
            }
        }        
    }
    void FilaManager()
    {
        if (fila[0] != null)
        {
            Cliente.estadoDoCliente estadoAtualDoCliente = fila[0].GetComponent<Cliente>().
                estadoAtualDoCliente;

            if (estadoAtualDoCliente == Cliente.estadoDoCliente.esta_na_fila)
            {
                fila[0].transform.position = caixaPoint.transform.position;
            }
            else return;
        }
    }

    

}
