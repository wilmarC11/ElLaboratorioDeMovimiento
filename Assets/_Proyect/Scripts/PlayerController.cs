using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 10f;
    
    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 12f;
    [SerializeField] private float checkRadio = 0.2f;
    
    [Header("Suelo")]
    [SerializeField] private LayerMask capaSuelo;
    [SerializeField] private Transform checkSuelo;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (checkSuelo == null)
        {
            GameObject go = new GameObject("CheckSuelo");
            go.transform.parent = transform;
            go.transform.localPosition = new Vector3(0, -0.5f, 0);
            checkSuelo = go.transform;
        }
    }
    
    void Update()
    {
        DetectarSuelo();
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Saltar();
        }
    }
    
    void FixedUpdate()
    {
        Mover();
    }
    
    private void Mover()
    {
        float movimiento = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(movimiento * velocidadMovimiento, rb.linearVelocity.y);
    }
    
    private void Saltar()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        Debug.Log("¡Salto!");
    }
    
    private void DetectarSuelo()
    {
        isGrounded = Physics2D.OverlapCircle(checkSuelo.position, checkRadio, capaSuelo);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (checkSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(checkSuelo.position, checkRadio);
        }
    }
}