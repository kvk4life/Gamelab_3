using UnityEngine;
using System.Collections;

public class GemFloatScript : MonoBehaviour
{
    public float floatSpeed;
    public float rotSpeed;
    public float floatLength;
    private bool mayFloatUp=false;
    private bool mayFloatDown=false;  
    // Use this for initialization
    void Start()
    {
        StartCoroutine("Float",true);
    }

    // Update is called once per frame
    void Update()
    {
        if(mayFloatUp)
        {
            transform.position+=new Vector3(0,floatSpeed,0)*Time.deltaTime;
        }
        if (mayFloatDown)
        {
            transform.position -= new Vector3(0, floatSpeed, 0) * Time.deltaTime;
        }
        transform.Rotate(0,0,rotSpeed*Time.deltaTime);
    }

    IEnumerator Float(bool upOrDown)
    {
        if(upOrDown)
        {
            mayFloatUp=true;
            yield return new WaitForSeconds(floatLength);
            mayFloatUp=false;
            StartCoroutine("Float",false);
        }
        else if(!upOrDown)
        {
            mayFloatDown = true;
            yield return new WaitForSeconds(floatLength);
            mayFloatDown = false;
            StartCoroutine("Float", true);
        }
    }
}
