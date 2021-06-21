using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public partial class weaponRepository
    {
        public int GetCapacity(long PK)
        {
            var config = GetByPK(PK);
            if (config == null)
                return 0;

            return config.capacity;
        }

        public string GetName(long PK)
        {
            var config = GetByPK(PK);
            if (config == null)
                return string.Empty;

            return config.name;

        }
    
        public float GetReloadVelocity(long PK)
        {
            var config = GetByPK(PK);
            if (config == null)
                return 0f;

            return (float)config.capacity / (float)config.fill_time;
        }
    
        public int GetSingleFireCount(long PK)
        {
            var config = GetByPK(PK);
            if (config == null)
                return 0;

            return config.single_amount;
        }
    }
}
