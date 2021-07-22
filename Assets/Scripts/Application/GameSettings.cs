using Frame.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GameSettings
{
    public class GameSettings
    {
        public static bool IsAutoLockOn
        {
            get
            {
                return Persistence.Read(PersistenceKeys.IsAutoLockOn, true);
            }
            set
            {
                Persistence.Write(PersistenceKeys.IsAutoLockOn, value);
            }
        }
    }
}
