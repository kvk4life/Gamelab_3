using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class Mining : MonoBehaviour
{
    public Inventory inventory;
    public RaycastHit hit;
    public bool isMining;
    public AudioSource selfAudio;
    public AudioClip mineSound;
    public Animator anim;
    public AnimationClip miner;
    public GameObject pickAxe;
    public GameObject backPanel;
    public GameObject mineParticle;
    public FirstPersonController fps;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if(Input.GetButtonDown("Use"))
        {
            if(Physics.Raycast(transform.position,transform.forward,out hit, 3f))
            {
                if(hit.transform.tag=="MiningNode")
                {
                    StartCoroutine("Mine", hit.transform.GetComponent<Ore>());
                }
            }
        }
    }

    IEnumerator Mine(Ore ore)
    {
        fps.enabled=false;
        backPanel.SetActive(true);
        StartCoroutine("AudioVisual", ore);
        isMining=true;
        while(ore.timeToMine.fillAmount<1)
        {
            ore.timeToMine.fillAmount+=0.01f;
            yield return new WaitForSeconds(0.03f);
        }
        print("CompleteMine");
        if(!inventory.CheckIfFull())
        {
            inventory.PutItemInInventory(ore.containedItem,inventory.GetOpenSlot());
            ore.timeToMine.fillAmount=0;
            ore.gameObject.SetActive(false);
        }
        isMining=false;
        StopAllCoroutines();
        backPanel.SetActive(false);
        fps.enabled=true;
    }

    IEnumerator AudioVisual(Ore ore)
    {
        anim.SetTrigger("Mine");
        yield return new WaitForSeconds(0.22f);
        selfAudio.PlayOneShot(mineSound);
        GameObject particle = (GameObject) Instantiate(mineParticle, ore.gameObject.transform.position, transform.rotation);
        Destroy(particle,2f);
        yield return new WaitForSeconds(miner.length/2);
        StartCoroutine("AudioVisual", ore);

    }
}
