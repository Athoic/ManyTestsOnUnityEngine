using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
namespace Repository
{
    public partial class weaponRepository
    {
        private Dictionary<long,weaponItemDO> _configDataMap = new Dictionary<long,weaponItemDO>();
        private List<weaponItemDO> _configDataList = new List<weaponItemDO>();

        public int Count { get {  return _configDataList.Count; } }

        private weaponRepository(){
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
    
                FileStream fileStream = new FileStream("weaponJsonConfig.json", FileMode.Open, FileAccess.Read);
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


            public weaponItemDO GetByIndex(int index){
                if (_configDataList.Count == 0 || index < 0 || index >= _configDataList.Count)
                    return null;
    
                return _configDataList[index];
            }


            public weaponItemDO GetByPK(long PK){
                if (_configDataMap.Count == 0 || !_configDataMap.ContainsKey(PK))
                    return null;
    
                return _configDataMap[PK];
            }


            private static weaponRepository _repository;

        public static weaponRepository GetInstance(){
                       if (_repository == null) _repository = new weaponRepository();
                       return _repository;
            }

        class Data{
            public List<weaponItemDO> data{get;set;}
        }
    }

    public class weaponItemDO
    {
        public long id{get;set;}

        public string name{get;set;}

        public int type{get;set;}

        public float range{get;set;}

        public int single_amount{get;set;}

        public float fill_time{get;set;}

        public int capacity{get;set;}

        public long single_interval{get;set;}

    }
}