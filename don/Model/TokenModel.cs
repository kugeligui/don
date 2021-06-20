namespace DON.Model
{
    public class TokenModel
    {
        public string token { get; set; }

        public double value { get; set; }

        public TokenModel()
        {

        }

        public TokenModel(string _token)
        {
            token = _token;
        }

        public TokenModel(string _token, double _value)
        {
            token = _token;
            value = _value;
        }
    }
}
