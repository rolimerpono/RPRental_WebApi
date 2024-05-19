﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using DataServices.Common.DTO.Room;

namespace DataServices.Common.DTO.RoomNumber
{
    public class RoomNumberDTO
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoomNo { get; set; }


        [MaxLength(500)]
        public string? Description { get; set; }


        [ForeignKey("RoomDTO")]
        public int RoomId { get; set; }

        [ValidateNever]
        public RoomDTO? RoomDTO { get; set; }

    }

}
