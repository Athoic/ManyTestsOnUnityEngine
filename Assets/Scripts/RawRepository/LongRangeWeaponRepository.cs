using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
namespace Repository
{
    public partial class LongRangeWeaponRepository
    {
        private Dictionary<long,LongRangeWeaponItemDO> _configDataMap = new Dictionary<long,LongRangeWeaponItemDO>();
        private List<LongRangeWeaponItemDO> _configDataList = new List<LongRangeWeaponItemDO>();

        public int Count { get {  return _configDataList.Count; } }

        private LongRangeWeaponRepository(){
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
    
                FileStream fileStream = new FileStream("Assets/Configs/LongRangeWeaponJsonConfig.json", FileMode.Open, FileAccess.Read);
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


            public LongRangeWeaponItemDO GetByIndex(int index){
                if (_configDataList.Count == 0 || index < 0 || index >= _configDataList.Count)
                    return null;
    
                return _configDataList[index];
            }


            public LongRangeWeaponItemDO GetByPK(long PK){
                if (_configDataMap.Count == 0 || !_configDataMap.ContainsKey(PK))
                    return null;
    
                return _configDataMap[PK];
            }


            private static LongRangeWeaponRepository _repository;

        public static LongRangeWeaponRepository GetInstance(){
                       if (_repository == null) _repository = new LongRangeWeaponRepository();
                       return _repository;
            }

        class Data{
            public List<LongRangeWeaponItemDO> data{get;set;}
        }
    }

    public class LongRangeWeaponItemDO
    {
        public long id{get;set;}

        public string name{get;set;}

        public int single_damage{get;set;}

        public float range{get;set;}

        public int single_amount{get;set;}

        public float fill_time{get;set;}

        public int capacity{get;set;}

        public long single_interval{get;set;}

        public int element{get;set;}

        public int weapon_type{get;set;}

        public int fire_type{get;set;}

        public int ammo_type{get;set;}

    }
}