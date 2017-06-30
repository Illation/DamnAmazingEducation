using UnityEngine;
[RequireComponent(typeof(CharacterController))]

/// <summary>
/// Controls all the actions Toad can make.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Fields
    // ------

    // Public variables
    public float movementSpeed = 10.0f; // How fast the player moves..
    public Camera cam = null;           // Reference to the Main Camera.
    public float interactDistance = 0.5f; // How far Toad can interact with objects

    // Private variables
    private CharacterController _controller = null; // Reference to the character controller.
    private Vector3 _moveDirection = Vector3.zero;  // Which direction he's moving.
    private float _sqrInteractDistance = 0;         // Interact distance squared, for performance.               

    // Methods
    // -------

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _sqrInteractDistance = interactDistance * interactDistance;
    }

    void Update()
    {
        HandleMovement(); // Handle all the movement.
        HandleInput(); // Handle all the input.
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check what the controller hit and act accordingly.
        switch (hit.gameObject.tag)
        {
            //case "Untagged":
            //    return;
            //default:
            //    Debug.Log("Tag not found!");
            //    return;
        }
    }

    // Handles all the movement.
    void HandleMovement()
    {
        _moveDirection = Input.GetAxisRaw("Vertical") * cam.transform.forward + Input.GetAxisRaw("Horizontal") * cam.transform.right;
        _moveDirection.y = 0;
        if (_moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(_moveDirection);
        _moveDirection.Normalize();
        _moveDirection *= movementSpeed;
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    // Handle all the necessary input.
    void HandleInput()
    {
            // Use A button on controller or E key to interact with closest object
            //if (Input.GetButtonDown("Interact")) Interact();
    }

    // Interact with the closest, reachable interactable object.
    //void Interact()
    //{
    //    if (interactableObjects.Length == 0) return;
    //    InteractableObject closestObject = interactableObjects[0];
    //    float closestDistance = (closestObject.transform.position - transform.position).sqrMagnitude;
    //    float distance = 0.0f;
    //    foreach (InteractableObject obj in interactableObjects)
    //    {
    //        if (obj.gameObject.GetComponent<PluckPatchController>() && !obj.GetComponent<PluckPatchController>().active) continue;
    //        distance = (obj.transform.position - transform.position).sqrMagnitude;
    //        if (distance < closestDistance)
    //        {
    //            closestDistance = distance;
    //            closestObject = obj;
    //        }
    //    }

    //    if (closestDistance <= _sqrInteractDistance)
    //    {
    //        if (closestObject.gameObject.GetComponent<PluckPatchController>() && !closestObject.GetComponent<PluckPatchController>().active) return;
    //        if (closestObject.gameObject.GetComponent<LeverController>() || closestObject.gameObject.GetComponent<TurnwheelController>()) leverSound.Play();
    //        closestObject.Interact();
    //    }
    //}

    //// See what the closest object is and position the button accordingly.
    //void CheckClosestInteraction()
    //{
    //    if (interactableObjects.Length == 0) return;
    //    InteractableObject closestObject = interactableObjects[0];
    //    float closestDistance = (closestObject.transform.position - transform.position).sqrMagnitude;
    //    float distance = 0.0f;
    //    foreach (InteractableObject obj in interactableObjects)
    //    {
    //        if (obj.gameObject.GetComponent<PluckPatchController>() && !obj.GetComponent<PluckPatchController>().active) continue;
    //        distance = (obj.transform.position - transform.position).sqrMagnitude;
    //        if (distance < closestDistance)
    //        {
    //            closestDistance = distance;
    //            closestObject = obj;
    //        }
    //    }

    //    if (closestDistance <= _sqrInteractDistance)
    //    {
    //        if (closestObject.gameObject.GetComponent<PluckPatchController>() && !closestObject.GetComponent<PluckPatchController>().active) interactButton.transform.position = new Vector3(1000, 1000, 1000);
    //        else closestObject.ShowInteractable(interactButton);
    //    }
    //    else interactButton.transform.position = new Vector3(1000, 1000, 1000);
    //}
}
