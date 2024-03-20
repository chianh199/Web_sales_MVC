using AutoMapper;
using MVC2.Data2;
using MVC2.Interfaces;
using MVC2.ViewModels;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.Cookies;
using MVC2.Helpers;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;


namespace MVC2.Repository
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly Food2Context _db;
        private readonly IMapper _mapper;
        public KhachHangRepository(Food2Context context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public bool AccountExists(string email)
        {
            var account = _db.Khachhangs.SingleOrDefault(a => a.Email == email);
            return account != null;
        }

        public bool DangKy(RegisterVM register)
        {
            var kh = _mapper.Map<Khachhang>(register);
            //kh.Id = db.Khachhangs.Max(k => k.Id) + 1;

            byte[] buffer = Encoding.UTF8.GetBytes(kh.Pwd);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            kh.Pwd = null;
            for (int i = 0; i < 9; i++)
            {
                kh.Pwd += buffer[i].ToString("x1");
            }
            _db.Khachhangs.Add(kh);
            var save = _db.SaveChanges();
            return save > 0 ? true : false;
        }

        public LoginVM DangNhap(LoginVM login)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(login.password);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            login.password = null;
            for (int i = 0; i < 9; i++)
            {
                login.password += buffer[i].ToString("x1");
            }
            var user = _db.Khachhangs.SingleOrDefault(kh => kh.Email == login.username && kh.Pwd == login.password);

            var user_log = new LoginVM
            {                
                Fullname = user.Fullname,
                username = user.Email ?? "",
                password = user.Pwd ?? ""
            };
            return user_log;
        }
    }
} 
