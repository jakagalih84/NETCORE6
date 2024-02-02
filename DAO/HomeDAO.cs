using Dapper;
using Microsoft.AspNetCore.Mvc;
using PELATIHANAPLIKASI.Models;
using System.Data.SqlClient;
using System.Reflection;

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

        public List<PesanModel> GetPesan()
        {
            using (SqlConnection conn = new SqlConnection(DBkoneksi.siatmakoneksi))
            {
                try
                {
                    string query = @"SELECT a.id_pesan, a.id_pengirim, a.isi_pesan, a.tgl, b.nama_user
                                    FROM [dbo].[Pesan] a
                                    JOIN [dbo].[Users] b ON b.id_user = a.id_pengirim
                                    ORDER BY a.tgl desc";

                    //var param = new { username = npm };
                    var data = conn.Query<PesanModel>(query).AsList();

                    return data;
                }
                catch (Exception ex)
                {
                    return new List<PesanModel>();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<ChatRoomModel> GetChatRoomUser(string id_user)
        {
            using (SqlConnection conn = new SqlConnection(DBkoneksi.siatmakoneksi))
            {
                try
                {
                    string query = @"SELECT a.nama_ruang, a.id_ruang, a.deskripsi
                                    FROM ChatRoom a
                                    JOIN [dbo].[UserChatRoom] b ON a.id_ruang = b.id_ruang AND b.id_user = @id_user";

                    //var param = new { username = npm };
                    var data = conn.Query<ChatRoomModel>(query, new { id_user = id_user }).AsList();

                    return data;
                }
                catch (Exception ex)
                {
                    return new List<ChatRoomModel>();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
