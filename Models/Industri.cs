namespace Projek_SIM.Models
{
    public class Industri
    {
        public int ipr_id { get; set; }
        public string ipr_nama { get; set; }
        public string ipr_cabang { get; set; }
        public string ipr_alamat { get; set; }
        public string ipr_grup { get; set; }
        public string ipr_status { get; set; }

        public Industri() { }

        public Industri(int ipr_id, string ipr_nama) 
        { 
            this.ipr_id = ipr_id;
            this.ipr_nama = ipr_nama;
        }

        public Industri(int ipr_id, string ipr_nama, string ipr_grup, string ipr_alamat)
        {
            this.ipr_id = ipr_id;
            this.ipr_nama = ipr_nama;
            this.ipr_grup = ipr_grup;
            this.ipr_alamat = ipr_alamat;
        }

    }
}
