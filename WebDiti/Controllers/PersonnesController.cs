using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WcfDiti.Models;
using Microsoft.AspNet.Identity.Owin;
using WebDiti.Models;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WebDiti.App_Start;

namespace WebDiti.Controllers
{
    public class PersonnesController : Controller
    {
        ServiceProjet.Service1Client Service = new ServiceProjet.Service1Client();
        //private ApplicationUserManager _userManager;
        //public PersonnesController()
        //{
        //}
        //public PersonnesController(ApplicationUserManager userManager)
        //{
        //    UserManager = userManager;
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        // GET: Personnes
        public async Task<ActionResult> Index([Bind(Include = "Email,Tel,debutDateNaissance,finDateNaissance")] PersonneViewModel personne)
        {
            //:WCF
            //return View(Service.ListPersonne(personne.Email, personne.Tel, personne.debutDateNaissance, personne.finDateNaissance));
            
            //:API ASP.NET
            return View(GetListPersonnes(personne.Email, personne.Tel, personne.debutDateNaissance, personne.finDateNaissance));
        }

        // GET: Personnes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = Service.getPersonneById(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // GET: Personnes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personnes/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nom,Prenom,Email,Tel,DateNaissance")] Personne personne, string Profil)
        {
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = personne.IdentifiantU, Email = personne.Email };
                //var result = await UserManager.CreateAsync(user, "Passer@123");
                //if (result.Succeeded)
                //{
                    //var chkUser = UserManager.AddToRole(user.Id, Profil);
                    //personne.IdUser = user.Id;
                    Service.AddPersonne(personne);
                    return RedirectToAction("Index");
                //}
            }

            return View(personne);
        }

        // GET: Personnes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = Service.getPersonneById(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nom,Prenom,Email,Tel,DateNaissance")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindByEmailAsync(personne.Email);
                //if (user != null)
                //{
                //    user.UserName = personne.IdentifiantU;
                //    var chkUser = await UserManager.UpdateAsync(user);
                    Service.UpdatePersonne(personne);
                    return RedirectToAction("Index");
                //}
            }
            return View(personne);
        }

        // GET: Personnes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personne personne = Service.getPersonneById(id);
            if (personne == null)
            {
                return HttpNotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //var user = await UserManager.FindByEmailAsync(Service.getPersonneById(id).Email);
            //if (user != null)
            //{
            //    var chkUser = await UserManager.DeleteAsync(user);
                Service.DeletePersonne(id);
            //}
            return RedirectToAction("Index");
        }

        #region api pour presonne
        
        /// <summary>
        /// LIST
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <param name="debutDateNaissance"></param>
        /// <param name="finDateNaissance"></param>
        /// <returns></returns>
        public List<Personne> GetListPersonnes(string email, string tel, string debutDateNaissance, string finDateNaissance)
        {
            HttpClient client;
            client = new HttpClient();
            var services = new List<Personne>();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ProjetApiURL"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(string.Format("diti/Values/GetListPersonnes?email={0}&tel={1}&debutDateNaissance={2}&finDateNaissance={3}", email, tel, debutDateNaissance, finDateNaissance)).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                services = JsonConvert.DeserializeObject<List<Personne>>(responseData);
            }
            return services;
        }
        public Personne GetPersonne(int id)
        {
            HttpClient client;
            client = new HttpClient();
            var services = new Personne();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ProjetApiURL"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(string.Format("diti/Values/GetPersonneById?id={0}", id)).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                services = JsonConvert.DeserializeObject<Personne>(responseData);
            }
            return services;
        }
        public void AddPersonne(Personne personne)
        {
            string Id = personne.Id > 0 ? personne.Id.ToString() : "0";
            var values = new Dictionary<string, string>
                    {
                       { "Id", Id },
                       { "Nom", personne.Nom },
                       { "Prenom", personne.Prenom},
                       { "Email", personne.Email},
                       { "Tel", personne.Tel },
                       { "DateNaissance", personne.DateNaissance!=null?personne.DateNaissance.ToString():string.Empty }
                    };
            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ProjetApiURL"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("diti/Values/AddPersonne", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                Helper.WriteLogSystem(ex.ToString());
            }
        }
        public void UpdatePersonne(Personne personne)
        {
            string Id = personne.Id > 0 ? personne.Id.ToString() : "0";
            var values = new Dictionary<string, string>
                    {
                       { "Id", Id },
                       { "Nom", personne.Nom },
                       { "Prenom", personne.Prenom},
                       { "Email", personne.Email},
                       { "Tel", personne.Tel },
                       { "DateNaissance", personne.DateNaissance!=null?personne.DateNaissance.ToString():string.Empty }
                    };
            var content = new FormUrlEncodedContent(values);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ProjetApiURL"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.PostAsync("diti/Values/UpdatePersonne", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                Helper.WriteLogSystem(ex.ToString());
            }
        }
        #endregion
        #region api PHP
        public List<Personne> GetPersonnes(string email, string tel, string debutDateNaissance, string finDateNaissance)
        {
            HttpClient client;
            client = new HttpClient();
            var services = new List<Personne>();
            client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ProjetApiPhpURL"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync(string.Format("asp_projects/list.php?email={0}&tel={1}&debutDateNaissance={2}&finDateNaissance={3}", email, tel, debutDateNaissance, finDateNaissance)).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                services = JsonConvert.DeserializeObject<List<Personne>>(responseData);
            }
            return services;
        }
        #endregion
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
