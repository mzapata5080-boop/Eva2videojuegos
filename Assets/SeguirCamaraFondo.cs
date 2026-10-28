using UnityEngine;

public class SeguirCamaraFondo : MonoBehaviour
{
    public Transform camara;
    public Vector3 offset = new Vector3(0f, 0f, 10f);

    void LateUpdate()
    {
        if (camara != null)
        {
            transform.position = new Vector3(camara.position.x + offset.x, camara.position.y + offset.y, offset.z);
        }
    }
}