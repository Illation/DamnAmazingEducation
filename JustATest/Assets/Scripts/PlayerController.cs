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

    public float normalSpeed = 7.0f;
    public float boostSpeed = 12.0f;
    public Camera cam = null;           // Reference to the Main Camera
    public float interactDistance = 0.5f; // How far player can interact with objects
    public bool playerTwo = false;
    public TextureOffsetController tracks;
    public ParticleSystem[] trackParticles;
    public ParticleSystem[] boostTrackParticles;
    public ParticleSystem smokeParticles;
    public GameObject spawn;//, exit, entry; unused currently
    [HideInInspector]
    public bool alive = true;

    // Private variables
    //private CharacterController _controller = null; // Reference to the character controller
    private Rigidbody _body = null;
    private Vector3 _moveDirection = Vector3.zero;  // Which direction he's moving
    private float _sqrInteractDistance = 0;         // Interact distance squared, for performance. 
    private bool _smoking = false;
    private bool _boostActive = false;
    private float _boostTimer = 0.0f;
    private float _movementSpeed; // How fast the player is moving
    private float _respawnTimer = -10.0f;

    public float ArmHeight = 20;
    [SerializeField]
    float RespawnHeightMultiplier = 5;

    // Methods
    // -------

    void Start() {
        _body = GetComponent<Rigidbody>();
        _sqrInteractDistance = interactDistance * interactDistance;
        _movementSpeed = normalSpeed;
        smokeParticles.Stop();
    }

    void Update() {
        _respawnTimer -= Time.deltaTime;
        ArmHeight = Mathf.Abs(_respawnTimer)* Mathf.Abs(_respawnTimer) * RespawnHeightMultiplier;
        if (!alive) {
            if (_respawnTimer > 0.0f)
            {
                transform.position = spawn.transform.position+new Vector3(0, ArmHeight, 0);

                if (_respawnTimer <= 0.0f) {
                    smokeParticles.Clear();
                }
            }
            else {
                alive = true;
                GetComponent<Collider>().enabled = true;
            }
            return;
        }

        if (_boostTimer > 0.0f) {
            _boostTimer -= Time.deltaTime;
            _movementSpeed = boostSpeed;
        }
        else {
            _boostActive = false;
            _movementSpeed = normalSpeed;
        }

        HandleMovement(); // Handle all the movement.
    }

    void HandleMovement() {
        if (!playerTwo) _moveDirection = Input.GetAxisRaw("Vertical") * cam.transform.forward + Input.GetAxisRaw("Horizontal") * cam.transform.right;
        else _moveDirection = Input.GetAxisRaw("Vertical 2") * cam.transform.forward + Input.GetAxisRaw("Horizontal 2") * cam.transform.right;
        _moveDirection.y = 0;
        if (_moveDirection != Vector3.zero) transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward, _moveDirection, 0.6f));
        _moveDirection.Normalize();
        //_body.velocity = _moveDirection * movementSpeed;
        if (_moveDirection.sqrMagnitude > Mathf.Epsilon) _body.velocity = transform.forward * _movementSpeed * _moveDirection.magnitude;
        else _body.velocity = Vector3.zero;
        if (_body.velocity.sqrMagnitude > Mathf.Epsilon) {
            tracks.active = true;
            if (!_boostActive) {
                boostTrackParticles[0].Stop();
                boostTrackParticles[1].Stop();
                if (!trackParticles[0].isEmitting) trackParticles[0].Simulate(0, true, true);
                if (!trackParticles[1].isEmitting) trackParticles[1].Simulate(0, true, true);
                trackParticles[0].Play();
                trackParticles[1].Play();
            }
            else {
                trackParticles[0].Stop();
                trackParticles[1].Stop();
                if (!boostTrackParticles[0].isEmitting) boostTrackParticles[0].Simulate(0, true, true);
                if (!boostTrackParticles[1].isEmitting) boostTrackParticles[1].Simulate(0, true, true);
                boostTrackParticles[0].Play();
                boostTrackParticles[1].Play();
            }
        }
        else {
            tracks.active = false;
            trackParticles[0].Stop();
            trackParticles[1].Stop();
            boostTrackParticles[0].Stop();
            boostTrackParticles[1].Stop();
        }
        _body.angularVelocity = Vector3.zero;

        //Sounds
        float pitch = _boostActive ? 2 : 1;
        float volume = (_body.velocity.magnitude / (_boostActive ? boostSpeed : normalSpeed)) * 4;
        GlobalSoundManager.instance.SetPlayerVolumePitch(!playerTwo, volume, pitch);
    }

    public void Damage() {
        if (!_smoking) {
            _smoking = true;
            smokeParticles.Simulate(0, true, true);
            smokeParticles.Play();
        }
        else {
            alive = false;
            _respawnTimer = 1.5f;
            _smoking = false;
            trackParticles[0].Stop();
            trackParticles[1].Stop();
            smokeParticles.Stop();
            transform.position = spawn.transform.position;
            GetComponent<Collider>().enabled = false;
            _body.velocity = Vector3.zero;
        }
    }

    public void SpeedBoost() {
        _boostActive = true;
        _boostTimer = 3.0f;
    }
}
