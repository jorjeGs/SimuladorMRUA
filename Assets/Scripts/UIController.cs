using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class canvasController : MonoBehaviour
{
    public TMP_InputField velocidad;
    public TMP_InputField aceleracion;
    public TMP_InputField tiempo;
    public TMP_Text salida;

    public void Simular()
    {
        salida.text = "La V sera: " + velocidad.text + " m/s, con una A de " + aceleracion.text + " m/s^2, por " + tiempo.text + " segundos.";
    }
}
