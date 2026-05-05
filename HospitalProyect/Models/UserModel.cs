using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HospitalProyect.Models
{
	public class UserModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "formato de correo invalido")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }



		[Required(ErrorMessage = "Debes asignar un rol")]
		public int RoleId { get; set; }

		[ValidateNever]
		public RoleModel Role { get; set; }
	}
}
