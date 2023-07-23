using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AsyncOperations
{
    public class Loader : MonoBehaviour
    {
        public Image sprite;

        private async void OnGUI()
        {
            if (GUI.Button(new Rect(100, 100, 100, 100), "1"))
            {
                Task t3 = Task.Delay(700);
                Task t1 = Task.Delay(1000).ContinueWith((c) =>
                {
                    sprite.gameObject.SetActive(false);
                },TaskScheduler.FromCurrentSynchronizationContext());
                
                Task t2 = Task.Delay(1900);
                Task f = Task.WhenAny(t1, t2, t3);
                await f;
            }
            
            Task c = Demo2();
            await c;

            Demo1();

            UniTask<int> result = Demo3();
            await result;
        }


        private async UniTask<int> Demo3()
        {
            await Task.Delay(100);

            return 1;
        }

        //private unsafe void Test1() { Action a = () => { }; lock (a) { Debug.LogError(1);} }
        

        private async void Demo1()
        {
            Debug.LogError(1);
            await Task.Delay(1000);
            Debug.LogError(2);
        }

        private async Task Demo2()
        {
            Debug.LogError(1);
            await Task.Delay(1000);
            Debug.LogError(2);
        }

    




        /*
        private async void GetUrl()
        {
            string url = "https://www.youtube.com/";

            using (HttpClient client = new HttpClient())
            {
                Task<string> strTask = client.GetStringAsync(url);
                string result = await strTask;
                Task voidTask = Task.Delay(2000);
                await voidTask;
                //string debug = result.Substring(0, 50);
                Debug.LogError(result);
            }
        }
*/

    
        

        /*
        private void OnGUI()
        {
            if (GUI.Button(new Rect(100, 100, 100, 100), "1"))
            {
                StartCoroutine(SetImage("https://cdn.write.corbpie.com/wp-content/uploads/2021/06/8k-video-resolution-and-below-16-by-9-ratio-video-size-graphic.png"));
            }
        }
        
        
        private IEnumerator SetImage(string url)
        {
            WWW www = new WWW(url);

            yield return www;

            Texture2D texture = www.texture;

            Rect rec = new Rect(0, 0, texture.width, texture.height);
            Sprite.Create(texture, rec, new Vector2(0, 0), 1);
            sprite.sprite = Sprite.Create(texture, rec, new Vector2(0, 0), .01f);
        }
        */
    }
}
