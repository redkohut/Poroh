using System;
using Bullet_Master.Scripts.Menu_Scene.Json;
using TMPro;
using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.Fortune_Wheel.Timer
{
    public class AvailabilityTimer : MonoBehaviour
    {
        private ulong _lastWheelOpen;
        [HideInInspector]public bool isWheelReady;
        [SerializeField]private float secondsToWait = 3600;
        [SerializeField]private TextMeshProUGUI timerText;

        private void Start()
        {
            //Load saved timer time
            _lastWheelOpen = GameData.LoadData().TimerTime;
            CheckWheelStatus();
        }

        private void Update()
        {
            if (!isWheelReady)
                CheckWheelStatus();
        }

        private void CheckWheelStatus()
        {
            ulong difference = ((ulong) DateTime.Now.Ticks - _lastWheelOpen);
            ulong m = difference / TimeSpan.TicksPerMillisecond;

            float secondsLeft = ((secondsToWait * 1000) - m) / 1000.0f;

            //Check timer status
            if (secondsLeft < 0)
                IfFortuneWheelReady();
            else
                IfFortuneWheelWaiting();
        }

        private void IfFortuneWheelReady()
        {
            //Show timer status
            timerText.text = "Ready!";
            isWheelReady = true;
        }

        private void IfFortuneWheelWaiting()
        {
            ulong difference = ((ulong)DateTime.Now.Ticks - _lastWheelOpen);
            ulong m = difference / TimeSpan.TicksPerMillisecond;

            float secondsLeft = ((secondsToWait * 1000) - m) / 1000.0f;
            
            //Makes beautiful text over time
            string timeLeft = "";
            int secondsPerHour = 3600;
            
            timeLeft += (int)secondsLeft / secondsPerHour + "h ";
            secondsLeft -= ((int)secondsLeft / secondsPerHour) * secondsPerHour;
            
            timeLeft += ((int)secondsLeft / 60).ToString("00") + "m ";
            timeLeft += ((int)secondsLeft % 60).ToString("00") + "s ";
            
            //Show timer time left
            timerText.text = timeLeft;
        }

        public void RestartTimer()
        {
            _lastWheelOpen = (ulong)DateTime.Now.Ticks;
            
            var mainSaves = GameData.LoadData();
            mainSaves.TimerTime = _lastWheelOpen;
            GameData.SaveData(mainSaves);

            isWheelReady = false;
        }
        
    }
}
