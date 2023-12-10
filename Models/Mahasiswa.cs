namespace Projek_SIM.Models
{
    public class Mahasiswa
    {   //atribut Mahasiswa  dan 
        public string mhs_id { get; set; }
        public string pro_singkatan { get; set; }
        public int kpr_id { get; set; }
        public string mhs_nama { get; set; }
        public int mhs_angkatan { get; set; }
        public string mhs_jenis_kelamin { get; set; }
        public string mhs_status { get; set; }
        public string ipr_grup { get; set; }
        
        // atribut Logbook
        public string create_date { get; set; }
        public string log_status { get; set; }

        //atribut Penilaian
        public string pnl_periode { get; set; }
        public decimal rata_nilai { get; set; }


        //model untuk di POST
        //Urutan parameter sesuai database
        public Mahasiswa(string mhs_id, string mhs_nama, int mhs_angkatan, string mhs_jenis_kelamin, string pro_singkatan, string mhs_status_kuliah)
        {
            this.mhs_id = mhs_id;
            this.mhs_nama = mhs_nama;
            this.mhs_angkatan = mhs_angkatan;
            this.mhs_jenis_kelamin = mhs_jenis_kelamin;
            this.pro_singkatan = pro_singkatan;
            this.mhs_status = mhs_status_kuliah;
        }

        public Mahasiswa(string mhs_id, string mhs_nama, string pro_singkatan,
            string create_date, string log_status, string pnl_periode, decimal rata_nilai )
        {
            this.mhs_id = mhs_id;
            this.mhs_nama = mhs_nama;
            this.pro_singkatan = pro_singkatan;
            this.create_date = create_date;
            this.log_status = log_status;
            this.pnl_periode = pnl_periode;
            this.rata_nilai = rata_nilai;
        }

    }
}
