using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
namespace Repository
{
    public partial class CloseCombatWeaponRepository
    {
        private Dictionary<long,CloseCombatWeaponItemDO> _configDataMap = new Dictionary<long,CloseCombatWeaponItemDO>();
        private List<CloseCombatWeaponItemDO> _configDataList = new List<CloseCombatWeaponItemDO>();

        public int Count { get {  return _configDataList.Count; } }

        private CloseCombatWeaponRepository(){
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
    
                FileStream fileStream = new FileStream("Assets/Configs/CloseCombatWeaponJsonConfig.json", FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                string jsonData = streamReader.ReadToEnd();
                Data  configData = javaScriptSerializer.Deserialize(jsonData, typeof(Data)) as Data;
    
                for(int i = 0, count = configData.data.Count; i < count; i++)
                {
                    var config = configData.data[i];
                    _configDataMap.Add(config.id, config);
                    _configDataList.Add(config);
                }
            }


            public CloseCombatWeaponItemDO GetByIndex(int index){
                if (_configDataList.Count == 0 || index < 0 || index >= _configDataList.Count)
                    return null;
    
                return _configDataList[index];
            }


            public CloseCombatWeaponItemDO GetByPK(long PK){
                if (_configDataMap.Count == 0 || !_configDataMap.ContainsKey(PK))
                    return null;
    
                return _configDataMap[PK];
            }


            private static CloseCombatWeaponRepository _repository;

        public static CloseCombatWeaponRepository GetInstance(){
                       if (_repository == null) _repository = new CloseCombatWeaponRepository();
                       return _repository;
            }

        class Data{
            public List<CloseCombatWeaponItemDO> data{get;set;}
        }
    }

    public class CloseCombatWeaponItemDO
    {
        public long id{get;set;}

        public string name{get;set;}

        public float range{get;set;}

        public long damage{get;set;}

        public int element{get;set;}

        public int close_combat_damage_type{get;set;}

        public string prefab_name{get;set;}

    }
}