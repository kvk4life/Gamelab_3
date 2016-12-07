using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
    public Transform destination;
    public Rigidbody rigid;
    public float moveSpeed;
    public GameObject player, playerCam;
    public RaycastHit hit;
    public bool isClimbing;
    public CharacterController cc;
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
            if(Physics.Raycast(playerCam.transform.position,playerCam.transform.forward,out hit,3f))
            {
                if(hit.transform.gameObject==this.gameObject)
                {
                    if(!isClimbing)
                    {
                        print("legggggoooooo");
                        StartCoroutine(ClimbLadder());
                    }
                }
            }
        }
    }

    public IEnumerator ClimbLadder()
    {
        GetComponent<AudioSource>().Play();
        print("fuuuuuuuuuuuuck");
        cc.enabled=false;
        isClimbing=true;
        rigid.useGravity=false;
        while(Vector3.Distance(player.transform.position,destination.position)>1)
        {
            player.transform.position = Vector3.Lerp(player.transform.position,destination.position, moveSpeed*Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        rigid.useGravity=true;
        isClimbing=false;
        cc.enabled=true;
        GetComponent<AudioSource>().Stop();
    }

}
