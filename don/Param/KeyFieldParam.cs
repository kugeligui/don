namespace DON.Param
{
    public class KeyFieldParam
    {
        public string key { get; set; }

        public string field { get; set; }

        public KeyFieldParam()
        {

        }

        public KeyFieldParam(string _key, string _field)
        {
            key = _key;
            field = _field;
        }
    }
}
