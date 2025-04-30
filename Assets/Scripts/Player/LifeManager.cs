using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [Header("Configuración de vida")]
    public Image barraDeVida;
    public float vidaMax = 3f;
    public float vidaAct;

    void Start()
    {
        // Al iniciar, vida actual = vida máxima
        vidaAct = vidaMax;
        ActualizarBarra();
    }

    void Update()
    {
        // Opcional: si la vida cambia desde otros lugares, siempre actualizamos la barra
        ActualizarBarra();
    }

    // Método para restar vida
    public void TakeDamage(float daño)
    {
        // Debug.Log("Recibido daño: " + daño);
        vidaAct -= daño;
        vidaAct = Mathf.Clamp(vidaAct, 0f, vidaMax);
        ActualizarBarra();

        if (vidaAct <= 0f)
            Die();
    }

    private void ActualizarBarra()
    {
        // barraDeVida.fillAmount = vidaAct / vidaMax;
    }

    private void Die()
    {
        Debug.Log("Jugador muerto");
        // Aquí pones tu lógica de muerte (reiniciar nivel, animación, etc.)
    }
}
