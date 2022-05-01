using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallBehavior : MonoBehaviour
{
    public bool canRespawn = false;

    GameObject target;
    Rigidbody ballRb;
    GameManager gameManager;
    bool alreadyTouched = false;
    bool canShoot = false;
    float horizontalInput;
    float verticalInput;
    float yTopRange = 40.0f;
    float yBottomRange = 13.5f;
    float zRange = 8.0f;
    Vector3 spawnLocation;
    
    [SerializeField] float speed = 10.0f;
    [SerializeField] Vector3 throwVector = new Vector3(-8, 13, 0);
    

    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        target = GameObject.Find("Target");
        throwVector = (target.transform.position - transform.position);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        GetInputs();
    }

    public void Shoot()
    {
        ballRb.AddForce(throwVector * gameManager.throwPower, ForceMode.Impulse);
        canShoot = false;
        canRespawn = true;
        StartCoroutine(DespawnCountdownRoutine());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canShoot = false;
        }

        if (collision.gameObject.CompareTag("Stand") && !alreadyTouched)
        {
            canShoot = true;
            alreadyTouched = true;
        }
    }

    IEnumerator DespawnCountdownRoutine()
    {
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }

    public IEnumerator RespawnCountdownRoutine()
    {
        yield return new WaitForSeconds(.7f);
        spawnLocation = new Vector3(0, 5, 0);
        Instantiate(gameManager.basketballPrefab, spawnLocation, gameManager.basketballPrefab.transform.rotation);
    }

    void GetInputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        target.transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        target.transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);

        if (target.transform.position.y < yBottomRange)
            target.transform.position = new Vector3(target.transform.position.x, yBottomRange, target.transform.position.z);
        if (target.transform.position.y > yTopRange)
            target.transform.position = new Vector3(target.transform.position.x, yTopRange, target.transform.position.z);

        if (target.transform.position.z < -zRange)
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -zRange);
        if (target.transform.position.z > zRange)
            target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, zRange);

        //sets trajectory for shot ball
        throwVector = (target.transform.position - transform.position);

        // When space is clicked, shoots ball
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
        }
        // if the ball can respawn, it will begin to respawn
        if (canRespawn)
        {
            StartCoroutine(RespawnCountdownRoutine());
            canRespawn = false;
        }
    }
}
