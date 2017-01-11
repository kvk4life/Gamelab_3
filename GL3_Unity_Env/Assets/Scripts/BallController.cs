using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    [SerializeField]
    private float speed;

    public Rigidbody rb;
    public bool started;


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Start ()
    {
      
	}

    void SwitchDirection()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector3(speed, 0, 0);
                started = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            SwitchDirection();
        }
	
	}
}
