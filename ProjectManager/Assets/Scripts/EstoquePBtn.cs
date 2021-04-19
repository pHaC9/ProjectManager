using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstoquePBtn : MonoBehaviour
{

    public void Pilha()
    {
        if (GameController.controller.dinheiro > 0)
        {
            GameController.controller.qtePilhas += 1;
            GameController.controller.dinheiro -= 25;
        }
    }      
    
}
