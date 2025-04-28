using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
{
 
    private bool isInRange;


    private bool dialogueStarted;


    [Header("Objects")]
   
    public GameObject dialoguePanel;
   
    // Variable para almacenar el texto visual.
    public TextMeshProUGUI dialogueText;

    // Variable para almacenar los textos del diálogo.
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    // Recorre el arreglo de los textos.
    private int lineIndex;

    // Tiempo de espera para que se escriban los caracteres.
    private float charTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueStarted)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }

        }

    }

    // Método para iniciar el diálogo.
    private void StartDialogue()
    {
        PlayerMovement.instance.canMove = false;
        dialogueStarted = true;
        dialoguePanel.SetActive(true);

        
        lineIndex = 0;

        StartCoroutine(TextTyping());
    }

    // Corutina para que el texto se escriba como máquina de escribir.
    private IEnumerator TextTyping()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(charTime);
        }
    }

    // Permite que la siguiente línea de diálogo se muestre.
    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(TextTyping());
        }
        else
        {
            PlayerMovement.instance.canMove = true;
            dialogueStarted = false;
            dialoguePanel.SetActive(false);

        }
    }

    // Se activa el diálgo cuando el jugador ingresa al collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;

           
        }
    }

    // Se desactiva el diálgo cuando el jugador sale del collider.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;

            
        }
    }



}
