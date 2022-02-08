﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcerciseApp.API.Domain.Models
{
    [Table("CardioExcercise")]
    public class CardioExcercise : BaseExcercise
    {
        public TimeSpan Duration { get; set; }
        public double Distance{ get; set; }
    }
}
