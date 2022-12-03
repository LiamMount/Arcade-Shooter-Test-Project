using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    GameManager gm;

    public GameObject dialogueBoxUI;

    public TextMeshProUGUI speakerName, dialogue;
    public Image speakerSprite;

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;

    public Animator anim;

    private Coroutine typing;

    void Awake()
    {
        dialogueBoxUI.SetActive(true);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gm.isPaused && gm.isCutscene)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ReadNext();
            }
        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.gm.isCutscene = true;

        instance.anim.SetBool("isOpen", true);

        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";

        instance.ReadNext();
    }

    public void ReadNext()
    {
        if (currentIndex > currentConvo.GetLength() && typing == null) // typing check is new
        {
            instance.anim.SetBool("isOpen", false);
            gm.isCutscene = false;
            return;
        }

        //speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker; // Old
        //speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speakerSprite; // Old

        if (typing == null)
        {
            speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker; // New
            speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speakerSprite; // New

            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
            currentIndex += 1; // New
        }
        else if (typing != null)
        {
            instance.StopCoroutine(typing);
            typing = null;
            dialogue.text = currentConvo.GetLineByIndex(currentIndex - 1).dialogue; // New
            //typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue)); // Old
        }

        //currentIndex += 1; // Old
    }

    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";
        bool complete = false;
        int index = 0;

        //New start
        string startTag = string.Empty;
        string endTag = string.Empty;
        //New end

        if (currentIndex == 0)
        {
            yield return new WaitForSeconds(0.35f);
        }

        while (!complete)
        {

            //New start
            char c = text[index];

            /*
            if (c != ' ')
            {
                //Sound?
            }
            */

            //Rich Text
            if (c == '<')
            {
                if (string.IsNullOrEmpty(startTag))
                {
                    int indexNow = index;

                    for (int j = indexNow; j < text.Length; j += 1)
                    {
                        startTag += text[j].ToString();

                        if (text[j] == '>')
                        {
                            indexNow = j + 1;

                            index = indexNow;

                            for (int k = indexNow; k < text.Length; k++)
                            {
                                char next = text[k];

                                if (next == '<')
                                {
                                    break;
                                }
                                else
                                {
                                    indexNow += 1;
                                }
                            }
                            break;
                        }
                    }

                    for (int j = indexNow; j < text.Length; j += 1)
                    {
                        endTag += text[j].ToString();

                        if (text[j] == '>')
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = index; j < text.Length; j++)
                    {
                        if (text[j] == '>')
                        {
                            index = j + 1;
                            break;
                        }
                    }

                    startTag = string.Empty;
                    endTag = string.Empty;
                }

                continue;
            }

            dialogue.text += string.Format("{0}{1}{2}", startTag, c, endTag);

            index += 1;
            yield return new WaitForSeconds(0.04f);
            //New end

            if (index == text.Length)
            {
                complete = true;
            }
        }

        typing = null;
    }
}
