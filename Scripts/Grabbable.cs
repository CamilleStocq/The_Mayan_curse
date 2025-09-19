using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool isPickedUp = false; // on peut pas l'attraper
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnPickedUp(Transform holdParent) // holdParent = main du joueur (où l’objet doit se trouver)
    {
        if (isPickedUp) return;

        isPickedUp = true;// l'objet est attraper

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.detectCollisions = false;
        }

        transform.SetParent(holdParent);// l'objet suis le player (devient enfant du player)
        transform.localPosition = Vector3.zero;// objet mis au centre de la main
        transform.localRotation = Quaternion.identity;// aucune rotation 
    }

    public void OnDrop() // pour quand on lache l'objet
    {
        if (!isPickedUp) return; // si l'objet n'est pas tenu
        isPickedUp = false;

        if (rb != null)
        {
            rb.isKinematic = false; // on rend la physique
            rb.useGravity = true;   // l'objet tombe
            rb.detectCollisions = true; 
        }

        transform.SetParent(null); // annul le parentage 
    }
}