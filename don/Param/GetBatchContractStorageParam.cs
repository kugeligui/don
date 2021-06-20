namespace DON.Param
{
    public class GetBatchContractStorageParam : BaseParam
    {
        public string id { get; set; }

        public KeyFieldParam[] key_fields { get; set; }
    }
}
