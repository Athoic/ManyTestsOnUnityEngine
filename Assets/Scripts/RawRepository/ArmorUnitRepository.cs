using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
namespace Repository
{
    public partial class ArmorUnitRepository
    {
        private Dictionary<long,ArmorUnitItemDO> _configDataMap = new Dictionary<long,ArmorUnitItemDO>();
        private List<ArmorUnitItemDO> _configDataList = new List<ArmorUnitItemDO>();

        public int Count { get {  return _configDataList.Count; } }

        private ArmorUnitRepository(){
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
    
                FileStream fileStream = new FileStream("Assets/Configs/ArmorUnitJsonConfig.json", FileMode.Open, FileAccess.Read);
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


            public ArmorUnitItemDO GetByIndex(int index){
                if (_configDataList.Count == 0 || index < 0 || index >= _configDataList.Count)
                    return null;
    
                return _configDataList[index];
            }


            public ArmorUnitItemDO GetByPK(long PK){
                if (_configDataMap.Count == 0 || !_configDataMap.ContainsKey(PK))
                    return null;
    
                return _configDataMap[PK];
            }


            private static ArmorUnitRepository _repository;

        public static ArmorUnitRepository GetInstance(){
                       if (_repository == null) _repository = new ArmorUnitRepository();
                       return _repository;
            }

        class Data{
            public List<ArmorUnitItemDO> data{get;set;}
        }
    }

    public class ArmorUnitItemDO
    {
        public long id{get;set;}

        public string name{get;set;}

        public List<long> long_range_weapon{get;set;}

        public List<long> close_combat_weapon{get;set;}

    }
}