using UnityEngine;
//[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]

/// <summary>
/// Controls all the actions Toad can make.
/// </summary>
public class PlayerController : MonoBehaviour {
    // Fields
    // ------

    // Public variables
    public float movementSpeed = 10.0f; // How fast the player moves
    public Camera cam = null;           // Reference to the Main Camera
    public float interactDistance = 0.5f; // How far player can interact with objects
    public bool playerTwo = false;
    public TextureOffsetController tracks;

    // Private variables
    //private CharacterController _controller = null; // Reference to the character controller
    private Rigidbody _body = null;
    private Vector3 _moveDirection = Vector3.zero;  // Which direction he's moving
    private float _sqrInteractDistance = 0;         // Interact distance squared, for performance.              

    // Methods
    // -------

    void Start() {
        _body = GetComponent<Rigidbody>();
        _sqrInteractDistance = interactDistance * interactDistance;
    }

    void Update() {
        HandleMovement(); // Handle all the movement.
        HandleInput(); // Handle all the input.
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        // Check what the controller hit and act accordingly.
        //switch (hit.gameObject.tag) {
        //    //case "Untagged":
        //    //    return;
        //    //default:
        //    //    Debug.Log("Tag not found!");
        //    //    return;
        //}
    }

    // Handles all the movement.
    //void HandleMovement() {
    //    if (!playerTwo) _moveDirection = Input.GetAxisRaw("Vertical") * cam.transform.forward + Input.GetAxisRaw("Horizontal") * cam.transform.right;
    //    else _moveDirection = Input.GetAxisRaw("Vertical 2") * cam.transform.forward + Input.GetAxisRaw("Horizontal 2") * cam.transform.right;
    //    _moveDirection.y = 0;
    //    if (_moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(_moveDirection);
    //    _moveDirection.Normalize();
    //    _moveDirection *= movementSpeed;
    //    _controller.Move(_moveDirection * Time.deltaTime);
    //}

    void HandleMovement() {
        if (!playerTwo) _moveDirection = Input.GetAxisRaw("Vertical") * cam.transform.forward + Input.GetAxisRaw("Horizontal") * cam.transform.right;
        else _moveDirection = Input.GetAxisRaw("Vertical 2") * cam.transform.forward + Input.GetAxisRaw("Horizontal 2") * cam.transform.right;
        _moveDirection.y = 0;
        if (_moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(_moveDirection);
        _moveDirection.Normalize();
        //_moveDirection *= movementSpeed;
        //_body.velocity = _moveDirection * Time.deltaTime;
        _body.velocity = _moveDirection * movementSpeed;
        if (_body.velocity.sqrMagnitude > Mathf.Epsilon) tracks.active = true;
        else tracks.active = false;
        _body.angularVelocity = Vector3.zero;
    }

    // Handle all the necessary input.
    void HandleInput() {
        // Use A button on controller or E key to interact with closest object
        // if (Input.GetButtonDown("Interact")) Interact();
    }
}
