namespace Projek_SIM.Models
{
    public class Pembimbing
    {
        public string pin_id { get; set; }
        public int ipr_id { get; set; }
        public string ipr_nama { get; set; }
        public string pin_nama { get; set; }
        public string pin_jabatan { get; set; }
        public string pin_email { get; set; }
        public string pin_password { get; set; }
        public string pin_status { get; set; }
        public virtual ICollection<Industri> Industri { get; set; }
        public virtual Industri vct_msids { get; set; }

        //Buat Create dan View Bag
        public Pembimbing()
        {
            this.Industri = new HashSet<Industri>();
        }

        //model untuk di POST
        //Urutan parameter sesuai database
        public Pembimbing(string pin_id, string pin_nama, string pin_email, string pin_jabatan, string ipr_nama, string pin_password, string pin_status, int ipr_id) 
        {
            this.pin_id  = pin_id;
            this.pin_nama = pin_nama;
            this.pin_email = pin_email;
            this.pin_jabatan = pin_jabatan;
            this.pin_password = pin_password;
            this.pin_status = pin_status;
            this.ipr_nama = ipr_nama;
            this.ipr_id = ipr_id;
        }

        //model untuk EDIT & CREATE
        public Pembimbing(string pin_id, int ipr_id, string pin_nama,
            string pin_jabatan, string pin_email, string pin_password)
        {
            this.pin_id = pin_id;
            this.ipr_id = ipr_id;
            this.pin_nama = pin_nama;
            this.pin_jabatan = pin_jabatan;
            this.pin_email = pin_email;
            this.pin_password = pin_password;
        }
        public Pembimbing(string pin_id)
        {
            this.pin_id = pin_id;
        }


        

    }
}
