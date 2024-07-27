using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class HealthBarr : MonoBehaviour
    {
        public Slider slider;

        public void MaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;
        }
        public void SetHealht(int healt)
        {
            slider.value = healt;
        }

    }