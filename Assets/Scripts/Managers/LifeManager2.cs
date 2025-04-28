using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LifeManager2 : MonoBehaviour
{
    public Image Barradevida;
    public float vidaAct;
    public float vidaMax;

    // Update is called once per frame
    void Update()
    {
        // c le resta la cantidad publica mediante un barrido
        Barradevida.fillAmount = vidaAct / vidaMax;

    }
}


