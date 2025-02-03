﻿namespace Core.DTOs
{
    public class VendorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CoordinatesId { get; set; }
        public CoordinatesDTO Coordinates { get; set; }
    }
}
