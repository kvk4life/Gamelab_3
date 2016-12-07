using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public enum QuestType { Kill, Collect, Timed};

public abstract class Quest : MonoBehaviour
{ 
    public abstract void StartQuest();
    public abstract void ProgressQuest();
    public abstract void CompleteQuest();
    public QuestGiver startNPC;
    public QuestGiver endNPC;
    public int xpReward;
    public Item itemReward;
}
