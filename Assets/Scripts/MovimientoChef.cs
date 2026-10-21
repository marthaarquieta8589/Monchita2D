using UnityEngine;

public class MovimientoChef : MonoBehaviour
{
    public float velocidad = 4f;
    private Rigidbody2D rb;
    private float movimientoX;
    private Animator animator;
    public float fuerzaSalto = 8f;
    private bool enSuelo = true;
public float velocidadRapida = 7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
{
    movimientoX = Input.GetAxisRaw("Horizontal");

    animator.SetBool("Caminando", movimientoX != 0);

    if (movimientoX > 0)
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
    else if (movimientoX < 0)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    if (Input.GetKeyDown(KeyCode.W) && enSuelo)
{
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
    enSuelo = false;
}
}
    void FixedUpdate()
{
    float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadRapida : velocidad;

    rb.linearVelocity = new Vector2(
        movimientoX * velocidadActual,
        rb.linearVelocity.y
    );
}

    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.name == "Suelo")
    {
        enSuelo = true;
    }
}

}

