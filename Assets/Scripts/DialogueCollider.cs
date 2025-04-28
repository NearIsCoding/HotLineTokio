
using System.Collections;

using TMPro;
using UnityEngine;

public class DialogueCollider : MonoBehaviour
{

    public SpriteRenderer playerSpriteRenderer; // el sprite del Player principal
    public SpriteRenderer childSpriteRenderer;  // el sprite de su hijo

    private bool isInRange;
    private bool dialogueStarted;

    [Header("Animators")]
    public Animator Cinematicas;
    public Animator CinematicBar;
    public Animator CharFaces;

    [Header("Dialogue Panels")]
    public GameObject dialoguePanel1;
    public GameObject dialoguePanel2;

    [Header("Dialogue Texts")]
    public TextMeshProUGUI dialogueText1;
    public TextMeshProUGUI dialogueText2;

    [Header("Dialogue Lines")]
    [SerializeField, TextArea(4, 6)]
    private string[] dialogueLines =
    {
        "Cinematica1",
        "Cinematica2",
        "Cinematica3",
        "Cinematica4",
        "p1:TakeshiOn",
        "p1:TakeshiLoop",
        "p1:EnemyOn",
        "p1:EnemyLoop",
        "p1:EnemyTakeshiOff"
    };

    private int lineIndex;
    private float charTime = 0.05f;

