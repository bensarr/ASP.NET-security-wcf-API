using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WcfDiti.Models;
using WebDiti.Models;

[assembly: OwinStartupAttribute(typeof(WebDiti.Startup))]
namespace WebDiti
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            //Déclaration du context qui permet d'accéder aux informations sécuritaires
            ApplicationDbContext context = new ApplicationDbContext();
            //Déclaration du context qui permet d'accéder à la base de données
            ServiceProjet.Service1Client service = new ServiceProjet.Service1Client();

            //Composant permettant de gérer les rôles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //Composant permetant de gérer les utilisateurs
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //Vérification si le rôle admin existe ,sinon créer le rôle admin et le super user
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool  
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                Profil p = new Profil();
                p.LibelleProfil = "Admin";
                service.AddProfil(p);
                //db.profils.Add(p);
                //db.SaveChanges();
                //Here we create a Admin super user who will maintain the website                 

                var user = new ApplicationUser();
                user.UserName = "Admin";
                user.Email = "bensarr37@gmail.com";
                string userPWD = "Passer@123";
                var chkUser = UserManager.Create(user, userPWD);

                Personne u = new Personne();
                u.Prenom = "BENJAMIN";
                u.Nom = "SWIFT";
                //u.IdentifiantU = user.UserName;
                u.Email = user.Email;
                u.Tel = "777777777";
                //u.IdUser = user.Id;
                service.AddPersonne(u);
                //db.utilisateurs.Add(u);
                //db.SaveChanges();

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating AGENT DE SAISIE role   
            if (!roleManager.RoleExists("Sécrétaire"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Sécrétaire";
                roleManager.Create(role);
                Profil p = new Profil();
                p.LibelleProfil = "Sécrétaire";
                service.AddProfil(p);
                //db.profils.Add(p);
                //db.SaveChanges();
            }
            var leUser = UserManager.FindByName("Hady");
            if (leUser == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Hady";
                user.Email = "Hady37@gmail.com";
                string userPWD = "Passer@123";
                var chkUser = UserManager.Create(user, userPWD);

                Personne u = new Personne();
                u.Nom = "SARR";
                u.Prenom = "HADY";
                //u.IdentifiantU = user.UserName;
                u.Email = user.Email;
                u.Tel = "777777777";
                //u.IdUser = user.Id;
                service.AddPersonne(u);
                //db.utilisateurs.Add(u);
                //db.SaveChanges();
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Sécrétaire");
                }
            }


        }
    }
}
