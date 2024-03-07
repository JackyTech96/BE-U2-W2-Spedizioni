using BE_U2_W2_Spedizioni.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace BE_U2_W2_Spedizioni.Controllers
{
    public class AggiornamentoStatoSpedizioneController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Agenzia_SpedizioniDB"].ConnectionString;

        public ActionResult Details(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            string query = "SELECT * FROM AggiornamentiStatoSpedizione WHERE IDSpedizione = @IDSpedizione";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@IDSpedizione", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                AggiornamentoStatoSpedizione aggiornamento = new AggiornamentoStatoSpedizione
                {
                    IDSpedizione = Convert.ToInt32(reader["IDSpedizione"]),
                    Stato = reader["Stato"].ToString(),
                    Luogo = reader["Luogo"].ToString(),
                    DescrizioneEventuale = reader["DescrizioneEventuale"].ToString(),
                    DataAggiornamento = Convert.ToDateTime(reader["DataAggiornamento"])
                };

                return View(aggiornamento);
            }
            else
            {
                // Se l'aggiornamento non esiste, ritorna una vista 404
                return HttpNotFound();
            }
        }

       
        private int GetNumeroTotaleSpedizioniInAttesa()
        {
            int numeroTotale = 0;

            SqlConnection conn = new SqlConnection(connectionString);

            // Implementa la logica per ottenere il numero totale di spedizioni in attesa di consegna dal tuo database
            try
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM AggiornamentiStatoSpedizione WHERE Stato != 'Consegnato'";
                SqlCommand cmd = new SqlCommand(query, conn);

                numeroTotale = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
                Console.WriteLine($"Errore durante il recupero del numero totale di spedizioni: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return numeroTotale;
        }

        [HttpGet]
        public ActionResult NumeroTotaleSpedizioniInAttesa()
        {
            int numeroTotale = GetNumeroTotaleSpedizioniInAttesa(); // Implementa questa logica

            return View("NumeroTotaleSpedizioniInAttesa", numeroTotale);
        }

    }
}
