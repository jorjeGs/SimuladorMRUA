using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MRUAMovement : MonoBehaviour
{
    public InputField initialVelocityInput;
    public InputField accelerationInput;
    public InputField timeInput;
    public Transform characterTransform;
    public Button simulateButton;
    public Button pauseButton;
    public Button resetButton;

    private float initialVelocity;
    private float acceleration;
    private float time;
    private bool isSimulating = false;
    private bool isPaused = false;
    private float simulationTimer = 0f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialVelocityInput.onEndEdit.AddListener(UpdateInitialVelocity);
        accelerationInput.onEndEdit.AddListener(UpdateAcceleration);
        timeInput.onEndEdit.AddListener(UpdateTime);
        simulateButton.onClick.AddListener(StartSimulation);
        pauseButton.onClick.AddListener(TogglePause);
        resetButton.onClick.AddListener(ResetSimulation);

        initialPosition = characterTransform.position;
    }

    private void UpdateInitialVelocity(string value)
    {
        float.TryParse(value, out initialVelocity);
    }

    private void UpdateAcceleration(string value)
    {
        float.TryParse(value, out acceleration);
    }

    private void UpdateTime(string value)
    {
        float.TryParse(value, out time);
    }

    private void StartSimulation()
    {
        if (!isSimulating)
        {
            isSimulating = true;
            simulationTimer = 0f;
            characterTransform.position = initialPosition;
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
    }

    private void ResetSimulation()
    {
        isSimulating = false;
        isPaused = false;
        simulationTimer = 0f;
        characterTransform.position = initialPosition;
    }

    private void Update()
    {
        if (isSimulating && !isPaused)
        {
            simulationTimer += Time.deltaTime;

            if (simulationTimer <= time)
            {
                // Calcular la posición del personaje usando la fórmula de MRUA
                float displacement = initialVelocity * simulationTimer + 0.5f * acceleration * simulationTimer * simulationTimer;

                // Actualizar la posición horizontal del personaje
                Vector3 newPosition = characterTransform.position;
                newPosition.x = displacement;
                characterTransform.position = newPosition;
            }
            else
            {
                isSimulating = false;
            }
        }
    }
}