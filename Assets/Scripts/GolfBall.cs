using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Rigidbody) )]
public class GolfBall : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip whistleSound;

    const int LOOKBACK_COUNT = 10;
    static List<GolfBall> GOLFBALLS = new List<GolfBall>();

    [SerializeField]
    private bool _awake = true;
    public bool awake {
        get { return _awake; }
        private set { _awake = value; }
    }

    private Vector3 prevPos;
    //This private list stores the history of projectile's move distance
    private List<float> deltas = new List<float>();
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        awake = true;
        prevPos = new Vector3(1000, 1000, 0);
        deltas.Add( 1000 );

        GOLFBALLS.Add( this );

    }

    void FixedUpdate() {
        //Adjusts pitch or volume based on speed
        float speed = GetComponent<Rigidbody>().velocity.magnitude;
        audioSource.volume = Mathf.Clamp( speed / 20f, 0.1f, 1f );
        audioSource.pitch = Mathf.Clamp( speed / 10f, 0.8f, 2f );

        if( rigid.isKinematic || !awake ) return;

        Vector3 deltaV3 = transform.position - prevPos;
        deltas.Add( deltaV3.magnitude );
        prevPos = transform.position;

        //Limit lookback; one of very few times that uses while
        while( deltas.Count > LOOKBACK_COUNT ) {
            deltas.RemoveAt( 0 );
        }

        //Iterate over deltas and find the greatest one
        float maxDelta = 0;
        
        foreach( float f in deltas ) {
            if( f > maxDelta ) maxDelta = f;
        }

        //If the projectile hasn't moved more than the sleep threshold
        if( maxDelta <= Physics.sleepThreshold ) {
            //Set awake to false and put the Rigidbody to sleep
            awake = false;
            rigid.Sleep();
        }
    }

    private void OnDestroy() {
        GOLFBALLS.Remove( this );
    }

    static public void DESTORY_PROJECTILES() {
        foreach( GolfBall g in GOLFBALLS ) {
            Destroy( g.gameObject );
        }
    }

}
