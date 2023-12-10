using Microsoft.AspNetCore.Mvc;
using PagedList;
using Projek_SIM.Models;
using System.Data;

namespace Projek_SIM.Controllers
{
    public class IndustriController : Controller
    {
        PolmanAstraLibrary.PolmanAstraLibrary lib = new PolmanAstraLibrary.PolmanAstraLibrary("Server=ASUS;Database=ERP_PoltekAstra;User=NISA;Password=123;");

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        private object httpClient;

        public IActionResult Index(string cari, int? page)
        {
            int pageSize = 20; // Jumlah item per halaman
            int pageNumber = page ?? 1; // Nomor halaman saat ini

            List<Industri> Industri = new List<Industri>();
            if (string.IsNullOrEmpty(cari))
            {
                cari = " ";
                dt = lib.CallProcedure("vct_getAllIndustri", new string[] { cari });
            }
            else
            {
                dt = lib.CallProcedure("vct_getAllIndustri", new string[] { cari });
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Industri.Add(new Industri(Convert.ToInt32(dt.Rows[i][0].ToString()), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString()));
            }

            // Membuat objek IPagedList<Industri> dengan menggunakan ToPagedList
            IPagedList<Industri> pagedIndustri = Industri.ToPagedList(pageNumber, pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = pagedIndustri.TotalItemCount;
            ViewBag.TotalPages = pagedIndustri.PageCount;
            ViewBag.cari = cari; // Menyimpan nilai pencarian untuk ditampilkan kembali di view

            if (string.IsNullOrEmpty(cari))
            {
                return View(pagedIndustri);
            }
            else
            {
                return View("Index", pagedIndustri);
            }

        }
    }
}