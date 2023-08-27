using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MRUAMovement : MonoBehaviour
{
    public TMP_InputField initialVelocityInput;
    public TMP_InputField accelerationInput;
    public TMP_InputField timeInput;
    public Transform characterTransform;
    public Button simulateButton;
    public Button pauseButton;
    public Button resetButton;
    public TMP_Text outputDistancia;
    public TMP_Text outputTiempo;
    public TMP_Text outputVelocidad;

    private float initialVelocity;
    private float acceleration;
    private float time;
    private bool isSimulating = false;
    private bool isPaused = false;
    private float simulationTimer = 0f;
    private float distancia = 0f;
    private float velocidad = 0f;

    private Vector3 initialPosition;

    private Rigidbody2D rigidBody;
    private Animator animator;

    private void Start()
    {
        initialVelocityInput.onEndEdit.AddListener(UpdateInitialVelocity);
        accelerationInput.onEndEdit.AddListener(UpdateAcceleration);
        timeInput.onEndEdit.AddListener(UpdateTime);
        simulateButton.onClick.AddListener(StartSimulation);
        pauseButton.onClick.AddListener(TogglePause);
        resetButton.onClick.AddListener(ResetSimulation);


        initialPosition = characterTransform.position;
        rigidBody= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
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
            distancia = 0f;
            velocidad = 0f;
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
        distancia = 0f;
        velocidad= 0f;
        characterTransform.position = initialPosition;
        outputDistancia.text = "";
        outputTiempo.text = "";
        outputVelocidad.text = "";
        changeDirection(1);
        animator.SetFloat("Speed", Math.Abs(velocidad));
    }

    private void changeDirection(int sentido)
    {
        transform.localScale = new Vector2(sentido, transform.localScale.y);
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

                // Sumar distancia para impresion
                distancia = displacement;

                // Calcular la velocidad 
                velocidad = initialVelocity + acceleration * simulationTimer;

                //Controlar animator
                animator.SetFloat("Speed", Math.Abs(velocidad));

                // Actualizar la posición horizontal del personaje
                Vector3 newPosition = characterTransform.position;
                newPosition.x = displacement;
                characterTransform.position = newPosition;

                //Mostrar en pantalla la distancia recorrida
                outputDistancia.text = $"Distancia: {distancia:F2} m";
                outputTiempo.text = "Tiempo: " + simulationTimer.ToString("0.##") + 's';
                outputVelocidad.text = $"Velocidad: {velocidad:F2} m/s";

                if(displacement < 0 && ((int)characterTransform.localScale.x) == (1))
                {
                    changeDirection(-1);
                }
            }
            else
            {
                isSimulating = false;
            }
        }
    }
}