using Dapper;
using PELATIHANAPLIKASI.Models;
using System.Data.SqlClient;

namespace PELATIHANAPLIKASI.DAO
{
    public class HomeDAO
    {
        public List<MahasiswaModel> GetMahasiswa()
        {
            using (SqlConnection conn = new SqlConnection(DBkoneksi.siatmakoneksi))
            {
                try
                {
                    string query = @"SELECT TOP 20 a.NPM, a.PASSWORD, 'Mahasiswa' AS ROLE, a.NAMA_MHS
                                    FROM MST_MHS_AKTIF a ORDER BY NOMHS DESC";

                    //var param = new { username = npm };
                    var data = conn.Query<MahasiswaModel>(query).ToList();

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

        public dynamic GetMahasiswaDetail(string npm)
        {
            using (SqlConnection conn = new SqlConnection(DBkoneksi.siatmakoneksi))
            {
                try
                {
                    string query = @"SELECT *
                                    FROM MST_MHS_AKTIF a 
                                    WHERE a.NPM = @username";

                    var param = new { username = npm };
                    var data = conn.QueryFirstOrDefault<dynamic>(query, param);

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

        public bool InsertMahasiswa(MahasiswaModel model)
        {
            using (SqlConnection conn = new SqlConnection(DBkoneksi.siatmakoneksi))
            {
                try
                {
                    string query = @"INSERT INTO MST_MHS_AKTIF (NPM, NAMA_MHS, PASSWORD, ISMASUKMIDDLE) 
                                    VALUES(@NPM, @NAMA_MHS, @PASSWORD, 0)";

                    //var param = new { username = npm };
                    var data = conn.Execute(query, model);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
