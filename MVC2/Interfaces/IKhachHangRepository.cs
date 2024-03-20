using MVC2.ViewModels;
using System.Security.Claims;

namespace MVC2.Interfaces
{
    public interface IKhachHangRepository
    {
        public bool DangKy(RegisterVM register);

        public LoginVM DangNhap(LoginVM login);

        public bool AccountExists (string email);
    }
}
