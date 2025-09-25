using UnityEngine;

public class QuestStarter : MonoBehaviour
{
    public Quest myQuest;
    public QuestManager Manager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager.Initialize();
        if (myQuest != null)
        {
            QuestManager.Instance.StartQuest(myQuest);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
