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
    [Authorize]
    public class SpedizioneController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Agenzia_SpedizioniDB"].ConnectionString;

        // GET: Spedizione
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Spedizione>listaSpedizioni = new List<Spedizione>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Spedizioni";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Spedizione spedizione = new Spedizione
                    {
                        IDSpedizione = Convert.ToInt32(reader["IDSpedizione"]),
                        IDCliente = Convert.ToInt32(reader["IDCliente"]),
                        NumeroIdentificativo = Convert.ToInt32(reader["NumeroIdentificativo"]),
                        DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]),
                        Peso = Convert.ToDecimal(reader["Peso"]),
                        Citta = reader["Citta"].ToString(),
                        Indirizzo = reader["Indirizzo"].ToString(),
                        NominativoDestinatario = reader["NominativoDestinatario"].ToString(),
                        CostoSpedizione = Convert.ToDecimal(reader["CostoSpedizione"]),
                        DataConsegnaPrevista = Convert.ToDateTime(reader["DataConsegnaPrevista"])
                    };
                    listaSpedizioni.Add(spedizione);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally 
            { 
                conn.Close(); 
            }

            return View(listaSpedizioni);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Spedizione nuovaSpedizione)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();

                string query = "INSERT INTO Spedizioni (IDCliente, NumeroIdentificativo, DataSpedizione, Peso, Citta, Indirizzo, NominativoDestinatario, CostoSpedizione, DataConsegnaPrevista) " +
                               "VALUES (@IDCliente, @NumeroIdentificativo, @DataSpedizione, @Peso, @Citta, @Indirizzo, @NominativoDestinatario, @CostoSpedizione, @DataConsegnaPrevista)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@IDCliente", nuovaSpedizione.IDCliente);
                cmd.Parameters.AddWithValue("@NumeroIdentificativo", nuovaSpedizione.NumeroIdentificativo);
                cmd.Parameters.AddWithValue("@DataSpedizione", nuovaSpedizione.DataSpedizione);
                cmd.Parameters.AddWithValue("@Peso", nuovaSpedizione.Peso);
                cmd.Parameters.AddWithValue("@Citta", nuovaSpedizione.Citta);
                cmd.Parameters.AddWithValue("@Indirizzo", nuovaSpedizione.Indirizzo);
                cmd.Parameters.AddWithValue("@NominativoDestinatario", nuovaSpedizione.NominativoDestinatario);
                cmd.Parameters.AddWithValue("@CostoSpedizione", nuovaSpedizione.CostoSpedizione);
                cmd.Parameters.AddWithValue("@DataConsegnaPrevista", nuovaSpedizione.DataConsegnaPrevista);

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

            return RedirectToAction("Index");
        }

        

        private List<Spedizione> GetSpedizioniOggi()
        {
            DateTime oggi = DateTime.Today;
            List<Spedizione> spedizioniOggi = new List<Spedizione>();

            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "SELECT * FROM Spedizioni WHERE CONVERT(DATE, DataConsegnaPrevista) = @Oggi";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Oggi", oggi);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Spedizione spedizione = new Spedizione
                    {
                        IDSpedizione = Convert.ToInt32(reader["IDSpedizione"]),
                        IDCliente = Convert.ToInt32(reader["IDCliente"]),
                        NumeroIdentificativo = Convert.ToInt32(reader["NumeroIdentificativo"]),
                        DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]),
                        Peso = Convert.ToDecimal(reader["Peso"]),
                        Citta = reader["Citta"].ToString(),
                        Indirizzo = reader["Indirizzo"].ToString(),
                        NominativoDestinatario = reader["NominativoDestinatario"].ToString(),
                        CostoSpedizione = Convert.ToDecimal(reader["CostoSpedizione"]),
                        DataConsegnaPrevista = Convert.ToDateTime(reader["DataConsegnaPrevista"])
                    };
                    spedizioniOggi.Add(spedizione);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Errore durante il recupero delle spedizioni odierna: {ex.Message}"); ;
            }
            finally 
            { 
                conn.Close(); 
            }

            return spedizioniOggi;
        }

        [HttpGet]
        public ActionResult ConsegnaOdierna()
        {
            List<Spedizione> spedizioniOggi = GetSpedizioniOggi();
            return View("ConsegnaOdierna", spedizioniOggi);
        }

        private Dictionary<string, int> GetNumeroTotaleSpedizioniPerCitta()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "SELECT Citta, COUNT(*) AS NumeroTotaleSpedizioni " +
                               "FROM Spedizioni " +
                               "GROUP BY Citta";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string cittaDestinataria = reader["Citta"].ToString();
                    int numeroTotaleSpedizioni = Convert.ToInt32(reader["NumeroTotaleSpedizioni"]);

                    result.Add(cittaDestinataria, numeroTotaleSpedizioni);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il recupero del numero totale di spedizioni per città: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }


            return result;
        }


        [HttpGet]
        public ActionResult NumeroTotaleSpedizioniCitta()
        {
            Dictionary<string, int> result =GetNumeroTotaleSpedizioniPerCitta();
            return View("NumeroTotaleSpedizioniCitta", result);
        }
    }
}
