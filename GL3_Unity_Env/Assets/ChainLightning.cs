using UnityEngine;
using System.Collections;
using DigitalRuby.ThunderAndLightning;

public class ChainLightning : MonoBehaviour {
    public LightningBoltPathScript bolt;
    public GameObject prefab;
    public GameObject[] objectsToAdd;
    public AudioClip[] clips;
    public int targetCap;
    public LightningBoltPrefabScript bolt2;
    public float chainTimeMin, chainTimeMax;
    public int lightningIntensity;
	// Use this for initialization
	void Start ()
    {
	    bolt=prefab.GetComponent<LightningBoltPathScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButtonDown("B"))
        {
            //GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0,clips.Length)]);
            //bolt.LightningPath.List.Clear();
            //bolt.LightningPath.List.Add(this.gameObject);
            //for(int i =0; i<objectsToAdd.Length; i++)
            //{
            //    bolt.LightningPath.List.Add(objectsToAdd[i]);
            //}
            //GameObject spawned  = (GameObject) Instantiate(prefab);
            //Destroy(spawned,2);
            StartCoroutine("ChainLightningStart");
        }
	}

    IEnumerator ChainLightningStart()
    {
        Chain(this.gameObject, objectsToAdd[0]);
        yield return new WaitForSeconds(Random.Range(chainTimeMin,chainTimeMax));
        for (int i =0; i<objectsToAdd.Length-1; i++)
        {
            Chain(objectsToAdd[i], objectsToAdd[i + 1]);
            yield return new WaitForSeconds(Random.Range(chainTimeMin, chainTimeMax));
        }
    }

    void Chain(GameObject targetToChain, GameObject nextTarget)
    {
        bolt2.Source=targetToChain;
        bolt2.Destination=nextTarget;
        for (int i = 0; i < lightningIntensity; i++)
        {
            GameObject spawned = (GameObject)Instantiate(bolt2.gameObject);
            Destroy(spawned, 2f);
        }
        GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0,clips.Length)]);
    }
}
