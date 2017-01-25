using UnityEngine;
using System.Collections;

public class DemonMakeShiftBs : MonoBehaviour
{

    public bool run;
    public Animator anim;
    public float hp;
    public NavMeshAgent agent;
    public GameObject player;
    public bool mayAttack;
    public int damage;
    public bool gtfoInPortal;
    public GameObject portal;
    GameObject spawnedPortal;
    public AudioSource audio;
    public AudioClip getHit,demonRip,axeHit;
    // Use this for initialization
    void Start()
    {
        player=GameObject.Find("Adolf");
        if(run)
        {
            agent.speed=3.5f;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) > 1.5 && !gtfoInPortal)
        {
            agent.SetDestination(player.transform.position);
            agent.Resume();
            anim.SetBool("Walk", true);
            if(run)
            {
                anim.SetBool("Run",true);
            }
        }
        if (Vector3.Distance(player.transform.position, this.transform.position) < 1.5 && !gtfoInPortal)
        {
            agent.Stop();
            anim.SetBool("Walk",false);
            anim.SetBool("Run", false);
            if (mayAttack)
            {
                StartCoroutine("Attack");
            }
        }
        if(gtfoInPortal)
        {
            agent.enabled=false;
            transform.position-=new Vector3(0,1*Time.deltaTime,0);
            StartCoroutine("PortalAwayAndDestroy");
        }
    }

    IEnumerator PortalAwayAndDestroy()
    {
        yield return new WaitForSeconds(2f);
        spawnedPortal.transform.FindChild("Base").GetComponent<ParticleSystem>().Stop();
        Destroy(spawnedPortal,3f);
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        
        hp-=damage;
        if(hp<=0)
        {
            transform.tag = "Untagged";
            audio.PlayOneShot(demonRip);
            GetComponent<DemonRoundSystem>().wave.EnemyKilled();
            player.GetComponent<GoldMng>().AddGold(10);
            spawnedPortal = (GameObject) Instantiate(portal,transform.position-new Vector3(0,-0.1f,0),transform.rotation);
            gtfoInPortal=true;
            agent.Stop();
        }
        else
        {
            audio.PlayOneShot(axeHit);
            audio.PlayOneShot(getHit);
        }
    }

    IEnumerator Attack()
    {
        mayAttack=false;
        anim.SetTrigger("Attack2");
        yield return new WaitForSeconds(1f);
        mayAttack=true;
    }

    void OnCollisionEnter(Collision other)
    {
        print("Ran");
        if(other.transform.tag=="Weapon")
        {
            TakeDamage(other.transform.GetComponent<WeaponManager>().weaponDamageH);
        }
    }


}
