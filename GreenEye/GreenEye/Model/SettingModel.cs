using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;



namespace GreenEye.Model
{
    public class SettingModel
    {
        public string MinGoodsReceipt { get; set; }
        public string MinStoreImport { get; set; }
        public string Maxdebt { get; set; }
        public string MinStoreSell { get; set; }
        public bool MaxDebtBool { get; set; }

        public void readData()
        {

        
            string file = File.ReadAllText( @"Model/setting.json");
            var data = JsonConvert.DeserializeObject<SettingModel >(file);

            MinGoodsReceipt = data.MinGoodsReceipt;
            MinStoreImport = data.MinStoreImport;
            Maxdebt = data.Maxdebt;
            MinStoreSell = data.MinStoreSell;
            MaxDebtBool = data.MaxDebtBool;


        }

        public void writeData(string MinGoodsReceipt, string MinStoreImport, string Maxdebt, string MinStoreSell, bool MaxDebtBool)
        {
            Debug.WriteLine("OKOK");
            Debug.WriteLine(MinGoodsReceipt);
            List<SettingModel> _data = new List<SettingModel>();
            _data.Add(new SettingModel()
            {
                MinGoodsReceipt = MinGoodsReceipt,
                MinStoreImport = MinStoreImport,
                Maxdebt =Maxdebt,
                MinStoreSell = MinStoreSell,
                MaxDebtBool = MaxDebtBool
                
            });
            

            string json = System.Text.Json.JsonSerializer.Serialize(_data[0]);

            string realfile = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + @"\Model\setting.json";
            if (File.Exists(realfile))
            {

                File.WriteAllText(realfile, json);
            }
            
            File.WriteAllText(@"Model/setting.json", json);
        }

    }
}
