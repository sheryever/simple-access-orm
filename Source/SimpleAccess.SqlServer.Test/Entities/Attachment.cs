using SimpleAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess.SqlServer.TestNetCore2.Entities
{
    [Entity("Attachments")]
    public partial class Attachment
    {
        #region Properties
        [Identity]
        [Display(Name = "Id")]
        [Required]
        public int Id { get; set; }

        [Display(Name = "IncidentId")]
        [Required]
        public int IncidentId { get; set; }

        [Display(Name = "OtherName")]
        [Required]
        [MaxLength(50)]
        public string OtherName { get; set; }

        #endregion Properties
    }
}
