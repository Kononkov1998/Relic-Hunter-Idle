using TMPro;
using UnityEngine;

namespace MonoBehaviours
{
    public class UI : MonoBehaviour
    {
        public Transform ActionTimersCanvas;
        public TextMeshProUGUI TreasuresText;

        public void SetTreasures(int count)
        {
            TreasuresText.text = $"Treasures collected: {count}";
        }
    }
}
