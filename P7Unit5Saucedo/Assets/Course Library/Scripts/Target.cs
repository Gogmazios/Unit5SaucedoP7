using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private float minS = 12;
    private float maxS = 16;
    //Speed = S
    private float maxT = 10;
    //Torque = T
    private float xRange = 4;
    private float ySpawnPos = -6;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem EP; 
    //explosionParticle = EP
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //targetRB = targetRb
        targetRB = GetComponent<Rigidbody>();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
        
        Vector3 RandomForce()
        {
            return Vector3.up * Random.Range(minS, maxS);
        }
        float RandomTorque()
        {
            return Random.Range(-maxT, maxT); 
        }
        Vector3 RandomSpawnPos()
        {
            return new Vector3(Random.Range(-xRange, xRange), ySpawnPos); 
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(EP, transform.position, EP.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); 
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver(); 
        }
    }
}
