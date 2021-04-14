using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugFeatures : MonoBehaviour
{
    //SE LIVRAR DESSE SCRIPT QUANDO O JOGO ESTIVER PRONTO!!!!!! APENAS PARA TESTES!!!!!
    //SE LIVRAR DESSE SCRIPT QUANDO O JOGO ESTIVER PRONTO!!!!!! APENAS PARA TESTES!!!!!
    //SE LIVRAR DESSE SCRIPT QUANDO O JOGO ESTIVER PRONTO!!!!!! APENAS PARA TESTES!!!!!
    //SE LIVRAR DESSE SCRIPT QUANDO O JOGO ESTIVER PRONTO!!!!!! APENAS PARA TESTES!!!!!
    //SE LIVRAR DESSE SCRIPT QUANDO O JOGO ESTIVER PRONTO!!!!!! APENAS PARA TESTES!!!!!

    float ogTimescale = 0;
    void Update()
    {
        DestroyClienteTeste();
        ReiniciarCena();
        ManipularTempoTestes();
    }

    // Tira o cliente que está no caixa - Aperte D
    void DestroyClienteTeste()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine("DestruirCliente");
        }
    }
    
    // Reinicia a cena - Aperte R
    public void ReiniciarCena()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }

    //Aperte + para acelerar o tempo
    //Aperte - para desacelerar o tempo
    //Aperte 0 para voltar ao fluxo de tempo normal
    public void ManipularTempoTestes()
    {
        ogTimescale = Time.timeScale;

        if (Input.GetKeyDown(KeyCode.KeypadPlus) || (Input.GetKeyDown(KeyCode.Plus)))
        {
            Time.timeScale *= 2;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus) || (Input.GetKeyDown(KeyCode.Minus)))
        {
            Time.timeScale /= 2;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) || (Input.GetKeyDown(KeyCode.Alpha0)))
        {
            Time.timeScale = 1;
        }
    }

    IEnumerator DestruirCliente()
    {
        Destroy(GameController.controller.fila[0]);
        GameController.controller.fila.Remove(GameController.controller.fila[0]);
        yield return null;
    }
}
