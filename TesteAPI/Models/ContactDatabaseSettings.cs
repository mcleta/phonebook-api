﻿namespace ContactApi.Models
{
    public class ContactDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string Database { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
