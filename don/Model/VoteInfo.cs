namespace DON.Model
{
    public class VoteInfo
    {
        /// <summary>
        /// 候选人
        /// </summary>
        public string option { get; set; }

        /// <summary>
        /// 投票数
        /// </summary>
        public string votes { get; set; }

        /// <summary>
        /// 被清零投票数
        /// </summary>
        public string cleared_votes { get; set; }
    }
}
