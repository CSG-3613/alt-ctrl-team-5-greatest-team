using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SceneManagement;

public class scr : MonoBehaviour
{
    private SerialPort port = new SerialPort("COM3", 9600);
    private string ardInput;

    // Start is called before the first frame update
    void Start()
    {
        port.Open();
        Debug.Log("Port is Open");
    }

    // Update is called once per frame
    void Update()
    {
        if (!port.IsOpen)
            return;
        ardInput = port.ReadLine();
        if (ardInput == "Big squeeze")
        {
            SceneManager.LoadScene(0);
        }
    }
    
}
