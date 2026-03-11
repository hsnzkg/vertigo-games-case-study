using Project.Scripts.Game.WheelGame.Data.Provider;
using Storage.Runtime;

namespace Project.Scripts.Storage
{
    public class GameplayStorage : IStorage
    {
        public IWheelItemCollectionProvider WheelItemCollectionProvider;

        public void Dispose()
        {
            WheelItemCollectionProvider = null;
        }
    }
}