﻿using System.ComponentModel.DataAnnotations;

namespace Core.Base;
public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
