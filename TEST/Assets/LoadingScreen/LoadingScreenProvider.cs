using System.Threading.Tasks;

namespace LoadingScreen
{
    public class LoadingScreenProvider : LocalAssetLoader
    {
        public Task<LoadingScreenAnimation> Load()
        {
            return LoadInternal<LoadingScreenAnimation>("LoadingScreen");
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}