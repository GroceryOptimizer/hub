﻿namespace Core.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Coordinates { get; set; } = null;

        public Vendor() { } // Required for EF Core

        public Vendor(string name, Coordinates coordinates)
        {
            Name = name;
            Coordinates = coordinates;
        }
    }
}
