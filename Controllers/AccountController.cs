using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PELATIHANAPLIKASI.DAO;
using PELATIHANAPLIKASI.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PELATIHANAPLIKASI.Controllers
{
    public class AccountController : Controller
    {
        AccountDAO dao;
        public AccountController()
        {
            dao = new AccountDAO();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult LoginAct(MahasiswaModel objek)
        {
            ClaimsIdentity identity;
            var datamhs = dao.GetMahasiswa(objek.NPM);
            if(datamhs != null)
            {
                // cek password
                var hashpass = _encPassMhs(objek.PASSWORD);
                if(hashpass == datamhs.PASSWORD)
                {
                    // password benar
                    identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.NameIdentifier , datamhs.NPM),
                        new Claim(ClaimTypes.Name, datamhs.NAMA_MHS),
                        new Claim(ClaimTypes.Role, datamhs.ROLE),
                        new Claim("username", datamhs.NPM),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(identity));
                }
                else
                {
                    // salah password
                }
            }

            return RedirectToAction("Index", "Home");
        }

        private string _encPassMhs(string password)
        {
            //password to RIPEMD160
            string hash = "";
            Encoding enc = Encoding.GetEncoding(1252);
            RIPEMD160 ripemdHasher = RIPEMD160.Create();
            byte[] data = ripemdHasher.ComputeHash(Encoding.Default.GetBytes(password));
            hash = enc.GetString(data);

            return hash;
        }
    }
}
