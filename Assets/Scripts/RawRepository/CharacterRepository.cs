using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
namespace Repository
{
    public partial class CharacterRepository
    {
        private Dictionary<long,CharacterItemDO> _configDataMap = new Dictionary<long,CharacterItemDO>();
        private List<CharacterItemDO> _configDataList = new List<CharacterItemDO>();

        public int Count { get {  return _configDataList.Count; } }

        private CharacterRepository(){
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
    
                FileStream fileStream = new FileStream("CharacterJsonConfig.json", FileMode.Open, FileAccess.Read);
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


            public CharacterItemDO GetByIndex(int index){
                if (_configDataList.Count == 0 || index < 0 || index >= _configDataList.Count)
                    return null;
    
                return _configDataList[index];
            }


            public CharacterItemDO GetByPK(long PK){
                if (_configDataMap.Count == 0 || !_configDataMap.ContainsKey(PK))
                    return null;
    
                return _configDataMap[PK];
            }


            private static CharacterRepository _repository;

        public static CharacterRepository GetInstance(){
                       if (_repository == null) _repository = new CharacterRepository();
                       return _repository;
            }

        class Data{
            public List<CharacterItemDO> data{get;set;}
        }
    }

    public class CharacterItemDO
    {
        public long id{get;set;}

        public string name{get;set;}

        public int unlock_type{get;set;}

        public int innerRank{get;set;}

        public string born{get;set;}

    }
}