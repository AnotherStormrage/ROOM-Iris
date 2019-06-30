using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class roomPlayedScript : MonoBehaviour
{
    public float distanceToSee;
    RaycastHit whatIHit;
    GameObject player;
    public GameObject pantalla;
    public GameObject flashlight;
    public GameObject Use;
    public GameObject Texto;
    public GameObject Instrucciones;

    TextMeshProUGUI textInst;
    TextMeshProUGUI textMesh;
    TextMeshProUGUI textUse;
    bool showUse;
    bool flashLightToke;
    bool ending;
    float tiempo;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        textMesh = Texto.GetComponent<TextMeshProUGUI>();
        textUse = Use.GetComponent<TextMeshProUGUI>();
        textInst = Instrucciones.GetComponent<TextMeshProUGUI>();
        showUse = true;
        flashLightToke = false;
        ending = false;
        tiempo = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (ending)
        {
            tiempo -= Time.deltaTime;
            if(tiempo <= 0)
            {                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }            
        }

        if (player.GetComponent<Inventory>().hasLantern && Input.GetKeyDown(KeyCode.Y))
        {
            flashlight.SetActive(true);
            flashLightToke = true;
        }

        textUse.enabled = false;
        if (textInst.enabled && textInst.text != "Press <E> to use")
        {
            textInst.enabled = true;
        }
        else
        {
            textInst.enabled = false;
        }

        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);

        if (Input.GetKeyDown(KeyCode.Q) && textMesh.enabled)
        {
            textMesh.enabled = false;
            showUse = true;
            textInst.enabled = false;
        }
        

        if (Physics.Raycast(this.transform.position, this.transform.forward, out whatIHit, distanceToSee))
        {
            
            if (whatIHit.collider.tag == "Interact" && showUse == true && 
                (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.puerta ||
                whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.movil||
                whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.lampara))
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
                    if (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.movil)
                    {
                        player.GetComponent<Inventory>().hasLantern = true;
                        Destroy(whatIHit.collider.gameObject);

                        textInst.text = "Press <Y> for turning on the flashlight";
                        textInst.enabled = true;
                    }
                    if (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.lampara)
                    {
                        showUse = false;
                        textMesh.text = "It's not working. What is happening here?";
                        textMesh.enabled = true;

                        textInst.text = "<Q> to quit.";
                        textInst.enabled = true;
                    }
                }
            }
            if (whatIHit.collider.tag == "Interact" && showUse == true &&
                whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.ordenador &&
                flashLightToke)
            {
                textUse.enabled = true;
                textInst.text = "Press <E> to use";
                textInst.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pantalla.SetActive(true);

                    showUse = false;
                    textMesh.text = "What the...";
                    textMesh.enabled = true;

                    textInst.text = "<Q> to quit.";
                    textInst.enabled = true;
                    ending = true;
                }
            }
        }
    }
}

