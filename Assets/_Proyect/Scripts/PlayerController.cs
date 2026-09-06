using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ===== VARIABLES SERIALIZADAS (Visibles en el Inspector) =====
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 10f; // Velocidad horizontal
    
    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 12f; // Fuerza del salto
    [SerializeField] private float checkRadio = 0.2f; // Radio del detector de suelo
    
    [Header("Suelo")]
    [SerializeField] private LayerMask capaSuelo; // Qué capas son consideradas "suelo"
    [SerializeField] private Transform checkSuelo; // Punto de referencia para detectar suelo
    
    // ===== VARIABLES PRIVADAS =====
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private bool isGrounded; // ¿Está el jugador en el suelo?
    
    // ===== MÉTODO START (Inicialización) =====
    void Start()
    {
        // Obtener la referencia al Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();
        
        // Si no se asignó el checkSuelo en el Inspector, lo creamos automáticamente
        if (checkSuelo == null)
        {
            GameObject go = new GameObject("CheckSuelo");
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3(0, -0.5f, 0);
            checkSuelo = go.transform;
        }
    }
    
    // ===== MÉTODO UPDATE (Se ejecuta cada frame) =====
    void Update()
    {
        // Detectar si está en el suelo
        DetectarSuelo();
        
        // Detectar entrada de salto (Espacio)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Saltar();
        }
    }
    
    // ===== MÉTODO FIXEDUPDATE (Físicas - Se ejecuta en intervalos fijos) =====
    void FixedUpdate()
    {
        // Movimiento horizontal
        Mover();
    }
    
    // ===== FUNCIÓN: Movimiento Horizontal =====
    private void Mover()
    {
        // Obtener entrada horizontal (-1, 0, 1)
        float movimiento = Input.GetAxisRaw("Horizontal");
        
        // Aplicar velocidad horizontal
        rb.linearVelocity = new Vector2(movimiento * velocidadMovimiento, rb.linearVelocity.y);
    }
    
    // ===== FUNCIÓN: Salto =====
    private void Saltar()
    {
        // Aplicar fuerza vertical
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
    }
    
    // ===== FUNCIÓN: Detección de Suelo =====
    private void DetectarSuelo()
    {
        // Crear un círculo invisible en la posición del checkSuelo
        // y detectar si colisiona con algo en la capa "Suelo"
        isGrounded = Physics2D.OverlapCircle(checkSuelo.position, checkRadio, capaSuelo);
    }
    
    // ===== MÉTODO: Dibujar el Gizmo del detector (Visual en Editor) =====
    private void OnDrawGizmosSelected()
    {
        if (checkSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(checkSuelo.position, checkRadio);
        }
    }
}