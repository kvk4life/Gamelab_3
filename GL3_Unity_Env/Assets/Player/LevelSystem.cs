using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSystem : MonoBehaviour

{
    public int level;
    public int currXP;
    public int expToLevel;
    public float xpMultiplier;
    public PlayerStats playerstats;
    public Image currXPImage;
    public Text percentage;
    public float totalXP;
    public AudioClip levelUp;
    public AudioSource source;
    // Use this for initialization
    void Start()
    {
        UpdateBars();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExpierence(int xpAmount)
    {
        totalXP+=xpAmount;
        currXP+=xpAmount;
        if(currXP>=expToLevel)
        {
            LevelUp(currXP-expToLevel);
        }
        UpdateBars();
    }

    void LevelUp(int excessXp)
    {
        if(source.isPlaying==false)
        {
            source.PlayOneShot(levelUp);
        }
        level++;
        playerstats.level=level;
        playerstats.UpdatePlayerStats();
        currXP=0;
        expToLevel=Mathf.RoundToInt(expToLevel*xpMultiplier);
        AddExpierence(excessXp); 
    }

    void UpdateBars()
    {
        float percentageTemp = (float) currXP/expToLevel;
        currXPImage.fillAmount = percentageTemp;
        percentageTemp=percentageTemp*100f;
        percentage.text=percentageTemp.ToString("F1");
    }
}
