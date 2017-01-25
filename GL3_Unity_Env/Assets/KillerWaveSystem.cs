using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillerWaveSystem : MonoBehaviour
{
    public int currentRound;
    public Text roundText;
    public Animator anim;
    public AudioClip begin, end;
    public AudioSource source;
    public int enemiesToSpawn;
    public int maxSimultaniosSpawn;
    private int currentAlive;
    private int tillNextRound;
    bool roundActive;
    bool maySpawn;
    public Transform[] spawnLocs;
    public GameObject enemy;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("FirstRound");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Y"))
        {
            StartCoroutine("ChangeRound");
        }
        if (tillNextRound == 0 && currentAlive==0 && roundActive==true)
        {
            StartCoroutine("ChangeRound");
        }
        if (roundActive)
        {
            if (maySpawn && tillNextRound > 0 && currentAlive < maxSimultaniosSpawn)
            {
                SpawnOne();
            }
        }
    }

    IEnumerator SlightDelay()
    {
        maySpawn = false;
        yield return new WaitForSeconds(Random.Range(0.8f, 1.8f));
        maySpawn = true;
    }

    public void EnemyKilled()
    {
        currentAlive--;
    }

    void SpawnOne()
    {
        if(currentRound>5)
        {
            if(Random.Range(0,100)<50)
            {
                enemy.GetComponent<DemonMakeShiftBs>().run=true;
            }
            else
            {
                enemy.GetComponent<DemonMakeShiftBs>().run = false;
            }
        if(currentRound>10)
            {
                enemy.GetComponent<DemonMakeShiftBs>().run = true;
            }
        }
        int pickSpawn = Random.Range(0,spawnLocs.Length);
        enemy.GetComponent<DemonRoundSystem>().wave=this;
        Instantiate(enemy,spawnLocs[pickSpawn].position,spawnLocs[pickSpawn].rotation);
        StartCoroutine("SlightDelay");
        currentAlive++;
        tillNextRound--;
    }

    IEnumerator FirstRound()
    {
        tillNextRound = enemiesToSpawn;
        anim.SetTrigger("Begin");
        currentRound = 1;
        roundText.text = currentRound.ToString();
        source.PlayOneShot(begin);
        yield return new WaitForSeconds(4f);
        roundActive = true;
        maySpawn=true;
    }

    IEnumerator ChangeRound()
    {
        roundActive = false;
        source.PlayOneShot(end);
        anim.SetTrigger("End");
        yield return new WaitForSeconds(end.length);
        maxSimultaniosSpawn = Mathf.RoundToInt(maxSimultaniosSpawn * 1.03f);
        enemiesToSpawn = Mathf.RoundToInt(enemiesToSpawn * 1.5f);
        currentRound++;
        roundText.text = currentRound.ToString();
        source.PlayOneShot(begin);
        anim.SetTrigger("Begin");
        yield return new WaitForSeconds(begin.length - 2f);
        roundActive = true;
    }
}
