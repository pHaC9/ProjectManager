using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Cliente : MonoBehaviour
{
    public static Cliente controller;
    private void Awake() { controller = this; }

    public string nomeDoPedido; // O que o cliente vai pedir
    private ArrayList itemPedido = new ArrayList();
    public int quantidadePedida; // O quanto ele vai pedir

    public GameObject destinoPoint; // O ponto na tela onde o cliente para pra ser atendido 

    public GameObject balaoFala; // O balão de fala do cliente
    public TextMeshProUGUI textoFala; // O texto da fala do cliente

    public bool sendoAtendido = false;

    // Start is called before the first frame update
    void Start()
    {
        // Randomizando o tipo de pedido e a quantidade
        itemPedido.AddRange(new List<string>() { "Pilhas", "Leite", "Ovos" });
        int pedido = Random.Range(0, 3);
        nomeDoPedido = itemPedido[pedido].ToString();
        quantidadePedida = Random.Range(1, 5);        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsDestination();
    }

    // Faz o objeto "Cliente" se mover até o balcão do caixa
    void MoveTowardsDestination()
    {
        Vector2 destino = destinoPoint.gameObject.GetComponent<Transform>().position;
        Vector2 posAtual = gameObject.GetComponent<Transform>().position;

        if (posAtual != destino)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), destino, 800 * Time.deltaTime);
        }
        else StartCoroutine(ClienteFazPedido());
    }

    // Essa é a sequência de eventos em que o cliente vai fazer o seu pedido
    IEnumerator ClienteFazPedido()
    {
        if (!sendoAtendido)
        {
            sendoAtendido = true; // Pra que essa co-rotina execute uma vez só
            yield return new WaitForSeconds(0.2f);
            balaoFala.SetActive(true); //Faz o balão de fala aparecer
            yield return new WaitForSeconds(0.1f);
            textoFala.text = "Gostaria de x" + quantidadePedida + " " + 
                nomeDoPedido + ", por favor."; //Muda o texto
            //StartCoroutine(DisplayTextoTypewriter());
            yield return null;
        }
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
