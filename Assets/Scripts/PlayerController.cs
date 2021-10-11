using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Arduino Manager")]
    [SerializeField] ArduinoManager arduino;

    [Header("Player Attribute")]
    [SerializeField] float runSpeed = 0.1f;
    [SerializeField] float horizontalJoystick;
    [SerializeField] float verticalJoystick;
    [SerializeField] int pinJoystick;
    [SerializeField] int controlManager;
    [SerializeField] int controlMax;

    public TextMeshProUGUI controllerText;

    void Update()
    {
        //perubahan data string menjadi float
        verticalJoystick = float.Parse(arduino.joystickData[0]);
        horizontalJoystick = float.Parse(arduino.joystickData[1]);
        pinJoystick = int.Parse(arduino.joystickData[2]);

        //kondisi apabila terdapat input dari button joystick
        if (pinJoystick != 0)
            controlManager++;

        //kondisi apabila tipe controller sudah melebihi maksimal
        if (controlManager == controlMax)
            controlManager = 0;

        switch (controlManager)
        {
            case 0:
                //program yang akan dijalankan saat tipe kontrol=0
                transform.Translate(new Vector3(horizontalJoystick, 0, verticalJoystick) * (runSpeed * Time.deltaTime));
                break;

            case 1:
                //program yang akan dijalankan saat tipe kontrol=1
                transform.Translate(new Vector3(horizontalJoystick, verticalJoystick, 0) * (runSpeed * Time.deltaTime));
                break;

            case 2:
                //program yang akan dijalankan saat tipe kontrol=2
                transform.Rotate(horizontalJoystick, 0, verticalJoystick, Space.Self);
                transform.Rotate(horizontalJoystick, 0, verticalJoystick, Space.World);
                break;
        }

        controllerText.text = "Type: " + controlManager;
    }
}
