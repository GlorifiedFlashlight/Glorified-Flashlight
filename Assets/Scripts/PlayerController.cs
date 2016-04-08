using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float Speed, Sprint, Jump;
    public Rigidbody Player;
    Vector3 movement;
    public GameObject Search, Beam, Torch;
    int floorMask;
    public float camRayLength;
    bool BeamMode, TorchMode;

    void Awake()
    {  
        floorMask = LayerMask.GetMask("Floor");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) JumpPlayer();
        if (Input.GetMouseButton(0)) BeamMode = true; else BeamMode = false;
        if (Input.GetMouseButton(1)) TorchMode = true; else TorchMode = false;
        if (BeamMode) { Search.SetActive(false); Beam.SetActive(true); }
        else if (TorchMode) { Search.SetActive(false); Torch.SetActive(true); }
        else { Search.SetActive(true); Beam.SetActive(false); Torch.SetActive(false); }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift)) Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Sprint);
        else Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Speed);
        Turning();
    }

    void JumpPlayer()
    {
        Player.AddForce(new Vector3(0.0f, Jump, 0.0f));
    }
    void Move(float _horizontal, float _vertical, float _speed)
    {
        movement.Set(_horizontal, 0f, _vertical);
        
        movement = movement.normalized * _speed * Time.deltaTime;

        Player.MovePosition(transform.position + movement);
    }
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            Player.MoveRotation(newRotatation);
        }
    }
}
