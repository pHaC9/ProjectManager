using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnCompra : MonoBehaviour
{    
    public void BuyProductionUpgrade1()
    {
        if (GameController.controller.qtePilhas > 0)
        {
            
            GameController.controller.qtePilhas--; //Item vendido é retirado do estoque
            GameController.controller.dinheiro += 25; //Valor do item vendido adicionado ao total de dinheiro

            Destroy(GameController.controller.fila[0]);
            GameController.controller.fila.Remove(GameController.controller.fila[0]);
        }
       
    }

}
