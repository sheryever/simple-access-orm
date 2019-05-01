using SimpleAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccessNetCore.SqlServer.ConsoleTest
{
    [Entity("Incidents")]
    public partial class Incident
    {
        #region Properties
        [Identity]
        [Display(Name = "Id")]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        #endregion Properties
    }

}
