using UnityEngine;
using System.Collections;

public class GetUpPoseHelper : MonoBehaviour
{
    //geeft de transform punten aan waar de ragdoll naar moet positioneren

    public GameObject indicator;
    public GameObject[] boneHlp;
    

    void Start()
    {
        boneHlp = GameObject.FindGameObjectsWithTag("BoneB");

        foreach (GameObject boner in boneHlp)
        {
            Instantiate(indicator);
            indicator.transform.position = boner.transform.position;
        }




    }

}
