﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartGate : Photon.MonoBehaviour, IPunObservable
{
    List<Player> places = new List<Player>();
    public int AmountOfLaps = 3;
    public GateManager gateManager;
    public Text finishText;
    public Text LapText;
    

    // Use this for initialization
    void Start ()
    {
        LapText.text = 1 + "/" + AmountOfLaps;
        EventManager.StartListening("FinishMatch", Reset);
    }

    void Reset()
    {
        places.Clear();
        LapText.text = 1 + "/" + AmountOfLaps;
        finishText.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.GetComponent<Player>().currentGate >= gateManager.Gates.Length && col.GetComponent<Player>().place < 1 && col.GetComponent<Player>().currentLap >= AmountOfLaps)
            {
                photonView.RPC("FinishPlayer", PhotonTargets.All, col.GetComponent<Player>().photonView.viewID);
            }

            else if (col.GetComponent<Player>().currentGate >= gateManager.Gates.Length)
            {
                if (col.GetComponent<Player>().photonView.isMine)
                {
                    col.GetComponent<Player>().SetLap();
                    if (col.GetComponent<Player>().currentLap > AmountOfLaps)
                        col.GetComponent<Player>().currentLap = AmountOfLaps;
                    LapText.text = col.GetComponent<Player>().currentLap + "/" + AmountOfLaps;
                    gateManager.Gates[0].nextGate(); 
                
                }
            }
        }
    }

    [PunRPC]
    public void FinishPlayer(int id)
    {
        GameObject col = PhotonView.Find(id).gameObject;
        col.GetComponent<Player>().place = places.Count + 1;
        places.Add(col.GetComponent<Player>());

        if (col.gameObject.layer == 8)
        {
            string mod;
            switch (places.Count)
            {
                case 1:
                    mod = "st";
                    break;
                case 2:
                    mod = "nd";
                    break;
                case 3:
                    mod = "rd";
                    break;
                default:
                    mod = "th";
                    break;
            }

            finishText.text = "You Got " + places.Count + mod + " Place";
            finishText.enabled = true;


        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {

        }
        else
        {

        }
    }
}
