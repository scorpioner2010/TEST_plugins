using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class Tester : MonoBehaviour
{

    private IEnumerator Test()
    {
        TweenerCore<Vector3, Vector3, VectorOptions> tween =  transform.DOMove(Vector3.down, 1);
        yield return tween;
    }
    
    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), "1"))
        {
            Demo();
        }
        
        if (GUI.Button(new Rect(100, 200, 100, 100), "1"))
        {
            Demo2();
        }
        
        if (GUI.Button(new Rect(100, 300, 100, 100), "1"))
        {
            //Demo3();
        }
    }
    
    public async void Demo()
    {
        Debug.LogError("Start frames");
        await UniTask.DelayFrame(500);
        Debug.LogError("500 frames");
    }

    public async void Demo2()
    {
        Debug.LogError("Start milliseconds");
        await Task.Delay(1000);
        Debug.LogError("1000 milliseconds");
    }
    
    
    
    
    
    

    
    
    /*
    async UniTaskVoid Demo3()
    {
        
        // sequential
        await transform.DOMoveX(2, 10);
        await transform.DOMoveZ(5, 20);
        

        // parallel with cancellation
        CancellationToken ct = this.GetCancellationTokenOnDestroy();

        await UniTask.WhenAll(transform.DOMoveX(10, 3).WithCancellation(ct), transform.DOScale(10, 3).WithCancellation(ct));
    }
    */

    







// You can return type as struct UniTask<T>(or UniTask), it is unity specialized lightweight alternative of Task<T>
// zero allocation and fast excution for zero overhead async/await integrate with Unity
    async UniTask<string> DemoAsync()
    {
        // You can await Unity's AsyncObject
        Object asset = await Resources.LoadAsync<TextAsset>("foo");
        string txt = (await UnityWebRequest.Get("https://...").SendWebRequest()).downloadHandler.text;
        await SceneManager.LoadSceneAsync("scene2");

        // .WithCancellation enables Cancel, GetCancellationTokenOnDestroy synchornizes with lifetime of GameObject
        var asset2 = await Resources.LoadAsync<TextAsset>("bar").WithCancellation(this.GetCancellationTokenOnDestroy());

        // .ToUniTask accepts progress callback(and all options), Progress.Create is a lightweight alternative of IProgress<T>
        var asset3 = await Resources.LoadAsync<TextAsset>("baz").ToUniTask(Progress.Create<float>(x => Debug.Log(x)));

        // await frame-based operation like a coroutine
        await UniTask.DelayFrame(100);

        // replacement of yield return new WaitForSeconds/WaitForSecondsRealtime
        await UniTask.Delay(TimeSpan.FromSeconds(10), ignoreTimeScale: false);

        // yield any playerloop timing(PreUpdate, Update, LateUpdate, etc...)
        await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);

        // replacement of yield return null
        await UniTask.Yield();
        await UniTask.NextFrame();

        // replacement of WaitForEndOfFrame(requires MonoBehaviour(CoroutineRunner))
        await UniTask.WaitForEndOfFrame(this); // this is MonoBehaviour

        // replacement of yield return new WaitForFixedUpdate(same as UniTask.Yield(PlayerLoopTiming.FixedUpdate))
        await UniTask.WaitForFixedUpdate();

        // You can await IEnumerator coroutines

        // return async-value.(or you can use `UniTask`(no result), `UniTaskVoid`(fire and forget)).
        return (asset as TextAsset)?.text ?? throw new InvalidOperationException("Asset not found");
    }

}


