using System.Threading.Tasks;
using LoadingScreen;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LoadingScreenProvider provider;
    private async void Start()
    {
        provider = new LoadingScreenProvider();
        LoadingScreenAnimation loading = await provider.Load();
        //after loading unload load screen;
        await Task.Delay(5000);
        
        provider.Unload();
    }
}