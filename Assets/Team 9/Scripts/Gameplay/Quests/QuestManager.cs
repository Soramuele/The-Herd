using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.Events;
using Unity.VisualScripting;


/// <summary>
/// Manager class to handle active quests and updates
/// </summary>
public class QuestManager : MonoBehaviour
{
    
    /// <summary>
    /// Singleton instance of the QuestManager.
    /// </summary>
    public static QuestManager Instance { get; private set; }

    /// <summary>
    /// List of currently active quests.
    /// </summary>
    private List<QuestProgress> _activeQuests = new List<QuestProgress>();

    /// <summary>
    /// List of completed quests.
    /// </summary>
    private List<QuestProgress> _completedQuests = new List<QuestProgress>();

    
    /// <summary>
    /// Initialise the singleton instance.
    /// </summary>
    private void Awake()
    {
        
    }

    public void Initialize()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    /// <summary>
    /// Starts a new quest and adds it to active quest list
    /// </summary>
    /// <param name="quest">Quest ScriptableObject to start</param>
    public void StartQuest(Quest quest)
    {
        var progress = new QuestProgress(quest);
        _activeQuests.Add(progress);
        
        //TODO: UPDATE QUEST UI / CREATE QUEST UI
    }

    
    /// <summary>
    /// Completes progress on a specific objective within a quest.
    /// </summary>
    /// <param name="questID">The ID of the quest to update.</param>
    /// <param name="objectiveID">The ID of the objective to complete.</param>
    /// <param name="amount">The amount to increment the objective's progress (default 1).</param>
    public void CompleteObjective(string questID, string objectiveID, int amount = 1)
    {
        foreach (var quest in _activeQuests)
        {
            if (quest.Quest.QuestID != questID) continue;

            var obj = quest.Objectives.FirstOrDefault(o => o.ObjectiveID == objectiveID);
           
            if (obj == null)
            {
                Debug.LogWarning("QUEST MANAGER: Quest Objective is null!");
                return;
            }
            
            obj.AddProgress(amount);
            Debug.Log($"Current Progress: {obj.CurrentAmount} / {obj.RequiredAmount}");
            if (quest.IsCompleted)
            {
                OnQuestCompleted(quest);
                Debug.Log("QUEST COMPLETE");
            }
            //TODO: QUEST UI UPDATE
        }
    }

    
    /// <summary>
    /// Retrieves a quest by its ID from active or completed quests.
    /// </summary>
    /// <param name="questID">The unique identifier of the quest.</param>
    /// <returns>
    /// The <see cref="QuestProgress"/> object if found; otherwise, null.
    /// </returns>
    public QuestProgress GetQuestProgressByID(string questID)
    {
        var questProg = _activeQuests.Find(q => q.Quest.QuestID == questID)
            ?? _completedQuests.Find(q => q.Quest.QuestID == questID);
        return questProg;
    }
    
    
    /// <summary>
    /// For testing CompleteObjective with UI-Buttons
    /// To be Removed
    /// </summary>
    /// <param name="objectiveID"></param>
    public void CompleteObjectiveString(string objectiveID)
    {
        CompleteObjective("TESTQUEST_001", objectiveID, 1);
    }

    
    /// <summary>
    /// Called when a quest is completed.
    /// Moves quest from active quests to completed quests.
    /// Handles completion logic.
    /// </summary>
    /// <param name="quest">The completed QuestProgress object</param>
    private void OnQuestCompleted(QuestProgress quest)
    {
        _activeQuests.Remove(quest);
        _completedQuests.Add(quest);
        
        Debug.Log($"Quest completed: {quest.Quest.QuestName}");
        //TODO: Get a reward????
    }
}
