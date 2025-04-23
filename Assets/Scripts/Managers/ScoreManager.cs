using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Image MonedaPuntos;
    public float MonedaAct;
    public float MonedaMax;

    void Start()
    {
        ActualizarBarra();
    }

    public void AgregarMoneda(float cantidad)
    {
        MonedaAct += cantidad;
        if (MonedaAct > MonedaMax)
        {
            MonedaAct = MonedaMax;
        }
        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        if (MonedaPuntos != null)
        {
            MonedaPuntos.fillAmount = MonedaAct / MonedaMax;
        }
    }
}

