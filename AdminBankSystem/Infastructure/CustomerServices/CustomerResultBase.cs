namespace AdminBankSystem.Infastructure.CustomerServices
{
    public class CustomerResultBase
    {


        public class CustomerResult<T> : CustomerResultBase where T : class
        {
            public IList<T> Results { get; set; }

            public CustomerResult()
            {
                Results = new List<T>();
            }
        }
    }
}
