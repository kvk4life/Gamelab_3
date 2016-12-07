using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using System.Collections;

public class BlackSmithing : MonoBehaviour
{

    public ItemToMake[] creatableItems;
    [Header("")]
    public Image progressBar;
    public GameObject backGroundBar;
    public Inventory inventory;
    public CheckArea area;
    public GameObject anvil;
    public FirstPersonController fps;
    public Animator hammer;
    public AudioSource audio;
    public AudioClip hitSound;
    public ParticleSystem particle;
    public Transform blackSmithPos;
    public CanvasGroup group;
    public GameObject pickaxe;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            gameObject.SetActive(false);
            fps.enabled=true;
        }
    }


    public void Make(int index)
    {
        for (int i = 0; i < creatableItems[index].needed.Length; i++)
        {
            if (creatableItems[index].slots[i].item == null)
            {
                return;
            }
            if(creatableItems[index].slots[i].item.itemName != creatableItems[index].needed[i].itemName)
            {
                return;
            }
        }
        StartCoroutine("BlackSmith", creatableItems[index].itemToCreate);
        for(int i2 = 0; i2<creatableItems[index].slots.Length; i2++)
        {
            Slot curSlot  = creatableItems[index].slots[i2];
            curSlot.occupied=false;
            curSlot.item=null;
            curSlot.UpdateSlotEmpty();;
        }

    }

    public IEnumerator BlackSmith(Item itemToMake)
    {
        pickaxe.SetActive(false);
        group.alpha=0;
        hammer.transform.gameObject.SetActive(true);
        StartCoroutine("AudioVisual");
        fps.enabled=false;
        fps.gameObject.transform.position=blackSmithPos.position;
        Camera.main.transform.LookAt(anvil.transform);
        backGroundBar.SetActive(true);
        while (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.03f);
        }
        backGroundBar.SetActive(false);
        Instantiate(itemToMake.itemPrefab,anvil.transform.position,itemToMake.itemPrefab.transform.rotation);
        fps.enabled = true;
        Camera.main.transform.LookAt(anvil.transform);
        hammer.transform.gameObject.SetActive(false);
        group.alpha = 1;
        gameObject.SetActive(false);
        progressBar.fillAmount=0f;
        pickaxe.SetActive(true);
        StopAllCoroutines();
    }

    public IEnumerator AudioVisual()
    {
        hammer.SetTrigger("Hit");
        yield return new WaitForSeconds(0.45f);
        particle.Play();
        yield return new WaitForSeconds(0.05f);
        audio.PlayOneShot(hitSound);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("AudioVisual");
    }
}

[System.Serializable]
public struct ItemToMake
{
    public Item[] needed;
    public Slot[] slots;
    public Item itemToCreate;
}
