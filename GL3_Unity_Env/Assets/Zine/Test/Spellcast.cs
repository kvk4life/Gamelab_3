using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class Spellcast : MonoBehaviour
{
    [SerializeField]
    public UnityEvent spellToCast;
    public Image bar;
    public float castTime;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("Cast");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Cast()
    {
        float curCast = 0;
        while (curCast < castTime)
        {
            bar.fillAmount = (curCast / castTime);
            curCast += 2 * Time.deltaTime;
            print(curCast);
            yield return null;
        }
    }
}
