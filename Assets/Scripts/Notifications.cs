using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#else
#endif
using UnityEngine;

public class Notifications : MonoBehaviour
{
    MainScript mainScript;
    // Start is called before the first frame update
    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("MainScript").GetComponent<MainScript>();
        InitNotifications();
        LoadNotifications();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */

    int fillupnotificationid = -1;
    int getbacknotificationid = -1;

    private void OnApplicationQuit()
    {
        OnAppStop();
    }
    public void OnAppStop()
    {
        Debug.Log("OnAppStop");
        double secondstofillup = mainScript.GetTimeToFillEnergy();
        DateTime? fillupdt = null;
        if (secondstofillup > 0)
        {
            fillupdt = DateTime.Now.AddSeconds(secondstofillup);
        }
        ScheduleNotificaniots(fillupdt);
        SaveNotifications();
    }

    void OnAppResume()
    {
        CancelNotifications();
    }
    void OnApplicationFocus(bool hasFocus) {
        if (hasFocus)
            OnAppResume();
        else
            OnAppStop();
    }

    void OnApplicationPause(bool pauseState) {
        if (pauseState)
            OnAppStop();
        else
            OnAppResume();
    }



#if UNITY_ANDROID

    void InitNotifications()
    {
        Debug.Log("InitNotifications1");
        var c = new AndroidNotificationChannel()
        {
            Id = "reminder_channel",
            Name = "Reminder Channel",
            Importance = Importance.High,
            Description = "Generic game notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
        Debug.Log("InitNotifications2");
    }

    void ScheduleNotificaniots(DateTime? filluptime) {
        AndroidNotification notification;
        if (filluptime != null && fillupnotificationid == -1) {
            notification = new AndroidNotification();
            notification.Title = "Your energy have just filled up!";
            notification.Text = "Start the game and have some fun!";
            notification.FireTime = (DateTime)filluptime;
            notification.SmallIcon = "icon_small";
            notification.LargeIcon = "icon_large";
            fillupnotificationid = AndroidNotificationCenter.SendNotification(notification, "reminder_channel");
            Debug.Log("fillupnotificationid = " + fillupnotificationid + " " + notification.FireTime);
        }
        if (getbacknotificationid == -1)
        {
            notification = new AndroidNotification();
            notification.Title = "The Earthlings miss you!";
            notification.Text = "Remind them that they are not alone in the universe.";
            notification.FireTime = System.DateTime.Now.AddDays(3);
            notification.SmallIcon = "icon_small";
            notification.LargeIcon = "icon_large";
            getbacknotificationid = AndroidNotificationCenter.SendNotification(notification, "reminder_channel");
            Debug.Log("getbacknotificationid = " + getbacknotificationid + " " + notification.FireTime);
        }
    }

    void CancelNotifications()
    {
        if (fillupnotificationid != -1)
        {
            AndroidNotificationCenter.CancelNotification(fillupnotificationid);
            Debug.Log("fillupnotificationid was " + fillupnotificationid);
        }
        if (getbacknotificationid != -1)
        {
            AndroidNotificationCenter.CancelNotification(getbacknotificationid);
            Debug.Log("getbacknotificationid was " + getbacknotificationid);
        }
        fillupnotificationid = -1;
        getbacknotificationid = -1;
    }
    
    void SaveNotifications()
    {
        PlayerPrefs.SetInt("fillupnotificationid", fillupnotificationid);
        PlayerPrefs.SetInt("getbacknotificationid", getbacknotificationid);
        PlayerPrefs.Save();
    }

    void LoadNotifications()
    {
        fillupnotificationid = PlayerPrefs.GetInt("fillupnotificationid", -1);
        getbacknotificationid = PlayerPrefs.GetInt("getbacknotificationid", -1);
    }
#elif UNITY_IOS

    void InitNotifications()
    {
        //nothing
    }

    void ScheduleNotificaniots(DateTime? filluptime) {

        if (filluptime != null && fillupnotificationid == -1)
        {
            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = TimeSpan.FromTicks(((DateTime)filluptime).Ticks - DateTime.Now.Ticks),
                Repeats = false
            };
            
            Debug.Log("fillupnotificationid: " + timeTrigger.TimeInterval);

            var notification = new iOSNotification()
            {
                // You can optionally specify a custom identifier which can later be 
                // used to cancel the notification, if you don't set one, a unique 
                // string will be generated automatically.
                Identifier = "fillup_notification_01",
                Title = "Your energy have just filled up!",
                Body = "Start the game and have some fun!",
                Subtitle = "",
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "category_a",
                ThreadIdentifier = "thread1",
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
            fillupnotificationid = 1;
        }
        if (getbacknotificationid == -1)
        {
            var timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(3, 0, 0, 0),
                Repeats = false
            };

            
            Debug.Log("getbacknotificationid: " + timeTrigger.TimeInterval);

            var notification = new iOSNotification()
            {
                // You can optionally specify a custom identifier which can later be 
                // used to cancel the notification, if you don't set one, a unique 
                // string will be generated automatically.
                Identifier = "getback_notification_01",
                Title = "The Earthlings miss you!",
                Body = "Remind them that they are not alone in the universe.",
                Subtitle = "",
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "category_a",
                ThreadIdentifier = "thread1",
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);

            getbacknotificationid = 1;
        }
    }

    void CancelNotifications()
    {
        if (fillupnotificationid != -1)
        {
            iOSNotificationCenter.RemoveScheduledNotification("fillupnotificationid");
            Debug.Log("fillupnotificationid deleted");
        }
        if (getbacknotificationid != -1)
        {
            iOSNotificationCenter.RemoveScheduledNotification("getback_notification_01");
            Debug.Log("getbacknotificationid deleted");
        }
        fillupnotificationid = -1;
        getbacknotificationid = -1;
    }

    void SaveNotifications()
    {
        //nothing
    }

    void LoadNotifications()
    {
        fillupnotificationid = 1;
        getbacknotificationid = 1;
    }
#else
#endif

}
