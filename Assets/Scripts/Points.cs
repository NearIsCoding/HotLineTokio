using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    //variabl
    public static Points Instance;
    public float CantidadPuntos;

    public void Awake()
    {
        if (Points.Instance == null)
        {
            Points.Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void SumPuntos(float Puntos)

    {
        CantidadPuntos += Puntos;
    }
}
