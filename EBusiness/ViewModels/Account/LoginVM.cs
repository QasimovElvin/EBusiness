using System.ComponentModel.DataAnnotations;

namespace EBusiness.ViewModels.Account
{
    public class LoginVM
    {
        public string EmailOrUsername { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;
    }
}
