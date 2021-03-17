using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenableMenu : MonoBehaviour
{   //Esse script cuida de métodos que executam a partir de botões

    //MÉTODO QUE LIGA/DESLIGA QUALQUER PANEL 
	public GameObject Panel;
    public AudioSource audioClip = null;
    public AudioSource audioClipErro = null;

    public void TogglePanel(){
        //audioClip.Play();
		if(Panel != null){
			bool isActive = Panel.activeSelf;
			Panel.SetActive(!isActive);
		}
	}
    public void DesligarPanel(GameObject panel)
    {
        //audioClip.Play();
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
   
    public int qte_a_adicionar = 0;
    public int qte_a_subtrair = 0;    

    //MÉTODOS PARA ADICIONAR/SUBTRAIR RECURSOS A PARTIR DE UM BUTTON    
    public void AdicionarDinheiro()
    {
        GlobalData.dinheiro = Mathf.Clamp(GlobalData.dinheiro + qte_a_adicionar,
            0,
            9999);
    }
    public void SubtrairDinheiro()
    {
        GlobalData.dinheiro = Mathf.Clamp(GlobalData.dinheiro - qte_a_subtrair,
            0,
            9999);
    }

    public void AdicionarPilhas()
    {
        if (GlobalData.dinheiro >= qte_a_subtrair && GlobalData.qtePilhas < 500)
        {
            //audioClip.Play();
            GlobalData.qtePilhas = Mathf.Clamp(GlobalData.qtePilhas + qte_a_adicionar, 
                0, 
                500);
            SubtrairDinheiro();
        }
        else
        {
            //audioClipErro.Play();
            return;
        }
    }
    public void AdicionarLeite()
    {
        if (GlobalData.dinheiro >= qte_a_subtrair && GlobalData.qteLeite < 500)
        {
            //audioClip.Play();
            GlobalData.qteLeite = Mathf.Clamp(GlobalData.qteLeite + qte_a_adicionar, 
                0, 
                500);
            SubtrairDinheiro();
        }
        else
        {
            //audioClipErro.Play();
            return;
        }
    }
    public void AdicionarOvos()
    {
        if (GlobalData.dinheiro >= qte_a_subtrair && GlobalData.qteOvos < 500)
        {
            //audioClip.Play();
            GlobalData.qteOvos = Mathf.Clamp(GlobalData.qteOvos + qte_a_adicionar, 
                0, 
                500);
            SubtrairDinheiro();
        }
        else
        {
            //audioClipErro.Play();
            return;
        }
    }

    public void VenderPilhas() //PILHAS CUSTAM 10 DINHEIRO
    {
        if (GlobalData.qtePilhas > 0) //SE AINDA TIVER PILHAS NO ESTOQUE:
        {
            //audioClip.Play();
            GlobalData.qtePilhas -= Cliente.controller.quantidadePedida;
            GlobalData.dinheiro = Mathf.Clamp(GlobalData.dinheiro + (GlobalData.pilhasCusto * Cliente.controller.quantidadePedida), 0, 500);
        }
        else
        {
            //audioClipErro.Play();
            return;
        }
    }
    public void VenderLeite() //LEITE CUSTA 8 DINHEIRO
    {
        if (GlobalData.qteLeite > 0) //SE AINDA TIVER LEITE NO ESTOQUE:
        {
            //audioClip.Play();
            GlobalData.qteLeite -= Cliente.controller.quantidadePedida;
            GlobalData.dinheiro = Mathf.Clamp(GlobalData.dinheiro + (GlobalData.leiteCusto * Cliente.controller.quantidadePedida), 0, 500);
        }
        else
        {
            //audioClipErro.Play();
            return;
        }
    }
    public void VenderOvos() //OVOS CUSTAM 15 DINHEIRO
    {
        if (GlobalData.qteOvos > 0) //SE AINDA TIVER OVOS NO ESTOQUE:
        {
            //audioClip.Play();
            GlobalData.qteOvos -= Cliente.controller.quantidadePedida;
            GlobalData.dinheiro = Mathf.Clamp(GlobalData.dinheiro + (GlobalData.ovosCusto * Cliente.controller.quantidadePedida), 0, 500);
        }
        else
        {
            //audioClipErro.Play();
            return;
        }
    }
}
