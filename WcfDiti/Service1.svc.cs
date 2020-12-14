using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfDiti.Models;

namespace WcfDiti
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {

        private DBProjetContext db = new DBProjetContext();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        /// <summary>
        /// Gérer les Erreurs
        /// </summary>
        /// <param name="erreur"></param>
        public static void WriteLogSystem(string erreur)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "DItI2020";
                eventLog.WriteEntry(string.Format("date: {0}, libelle: {1}, description {2}", DateTime.Now, "Diti2020", erreur), EventLogEntryType.Information, 101, 1);
            }
        }
        //public bool AddProfil(Profil p)
        //{
        //    bool rep = false;
        //    try
        //    {
        //        db.profils.Add(p);
        //        db.SaveChanges();
        //        rep = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogSystem(ex.ToString());
        //    }
        //    return rep;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns>listProfils</returns>
        //public List<Profil> ListProfil()
        //{
        //    return db.profils.ToList();
        //}
        public bool AddPersonne(Personne p)
        {
            bool rep = false;
            try
            {
                db.personnes.Add(p);
                db.SaveChanges();
                rep = true;
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
            return rep;
        }

        public bool DeletePersonne(int? id)
        {
            bool rep = false;
            try
            {
                Personne u = db.personnes.Find(id);
                db.personnes.Remove(u);
                db.SaveChanges();
                rep = true;
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
            return rep;
        }
        public Personne getPersonneById(int? id)
        {
            return db.personnes.Find(id);
        }

        public List<Personne> ListPersonne(string email, string tel, string debutDateNaissance, string finDateNaissance)
        {
            var list = db.personnes.ToList();
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

        public bool UpdatePersonne(Personne p)
        {
            bool rep = false;
            try
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                rep = true;
            }
            catch (Exception ex)
            {
                WriteLogSystem(ex.ToString());
            }
            return rep;
        }
    }
}
