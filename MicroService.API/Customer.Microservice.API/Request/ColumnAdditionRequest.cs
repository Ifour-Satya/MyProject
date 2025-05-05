namespace Customer.Microservice.API.Request
{
    public class ColumnAdditionRequest
    {
        public string TableName { get; set; }
        public List<AddColumnRequest> Columns { get; set; }
    }
}
