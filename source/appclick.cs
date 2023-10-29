using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using Photon.Pun;
namespace MonkePhoneApps
{
    public class appclick : MonoBehaviour
    {
        public GameObject gorilla;
        public GameObject APOM;
        public GameObject backbutton;
        public GameObject app;
        public GameObject appscreens;
        public GameObject powerbutton;
        bool clicked;
        private float touchTime = 0f;
        private const float debounceTime = 0.1f;
        void Start()
        {

            clicked = true;
    
            clicked = false;
            
         
            Debug.Log(this.gameObject.name);

        }
        public void Update()
        {
            if (PhotonNetwork.InRoom)
            {
                appscreens.transform.Find("info screen/cur room/PIR").gameObject.GetComponent<TextMeshPro>().text = PhotonNetwork.CurrentRoom.ToString();
            }
            else
            {
                appscreens.transform.Find("info screen/cur room/PIR").gameObject.GetComponent<TextMeshPro>().text = "Not in room";
            }
        }

        private void OnTriggerEnter(Collider other)
        {
                if (touchTime + debounceTime >= Time.time) return;

                if (other.TryGetComponent(out GorillaTriggerColliderHandIndicator indicator))
                {
                    if (!indicator.isLeftHand)
                    {   
                        touchTime = Time.time;

                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, indicator.isLeftHand, 0.05f);
                        GorillaTagger.Instance.StartVibration(indicator.isLeftHand, GorillaTagger.Instance.tapHapticStrength / 2f, GorillaTagger.Instance.tapHapticDuration);
                       
                        Debug.Log(this.gameObject.name + " was pressed!");
                        didclick(clicked, this.gameObject.name);
                    }
                }
        }

        private void didclick(bool click, string name)
        {
            switch (name)
            {
                case "power button":
                    if (click)
                    {
                        clicked = false;
                        app.SetActive(true);
                        powerbutton.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                    if (!click)
                    {
                        clicked = true;
                        app.SetActive(false);
                        appscreens.transform.Find("cam app").gameObject.SetActive(false);
                        APOM.transform.Find("app background").gameObject.SetActive(true);
                        backbutton.SetActive(false);
                        appscreens.transform.Find("info screen").gameObject.SetActive(false);

                        powerbutton.GetComponent<MeshRenderer>().material.color = Color.red;

                    }
                    break;
                case "app 1":
                    app.SetActive(false);
                    appscreens.transform.Find("cam app").gameObject.SetActive(true);
                    APOM.transform.Find("app background").gameObject.SetActive(false);

                    backbutton.SetActive(true);
                    break;
                case "app 2":
                    app.SetActive(false);
                    appscreens.transform.Find("info screen").gameObject.SetActive(true);
                    APOM.transform.Find("app background").gameObject.SetActive(true);
                    appscreens.transform.Find("cam app").gameObject.SetActive(false);
                    appscreens.transform.Find("info screen/NAME /gtag name").gameObject.GetComponent<TextMeshPro>().text = PhotonNetwork.NickName;

                    backbutton.SetActive(true);
                    break;
                case "Back":
                    app.SetActive(true);
                    appscreens.transform.Find("cam app").gameObject.SetActive(false);
                    APOM.transform.Find("app background").gameObject.SetActive(true);
                    appscreens.transform.Find("info screen").gameObject.SetActive(false);
                    backbutton.SetActive(false);
                    break;
                default:
                    Debug.LogError("if a new button is added, add it in the didclick function.");
                    break;
            }
        }

    }
}