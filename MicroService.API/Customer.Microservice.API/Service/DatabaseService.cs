
using Customer.Microservice.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Customer.Microservice.API.Service
{
    public class DatabaseService : IDatabaseService
    {
        private readonly CustomerDbContext _customerDbContext;
        public DatabaseService(CustomerDbContext customerDbContext) 
        {
            _customerDbContext = customerDbContext;
        }    
        public async Task AddColumnToTableAsync(string tableName, string columnName, string columnType, bool allowNull, string? DefaultValue)
        {
            var sql = $"ALTER TABLE {tableName} ADD {columnName} {columnType}";
            await _customerDbContext.Database.ExecuteSqlRawAsync(sql);
        }

        //public void AddPropertyToClass(string tableName, string columnName, string columnType)
        //{

        //    string modelFolderPath = @"D:\MProject\MyProject\MicroService.API\Customer.Microservice.API\Model";


        //    // Build the full path to the class file
        //    string classFilePath = Path.Combine(modelFolderPath, $"{tableName}.cs");

        //    if (File.Exists(classFilePath))
        //    {
        //        var currentContent = File.ReadAllText(classFilePath);
        //        if (!currentContent.Contains($"public {columnType} {columnName}"))
        //        {
        //            var propertyLine = $"    public {MapSqlTypeToCSharp(columnType)} {columnName} {{ get; set; }}";
        //            var updatedContent = currentContent.Replace("}", $"{propertyLine}\n}}");
        //            File.WriteAllText(classFilePath, updatedContent);
        //        }
        //    }
        //    else
        //    {
        //        var classContent = $@"namespace Customer.Microservice.API.Model
        //        {{
        //             public class {tableName}
        //             {{
        //                public {MapSqlTypeToCSharp(columnType)} {columnName} {{ get; set; }}
        //             }}
        //        }}";
        //        File.WriteAllText(classFilePath, classContent);
        //    }
        //}

        //private string MapSqlTypeToCSharp(string sqlType)
        //{
        //    return sqlType switch
        //    {
        //        "VARCHAR(100)" => "string",
        //        "INT" => "int",
        //        _ => "object" // Add more mappings as needed
        //    };
        //}











        //public void AddPropertyToClass(string tableName, string columnName, string columnType)
        //{
        //    // Define the path to the Models folder
        //    string modelFolderPath = @"D:\MProject\MyProject\MicroService.API\Customer.Microservice.API\Model";

        //    // Build the full path to the class file
        //    string classFilePath = Path.Combine(modelFolderPath, $"{tableName}.cs");

        //    if (File.Exists(classFilePath))
        //    {
        //        // Read the current content of the class file
        //        var currentContent = File.ReadAllText(classFilePath);

        //        // Check if the property already exists
        //        if (!currentContent.Contains($"public {MapSqlTypeToCSharp(columnType)} {columnName}"))
        //        {
        //            // Find the start of the class definition
        //            string classDefinition = $"public class {tableName}";
        //            int classStartIndex = currentContent.IndexOf(classDefinition);

        //            // If we found the class definition, now find the closing brace for the class
        //            if (classStartIndex >= 0)
        //            {
        //                // Find the last closing brace within the class body
        //                int classClosingBraceIndex = currentContent.LastIndexOf("}");

        //                // Ensure that we are inserting before the last closing brace of the class
        //                if (classClosingBraceIndex > 0 && classStartIndex < classClosingBraceIndex)
        //                {
        //                    // Construct the new property
        //                    var propertyLine = $"\n        public {MapSqlTypeToCSharp(columnType)} {columnName} {{ get; set; }}";

        //                    // Insert the new property just before the last closing brace of the class
        //                    string updatedContent = currentContent.Substring(0, classClosingBraceIndex) + propertyLine + currentContent.Substring(classClosingBraceIndex);

        //                    // Write the updated content back to the file
        //                    File.WriteAllText(classFilePath, updatedContent);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Create a new class file if it does not exist
        //        var classContent = $@"namespace Customer.Microservice.API.Model
        //{{
        //    public class {tableName}
        //    {{
        //        public {MapSqlTypeToCSharp(columnType)} {columnName} {{ get; set; }}
        //    }}
        //}}";
        //        File.WriteAllText(classFilePath, classContent);
        //    }
        //}

        //private string MapSqlTypeToCSharp(string sqlType)
        //{
        //    // Map SQL types to C# types
        //    return sqlType switch
        //    {
        //        "VARCHAR(100)" => "string",
        //        "INT" => "int",
        //        "FLOAT" => "float",
        //        "DATETIME" => "DateTime",
        //        "BIT" => "bool",
        //        _ => "object" // Default type if mapping is not specified
        //    };
        //}







        //        public void AddPropertyToClass(string tableName, string columnName, string columnType)
        //        {
        //            // Define the path to the Models folder
        //            string modelFolderPath = @"D:\MProject\MyProject\MicroService.API\Customer.Microservice.API\Model";

        //            // Build the full path to the class file
        //            string classFilePath = Path.Combine(modelFolderPath, $"{tableName}.cs");

        //            if (File.Exists(classFilePath))
        //            {
        //                // Read the current content of the class file
        //                var currentContent = File.ReadAllText(classFilePath);

        //                // Check if the property already exists
        //                if (!currentContent.Contains($"public {MapSqlTypeToCSharp(columnType)} {columnName}"))
        //                {
        //                    // Find the start of the class definition
        //                    string classDefinition = $"public class {tableName}";
        //                    int classStartIndex = currentContent.IndexOf(classDefinition);

        //                    // If we found the class definition, now find the second last closing brace for the class
        //                    if (classStartIndex >= 0)
        //                    {
        //                        // Find the first closing brace from the end
        //                        int lastClosingBraceIndex = currentContent.LastIndexOf("}");

        //                        // Find the second last closing brace by searching before the first one
        //                        int secondLastClosingBraceIndex = currentContent.LastIndexOf("}", lastClosingBraceIndex - 1);

        //                        // Ensure that we are inserting before the second last closing brace of the class
        //                        if (secondLastClosingBraceIndex > 0 && classStartIndex < secondLastClosingBraceIndex)
        //                        {
        //                            // Construct the new property
        //                            var propertyLine = $"\n        public {MapSqlTypeToCSharp(columnType)} {columnName} {{ get; set; }}";

        //                            // Insert the new property just before the second last closing brace of the class
        //                            string updatedContent = currentContent.Substring(0, secondLastClosingBraceIndex) + propertyLine + currentContent.Substring(secondLastClosingBraceIndex);

        //                            // Write the updated content back to the file
        //                            File.WriteAllText(classFilePath, updatedContent);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // Create a new class file if it does not exist
        //                var classContent = $@"namespace Customer.Microservice.API.Model
        //{{
        //    public class {tableName}
        //    {{
        //        public {MapSqlTypeToCSharp(columnType)} {columnName} {{ get; set; }}
        //    }}
        //}}";
        //                File.WriteAllText(classFilePath, classContent);
        //            }
        //        }

        //        private string MapSqlTypeToCSharp(string sqlType)
        //        {
        //            // Map SQL types to C# types
        //            return sqlType switch
        //            {
        //                "VARCHAR(100)" => "string",
        //                "INT" => "int",
        //                "FLOAT" => "float",
        //                "DATETIME" => "DateTime",
        //                "BIT" => "bool",
        //                _ => "object" // Default type if mapping is not specified
        //            };
        //        }





        public void AddPropertyToClass(string tableName, string columnName, string columnType)
        {
            string modelFolderPath = @"D:\MProject\MyProject\MicroService.API\Customer.Microservice.API\Model";

            string classFilePath = Path.Combine(modelFolderPath, $"{tableName}.cs");

            if (File.Exists(classFilePath))
            {
                var currentContent = File.ReadAllText(classFilePath);

                if (!currentContent.Contains($"public {MapSqlTypeToCSharp(columnType)} {columnName}"))
                {
                    string classDefinition = $"public class {tableName}";
                    int classStartIndex = currentContent.IndexOf(classDefinition);

                    if (classStartIndex >= 0)
                    {
                        int lastClosingBraceIndex = currentContent.LastIndexOf("}");
                        int secondLastClosingBraceIndex = currentContent.LastIndexOf("}", lastClosingBraceIndex - 1);

                        if (secondLastClosingBraceIndex > 0 && classStartIndex < secondLastClosingBraceIndex)
                        {
                            var propertyLine = $"\n        public {MapSqlTypeToCSharp(columnType)} {columnName} {{ get; set; }}";

                            // Insert the new property just before the second last closing brace of the class
                            string updatedContent = currentContent.Substring(0, secondLastClosingBraceIndex) + propertyLine + "\n" + currentContent.Substring(secondLastClosingBraceIndex);

                            File.WriteAllText(classFilePath, updatedContent);
                        }
                    }
                }
            }
            else
            {
                var classContent = $@"namespace Customer.Microservice.API.Model
{{
    public class {tableName}
    {{
        public {MapSqlTypeToCSharp(columnType)} {columnName} {{ get; set; }}
    }}
}}";
                File.WriteAllText(classFilePath, classContent);
            }
        }

        private string MapSqlTypeToCSharp(string sqlType)
        {
            // Map SQL types to C# types
            return sqlType switch
            {
                "VARCHAR(100)" => "string",
                "INT" => "int",
                "FLOAT" => "float",
                "DATETIME" => "DateTime",
                "BIT" => "bool",
                _ => "object" 
            };
        }

    }
}
