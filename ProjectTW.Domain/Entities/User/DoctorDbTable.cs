using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.Domain.Entities.User
{
    public class DoctorDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(30, ErrorMessage = "Username cannot be longer than 30 characters")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "Password cannot be shorter than 8 characteres")]
        public string Password { get; set; }

        [Required]
        public DoctorSpeciality Specilality { get; set; }

        public string ProfileImage {  get; set; }

        public string Biography { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        public UserRole Level { get; set; }
    }
}
