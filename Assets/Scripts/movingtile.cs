using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingtile : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 dir;
    public float freq,amp;
    float time;
    private RewindAbstract _rewindAbstract;
    void Start()
    {
        time = 0;
        _rewindAbstract = GetComponent<RewindAbstract>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {

        if (_rewindAbstract != null && !_rewindAbstract.IsTracking)
        {
            time = time - Time.fixedDeltaTime;
            return;
        }
        else
        {
            time += Time.fixedDeltaTime;
            transform.position += dir.normalized * (amp * Mathf.Sin(time * freq) * Time.fixedDeltaTime);
        }
    }
}
