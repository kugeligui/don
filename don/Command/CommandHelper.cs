using System.Collections.Generic;
using System.Text;

namespace DON.Command
{
    public class CommandHelper
    {
        public class CommandInfo
        {
            /// <summary>
            /// 命令
            /// </summary>
            public string command { get; }

            /// <summary>
            /// 描述
            /// </summary>
            public string desc { get; }

            public CommandInfo(string _command, string _desc)
            {
                this.command = _command;
                this.desc = _desc;
            }
        }


        private static List<CommandInfo> _commands = null;

        public static List<CommandInfo> Commands
        {
            get
            {
                if (_commands == null)
                {
                    List<CommandInfo> list = new List<CommandInfo>();
                    list.Add(new CommandInfo(BalanceCommand.command, BalanceCommand.desc));
                    list.Add(new CommandInfo(ContractCommand.command, ContractCommand.desc));
                    list.Add(new CommandInfo(StakingCommand.command, StakingCommand.desc));
                    list.Add(new CommandInfo(SwapCommand.command, SwapCommand.desc));
                    list.Add(new CommandInfo(TokenCommand.command, TokenCommand.desc));
                    _commands = list;
                }
                return _commands;
            }
        }

        /// <summary>
        /// 获取帮助信息
        /// </summary>
        /// <returns></returns>
        public static string GetHelp()
        {
            StringBuilder sb = new StringBuilder("Usage:\n    don [command]\nAvailable Commands:\n");
            List<CommandHelper.CommandInfo> commands = CommandHelper.Commands;
            foreach (CommandHelper.CommandInfo item in commands)
            {
                sb.AppendLine($"    {item.command}\t{item.desc}");
            }
            return sb.ToString();
        }
    }
}
