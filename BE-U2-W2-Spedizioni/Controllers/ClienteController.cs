using BE_U2_W2_Spedizioni.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE_U2_W2_Spedizioni.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Agenzia_SpedizioniDB"].ConnectionString;
        // GET: Cliente
        public ActionResult Index()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Cliente> listaClienti = new List<Cliente>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Clienti";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        IDCliente = Convert.ToInt32(reader["IDCliente"]),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        CodiceFiscale = reader["CodiceFiscale"].ToString(),
                        PartitaIVA = reader["PartitaIVA"].ToString(),
                        TipoCliente = reader["TipoCliente"].ToString()
                    };
                    listaClienti.Add(cliente);

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

            ViewBag.msgSuccess = TempData["msgSuccess"];
            return View(listaClienti);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cliente nuovoCliente)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "";

                if (nuovoCliente.TipoCliente == "Privato")
                {
                    query = ("INSERT INTO Clienti " +
                      "(Nome,Cognome, TipoCliente, CodiceFiscale) " +
                      "VALUES (@Nome,@Cognome, @TipoCliente, @CodiceFiscale)");
                }
                else if (nuovoCliente.TipoCliente == "Azienda")
                {
                    query = ("INSERT INTO Clienti " +
                      "(Nome,Cognome, TipoCliente, PartitaIVA) " +
                      "VALUES (@Nome,@Cognome, @TipoCliente, @PartitaIVA)");
                }
                // crea comando
                SqlCommand cmd = new SqlCommand(query, conn);

                // aggiungi parametri
                cmd.Parameters.AddWithValue("@Nome", nuovoCliente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", nuovoCliente.Cognome);
                cmd.Parameters.AddWithValue("@TipoCliente", nuovoCliente.TipoCliente);

                if (nuovoCliente.TipoCliente == "Privato")
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", nuovoCliente.CodiceFiscale);
                }
                else if (nuovoCliente.TipoCliente == "Azienda")
                {
                    cmd.Parameters.AddWithValue("@PartitaIVA", nuovoCliente.PartitaIVA);
                }

                // esegui comando
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ViewBag.msgErrore = "Errore: " + ex.Message;
            }
            finally
            {
                conn.Close();
            }

            TempData["msgSuccess"] = "Cliente " + nuovoCliente.Nome + " creato con successo!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Cliente clienteDaModificare = GetClienteById(id);

            if (clienteDaModificare == null)
            {
                return HttpNotFound();
            }

            return View(clienteDaModificare);
        }

        [HttpPost]
        public ActionResult Edit(Cliente clienteModificato)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();

                    string query = "";

                    if (clienteModificato.TipoCliente == "Privato")
                    {
                        query = "UPDATE Clienti " +
                                "SET Nome = @Nome, Cognome = @Cognome, TipoCliente = @TipoCliente, CodiceFiscale = @CodiceFiscale " +
                                "WHERE IDCliente = @IDCliente";
                    }
                    else if (clienteModificato.TipoCliente == "Azienda")
                    {
                        query = "UPDATE Clienti " +
                                "SET Nome = @Nome, Cognome = @Cognome, TipoCliente = @TipoCliente, PartitaIVA = @PartitaIVA " +
                                "WHERE IDCliente = @IDCliente";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@IDCliente", clienteModificato.IDCliente);
                    cmd.Parameters.AddWithValue("@Nome", clienteModificato.Nome);
                    cmd.Parameters.AddWithValue("@Cognome", clienteModificato.Cognome);
                    cmd.Parameters.AddWithValue("@TipoCliente", clienteModificato.TipoCliente);

                    if (clienteModificato.TipoCliente == "Privato")
                    {
                        cmd.Parameters.AddWithValue("@CodiceFiscale", clienteModificato.CodiceFiscale);
                    }
                    else if (clienteModificato.TipoCliente == "Azienda")
                    {
                        cmd.Parameters.AddWithValue("@PartitaIVA", clienteModificato.PartitaIVA);
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ViewBag.msgErrore = "Errore: " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }

                TempData["msgSuccess"] = "Cliente " + clienteModificato.Nome + " modificato con successo!";
                return RedirectToAction("Index");
            }

            // Se il modello non è valido, torna alla vista di modifica con i dati del cliente
            return View(clienteModificato);
        }

        private Cliente GetClienteById(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            Cliente cliente = null;

            try
            {
                conn.Open();

                string query = "SELECT * FROM Clienti WHERE IDCliente = @IDCliente";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDCliente", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        IDCliente = Convert.ToInt32(reader["IDCliente"]),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        CodiceFiscale = reader["CodiceFiscale"].ToString(),
                        PartitaIVA = reader["PartitaIVA"].ToString(),
                        TipoCliente = reader["TipoCliente"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Si è verificato un errore durante la ricerca del cliente: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return cliente;
        }
    }


}