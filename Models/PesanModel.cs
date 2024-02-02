namespace PELATIHANAPLIKASI.Models
{
    public class PesanModel
    {
        public string id_pesan { get; set; }
        public string id_pengirim { get; set; }
        public string id_ruang { get; set; }
        public string isi_pesan { get; set; }
        public DateTime tgl { get; set; }
        public string nama_user { get; set; }
    }

    public class ChatRoomModel
    {
        public string id_ruang { get; set; }
        public string nama_ruang { get; set; }
        public string deskripsi { get; set; }
    }
}
