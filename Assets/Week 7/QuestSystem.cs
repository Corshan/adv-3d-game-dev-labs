using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    int currentStage;
    List<string> actions, targets, xps;
    List<bool> objectivesAchieved;
    string stageTitle, stageDescription, stageObjectives, startingPointForPlayer;
    public enum PossibleActions { do_nothing = 0, talk_to = 1, aquire = 2, destroy_one = 3, enter_a_place_called = 4 }
    List<PossibleActions> actionsForQuest;
    GameObject stagePanel, stageTitleText, stageDesriptionText, stageObjectivesText;

    public GameObject player;
    bool panelDisplay = true;

    public void MovePlayerToStartingPoint()
    {
        GameObject p = Instantiate(player);
        p.name = "Player";
        p.transform.position = GameObject.Find("startingPoint").transform.position;
    }

    public void LoadQuest()
    {
        print("Loading Quest");
        TextAsset textAsset = (TextAsset)Resources.Load("quest");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        stageObjectives = "For this stage, you need to: \n";

        foreach (XmlNode stage in doc.SelectNodes("quest/stage"))
        {
            if (stage.Attributes.GetNamedItem("id").Value == "" + currentStage)
            {
                stageTitle = stage.Attributes.GetNamedItem("name").Value;
                stageDescription = stage.Attributes.GetNamedItem("description").Value;


                foreach (XmlNode results in stage)
                {
                    print("For this stage, you need to: \n");

                    foreach (XmlNode result in results)
                    {
                        string action = result.Attributes.GetNamedItem("action").Value;
                        string target = result.Attributes.GetNamedItem("target").Value;
                        string xp = result.Attributes.GetNamedItem("xp").Value;

                        actions.Add(action);
                        targets.Add(target);
                        xps.Add(xp);
                        objectivesAchieved.Add(false);

                        print(action + " " + target + " [" + xp + "XP]");
                        stageObjectives += "\n ->" + action + " " + target + " [" + xp + "XP]";
                    }
                }
            }
        }
    }

    public void Init()
    {
        stageTitleText = GameObject.Find("stageTitle");
        stageObjectivesText = GameObject.Find("stageObjectives");
        stageDesriptionText = GameObject.Find("stageDescription");
        stagePanel = GameObject.Find("stagePanel");

        actions = new List<string>();
        targets = new List<string>();
        xps = new List<string>();
        objectivesAchieved = new List<bool>();
        actionsForQuest = new List<PossibleActions>();
        LoadQuest();
    }

    void DisplayQuestInfo()
    {
        stageTitleText.GetComponent<TextMeshProUGUI>().text = stageTitle;
        stageDesriptionText.GetComponent<TextMeshProUGUI>().text = stageDescription;
        stageObjectivesText.GetComponent<TextMeshProUGUI>().text = stageObjectives + "\n Press H to Hide/Display this information";
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        DisplayQuestInfo();
        MovePlayerToStartingPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            panelDisplay = !panelDisplay;
            stagePanel.SetActive(panelDisplay);
        }
    }

    public void Notify(PossibleActions action, string target)
    {
        print("Notified " + action +" " + target);
    }
}
