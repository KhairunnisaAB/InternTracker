using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projek_SIM.Models;
using System.Data;
using PagedList;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projek_SIM.Controllers
{
    public class PembimbingController : Controller
    {
        PolmanAstraLibrary.PolmanAstraLibrary lib = new PolmanAstraLibrary.PolmanAstraLibrary("Server=ASUS;Database=ERP_PoltekAstra;User=NISA;Password=123;");

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        private object httpClient;

        public IActionResult Index(string cari, int? page)
        {
            int pageSize = 10; // Jumlah item per halaman
            int pageNumber = page ?? 1; // Nomor halaman saat ini

            List<Pembimbing> Pembimbing = new List<Pembimbing>();
            if (string.IsNullOrEmpty(cari))
            {
                cari = " ";
                dt = lib.CallProcedure("vct_getAllPembimbing", new string[] { cari });
            }
            else
            {
                dt = lib.CallProcedure("vct_getAllPembimbing", new string[] { cari });
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Pembimbing.Add(new Pembimbing(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), 
                    dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), Convert.ToInt32(dt.Rows[0][7].ToString())));
            }

            // Membuat objek IPagedList<Pembimbing> dengan menggunakan ToPagedList
            IPagedList<Pembimbing> pagedPembimbing = Pembimbing.ToPagedList(pageNumber, pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = pagedPembimbing.TotalItemCount;
            ViewBag.TotalPages = pagedPembimbing.PageCount;
            ViewBag.cari = cari; // Menyimpan nilai pencarian untuk ditampilkan kembali di view

            if (string.IsNullOrEmpty(cari))
            {
                return View(pagedPembimbing);
            }
            else
            {
                return View("Index", pagedPembimbing);
            }

        }


        // GET: Pembimbing/Create
        public async Task<ActionResult> Create()
        {
            Pembimbing vct_mspin = new Pembimbing();
            
            List<Industri> ids = new List<Industri>();
            dt = lib.CallProcedure("vct_getAllIndustri", new string[] { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ids.Add(new Industri(Convert.ToInt32(dt.Rows[i][0].ToString     ()), dt.Rows[i][1].ToString()));
            }

            // Pastikan bahwa ids tidak kosong sebelum membuat SelectList
            if (ids.Count > 0)
            {
                ViewBag.ipr_id = new SelectList(ids, "ipr_id", "ipr_nama");
            }

            return View(vct_mspin);
        }


        // POST: Pembimbing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pembimbing vct_msPembimbing)
        {

            try
            {
                dt = lib.CallProcedure("vct_createPembimbing", new string[] { vct_msPembimbing.pin_id, vct_msPembimbing.ipr_id.ToString(), vct_msPembimbing.pin_nama,
                    vct_msPembimbing.pin_jabatan, vct_msPembimbing.pin_email, vct_msPembimbing.pin_password });
                TempData["SuccessMessage"] = "Data berhasil ditambahkan.";
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }              

            return View(vct_msPembimbing);
        }

        // GET: Pembimbing/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Pembimbing");
            }

            // Ambil data Pembimbing dari database berdasarkan id
            dt = lib.CallProcedure("vct_getAllPembimbing", new string[] { id.ToString() });
            Pembimbing pembimbing = new Pembimbing(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(),
                dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString(), Convert.ToInt32(dt.Rows[0][7].ToString()));


            // Buat SelectList untuk dropdown
            List<Industri> industriList = new List<Industri>();

            dt = lib.CallProcedure("vct_getAllIdustri", new string[] { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                industriList.Add(new Industri(Convert.ToInt32(dt.Rows[i][0].ToString()), dt.Rows[i][1].ToString()));
            }

            if (industriList.Count > 0)
            {
                ViewBag.ipr_id = new SelectList(industriList, "ipr_id", "ipr_nama", pembimbing.ipr_id);

                ViewBag.PembimbingId = pembimbing.ipr_nama;
                ViewBag.PembimbingNm = pembimbing.ipr_id;// Tambahkan ini untuk melihat nilai di halaman web
            }

            return View(pembimbing);
        }


        // POST: Pembimbing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Pembimbing vct_msPembimbing)
        {
                dt = lib.CallProcedure("vct_editPembimbing", new string[] { vct_msPembimbing.pin_id, vct_msPembimbing.ipr_id.ToString(), vct_msPembimbing.pin_nama,
                     vct_msPembimbing.pin_jabatan, vct_msPembimbing.pin_email, vct_msPembimbing.pin_password });
                TempData["SuccessMessage"] = "Data berhasil diedit.";
                return RedirectToAction("Index");
        
            return View(vct_msPembimbing);
        }

        [HttpPost]
        public ActionResult Hapus(string id)
        {
            try
            {
                if (id != null)
                {
                    Console.WriteLine("Masuk");
                    dt = lib.CallProcedure("vct_hapusPembimbing", new string[] { id.ToString() });
                    return Json(new { success = true, message = "Data Berhasil Dihapus." });
                }
                else
                {
                    return Json(new { success = false, message = "Data tidak ditemukan." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Gagal memperbarui status: " + ex.Message });
            }
        }
    }
}
