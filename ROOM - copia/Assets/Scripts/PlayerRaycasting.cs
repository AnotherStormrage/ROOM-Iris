using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRaycasting : MonoBehaviour
{
    public float distanceToSee;
    RaycastHit whatIHit;
    public GameObject player;
    public GameObject Use;
    public GameObject Texto;
    public GameObject Instrucciones;
    public GameObject pantallaOrd;

    
    GameObject lampara;
    GameObject lamparaTecho;
    Light luzLampara;
    TextMeshProUGUI textInst;
    TextMeshProUGUI textMesh;
    TextMeshProUGUI textUse;
    bool showUse;
    bool ordenadorApagado;
    bool roomPlayed;
    bool roomPlayable;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        textMesh = Texto.GetComponent<TextMeshProUGUI>();
        textUse = Use.GetComponent<TextMeshProUGUI>();
        textInst = Instrucciones.GetComponent<TextMeshProUGUI>();
        lampara = GameObject.Find("lampara");
        lamparaTecho = GameObject.Find("luzHabitacion");
        luzLampara = lampara.GetComponent<Light>();
        showUse = true;
        ordenadorApagado = false;
        roomPlayed = false;
        roomPlayable = true;
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
                    if(textInst.text == "Press <E> to use")
                    {
                        textInst.enabled = false;
                    }

                  
                    if (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.ordenador && ordenadorApagado)
                    {
                        showUse = false;
                        textMesh.text = "Should I play it?";
                        textMesh.enabled = true;

                        textInst.text = "<Q> to quit. <Y> to PLAY";
                        textInst.enabled = true;
                        roomPlayed = true;
                    }

                    else if (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.ordenador && roomPlayable)
                    {
                        showUse = false;
                        textMesh.text = "This game... I can't understand why everyone is talking about it.";
                        textMesh.enabled = true;
                        textInst.text = "Press <Q> to quit, <Y> to CONTINUE";
                        textInst.enabled = true;
                        pantallaOrd.SetActive(true);
                        ordenadorApagado = true;
                    }
                    if (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.cuadroMesita)
                    {
                        showUse = false;
                        textMesh.text = "I miss them a lot...";
                        textMesh.enabled = true;

                        textInst.text = "Press <Q> to quit";
                        textInst.enabled = true;
                    }
                    if (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.lampara)
                    {
                        if (luzLampara.enabled == false)
                        {
                            luzLampara.enabled = true;
                        }
                        else
                        {
                            luzLampara.enabled = false;
                        }

                    }
                    if (whatIHit.collider.gameObject.GetComponent<Interact>().whichTypeAmI == Interact.interact.movil)
                    {
                        showUse = false;
                        textMesh.text = "Social media is crazy talking about ROOM. A lot of strange things happens to the people who play it.";
                        textMesh.enabled = true;

                        textInst.text = "Press <Q> to quit";
                        textInst.enabled = true;
                    }                    
                }
            }       
        }
        if (roomPlayed && Input.GetKeyDown(KeyCode.Y))
        {
            lamparaTecho.SetActive(false);
            ordenadorApagado = false;
            textInst.text = "Press <Q> to quit";
            textMesh.text = "What?";
            pantallaOrd.SetActive(false);
            roomPlayable = false;
            this.GetComponent<roomPlayedScript>().enabled = true;
            this.GetComponent<PlayerRaycasting>().enabled = false;
        }

        if (ordenadorApagado && Input.GetKeyDown(KeyCode.Y))
        {
            showUse = false;
            textMesh.text = "Should I play it?";
            textMesh.enabled = true;

            textInst.text = "<Q> to quit. <Y> to PLAY";
            textInst.enabled = true;
            roomPlayed = true;            
        }
    }
}
/*
           if(Input.GetKeyDown(KeyCode.E))
           {
               if (whatIHit.collider.tag == "Keycards")
               {
                   if (whatIHit.collider.gameObject.GetComponent<KeyCards>().whatKeyAmI == KeyCards.Keycards.orangeKey)
                   {
                       player.GetComponent<Inventory>().hasOrangeKey = true;
                       Destroy(whatIHit.collider.gameObject);
                   }
                   if (whatIHit.collider.gameObject.GetComponent<KeyCards>().whatKeyAmI == KeyCards.Keycards.blueKey)
                   {
                       player.GetComponent<Inventory>().hasBlueKey = true;
                       Destroy(whatIHit.collider.gameObject);
                   }
                   if (whatIHit.collider.gameObject.GetComponent<KeyCards>().whatKeyAmI == KeyCards.Keycards.yellowKey)
                   {
                       player.GetComponent<Inventory>().hasYellowKey = true;
                       Destroy(whatIHit.collider.gameObject);
                   }
               }*/
