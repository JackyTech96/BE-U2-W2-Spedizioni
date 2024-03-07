using BE_U2_W2_Spedizioni.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE_U2_W2_Spedizioni.Controllers
{
    public class SignupController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Agenzia_SpedizioniDB"].ConnectionString;

        // GET: Signup
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Signup
        [HttpPost]
        public ActionResult Signup(Utente nuovoUtente)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();

                string query = "INSERT INTO Utenti(Username, Password) " + "VALUES (@Username, @Password)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("Username", nuovoUtente.Username);
                cmd.Parameters.AddWithValue("Password", nuovoUtente.Password);

                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {

                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Benvenuto", "Login");
        }
    }
}