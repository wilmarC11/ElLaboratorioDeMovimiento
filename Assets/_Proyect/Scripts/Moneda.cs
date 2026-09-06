using UnityEngine;

public class Moneda : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que tocó es el jugador
        if (other.CompareTag("Player"))
        {
            // Destruir la moneda (recogerla)
            Destroy(gameObject);
            Debug.Log("¡Moneda recogida! +1 punto");
        }
    }
}