using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class roomPlayedScript : MonoBehaviour
{
    public float distanceToSee;
    RaycastHit whatIHit;
    public GameObject player;
    public GameObject Use;
    public GameObject Texto;
    public GameObject Instrucciones;
    public GameObject pantallaOrd;

    TextMeshProUGUI textInst;
    TextMeshProUGUI textMesh;
    TextMeshProUGUI textUse;
    bool showUse;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        textMesh = Texto.GetComponent<TextMeshProUGUI>();
        textUse = Use.GetComponent<TextMeshProUGUI>();
        textInst = Instrucciones.GetComponent<TextMeshProUGUI>();
        showUse = true;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);

        if (Input.GetKeyDown(KeyCode.Q) && textMesh.enabled)
        {
            textMesh.enabled = false;
            showUse = true;
            textInst.enabled = false;
        }
        textUse.enabled = false;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out whatIHit, distanceToSee))
        {
            
            if (whatIHit.collider.tag == "Interact" && showUse == true)
            {
                textUse.enabled = true;
                textInst.text = "Press <E> to use";
                textInst.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (textInst.text == "Press <E> to use")
                    {
                        textInst.enabled = false;
                    }                                        
                    if (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.puerta)
                    {
                        showUse = false;
                        textMesh.text = "It's closed...";
                        textMesh.enabled = true;

                        textInst.text = "<Q> to quit.";
                        textInst.enabled = true;
                    }                                      
                }
            }
        }
    }
}
