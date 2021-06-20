using DON.Model;
using DON.Param;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;

namespace DON.Utils
{
    /// <summary>
    /// IOST工具类
    /// </summary>
    public class IOSTApiUtils
    {
        private readonly static string BASE_URI = "https://api.iost.io/";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">合约的ID</param>
        /// <param name="by_longest_chain">true - 从最长链得到数据，false - 从不可逆块得到数据</param>
        /// <returns></returns>
        public static ContractModel GetContract(string id, bool by_longest_chain = true)
        {
            return GetData<ContractModel>("getContract/" + id + "/" + by_longest_chain.ToString().ToLower());
        }


        /// <summary>
        /// 获取账号指定代币的余额
        /// </summary>  
        /// <param name="account">账号名</param>
        /// <param name="token">代币名字</param>
        /// <param name="by_longest_chain">true - 从最长链得到数据，false - 从不可逆块得到数据</param>
        /// <returns></returns>
        public static BalanceModel GetTokenBalance(string account, string token, bool by_longest_chain = true)
        {
            return GetData<BalanceModel>("getTokenBalance/" + account + "/" + token + "/" + by_longest_chain.ToString().ToLower());
        }

        /// <summary>
        /// 获取 token 信息
        /// </summary>  
        /// <param name="token">代币名字</param>
        /// <param name="by_longest_chain">true - 从最长链得到数据，false - 从不可逆块得到数据</param>
        /// <returns></returns>
        public static TokenInfo getTokenInfo(string token, bool by_longest_chain = true)
        {
            return GetData<TokenInfo>("getTokenInfo/" + token + "/" + by_longest_chain.ToString().ToLower());
        }

        /// <summary>
        /// 批量获取合约的存储数据
        /// </summary>
        /// <param name="id">智能合约的ID</param>
        /// <param name="keyFields">要批量查询的 key-field，返回值的顺序与传入顺序一致</param>
        /// <param name="by_longest_chain">true - 从最长链得到数据，false - 从不可逆块得到数据</param>
        /// <returns></returns>
        public static ContractStorageModel GetBatchContractStorage(string id, KeyFieldParam[] keyFields, bool by_longest_chain = true)
        {
            GetBatchContractStorageParam param = new GetBatchContractStorageParam();
            param.by_longest_chain = by_longest_chain;
            param.id = id;
            param.key_fields = keyFields;
            return PostData<ContractStorageModel>("getBatchContractStorage", param);
        }

        /// <summary>
        /// 批量获取合约的存储数据
        /// </summary>
        /// <param name="id">智能合约的ID</param>
        /// <param name="keyFields">要批量查询的 key-field，返回值的顺序与传入顺序一致</param>
        /// <param name="by_longest_chain">true - 从最长链得到数据，false - 从不可逆块得到数据</param>
        /// <returns></returns>
        public static NodeInfo GetNodeInfo(string server)
        {
            GetBatchContractStorageParam param = new GetBatchContractStorageParam();
            return GetData<NodeInfo>(server + "/getNodeInfo");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static T GetData<T>(string uri) where T : class
        {
            if (!uri.StartsWith("http"))
            {
                uri = BASE_URI + uri;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                //返回json字符串
                string json = myStreamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static T PostData<T>(string uri, object data) where T : class
        {
            if (!uri.StartsWith("http"))
            {
                uri = BASE_URI + uri;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            if (data != null)
            {
                request.ContentType = "application/json;charset=UTF-8";
                string para = JsonConvert.SerializeObject(data);
                byte[] bytes = Encoding.UTF8.GetBytes(para);
                request.ContentLength = bytes.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                    reqStream.Close();
                }
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                //返回json字符串
                string json = myStreamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
