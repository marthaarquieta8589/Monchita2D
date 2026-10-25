using UnityEngine;

public class Moneda : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FirebaseManager firebase =
                FindFirstObjectByType<FirebaseManager>();

            if (firebase != null)
            {
                firebase.monedas++;
                firebase.GuardarDatos();

                Debug.Log("Moneda recolectada. Total: " + firebase.monedas);
            }

            Destroy(gameObject);
        }
    }
}


