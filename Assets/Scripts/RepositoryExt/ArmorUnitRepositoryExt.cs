using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public partial class ArmorUnitRepository
    {
        public List<long> GetLongRangeWeaponIDs(long PK)
        {
            var config=GetByPK(PK);
            if (config == null)
                return null;

            return config.long_range_weapon;
        }

        public List<long> GetCloseCombatWeaponIDs(long PK)
        {
            var config=GetByPK(PK);
            if (config == null)
                return null;

            return config.close_combat_weapon;
        }
    
        public string GetArmorName(long PK)
        {
            var config = GetByPK(PK);
            if (config == null)
                return string.Empty;

            return config.name;
        }
    }
}
