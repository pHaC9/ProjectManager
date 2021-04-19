using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCompra : MonoBehaviour
{


    public void BuyProductionUpgrade1()
    {
        if (GameController.controller.qtePilhas > 0)
        {
            
            GameController.controller.qtePilhas--;
            GameController.controller.dinheiro += 25;

            Destroy(GameController.controller.fila[0]);
            GameController.controller.fila.Remove(GameController.controller.fila[0]);
        }
       
    }

}
