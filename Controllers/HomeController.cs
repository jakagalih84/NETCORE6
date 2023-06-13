using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PELATIHANAPLIKASI.DAO;
using PELATIHANAPLIKASI.Models;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;

namespace PELATIHANAPLIKASI.Controllers
{
    [Authorize(Roles = "Mahasiswa")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HomeDAO dao;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            dao = new HomeDAO();
        }

        public IActionResult Index()
        {
            var data = dao.GetMahasiswa();

            return View(data);
        }

        public IActionResult Detail(string id)
        {
            dynamic objek = new ExpandoObject();
            var data = dao.GetMahasiswaDetail(id);

            objek.Data = data;
            objek.fakultas = "";

            return View(objek);
        }

        public IActionResult Tambah()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Tambah(MahasiswaModel model)
        {
            model.PASSWORD = _encPassMhs(model.PASSWORD);
            dao.InsertMahasiswa(model);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult JsonObjek(string id)
        {
            dynamic objek = new ExpandoObject();
            var data = dao.GetMahasiswaDetail(id);

            objek.Data = data;
            return Json(objek);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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