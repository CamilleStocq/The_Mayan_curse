using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [Header("Déplacement")]
    public Transform holdPoint; // point où l'objet sera tenu
    private Grabbable heldObject; // objet actuellement tenu
    private Grabbable nearbyObject; // objet dans le trigger du joueur

    private Rigidbody rb;
    public float moveSpeed = 5f; // vitesse du joueur
    public GameObject Player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ---------------- Déplacement ----------------
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);

        // ---------------- Interaction objet(s) ----------------
        if (Input.GetKeyDown(KeyCode.E)) // si on appui sur E
        {
            if (heldObject == null && nearbyObject != null) // si on tient pas d'objet et qu'il y a un objet à attraper
            {
                
                heldObject = nearbyObject; // attraper l'objet qui est dans le trigger
                heldObject.OnPickedUp(holdPoint); //enfant du player et prends sa physique
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && heldObject != null) // si on appui sur Q et qu'on tient un objet
        {
            heldObject.OnDrop(); // on lache l'objet
            heldObject = null; // tenir = null
        }
    }

    // ---------------- Détection trigger ----------------
    private void OnTriggerEnter(Collider other)
    {
        Grabbable grabbable = other.GetComponent<Grabbable>(); // est ce qu'il a le script Grabbable
        if (grabbable != null && !grabbable.isPickedUp)
        {
            nearbyObject = grabbable; // objet dispo pour être attrapé
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Grabbable>() == nearbyObject) // si il a le script et qu'il n'est pas encore attraper alors...
        {
            nearbyObject = null; // plus dans le trigger
        }
    }
}
