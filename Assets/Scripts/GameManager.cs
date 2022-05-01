using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject basketballPrefab;
    public TextMeshProUGUI powerText;
    public float throwPower = 1;

    GameObject target;
    Vector3 spawnLocation;
    bool targetInvis = true;

    void Start()
    {
        spawnLocation = new Vector3(0, 5, 0);
        Instantiate(basketballPrefab, spawnLocation, basketballPrefab.transform.rotation);
        target = GameObject.Find("Target");
        target.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && throwPower >= .8000f)
        {
            throwPower -= .004f;

        }
        if (Input.GetKey(KeyCode.E) && throwPower <= 1.8f)
        {
            throwPower += .004f;
        }
        powerText.text = "Power: " + throwPower;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (targetInvis)
            {
                target.GetComponent<Renderer>().enabled = true;
                targetInvis = false;
            }
            else
            {
                target.GetComponent<Renderer>().enabled = false;
                targetInvis = true;
            }
        }
    }


    public IEnumerator RespawnCountdownRoutine()
    {
        yield return new WaitForSeconds(.5f);
        Instantiate(basketballPrefab, spawnLocation, basketballPrefab.transform.rotation);
    }
}
