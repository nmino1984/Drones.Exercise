﻿using System.ComponentModel.DataAnnotations;

namespace Drones.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
