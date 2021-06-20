namespace DON.Model
{
    public class NodeInfo
    {
        /// <summary>
        /// 构建可执行程序的时间
        /// </summary>
        public string build_time { get; set; }
        /// <summary>
        /// 版本的git hash
        /// </summary>
        public string git_hash { get; set; }
        /// <summary>
        /// 节点运行模式， ModeNormal - 正常模式，ModeSync - 同步块模式，ModeInit - 初始化模式
        /// </summary>
        public string mode { get; set; }
        /// <summary>
        /// 网络连接信息
        /// </summary>
        public NetworkInfo[] network { get; set; }
        /// <summary>
        /// 代码版本号
        /// </summary>
        public string code_version { get; set; }

        /// <summary>
        /// 服务器当前时间戳，单位纳秒
        /// </summary>
        public string server_time { get; set; }
    }
}
