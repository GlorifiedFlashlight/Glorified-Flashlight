using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float Speed, Sprint, Jump;
    public Rigidbody Player;
    Vector3 movement;
    int floorMask;
    public float camRayLength;
    public float health;

    void Awake()
    {  
        floorMask = LayerMask.GetMask("Floor");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) JumpPlayer();
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
