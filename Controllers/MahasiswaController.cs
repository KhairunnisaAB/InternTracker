using Microsoft.AspNetCore.Mvc;
using Projek_SIM.Models;
using System.Data;
using PolmanAstraLibrary;
using PagedList; 
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projek_SIM.Controllers
{
    public class MahasiswaController : Controller
    {
        PolmanAstraLibrary.PolmanAstraLibrary lib = new PolmanAstraLibrary.PolmanAstraLibrary("Server=ASUS;Database=ERP_PoltekAstra;User=NISA;Password=123;");

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        private object httpClient;

        public IActionResult Index(string cari, int? page)
        {
            int pageSize = 20; // Jumlah item per halaman
            int pageNumber = page ?? 1; // Nomor halaman saat ini

            List<Mahasiswa> Mahasiswa = new List<Mahasiswa>();
            if (string.IsNullOrEmpty(cari))
            {
                cari = " ";
                dt = lib.CallProcedure("vct_getAllMahasiswa", new string[] { cari });
            }
            else
            {
                dt = lib.CallProcedure("vct_getAllMahasiswa", new string[] { cari });
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Mahasiswa.Add(new Mahasiswa(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), Convert.ToInt32(dt.Rows[i][2].ToString()), dt.Rows[i][3].ToString(),
                    dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString()));
            }

            // Membuat objek IPagedList<Mahasiswa> dengan menggunakan ToPagedList
            IPagedList<Mahasiswa> pagedMahasiswa = Mahasiswa.ToPagedList(pageNumber, pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = pagedMahasiswa.TotalItemCount;
            ViewBag.TotalPages = pagedMahasiswa.PageCount;
            ViewBag.cari = cari; // Menyimpan nilai pencarian untuk ditampilkan kembali di view

            if (string.IsNullOrEmpty(cari))
            {
                return View(pagedMahasiswa);
            }
            else
            {
                return View("Index", pagedMahasiswa);
            }

        }

        public async Task<ActionResult> Detail(string? NIM)
        {
            Console.WriteLine(NIM);

            if (string.IsNullOrEmpty(NIM))
            {
                return RedirectToAction("Detail");
            }

            // Ambil data Pembimbing dari database berdasarkan id
            dt = lib.CallProcedure("vct_getDetailMahasiswa", new string[] { NIM.ToString() });
            Mahasiswa mahasiswa = new Mahasiswa(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(),
                                dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), Convert.ToDecimal(dt.Rows[0][6].ToString()));

            ViewBag.Mahasiswa = mahasiswa;
            ViewBag.nim = mahasiswa.mhs_id;
            ViewBag.nama = mahasiswa.mhs_nama;
            ViewBag.prodi = mahasiswa.pro_singkatan;

            return View(mahasiswa);
        }
        
    }

}

