using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f;

    public Animator animator;
    
    public float fuerzaSalto = 7f;
    
    public float distanciaLineaSuelo = 1.5f; 
    
    public LayerMask capaSuelo;
    
    public bool tocandoElSuelo = true;

    private Rigidbody2D rb;

    public AudioClip soundSalto;
    public AudioClip soundDano;
    public AudioClip soundAtaque;
    private AudioSource audioSource;

    public GameObject prefabAtaque;
    public Transform puntoDisparo;
    public float tiempoEntreAtaques = 1f;
    private float tiempoSiguienteAtaque = 0f;

    public int vidasMaximas = 3;
    public int vidasActuales;
    public float tiempoInvulnerable = 1.2f;
    private float tiempoSiguienteDano = 0f;
    private bool estaMuerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
        vidasActuales = vidasMaximas;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // actualiza el ui mediante el gamemanager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ActualizarTextoVidas(vidasActuales);
        }
    }

    void Update()
    {
        if (estaMuerto) return;

        float inputH = Input.GetAxis("Horizontal");
        float velocidadH = inputH * velocidad * Time.deltaTime;

        float currentScaleX = Mathf.Abs(transform.localScale.x);
        float currentScaleY = transform.localScale.y;
        float currentScaleZ = transform.localScale.z;

        // Voltea el srpite de goku hacia la direcion a la que vaya
        if (inputH > 0)
        {
            transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
        }
        else if (inputH < 0)
        {
            transform.localScale = new Vector3(-currentScaleX, currentScaleY, currentScaleZ);
        }

        transform.position = new Vector3(transform.position.x + velocidadH, transform.position.y, transform.position.z);

        // raycast para detectar si esta tocando el piso
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanciaLineaSuelo, capaSuelo);
        tocandoElSuelo = (hit.collider != null && hit.collider.gameObject != gameObject);

        // funcion salto
        if (Input.GetKeyDown(KeyCode.Space) && tocandoElSuelo)
        {
            rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);

            if (audioSource != null && soundSalto != null)
            {
                audioSource.PlayOneShot(soundSalto);
            }
        }

        // ataque con la letra f y coldown de ataque
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= tiempoSiguienteAtaque)
        {
            DispararAtaque();
            tiempoSiguienteAtaque = Time.time + tiempoEntreAtaques;
        }

        if (animator != null)
        {
            animator.SetFloat("isMove", Mathf.Abs(inputH));
            animator.SetBool("tocandoElSuelo", tocandoElSuelo);
            animator.SetFloat("velocidadY", rb.linearVelocity.y);
        }
    }

    void DispararAtaque()
    {
        if (animator != null)
        {
            animator.SetTrigger("atacar");
        }

        if (audioSource != null && soundAtaque != null)
        {
            audioSource.PlayOneShot(soundAtaque);
        }

        // se llama el proyecil y lo lanza donde mire goku
        if (prefabAtaque != null && puntoDisparo != null)
        {
            GameObject nuevoAtaque = Instantiate(prefabAtaque, puntoDisparo.position, Quaternion.identity);
            float direccion = Mathf.Sign(transform.localScale.x);
            Vector3 escalaAtaque = nuevoAtaque.transform.localScale;
            nuevoAtaque.transform.localScale = new Vector3(Mathf.Abs(escalaAtaque.x) * direccion, escalaAtaque.y, escalaAtaque.z);
        }
    }

    //control de daños
    public void RecibirDano(int cantidad)
    {
        if (estaMuerto || Time.time < tiempoSiguienteDano) return;

        vidasActuales -= cantidad;
        tiempoSiguienteDano = Time.time + tiempoInvulnerable;

        if (audioSource != null && soundDano != null)
        {
            audioSource.PlayOneShot(soundDano);
        }

        if (animator != null)
        {
            animator.SetTrigger("recibirDanio");
        }

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 6f);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ActualizarTextoVidas(vidasActuales);
        }
        if (vidasActuales <= 0)
        {
            Morir();
        }
    }

    void Morir()
    //lanza la pantalla de muerte
    {
        estaMuerto = true;
        rb.linearVelocity = Vector2.zero;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.PerderJuego();
        }
    }

    // Dibuja la línea de detección de suelo en la vista Scene
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanciaLineaSuelo);
    }
}