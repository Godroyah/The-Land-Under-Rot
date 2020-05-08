using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetEventInputs : MonoBehaviour
{
    public StandaloneInputModule thisModule;
    GameController gameController;
    InputReader.gameInputState currentInputState;

    //private void Start()
    //{
    //    gameController = GameController.Instance;
    //    thisModule = GetComponent<StandaloneInputModule>();

    //    thisModule.horizontalAxis = gameController.horizontalInput;
    //    thisModule.verticalAxis = gameController.verticalInput;
    //    thisModule.submitButton = gameController.submitInput;
    //    thisModule.cancelButton = gameController.cancelInput;

    //    currentInputState = gameController.inputReader.in_State;
    //}

    // Update is called once per frame
    void Update()
    {
        if(gameController == null)
        {
            gameController = GameController.Instance;
        }
        if(thisModule == null)
        {
            thisModule = GetComponent<StandaloneInputModule>();
        }
        if (currentInputState != gameController.inputReader.in_State)
        {
            thisModule.horizontalAxis = gameController.horizontalInput;
            thisModule.verticalAxis = gameController.verticalInput;
            thisModule.submitButton = gameController.submitInput;
            thisModule.cancelButton = gameController.cancelInput;

            currentInputState = gameController.inputReader.in_State;
        }
    }
}
