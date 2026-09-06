using UnityEngine;

public class MovimientoPrueba : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocidad = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Movimiento con teclas A/D
        float movX = 0;
        if (Input.GetKey(KeyCode.A)) movX = -1;
        if (Input.GetKey(KeyCode.D)) movX = 1;
        
        rb.linearVelocity = new Vector2(movX * velocidad, rb.linearVelocity.y);
        
        // Debug en consola
        if (movX != 0)
        {
            Debug.Log("Moviendo: " + movX);
        }
    }
}