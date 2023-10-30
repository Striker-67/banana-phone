using BepInEx;
using MonkePhoneApps;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;
using System.Collections;
using System.Collections.Generic;

namespace MonkePhone
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla")]
    [BepInPlugin("com.striker.gorillatag.monkephone", "MonkePhone", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public GameObject Phone;
        public GameObject powerbutton;
        public GameObject gorillaplayer;
        public GameObject leftHand;
        public GameObject apps;
        public GameObject cam;
        public GameObject app1;
        public GameObject app2;
        public GameObject APOM;
        GameObject appscreens;
        GameObject backbutton;
        void Start() => Utilla.Events.GameInitialized += OnGameInitialized;
        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }
        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }
        void OnGameInitialized(object sender, EventArgs e)
        {
            var bundle = LoadAssetBundle("MonkePhone.Resources.phone");
            var asset = bundle.LoadAsset<GameObject>("phone");
            Phone = GameObject.Instantiate(asset);

            leftHand = GorillaLocomotion.Player.Instance.leftControllerTransform.gameObject;

            SetUp();
        }
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
        void AddClicksToButtons()
        {
            apps.AddComponent<appclick>();
            powerbutton.AddComponent<appclick>();
            backbutton.AddComponent<appclick>();
            APOM.AddComponent<appclick>();
        }

        void SetUp()
        {
         

            Phone.transform.parent = leftHand.transform;
           
          
            Phone.transform.localPosition = new Vector3(0.0392f, - 0.0946f, - 0.1213f);
            Phone.transform.localRotation = Quaternion.Euler(11.625f, 84.8206f, 52.0238f);
            Phone.transform.localScale = new Vector3(0.4f, 0.4f, 1f);

            apps = leftHand.transform.Find("phone(Clone)/phone model/button/apps").gameObject;
            app1 = leftHand.transform.Find("phone(Clone)/phone model/button/apps/app 1").gameObject;
            app2 = leftHand.transform.Find("phone(Clone)/phone model/button/apps/app 2").gameObject;
            powerbutton = leftHand.transform.Find("phone(Clone)/phone model/button/power button").gameObject;
            appscreens = leftHand.transform.Find("phone(Clone)/phone model/app screens").gameObject;
            APOM = leftHand.transform.Find("phone(Clone)/phone model/Apart of model").gameObject;
            backbutton = leftHand.transform.Find("phone(Clone)/phone model/button/Back").gameObject;

           
            Phone.AddComponent<Rigidbody>().isKinematic = true;

            AddClicksToButtons();

            appclick appClick = apps.GetComponent<appclick>();
            appclick powerButtonAppClick = powerbutton.GetComponent<appclick>();
            List<appclick> appsClick = new List<appclick>();
            appsClick.Add(app1.AddComponent<appclick>());
            appsClick.Add(app2.AddComponent<appclick>());
        
            appClick.app = apps;
            appClick.powerbutton = powerbutton;
            appClick.appscreens = appscreens;
            appClick.APOM = APOM;
            appClick.backbutton = backbutton;
            powerButtonAppClick.powerbutton = powerbutton;
            powerButtonAppClick.appscreens = appscreens;
            powerButtonAppClick.APOM = APOM;
            powerButtonAppClick.backbutton = backbutton;
            powerButtonAppClick.app = apps;
            powerButtonAppClick.powerbutton = powerbutton;
            powerButtonAppClick.appscreens = appscreens;
            powerButtonAppClick.APOM = APOM;
            powerButtonAppClick.backbutton = backbutton;
            backbutton.GetComponent<appclick>().app = apps;
            backbutton.GetComponent<appclick>().powerbutton = powerbutton;
            backbutton.GetComponent<appclick>().APOM = APOM;
            backbutton.GetComponent<appclick>().appscreens = appscreens;
            backbutton.GetComponent<appclick>().backbutton = backbutton;



            for (int i = 0; i < appsClick.Count; i++)
            {
                appsClick[i].app = apps;
                appsClick[i].powerbutton = powerbutton;
                appsClick[i].APOM = APOM;
                appsClick[i].appscreens = appscreens;
                appsClick[i].backbutton = backbutton;
                appsClick[i].transform.SetParent(apps.transform, false);
            }
            apps.SetActive(false);
            powerbutton.GetComponent<MeshRenderer>().material.color = Color.red;
            app1.layer = 18; 
            app2.layer = 18; 
            powerbutton.layer = 18;
            backbutton.layer = 18;
            apps.layer = 18;
        }

        /*        
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }
        
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
        */
    }
}
