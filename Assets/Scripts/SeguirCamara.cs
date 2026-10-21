using UnityEngine;

public class SeguirCamara : MonoBehaviour
{
    public Transform objetivo;
    public float suavizado = 5f;

    void LateUpdate()
    {
        if (objetivo != null)
        {
            Vector3 posicionDeseada = new Vector3(
                objetivo.position.x,
                objetivo.position.y,
                transform.position.z
            );

            transform.position = Vector3.Lerp(
                transform.position,
                posicionDeseada,
                suavizado * Time.deltaTime
            );
        }
    }
}

