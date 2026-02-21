using NF.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NF.ViewModel.Models.Schedule
{
   public class ScheduleDetailAttachmentViewDTO: ScheduleDetailAttachment
    {
        public string CreateUserName { get; set; }
        public string CategoryName { get; set; }
    }
}
