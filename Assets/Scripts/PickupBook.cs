using UnityEngine;

public class PickUpBook : MonoBehaviour
{
    public float interactRange = 3f;
    public Camera playerCamera;
    public Transform holdPosition;  // Where the book will be held
    public LayerMask interactableLayer;
    public KeyCode interactKey = KeyCode.E;
    public AudioSource pickUpSound;

    private GameObject currentBook;
    private bool isHolding = false;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (!isHolding)
                TryPickBook();
            else
                DropBook();
        }
    }

    void TryPickBook()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            if (hit.collider.CompareTag("Book"))
            {
                currentBook = hit.collider.gameObject;

                // Disable physics while holding
                Rigidbody rb = currentBook.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;

                // Attach to hold position
                currentBook.transform.SetParent(holdPosition);
                currentBook.transform.localPosition = Vector3.zero;
                currentBook.transform.localRotation = Quaternion.identity;

                if (pickUpSound != null)
                    pickUpSound.Play();

                isHolding = true;
            }
        }
    }

    void DropBook()
    {
        if (currentBook == null) return;

        // Detach and enable physics
        currentBook.transform.SetParent(null);
        Rigidbody rb = currentBook.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        currentBook = null;
        isHolding = false;
    }
}
