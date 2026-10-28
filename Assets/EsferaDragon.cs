using UnityEngine;

public class EsferaDragon : MonoBehaviour
{
    public AudioClip soundRecoger;
    [Range(0f, 1f)] public float volumen = 0.8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.gameObject.name.Contains("goku"))
        {
            if (soundRecoger != null)
            {
                AudioSource.PlayClipAtPoint(soundRecoger, Camera.main.transform.position, volumen);
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AgregarEsfera();
            }

            Destroy(gameObject);
        }
    }
}