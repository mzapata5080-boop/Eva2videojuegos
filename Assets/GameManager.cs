using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int esferasRecolectadas = 0;
    public int totalEsferas = 7;

    // interfaz visual
    public TextMeshProUGUI textoContador;
    public TextMeshProUGUI textoVidas;
    public GameObject mensajeVictoria;
    public GameObject mensajeDerrota;
    public GameObject panelPausa;

    //configuración del volumen del sonido
    public AudioClip soundVictory;
    [Range(0f, 1f)] public float volumenVictoria = 0.4f;

    public AudioClip soundDefeated;
    [Range(0f, 1f)] public float volumenDerrota = 0.4f;

    private AudioSource audioSource;
    private bool juegoTerminado = false;
    private bool haGanado = false;
    private bool juegoPausado = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //inicia el juego junto a al soundtrack
    void Start()
    {
        Time.timeScale = 1f;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        ActualizarTextoContador();

        //oculta los mensajes de victoria, derrota y pausa hasta su activacion
        if (mensajeVictoria != null) mensajeVictoria.SetActive(false);
        if (mensajeDerrota != null) mensajeDerrota.SetActive(false);
        if (panelPausa != null) panelPausa.SetActive(false);
    }
    //funcion para la tecla escape para pausr
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !juegoTerminado)
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }

        //reinicia el juego con el boton r luego de conseguir las 7 eseferas
        if (haGanado && Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarNivel();
        }
    }

    //Funciones del menu pausa

    // congela el juego y abre el menu 
    public void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }
    }

    // descongela el juego 
    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
    }

    // regresa al menu principal del juego
    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    //funciones de vidas y conteo de esferas

    public void AgregarEsfera()
    {
        if (juegoTerminado) return;

        esferasRecolectadas++;
        ActualizarTextoContador();

        if (esferasRecolectadas >= totalEsferas)
        {
            GanarJuego();
        }
    }

    void ActualizarTextoContador()
    {
        if (textoContador != null)
        {
            textoContador.text = "Esferas: " + esferasRecolectadas + " / " + totalEsferas;
        }
    }

    public void ActualizarTextoVidas(int vidas)
    {
        if (textoVidas != null)
        {
            string corazones = "";
            for (int i = 0; i < 3; i++)
            {
                if (i < vidas)
                {
                    corazones += "<color=#FF2222>♥ </color>";
                }
                else
                {
                    corazones += "<color=#555555>♥ </color>";
                }
            }
            textoVidas.text = corazones;
        }
    }

    // funciones para la condicion de victoria y derrota

    void GanarJuego()
    {
        juegoTerminado = true;
        haGanado = true;

        if (mensajeVictoria != null)
        {
            mensajeVictoria.SetActive(true);
        }

        if (audioSource != null && soundVictory != null)
        {
            audioSource.PlayOneShot(soundVictory, volumenVictoria);
        }
    }

    public void PerderJuego()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        StartCoroutine(RutinaDerrota());
    }

    private IEnumerator RutinaDerrota()
    {
        if (mensajeDerrota != null)
        {
            mensajeDerrota.SetActive(true);
        }

        if (audioSource != null && soundDefeated != null)
        {
            audioSource.PlayOneShot(soundDefeated, volumenDerrota);
        }

        yield return new WaitForSeconds(1.8f);

        ReiniciarNivel();
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}