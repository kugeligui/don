namespace DON.Param
{
    public class BaseParam
    {
        public bool by_longest_chain { get; set; }

        public override string ToString()
        {
            return base.ToString().ToLower();
        }
    }
}
