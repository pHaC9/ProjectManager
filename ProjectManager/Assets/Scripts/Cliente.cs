using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Cliente : MonoBehaviour
{
    public static Cliente controller;
    private void Awake() { controller = this; }

    private ArrayList itemPedido = new ArrayList();
    public string nomeDoPedido; // O que o cliente vai pedir
    public int quantidadePedida; // O quanto ele vai pedir        

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
        itemPedido.AddRange(new List<string>() { "Pilhas", "Leite", "Ovos" });
        int pedido = Random.Range(0, 3);
        nomeDoPedido = itemPedido[pedido].ToString();
        quantidadePedida = Random.Range(1, 5);
    }

    /* MÉTODO OBSOLETO - MUDAR DE 2D PARA 3D TORNA ESSE BLOCO DE CÓDIGO INÚTIL, 
     * VOU DEIXAR ELE AQUI SE A GENTE PRECISAR DELE NO FUTURO
     * 
    // Faz o objeto "Cliente" se mover até o balcão do caixa
    void MoveTowardsDestination()
    {
        Vector2 destino = destinoPoint.gameObject.GetComponent<Transform>().position;
        Vector2 posAtual = gameObject.GetComponent<Transform>().position;
        
        if (posAtual.x != destino.x)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), destino, 800 * Time.deltaTime);
        }
        else StartCoroutine(ClienteFazPedido());        
    }
    */

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
        }
        else if (GameController.controller.clienteDoPoint2 == gameObject)
        {
            GameController.controller.clienteDoPoint2 = null;
            estadoAtualDoCliente = estadoDoCliente.esta_na_fila;
        }
        else if (GameController.controller.clienteDoPoint3 == gameObject)
        {
            GameController.controller.clienteDoPoint3 = null;
            estadoAtualDoCliente = estadoDoCliente.esta_na_fila;
        }        
    }


    IEnumerator ContagemRegressiva()
    {
        while (timer.fillAmount < 1f)
        {
            timer.fillAmount += Mathf.Clamp(0.1f * Time.deltaTime, 0, 1);
            yield return null;
        }
    }



    // Essa é a sequência de eventos em que o cliente vai fazer o seu pedido
    // ADAPTAR PRO NOVO CENÁRIO EM 3D!!!
    IEnumerator ClienteFazPedido()
    {        
        //sendoAtendido = true;  Pra que essa co-rotina execute uma vez só
        yield return new WaitForSeconds(0.2f);
        balaoFala.SetActive(true); //Faz o balão de fala aparecer
        yield return new WaitForSeconds(0.1f);
        textoFala.text = "Gostaria de x" + quantidadePedida + " " + 
            nomeDoPedido + ", por favor."; //Muda o texto
        //StartCoroutine(DisplayTextoTypewriter());
        yield return null;        
    }


    // Co-rotina que em teoria faria o texto da fala do cliente 
    // aparecer letra por letra, mas não consegui fazer funcionar direito ainda
    IEnumerator DisplayTextoTypewriter()
    {        
        textoFala.text = "Gostaria de x" + quantidadePedida + " " + nomeDoPedido + ", por favor.";
        int totalVisibleCharacters = textoFala.textInfo.characterCount;

        for (int i = 0; i < totalVisibleCharacters + 1; i++)
        {
            int visibleCount = i % (totalVisibleCharacters + 1);
            textoFala.maxVisibleCharacters = visibleCount;
            yield return new WaitForSeconds(0.2f);
            i++;
        }      
    }
}
