using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.DTOs.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Ad alanı boş geçilemez.")]
        [MaxLength(30, ErrorMessage ="Ad alanı en fazla 30 karakter olabilir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş geçilemez.")]
        [MaxLength(30, ErrorMessage = "Soyad alanı en fazla 30 karakter olabilir.")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Kullanıcı adı alanı boş geçilemez.")]
        [MinLength(3, ErrorMessage = "Kullanıcı adı en az 3 karakter olabilir.")]
        [MaxLength(20, ErrorMessage = "Kullanıcı adı en fazla 20 karakter olabilir.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Kullanıcı adı sadece harf ve rakamlardan oluşabilir.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email alanı boş geçilemez.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola alanı boş geçilemez.")]
        [MinLength(6, ErrorMessage = "Parola en az 6 karakter olabilir.")]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
