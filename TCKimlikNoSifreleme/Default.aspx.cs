using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace TCKimlikNoSifreleme
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Yeni kullanıcı kayıt ederken bilgileri sifreleyerek kaydetme işlemi.
        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string tcNo = txtTCNo.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            string encryptedTCNo = Encrypt(tcNo);
            string hashedSifre = HashPassword(sifre);

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Kullanici (TCNo, Sifre) VALUES (@TCNo, @Sifre)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TCNo", encryptedTCNo);
                cmd.Parameters.AddWithValue("@Sifre", hashedSifre);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            Response.Write("<script>alert('TC Kimlik Numarası ve Şifre başarıyla şifrelenip kaydedildi.');</script>");
            txtTCNo.Text = "";
            txtSifre.Text = "";
        }

        //Doğrulanmış bilgiler ile sisteme giriş yapma.
        protected void btnGiris_Click(object sender, EventArgs e)
        {
            string tcNo = txtLoginTCNo.Text.Trim();
            string sifre = txtLoginSifre.Text.Trim();

            string encryptedTCNo = Encrypt(tcNo);
            string hashedSifre = HashPassword(sifre);

            bool isValid = ValidateUser(encryptedTCNo, hashedSifre);

            if (isValid)
            {
                Response.Write("<script>alert('Giriş başarılı.');</script>");
            }
            else
            {
                Response.Write("<script>alert('Geçersiz TC Kimlik Numarası veya Şifre.');</script>");
            }
        }

        //TCKN ve Sifre Doğrulama İslemi.
        private bool ValidateUser(string encryptedTCNo, string hashedSifre)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_ValidateUser", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EncryptedTCNo", encryptedTCNo);
                    cmd.Parameters.AddWithValue("@HashedSifre", hashedSifre);

                    con.Open();
                    int result = (int)cmd.ExecuteScalar();
                    con.Close();

                    return result == 1;
                }
            }
        }

        //TCKN Sifreleme İslemi.
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x65, 0x45, 0x78, 0x71, 0x33, 0x54, 0x00, 0x12, 0x13, 0x14, 0x15, 0x16 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        //Password Sifreleme İslemi.
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
