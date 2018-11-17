using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LoginObjects
{
    public partial class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }
    }
}
