using Microsoft.AspNetCore.Mvc;
using Projek_SIM.Models;
using System.Data;

namespace Projek_SIM.Controllers
{
    public class ProdiController : Controller
    {
        PolmanAstraLibrary.PolmanAstraLibrary lib = new PolmanAstraLibrary.PolmanAstraLibrary("Server=ASUS;Database=ERP_PoltekAstra;User=NISA;Password=123;");

        DataTable dt = new DataTable();


        public IActionResult Index()
        {
            List<Prodi> prodi = new List<Prodi>();
            dt = lib.CallProcedure("vct_getProdi", new string[] { });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Prodi item = new Prodi();
                item.pro_id = Convert.ToInt32(dt.Rows[i][0].ToString());
                item.pro_nama = dt.Rows[i][2].ToString();
                prodi.Add(item);
            }

            return View(prodi);
        }
    }

    nisa cinta labib
}
