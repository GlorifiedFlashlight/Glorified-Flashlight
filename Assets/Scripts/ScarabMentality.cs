using UnityEngine;

public class ScarabMentality : MonoBehaviour {
    
    private Rigidbody rb;
    private Vector3 direction;
    public GameObject player;
    private Light light;
    public PlayerController health;
    private Transform playerWaypoint;

    public float speed;
    public float territoryRadius;
    public float scarabHealth;
    private bool attack;
    private float timeElapsed;

    // Use this for initialization
    void Start () {

        direction = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f)).normalized;

        transform.Rotate(direction);
        rb = GetComponent<Rigidbody>();

        playerWaypoint = null;
        light = player.GetComponentInChildren<Light>();
        attack = false;

        health = player.GetComponent<PlayerController>();
        timeElapsed = 0;
    }
	
	// Update is called once per frame
	void Update () {

        timeElapsed += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) <= territoryRadius && timeElapsed >= 2.0f)
        {
            attack = true;
            if (playerWaypoint == null)
                playerWaypoint = player.transform;
        }
        else
        {
            attack = false;
            timeElapsed = 0;
        }

        if (attack == true)
        {
            if (Vector3.Distance(transform.position, light.transform.position) <= 1 && light.spotAngle == 60)
            {
                scarabHealth -= 0.01f;
                if (scarabHealth <= 0.0f)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    attack = false;
                    FleePlayer();
                }
            }
            else
                ChasePlayer();
        }
        else
        {
            Vector3 newposition = transform.position + direction * speed * Time.deltaTime;

            rb.MovePosition(newposition);
        }
    }
    

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player && health.health > 0)
        {
            attack = false;
            health.health -= 0.5f;

        }

            direction = col.contacts[0].normal + (direction.normalized * -1.0f);
            direction = Quaternion.AngleAxis(Random.Range(-30.0f, 30.0f), Vector3.forward) * direction;

            transform.Rotate(new Vector3(0, direction.y, 0));
        
    }

    void ChasePlayer()
    {
        if (health.health > 0 && playerWaypoint != null)
        {
            transform.LookAt(playerWaypoint.transform.position);


            transform.Translate(Vector3.MoveTowards(transform.position, playerWaypoint.transform.position, territoryRadius) * speed * Time.deltaTime);

            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

            if (transform.position == playerWaypoint.position)
            {
                playerWaypoint = null;
                attack = false;
            }
        }
        else
            attack = false;
    }

    void FleePlayer()
    {
        Vector3 fleeFrom = transform.position - player.transform.position;

        fleeFrom.Normalize();
        transform.Translate(fleeFrom * speed * Time.deltaTime);
    }
}
