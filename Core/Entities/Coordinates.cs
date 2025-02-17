﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[ComplexType]
public class Coordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
