using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueCollider : MonoBehaviour
{
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
        "p1:Cinematica1",
        "p1:Cinematica2",
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

            if (currentLine.Contains("p1:Cinematica1"))
            {
                StartCoroutine(CinematicaAnim());
            }

            

            
            else if (currentLine.Contains("TakeshiOn"))
            {
                StartCoroutine(TakeshiEntrance());
            }
            else
            {
                NextDialogueLine();
            }
        }
    }

    IEnumerator TakeshiEntrance()
    {
        CharFaces.SetBool("TakeshiOn", true);
        yield return new WaitForSeconds(2);
    }

    IEnumerator CinematicaAnim()
    {
        dialoguePanel1.SetActive(false);
        dialoguePanel2.SetActive(false);
        Cinematicas.SetBool("Cinematica1", true);

        yield return new WaitForSeconds(2);

        Cinematicas.SetBool("Cinematica1", false);
        NextDialogueLine();
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


        if (line.Contains("p1:Cinematica1"))
        {
            StartCoroutine(CinematicaAnim());
            yield break;
        }

        if (line.Contains("p1:TakeshiOn"))
        {
            CharFaces.SetBool("TakeshiOn", true);
            
            yield break;

        }

        if (line.Contains("p1:EnemyOn"))
        {
            CharFaces.SetBool("TakeshiOn", false);
            CharFaces.SetBool("EnemyOn", true);


            yield break;

        }

        if (line.Contains("p1:TakeshiLoop"))
        {
            CharFaces.SetBool("TakeshiOn", true);
            CharFaces.SetBool("EnemyLoop", false);
            CharFaces.SetBool("TakeshiLoop", true);


            yield break;

        }

        if (line.Contains("p1:EnemyLoop"))
        {
            CharFaces.SetBool("EnemyOn", true);
            CharFaces.SetBool("EnemyLoop", true);
            CharFaces.SetBool("TakeshiLoop", false);


            yield break;

        } 
        
        
        if (line.Contains("p1: EnemyTakeshiOff"))
        {
            CharFaces.SetBool("TakeshiOn", false);
            CharFaces.SetBool("EnemyOn", false);
            CharFaces.SetBool("EnemyLoop", false);
            CharFaces.SetBool("TakeshiLoop", false);


            CharFaces.SetBool("p1:EnemyTakeshiOff", true);




            yield break;

        }





    

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
