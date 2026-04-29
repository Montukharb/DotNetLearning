using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;

namespace Basics.OOPS
{
    internal class Employee
    {
        public int id { get; set; }

        //change property name in json file
        [JsonPropertyName("user_Name")]
        public string? fullname { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public int age { get; set; }
        public string? empdepartment { get; set; }
        public double salary { get; set; }
        public string? city { get; set; } = null;

        [JsonIgnore]
        //ab password json searialize ma add nahi hogia
        public string? password { get; set; }

    }
    internal class SerializationAndDeSerialization
    {
        Employee emp = new()
        {
            //fill all data members of employee class
            id = 1,
            fullname = "John Doe",
            email = "john.doe@example.com",
            phone = "123-456-7890",
            age = 30,
            empdepartment = "IT",
            salary = 50000.0

        };

        internal string json;
        internal dynamic options = new JsonSerializerOptions
        {
            WriteIndented = true, //pretty format json file
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,  //ignore null value in json file
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper, //change property name in json file to snake case upper

        };
        internal SerializationAndDeSerialization()
        {


            json = JsonSerializer.Serialize(emp, options);
            WriteLine(json);

            var obj = JsonSerializer.Deserialize<Employee>(json, options);
            //WriteLine(obj);

        }




        public void TestJsonSchemaExporter()
        {
            // 'JsonSerializerOptions instance must specify a TypeInfoResolver setting before being marked as read-only.'
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IncludeFields = true, //normally properties are serialized but if you want to serialize fields also then set this property to true
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, //by default json serializer escape special characters but if you want to avoid that then set this property to UnsafeRelaxedJsonEscaping optional
                TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver()
                //c# class ka meta data json schema ma convert karne ke liye TypeInfoResolver property set karna zaruri hai
            };

            var node = JsonSchemaExporter.GetJsonSchemaAsNode(options, typeof(Employee)); //c# class ka meta data json schema ma convert karne ke liye GetJsonSchemaAsNode method ka use karna zaruri hai
            string jsonSchema = node.ToJsonString(options);
            WriteLine(jsonSchema);
        }

            
    }
}
