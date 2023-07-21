using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace LoadingScreen
{
    public class LoadingScreenAnimation : MonoBehaviour
    {
        private TMP_Text _text;
        private int _amountDots = 0;
        public int speedLoading = 500;
        private void Start()
        {
            _text = GetComponentsInChildren<TMP_Text>().First();
            Canvas canvas = FindAnyObjectByType<Canvas>();
            transform.parent = canvas.transform;
            transform.localScale = Vector3.one;
            LoadingAnimation();
            
            RectTransform rect = GetComponent<RectTransform>();
            rect.offsetMin = new Vector2(0, 0);
            rect.offsetMax = new Vector2(0, 0);
            rect.localPosition = new Vector3(0, 0,0);
        }

        private async void LoadingAnimation()
        {
            await Task.Delay(speedLoading);
            string dots = string.Empty;
        
            for (int i = 0; i < _amountDots; i++)
            {
                dots += ".";
            }

            _amountDots++;

            if (_amountDots > 3)
            {
                _amountDots = 0;
            }
        
            _text.text = "Loading"+dots;
            LoadingAnimation();
        }
    
    }
}