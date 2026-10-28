using UnityEngine;

public class DanioLava : MonoBehaviour
{
    public int puntosDano = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AplicarDano(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        AplicarDano(collision.gameObject);
    }

    void AplicarDano(GameObject objeto)
    {
        if (objeto.CompareTag("Player") || objeto.name.Contains("goku"))
        {
            PlayerMovement player = objeto.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.RecibirDano(puntosDano);
            }
        }
    }
}