using UnityEngine;
using UnityEngine.Tilemaps;

public class Proyectil : MonoBehaviour
{
    //velocidad de movimiento del proyectil y tiempo en pantalla
    public float velocidad = 10f;
    public float tiempoVida = 3f;

    //radio de destrcucion 3x3
    public int radioDestruccion = 1; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        //aplica la velocidad del proyectil dependiendo de donde se este mirando goku
        float direccion = Mathf.Sign(transform.localScale.x);
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(velocidad * direccion, 0f);
        }

        // destrucion automatica en caso de no tocar nada
        Destroy(gameObject, tiempoVida);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // detecta colisones con el tilemap de hielo
        Tilemap tilemap = collision.GetComponent<Tilemap>();

        if (tilemap == null)
        {
            tilemap = collision.GetComponentInParent<Tilemap>();
        }

        if (tilemap != null && (collision.CompareTag("Hielo") || collision.gameObject.name.Contains("Hielo")))
        {
            DestruirBloquesEnArea(tilemap);
            //destruye el ataque de goku en caso de impacto
            Destroy(gameObject); 
            return;
        }

        // destruccion en caso de impactar con alguna parte del mapa 
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo") && !collision.gameObject.name.Contains("Hielo"))
        {
            Destroy(gameObject);
        }
    }

    // calculo de destrucion del ataque de goku
    void DestruirBloquesEnArea(Tilemap tilemap)
    {
        // obtiene la celda de impacto 
        Vector3Int celdaImpacto = tilemap.WorldToCell(transform.position);

        // calcula el impacto en el radio del ataque
        for (int x = -radioDestruccion; x <= radioDestruccion; x++)
        {
            for (int y = -radioDestruccion; y <= radioDestruccion + 1; y++) 
            {
                Vector3Int coordenadaAEliminar = new Vector3Int(celdaImpacto.x + x, celdaImpacto.y + y, celdaImpacto.z);

                if (tilemap.HasTile(coordenadaAEliminar))
                {
                    tilemap.SetTile(coordenadaAEliminar, null);
                }
            }
        }
    }
}