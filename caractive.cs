using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caractive : MonoBehaviour

{

    public GameObject Player;
    public GameObject Playercamera;
    public GameObject CarCamera;
    CarController car;
    public bool carEnter = false;
    
    // Start is called before the first frame update
    void Start()
    {
        CarCamera.SetActive(false);
        car = GetComponent<CarController>();
        car.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(carEnter== true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CarCamera.SetActive(true);
                Player.SetActive(false);
                Playercamera.SetActive(false);
                car.enabled = true;
            }
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            carEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PLayer")
        {
            carEnter = false;
        }
    }
}
