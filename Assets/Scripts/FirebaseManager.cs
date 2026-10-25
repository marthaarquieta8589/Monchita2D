using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private FirebaseFirestore db;

    public int monedas = 0;
    public int nivel = 1;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al revisar Firebase: " + task.Exception);
                return;
            }

            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase conectado correctamente");

                db = FirebaseFirestore.DefaultInstance;

                Debug.Log("Firestore inicializado");

                CargarDatos();
            }
            else
            {
                Debug.LogError(
                    "Firebase no disponible: " + task.Result
                );
            }
        });
    }

    public void CargarDatos()
    {
        Debug.Log("Intentando cargar datos desde Firestore...");

        DocumentReference docRef =
            db.Collection("jugadores").Document("jugador1");

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(
                    "Error al cargar datos: " + task.Exception
                );
                return;
            }

            DocumentSnapshot snapshot = task.Result;

            if (snapshot.Exists)
            {
                Debug.Log("Documento jugador1 encontrado");

                Dictionary<string, object> datos =
                    snapshot.ToDictionary();

                if (datos.ContainsKey("monedas"))
                {
                    monedas =
                        Convert.ToInt32(datos["monedas"]);
                }

                if (datos.ContainsKey("nivel"))
                {
                    nivel =
                        Convert.ToInt32(datos["nivel"]);
                }

                Debug.Log(
                    "Datos cargados: monedas = "
                    + monedas
                    + ", nivel = "
                    + nivel
                );
            }
            else
            {
                Debug.LogWarning(
                    "No existe el documento jugador1"
                );
            }
        });
    }

    public void GuardarDatos()
    {
        if (db == null)
        {
            Debug.LogError(
                "Firestore todavía no está inicializado"
            );
            return;
        }

        Dictionary<string, object> datos =
            new Dictionary<string, object>
            {
                { "monedas", monedas },
                { "nivel", nivel }
            };

        DocumentReference docRef =
            db.Collection("jugadores").Document("jugador1");

        docRef.SetAsync(datos).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(
                    "Error al guardar datos: "
                    + task.Exception
                );
                return;
            }

            Debug.Log(
                "Datos guardados: monedas = "
                + monedas
                + ", nivel = "
                + nivel
            );
        });
    }
    public void PasarNivel2()
{
    nivel = 2;
    GuardarDatos();

    Debug.Log("Nivel actualizado a 2");
}
}

