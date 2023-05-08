using System;

namespace Infrastructure.Entities.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public int Money;
        public int Gems;
        public string LastScene;

        public int LocationIndex;

        public string GunName;
        
    }
}