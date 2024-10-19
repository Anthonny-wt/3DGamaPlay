using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelInteraction : MonoBehaviour
{
    public GameObject canvas; // Canvas para mostrar el c�digo
    public TMP_InputField inputFieldGreen;  // Cambiar a TMP_InputField
    public TMP_InputField inputFieldOrange; // Cambiar a TMP_InputField
    public TMP_InputField inputFieldYellow; // Cambiar a TMP_InputField
    public Animator doorAnimator; // La puerta con la animaci�n de abrir
    private bool isCodeCorrect = false;
    private bool doorOpened = false; // A�adimos esta variable para controlar el estado de la puerta

    void Start()
    {
        // Al inicio, desactivar el canvas
        canvas.SetActive(false);
    }

    // Llamar este m�todo cuando el panel sea presionado
    public void OnPanelClicked()
    {
        // Solo mostrar el canvas si la puerta no ha sido abierta a�n
        if (!doorOpened)
        {
            canvas.SetActive(true); // Mostrar el canvas
            PositionCanvasInFrontOfPlayer(); // Opcional, ver m�s abajo
        }
    }

    // Verificar el c�digo ingresado por el jugador
    public void CheckCode()
    {
        // Variables para almacenar los valores convertidos
        int greenValue, orangeValue, yellowValue;

        // Intentar convertir el texto a entero
        bool greenValid = int.TryParse(inputFieldGreen.text, out greenValue);
        bool orangeValid = int.TryParse(inputFieldOrange.text, out orangeValue);
        bool yellowValid = int.TryParse(inputFieldYellow.text, out yellowValue);

        // Verificar si todos los campos de entrada tienen un valor v�lido
        if (greenValid && orangeValid && yellowValid)
        {
            // Verificar si el c�digo es correcto
            if (greenValue == 3 && orangeValue == 5 && yellowValue == 8)
            {
                isCodeCorrect = true;
                canvas.SetActive(false); // Esconder el canvas al ingresar el c�digo correcto
                OpenDoor(); // Llamar a la funci�n para abrir la puerta
            }
            else
            {
                Debug.Log("C�digo incorrecto");
                // C�digo incorrecto, esconder el canvas pero permitir que se pueda volver a abrir
                canvas.SetActive(false); // Esconder el canvas
            }
        }
        else
        {
            Debug.Log("Uno o m�s campos contienen valores no v�lidos");
            // C�digo inv�lido, esconder el canvas pero permitir que se pueda volver a abrir
            canvas.SetActive(false); // Esconder el canvas
        }
    }

    // M�todo para abrir la puerta solo si no ha sido abierta
    private void OpenDoor()
    {
        if (!doorOpened && isCodeCorrect) // Solo abrir si no ha sido abierta y el c�digo es correcto
        {
            doorAnimator.SetBool("isOpen", true); // Activar la animaci�n de apertura de la puerta
            doorOpened = true; // Marcar la puerta como abierta
        }
    }

    // Opcional: Colocar el canvas frente al jugador
    private void PositionCanvasInFrontOfPlayer()
    {
        Camera playerCamera = Camera.main; // O la c�mara del jugador
        canvas.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 2f; // Ajustar distancia frente al jugador
        canvas.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);
    }
}


