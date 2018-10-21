using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Nexient.Business.Interface.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nexient.BlockChain
{
    public class BlockChainInfoProvider : IDisposable
    {
        public BlockChainInfoProvider(){

        }

        public void Dispose()
        {
           
        }

        public object GetBlockChain()
        {
            String blockChain = string.Empty;
            WebClient web = new WebClient();
            System.IO.Stream stream = web.OpenRead("http://localhost:3000/blockchain");
            //List<BlockChainResponse> items1 = JsonConvert.DeserializeObject<List<BlockChainResponse>>(items.ToString());
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                blockChain = reader.ReadToEnd();

                var items = JObject.Parse(blockChain)["blockchain"];
                foreach (var item in items.Children())
                {
                    var itemProperties = item.Children<JProperty>();
                    
                }

               
            }


            return blockChain;

        }


    }
}
