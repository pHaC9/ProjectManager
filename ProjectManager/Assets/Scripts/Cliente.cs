using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cliente : MonoBehaviour
{
    public static Cliente controller;
    private void Awake() { controller = this; }

    //private ArrayList itemPedido = new ArrayList();
    Item itemPedido;
    Image itemImage;

    public enum estadoDoCliente
    {
        entrou_na_loja,
        pegando_produtos,
        esta_na_fila,
        padrao
    }
    public estadoDoCliente estadoAtualDoCliente;

    public GameObject balaoFala; // O balão de fala do cliente
    public TextMeshProUGUI textoFala; // O texto da fala do cliente
    Camera cameraToLookAt;    
    public Image timer;

    // Start is called before the first frame update
    void Start()
    {
        cameraToLookAt = Camera.main;
        estadoAtualDoCliente = estadoDoCliente.entrou_na_loja;
        RandomizarPedido();
    }

    // Update is called once per frame
    void Update()
    {
        ClienteHandler();
    }

    // Randomizando o tipo de pedido e a quantidade
    void RandomizarPedido()
    {
        int i = Random.Range(0, ItemList.items.Count-1);
        itemPedido = ItemList.items[i];
        itemImage.sprite = itemPedido.sprite;
    }
    

    void ClienteHandler() //Toma conta de todo o comportamento de um cliente
    {
        if (estadoAtualDoCliente != estadoDoCliente.padrao)
        {
            //Cliente espera um pouco e vai para uma das prateleiras disponíveis
            if (estadoAtualDoCliente == estadoDoCliente.entrou_na_loja)
            {
                StartCoroutine("ClienteEntraNaLoja");
            }
            //Cliente demora um pouco pra "pegar" os produtos
            else if (estadoAtualDoCliente == estadoDoCliente.pegando_produtos)
            {
                StartCoroutine("ClientePegaProdutos");
            }
            //Se houver alguem no início da fila, este vai para o final
            else if (estadoAtualDoCliente == estadoDoCliente.esta_na_fila)
            {
                
            }
        }        
        else return;
    }

    IEnumerator ClienteEntraNaLoja()
    {
        estadoAtualDoCliente = estadoDoCliente.padrao;
        yield return new WaitForSeconds(1f);
        // Esse trecho de código determina pra qual prateleiraPoint o cliente vai
        if (GameController.controller.clienteDoPoint1 == null)
        {
            transform.position = GameController.controller.prateleiraPoint1.transform.position;
            GameController.controller.clienteDoPoint1 = gameObject;
            estadoAtualDoCliente = estadoDoCliente.pegando_produtos;
        }
        else if (GameController.controller.clienteDoPoint2 == null)
        {
            transform.position = GameController.controller.prateleiraPoint2.transform.position;
            GameController.controller.clienteDoPoint2 = gameObject;
            estadoAtualDoCliente = estadoDoCliente.pegando_produtos;
        }
        else if (GameController.controller.clienteDoPoint3 == null)
        {
            transform.position = GameController.controller.prateleiraPoint3.transform.position;
            GameController.controller.clienteDoPoint3 = gameObject;
            estadoAtualDoCliente = estadoDoCliente.pegando_produtos;
        }
        yield return null;
    }
    IEnumerator ClientePegaProdutos()
    {
        estadoAtualDoCliente = estadoDoCliente.padrao;
        //Ativar timer;
        timer.gameObject.SetActive(true);

        // Fazer timer virar pra camera 
        Vector3 v = cameraToLookAt.transform.position - timer.transform.position;
        v.x = v.z = 0.0f;
        timer.transform.LookAt(cameraToLookAt.transform.position - v);
        timer.transform.Rotate(0, 180, 0);

        //Fazer contagem regressiva;
        yield return StartCoroutine("ContagemRegressiva");        
        yield return new WaitForSeconds(0.2f);

        // Desativar timer;
        timer.gameObject.SetActive(false);

        //Adicionar cliente na fila
        GameController.controller.fila.Add(gameObject);

        //Limpar prateleiraPoint que esse cliente estava ocupando
        if (GameController.controller.clienteDoPoint1 == gameObject)
        {
            GameController.controller.clienteDoPoint1 = null;
            estadoAtualDoCliente = estadoDoCliente.esta_na_fila;
            balaoFala.gameObject.SetActive(true);
            int totalVisibleCharacters = textoFala.textInfo.characterCount;
        }
        else if (GameController.controller.clienteDoPoint2 == gameObject)
        {
            GameController.controller.clienteDoPoint2 = null;
            estadoAtualDoCliente = estadoDoCliente.esta_na_fila;
            balaoFala.gameObject.SetActive(true);
        }
        else if (GameController.controller.clienteDoPoint3 == gameObject)
        {
            GameController.controller.clienteDoPoint3 = null;
            estadoAtualDoCliente = estadoDoCliente.esta_na_fila;
            balaoFala.gameObject.SetActive(true);            
        }
    }
    IEnumerator ContagemRegressiva()
    {
        float time = 3f;
        while (timer.fillAmount < 1f)
        {
            timer.fillAmount += Mathf.Clamp(1.0f/time * Time.deltaTime, 0, 1);
            yield return null;
        }
    }       
}
