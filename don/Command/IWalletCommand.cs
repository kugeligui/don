using Newtonsoft.Json;
using System.Diagnostics;

namespace DON.Command
{
    public class IWalletCommand
    {
        public static string Call(string server, string account, string contractAddress, string method, object[] data)
        {
            if (data == null)
            {
                data = new object[] { };
            }
            string cmd =
            DonConfig.IWalletPath + "iwallet.exe --server " + server + " --account " + account + " call " + contractAddress + " " + method + " " + JsonConvert.SerializeObject(JsonConvert.SerializeObject(data));
            return ExcuteCommand(cmd);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private static string ExcuteCommand(string cmd)
        {
            cmd = cmd + "&exit";
            Process process = new Process();

            process.StartInfo.FileName = @"cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();//启动程序
            process.StandardInput.WriteLine(cmd); //向cmd窗口写入命令
            process.StandardInput.AutoFlush = true;

            //获取输出信息
            string strOuput = process.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            process.WaitForExit();
            process.Close();
            return strOuput;
        }
    }
}