    void Start()
    {
        isInRange = false;
        dialoguePanel1.SetActive(false);
        dialoguePanel2.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            string currentLine = dialogueLines[lineIndex];

            if (currentLine.Contains("Cinematica1"))
            {
                StartCoroutine(CinematicaAnim());
            }

            if (currentLine.Contains("Cinematica2"))
            {
                StartCoroutine(CinematicaAnim2());
            }

            if (currentLine.Contains("Cinematica3"))
            {
                StartCoroutine(CinematicaAnim3());
            }


            if (currentLine.Contains("Cinematica4"))
            {
                StartCoroutine(CinematicaAnim4());
            }

            if (currentLine.Contains("TakeshiOn"))
            {
                StartCoroutine(TakeshiEntrance());
                

            }




             if (currentLine.Contains("EnemyOn"))
            {
                StartCoroutine(EnemyEntrance());
            }


             if (currentLine.Contains("TakeshiLoop"))
            {
                StartCoroutine(TakeshiLoop());
            }

             if (currentLine.Contains("EnemyLoop"))
            {
                StartCoroutine(EnemyLoop());
            }

             if (currentLine.Contains("EnemyTakeshiOff"))
            {
                StartCoroutine(EnemyTakeshiOff());
            }



            else
            {
                NextDialogueLine();
            }
        }
    }

    void SetPlayerTransparency(float alpha)
    {
        if (playerSpriteRenderer != null)
        {
            Color color = playerSpriteRenderer.color;
            color.a = alpha;
            playerSpriteRenderer.color = color;
        }

        if (childSpriteRenderer != null)
        {
            Color color = childSpriteRenderer.color;
            color.a = alpha;
            childSpriteRenderer.color = color;
        }
    }


    IEnumerator TakeshiEntrance()
    {
        CharFaces.SetBool("TakeshiOn", true);
       yield return new WaitForSeconds(1f);
       
        
    }

    IEnumerator EnemyEntrance()
    {
        CharFaces.SetBool("TakeshiOn", false);
        CharFaces.SetBool("EnemyOn", true);
        yield break;
    
    }

    IEnumerator TakeshiLoop()
    {
        CharFaces.SetBool("TakeshiOn", true);
        CharFaces.SetBool("EnemyLoop", false);
        CharFaces.SetBool("TakeshiLoop", true);
        yield break;


    }

    IEnumerator EnemyLoop()
    {
        CharFaces.SetBool("EnemyOn", true);
        CharFaces.SetBool("EnemyLoop", true);
        CharFaces.SetBool("TakeshiLoop", false);
        yield break;


    }


    IEnumerator EnemyTakeshiOff()
    {
        CharFaces.SetBool("EnemyOn", false);
        CharFaces.SetBool("TakeshiOn", false);
        CharFaces.SetBool("TakeshiLoop", false);
        CharFaces.SetBool("EnemyLoop", false);
        CharFaces.SetBool("EnemyTakeshiOff", true);
        yield return new WaitForSeconds(0.1f);
        CharFaces.SetBool("EnemyTakeshiOff", false);
        NextDialogueLine();
        yield break;


    }



    IEnumerator CinematicaAnim()
    {
        


        dialoguePanel1.SetActive(false);
        dialoguePanel2.SetActive(false);
        Cinematicas.SetBool("Cinematica1", true);
        SetPlayerTransparency(0f); // o el valor que quieras, por ejemplo 0.5 de transparencia

        yield return new WaitForSeconds(3);
        NextDialogueLine();

    }
    
    
    IEnumerator CinematicaAnim2()
    {
        dialoguePanel1.SetActive(false);
        dialoguePanel2.SetActive(false);
        Cinematicas.SetBool("Cinematica2", true);

        yield return new WaitForSeconds(1);


        NextDialogueLine();
    }


    IEnumerator CinematicaAnim3()
    {
        dialoguePanel1.SetActive(false);
        dialoguePanel2.SetActive(false);
        Cinematicas.SetBool("Cinematica3", true);

        yield return new WaitForSeconds(4);

        
        NextDialogueLine();
    }


    IEnumerator CinematicaAnim4()
    {
        dialoguePanel1.SetActive(false);
        dialoguePanel2.SetActive(false);
        Cinematicas.SetBool("Cinematica4", true);

        yield return new WaitForSeconds(2);
        


        Cinematicas.SetBool("Cinematica1", false);
        Cinematicas.SetBool("Cinematica2", false);
        Cinematicas.SetBool("Cinematica3", false);
        Cinematicas.SetBool("Cinematica4", false);

        NextDialogueLine();
        SetPlayerTransparency(1f); // volver a opaco normal
    }

    private void StartDialogue()
    {

        CinematicBar.SetBool("CinematicBars", true);
        CinematicBar.SetBool("BarsOff", false);
        PlayerMovement.instance.canMove = false;
        PlayerMovement.instance.animator.SetBool("isWalking", false);
        PlayerMovement.instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        dialogueStarted = true;
        lineIndex = 0;
        StartCoroutine(TextTyping());

    }





    private IEnumerator TextTyping()
    {
        dialogueText1.text = string.Empty;
        dialogueText2.text = string.Empty;

        string line = dialogueLines[lineIndex];

        // Palabras clave que activan acciones y no muestran texto
        if (line.Contains("TakeshiOn"))
        {
            StartCoroutine(TakeshiEntrance());
            yield return new WaitForSeconds(1f); // Espera opcional
            NextDialogueLine();
            yield break;
        }
        else if (line.Contains("TakeshiLoop"))
        {
            StartCoroutine(TakeshiLoop());
            yield return new WaitForSeconds(1f);
            NextDialogueLine();
            yield break;
        }
        else if (line.Contains("EnemyOn"))
        {
            StartCoroutine(EnemyEntrance());
            yield return new WaitForSeconds(1f);
            NextDialogueLine();
            yield break;
        }
        else if (line.Contains("EnemyLoop"))
        {
            StartCoroutine(EnemyLoop());
            yield return new WaitForSeconds(1f);
            NextDialogueLine();
            yield break;
        }
        else if (line.Contains("EnemyTakeshiOff"))
        {
            StartCoroutine(EnemyTakeshiOff());
            yield return new WaitForSeconds(1f);
            yield break; // Esta ya llama NextDialogueLine dentro del coroutine
        }
        else if (line.Contains("Cinematica1"))
        {
            StartCoroutine(CinematicaAnim());
            yield break;
        }
        else if (line.Contains("Cinematica2"))
        {
            StartCoroutine(CinematicaAnim2());
            yield break;
        }

        else if (line.Contains("Cinematica3"))
        {
            StartCoroutine(CinematicaAnim3());
            yield break;
        }

        else if (line.Contains("Cinematica4"))
        {
            StartCoroutine(CinematicaAnim4());
            yield break;
        }


        // Mostrar texto normal
        bool usePanel1 = true;

        if (line.StartsWith("p1:"))
        {
            line = line.Replace("p1:", "");
            usePanel1 = true;
        }
        else if (line.StartsWith("p2:"))
        {
            line = line.Replace("p2:", "");
            usePanel1 = false;
        }

        GameObject activePanel = usePanel1 ? dialoguePanel1 : dialoguePanel2;
        GameObject inactivePanel = usePanel1 ? dialoguePanel2 : dialoguePanel1;
        TextMeshProUGUI activeText = usePanel1 ? dialogueText1 : dialogueText2;

        activePanel.SetActive(true);
        inactivePanel.SetActive(false);

        foreach (char ch in line)
        {
            activeText.text += ch;
            yield return new WaitForSeconds(charTime);
        }
    }



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

            dialoguePanel1.SetActive(false);
            dialoguePanel2.SetActive(false);

            PlayerMovement.instance.animator.SetBool("isWalking", true);
            StopAllCoroutines();

            CinematicBar.SetBool("BarsOff", true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            if (!dialogueStarted)
            {
                StartDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
