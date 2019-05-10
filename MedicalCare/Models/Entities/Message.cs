using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int ReceiverId { get; set; }

        public int SenderId { get; set; }

        public ApplicationUser User { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public MessageStatus Status { get; set; }

        public ReadStatus ReadStatus { get; set; }
    }
}
