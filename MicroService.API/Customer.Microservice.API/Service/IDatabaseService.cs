namespace Customer.Microservice.API.Service
{
    public interface IDatabaseService
    {
        Task AddColumnToTableAsync(string tableName, string columnName, string columnType, bool allowNull, string? defaultValue);
        void AddPropertyToClass(string tableName, string columnName, string columnType);
    }
}
