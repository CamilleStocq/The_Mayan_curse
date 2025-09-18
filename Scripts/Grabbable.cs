using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public bool isPickedUp = false;

    public void OnPickedUp(Transform holdParent)
    {
        if (isPickedUp) return;
        isPickedUp = true;

        GetComponent<Rigidbody>().isKinematic = true;

        transform.SetParent(holdParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void OnDrop()
    {
        if (!isPickedUp) return;
        isPickedUp = false;

        GetComponent<Rigidbody>().isKinematic = false;

        transform.SetParent(null);
    }
}