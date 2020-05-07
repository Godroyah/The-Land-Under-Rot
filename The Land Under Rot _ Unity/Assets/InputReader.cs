using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    GameController gameController;

    //public string[] buttons;
    //public string[] axes;

    //Controller String Handles
    string ps4Handle;
    string xboxHandle;


    public enum gameInputState
    {
        MouseKeyboard,
        Xbox,
        PS4
    }

    public gameInputState in_State = gameInputState.MouseKeyboard;

    private void Start()
    {
        gameController = GameController.Instance;
        gameController.inputReader = this;
    }

    private void Update()
    {
        //Debug.Log(isMouseKeyboard());
        //isMouseKeyboard();
        isPS4Input();
        isXboxInput();
        OnInput();
    }

    void OnInput()
    {
        if (isXboxInput())
        {
            if(in_State != gameInputState.Xbox)
            {
                in_State = gameInputState.Xbox;
                Debug.Log("Xbox controller is active");
            }
        }
        else if (isPS4Input())
        {
            if(in_State != gameInputState.PS4)
            {
                in_State = gameInputState.PS4;
                Debug.Log("PS4 controller is active");
            }
        }
        else
        {
            if(in_State != gameInputState.MouseKeyboard)
            {
                in_State = gameInputState.MouseKeyboard;
                Debug.Log("Mouse & Keyboard are active");
            }
        }
    }

    public gameInputState GetInputState()
    {
        return in_State;
    }

    private bool isMouseKeyboard()
    {
        //input from mouse and keyboard buttons
        
        //if(Event.current.isKey || Event.current.isMouse)
        //{
        //    Debug.Log("Not getting through?");
        //    return true;
        //}
        //mouse movement
        if(Input.GetAxis("CamX PC") != 0.0f || Input.GetAxis("CamY PC") != 0.0f)
        {
            return true;
        }
        return false;
    }

    private bool isXboxInput()
    {
        //joystick buttons
        string[] controllers = Input.GetJoystickNames();

        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i].Contains("Xbox"))
            {
                Debug.Log(controllers[i]);
                return true;
            }
        }
        return false;
    }

    private bool isPS4Input()
    {

        string[] controllers = Input.GetJoystickNames();

        for (int i = 0; i < controllers.Length; i++)
        {
            if (controllers[i].Contains("Wireless") || controllers[i].Contains("Sony"))
            {
                Debug.Log(controllers[i]);
                return true;
            }
        }
        return false;
    }

    //Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //Update is called once per frame
    //void Update()
    //{
       
    //}
}
