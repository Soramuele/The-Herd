using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Represents a quest definition.
/// Stored as a ScriptableObject for configuration.
/// </summary>
[CreateAssetMenu(menuName = "Quest/Quests")]
public class Quest : ScriptableObject
{
    /// <summary>
    /// Unique identifier for the quest.
    /// </summary>
    //[SerializeField]
    public string _questID;
    
    /// <summary>
    /// Public accessor for quest ID.
    /// </summary>
    public string QuestID => _questID;
    
    /// <summary>
    /// Display name of the quest.
    /// </summary>
    public string QuestName;
    
    /// <summary>
    /// Quest description text.
    /// </summary>
    public string Description;
    
    /// <summary>
    /// List of all quest objectives required to complete the quest.
    /// </summary>
    public List<QuestObjective> Objectives;
    
}


/// <summary>
/// Represents a single objective in the quest.
/// </summary>
[Serializable]
public class QuestObjective
{
    /// <summary>
    /// ObjectiveID, use the corresponding NPC/Item/Area ID here.
    /// </summary>
    public string ObjectiveID;
    
    /// <summary>
    /// Objective description.
    /// </summary>
    public string Description;
    
    /// <summary>
    /// Type of the Objective
    /// </summary>
    public ObjectiveType Type;
    
    /// <summary>
    /// Required amount to complete the objective.
    /// </summary>
    public int RequiredAmount;
    
    /// <summary>
    /// Current amount the player does have.
    /// </summary>
    public int CurrentAmount;

    
    /// <summary>
    /// Returns true if the current amount is equal to required amount.
    /// </summary>
    public bool IsCompleted => CurrentAmount >= RequiredAmount;

    public void AddProgress(int amount)
    {
        CurrentAmount += CurrentAmount + amount;
    }
}


/// <summary>
/// Represents the type of objective.
/// </summary>
public enum ObjectiveType{ CollectItem, DefeatEnemy, ReachLocation, TalkNPC, Custom}


/// <summary>
/// Tracks the progress of a quest.
/// </summary>
[Serializable]
public class QuestProgress
{
    /// <summary>
    /// Quest to track.
    /// </summary>
    public Quest Quest;
    
    /// <summary>
    /// Copied objective list
    /// </summary>
    public List<QuestObjective> Objectives;

    /// <summary>
    /// Creates a new QuestProgress instance by cloning the objectives from the quest.
    /// </summary>
    /// <param name="quest">Quest to track.</param>
    public QuestProgress(Quest quest)
    {
        this.Quest = quest;
        Objectives = new List<QuestObjective>();

        foreach (var obj in quest.Objectives)
        {
            Objectives.Add(new QuestObjective
            {
                ObjectiveID = obj.ObjectiveID,
                Description = obj.Description,
                Type = obj.Type,
                RequiredAmount = obj.RequiredAmount,
                CurrentAmount = obj.CurrentAmount
            });
        }
    }

    /// <summary>
    /// Returns true if all quest objectives are completed.
    /// </summary>
    public bool IsCompleted => Objectives.TrueForAll(o => o.IsCompleted);
}