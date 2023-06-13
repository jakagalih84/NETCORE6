using Dapper;
using PELATIHANAPLIKASI.Models;
using System.Data.SqlClient;

namespace PELATIHANAPLIKASI.DAO
{
    public class AccountDAO
    {
        public MahasiswaModel GetMahasiswa(string npm)
        {
            using (SqlConnection conn = new SqlConnection(DBkoneksi.siatmakoneksi))
            {
                try
                {
                    string query = @"SELECT a.NPM, a.PASSWORD, 'Mahasiswa' AS ROLE, a.NAMA_MHS
                                    FROM MST_MHS_AKTIF a
                                    WHERE a.NPM = @username";

                    var param = new { username = npm };
                    var data = conn.QueryFirstOrDefault<MahasiswaModel>(query, param);

                    return data;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
