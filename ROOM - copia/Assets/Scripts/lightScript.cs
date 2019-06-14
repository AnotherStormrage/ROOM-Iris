using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour
{
    Light luz;
    float random;
    float tiempo;

    // Start is called before the first frame update
    void Start()
    {
        luz = this.GetComponent<Light>();
        random = 0;
        tiempo = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        if(tiempo <= 0)
        {
            random = Random.Range(1f, 10f);
            if (random <= 9f)
            {
                luz.intensity = 6.5f;
            }
            else
            {
                luz.intensity = 3f;
            }
            tiempo = 0.05f;
        }
    }
}
