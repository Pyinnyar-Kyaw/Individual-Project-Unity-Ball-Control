using System.IO.Ports;
using UnityEngine;

public class ArduinoManager : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM6", 9600);
    public float pitch, roll;

    public Rigidbody player;
    public float tiltForce = 0.2f;

    public float basePitch = 0f;
    public float baseRoll = 0f;
    private bool calibrated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 100;

        if (serialPort.IsOpen)
        {
            Debug.Log("serial port is open");
        }
        else
        {
            Debug.Log("serial port not open");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine(); // e.g., "512,520"
                string[] values = data.Split(',');

                Debug.Log(data);

                if (values.Length == 2)
                {
                    pitch = float.Parse(values[0]);
                    roll = float.Parse(values[1]);

                    if (!calibrated)
                    {
                        basePitch = pitch;
                        baseRoll = roll;
                        calibrated = true;
                        Debug.Log("Calibrated: " + basePitch + ", " + baseRoll);
                    }

                    float calibratedPitch = pitch - basePitch;
                    float calibratedRoll = roll - baseRoll;

                    // Optional: Clamp values
                    calibratedPitch = Mathf.Clamp(calibratedPitch, -10f, 10f);
                    calibratedRoll = Mathf.Clamp(calibratedRoll, -10f, 10f);

                    float deadzone = 3.2f; // you can tweak this

                    if (Mathf.Abs(calibratedPitch) < deadzone) calibratedPitch = 0f;
                    if (Mathf.Abs(calibratedRoll) < deadzone) calibratedRoll = 0f;


                    if (player != null)
                    {
                        Vector3 tilt = new Vector3(calibratedRoll, 0, -calibratedPitch);
                        player.AddForce(tilt * tiltForce);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Serial Read Error: " + ex.Message);
            }
        }
    }
}