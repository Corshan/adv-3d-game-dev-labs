using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;

public class DialogueSystem : MonoBehaviour
{
    string nameOfCharacter;
    Dialogue[] dialogues;
    int numberOfDialogues;
    int currentDialogueIndex;
    bool waitingForUserInput;
    bool dialogueIsActive;

    GameObject dialogbox, dialogPanel;

    // Start is called before the first frame update
    void Start()
    {
        dialogbox = GameObject.Find("dialogueBox");
        dialogPanel = GameObject.Find("dialoguePanel");
        GameObject.Find("dialogueImage").GetComponent<RawImage>().texture = Resources.Load<Texture2D>(gameObject.name);

        nameOfCharacter = gameObject.name;
        numberOfDialogues = CalculateNumDialogues();
        dialogues = new Dialogue[numberOfDialogues];

        LoadDialogues();
        StartDialogue();
    }

    public void StartDialogue()
    {
        waitingForUserInput = false;
        dialogueIsActive = true;
    }

    public int CalculateNumDialogues()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("dialogues");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        int dialogueIndex = 0;

        foreach (XmlNode character in doc.SelectNodes("dialogues/character"))
        {
            if (character.Attributes.GetNamedItem("name").Value == nameOfCharacter)
            {
                foreach (XmlNode dialogueFromXML in doc.SelectNodes("dialogues/character/dialogue"))
                {
                    dialogueIndex++;
                }
            }
        }

        return dialogueIndex;
    }

    public void LoadDialogues()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("dialogues");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        int dialogueIndex = 0;

        foreach (XmlNode character in doc.SelectNodes("dialogues/character"))
        {
            if (character.Attributes.GetNamedItem("name").Value == nameOfCharacter)
            {
                dialogueIndex = 0;
                foreach (XmlNode dialogueFromXML in doc.SelectNodes("dialogues/character/dialogue"))
                {
                    dialogues[dialogueIndex] = new Dialogue();
                    dialogues[dialogueIndex].message = dialogueFromXML.Attributes.GetNamedItem("content").Value;
                    int choiceIndex = 0;
                    dialogues[dialogueIndex].response = new string[2];
                    dialogues[dialogueIndex].targetForResponse = new int[2];

                    foreach (XmlNode choice in dialogueFromXML)
                    {
                        dialogues[dialogueIndex].response[choiceIndex] = choice.Attributes.GetNamedItem("content").Value;
                        dialogues[dialogueIndex].targetForResponse[choiceIndex] = int.Parse(choice.Attributes.GetNamedItem("target").Value);
                        choiceIndex++;
                    }

                    // print(dialogues[dialogueIndex].message);
                    // print("[A]" + dialogues[dialogueIndex].response[0]);
                    // print("[B]" + dialogues[dialogueIndex].response[1]);

                    dialogueIndex++;
                }
            }
        }
    }

    public void DisplayDialogue2()
    {
        string textToDisplay = "[" + gameObject.name + "] " + dialogues[currentDialogueIndex].message +
                                "\n[A] > " + dialogues[currentDialogueIndex].response[0] +
                                "\n[B] > " + dialogues[currentDialogueIndex].response[1];

        GameObject.Find("dialogueBox").GetComponent<TextMeshProUGUI>().text = textToDisplay;
    }

    public void DisplayDialogue1()
    {
        print(dialogues[currentDialogueIndex].message);
        print("[A]" + dialogues[currentDialogueIndex].response[0]);
        print("[B]" + dialogues[currentDialogueIndex].response[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueIsActive)
        {
            if (!waitingForUserInput)
            {
                if (currentDialogueIndex != -1)
                {
                    DisplayDialogue2();
                }
                else
                {
                    dialogueIsActive = false;
                    waitingForUserInput = false;
                    currentDialogueIndex = 0;
                }
                waitingForUserInput = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currentDialogueIndex = dialogues[currentDialogueIndex].targetForResponse[0];
                    waitingForUserInput = false;
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    currentDialogueIndex = dialogues[currentDialogueIndex].targetForResponse[1];
                    waitingForUserInput = false;
                }

            }

        }
    }
}
