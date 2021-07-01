using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public partial class CloseCombatWeaponRepository
    {
        public string GetPrefabName(long PK)
        {
            var config = GetByPK(PK);
            if (config == null)
                return string.Empty;

            return config.prefab_name;
        }
    }
}
