using UnityEngine;
using System.Collections;

public class AnimManager : MonoBehaviour
{
    public Animator anim;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Movement",Input.GetAxis("Vertical"));
        if(Input.GetButtonDown("Sprint"))
        {
            anim.SetBool("Sprint", true);
        }
        if (Input.GetButtonUp("Sprint"))
        {
            anim.SetBool("Sprint", false);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("Pyro");
        }
    }
}
