using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MaxScoreText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            text.text = $"0";
        }
    }
}