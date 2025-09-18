using UnityEngine;
using System.Collections;

public class RotateWheels : MonoBehaviour
{
    [SerializeField] private Transform wheel;  //la roue qui fait tourner 
    [SerializeField] private Transform[] bridges;  // pontons qui doivent suivre le mouvement de la roues
    [SerializeField] private GameObject interactWithE;    // un petit "E" apparait sur l'écran

    private bool playerInTrigger = false; // est ce que le joueur est dans le trigger ?
    private bool isRotating = false; // pas de rotation
    private float rotationStep = 90f; //la roue et ponton tourne de 90°
    private float rotationDuration = 5f; // temps que le ponton met pour tourner à sa position finale

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E)) // si le joueur est dan le trigger et on appuie sur le E alors ...
        {

            StartCoroutine(RotateWheelAndBridges(rotationStep, rotationDuration)); // demarre la coroutine pour faire tourner les pontons

            interactWithE.SetActive(false); // cacher le E après interaction

            IEnumerator RotateWheelAndBridges(float angle, float duration) // va TOUJOURS avec la coroutine. angle et vitesse de la rotation
            {
                isRotating = true; // ca tourne 

                Quaternion wheelStartRot = wheel.rotation; // rotation actuelle de la wheel
                Quaternion wheelEndRot = wheelStartRot * Quaternion.Euler(0, 0, angle); // tourne autour de l'axe Z

                Quaternion[] bridgesStartRot = new Quaternion[bridges.Length]; // tableau debut et fin de la rotation
                Quaternion[] bridgesEndRot = new Quaternion[bridges.Length];

                for (int i = 0; i < bridges.Length; i++) // Remplit les tableaux
                {
                    bridgesStartRot[i] = bridges[i].rotation;
                    bridgesEndRot[i] = bridgesStartRot[i] * Quaternion.Euler(0, 0, angle);
                }

                float startedTime = 0f; // temps depuis le début de la rotation

                while (startedTime < duration)
                {
                    startedTime += Time.deltaTime; // ajoute temps écoulé
                    float t = startedTime / duration; // calcule temps rotation 

                    wheel.rotation = Quaternion.Slerp(wheelStartRot, wheelEndRot, t); //fait tourner la roue progressivement

                    for (int i = 0; i < bridges.Length; i++)
                    {
                        bridges[i].rotation = Quaternion.Slerp(bridgesStartRot[i], bridgesEndRot[i], t);
                    }

                    yield return null; // attend la prochaine frame avant de continuer la boucle
                }

                
                wheel.rotation = wheelEndRot;//rotation finale exacte
                for (int i = 0; i < bridges.Length; i++)
                {
                    bridges[i].rotation = bridgesEndRot[i];
                }

                isRotating = false; // fin de la rotation
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // uniquement si le joueur touche
        {
            playerInTrigger = true;
            interactWithE.SetActive(true); // afficher le E
            //Debug.Log("roue demarre");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            interactWithE.SetActive(false); // cacher le E
            //Debug.Log("roue stop");
        }
    }
}
