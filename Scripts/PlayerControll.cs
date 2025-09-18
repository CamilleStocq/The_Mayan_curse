using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Le Rigidbody n'est pas attaché au joueur !");
        }
    }

    void Update()
    {
        // Récupération des inputs
        float moveX = Input.GetAxis("Horizontal"); // A/D ou flèches gauche/droite
        float moveZ = Input.GetAxis("Vertical");   // W/S ou flèches haut/bas

        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;

        // Déplacement
        rb.MovePosition(transform.position + movement);
    }
}