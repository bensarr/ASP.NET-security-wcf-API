using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiDiti.Controllers
{
    public class ValuesController : ApiController
    {

        private DBPersonneEntities db = new DBPersonneEntities();
        /// <summary>
        /// ERRORS LOG
        /// </summary>
        /// <param name="erreur"></param>
        public static void WriteLogSystem(string erreur)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "DItI2020";
                eventLog.WriteEntry(string.Format("date: {0}, libelle: {1}, description {2}", DateTime.Now, "DItI2020", erreur), EventLogEntryType.Information, 101, 1);
            }
        }
        /// <summary>
        /// LIST
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <param name="debutDateNaissance"></param>
        /// <param name="finDateNaissance"></param>
        /// <returns></returns>
        public List<Personnes> GetListPersonnes(string email, string tel, string debutDateNaissance, string finDateNaissance)
        {
            var list = db.Personnes.ToList();
            if (!string.IsNullOrEmpty(email))
            {
                list = list.Where(a => a.Email.ToUpper().Contains(email.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(tel))
            {
                list = list.Where(a => a.Tel.ToUpper().Contains(tel.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(debutDateNaissance))
            {
                DateTime laDate = DateTime.Parse(debutDateNaissance);
                list = list.Where(a => a.DateNaissance >= laDate).ToList();
            }
            if (!string.IsNullOrEmpty(finDateNaissance))
            {
                DateTime laDate = DateTime.Parse(finDateNaissance);
                list = list.Where(a => a.DateNaissance <= laDate).ToList();
            }
            return list;
        }

        // POST api/values
        /// <summary>
        /// AJOUT
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void AddPersonne([FromBody] Personnes value)
        {
            try
            {
                db.Personnes.Add(value);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
        }
        public Personnes GetPersonneById(int? id)
        {
            return db.Personnes.Find(id);
        }
        /// <summary>
        /// EDITE
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void UpdatePersonne([FromBody] Personnes value)
        {
            try
            {
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
        }
        [HttpGet]
        public void DeletePersonne(int id)
        {
            try
            {
                Personnes u = db.Personnes.Find(id);
                db.Personnes.Remove(u);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
        }
    }
}
