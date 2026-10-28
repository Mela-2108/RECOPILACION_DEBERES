using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ConAuntenticacion.Models;

namespace ConAuntenticacion
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ApplicationDbContext db = new ApplicationDbContext();

            CrearRoles(db);
            CrearSuperUsuario(db);
            CrearAdministrador(db);
            CrearAgente(db);
            AsignarPermisos(db);

            db.Dispose();
        }

        private void CrearRoles(ApplicationDbContext db)
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            string[] roles = { "SuperAdmin", "ADM", "AGENTE", "Create", "Edit", "Details", "Delete" };

            foreach (var rol in roles)
            {
                if (!rolemanager.RoleExists(rol))
                {
                    rolemanager.Create(new IdentityRole(rol));
                }
            }
        }

        private void CrearSuperUsuario(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindByEmail("superadmin@gmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "SuperAdmin",
                    Email = "superadmin@gmail.com"
                };
                usermanager.Create(user, "superadmin123");
            }
        }

        private void CrearAdministrador(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            // El usuario es ADM y su correo es el mail ESPE (matorres30@espe.edu.ec)
            var user = usermanager.FindByName("ADM");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "ADM",
                    Email = "matorres30@espe.edu.ec"
                };
                usermanager.Create(user, "adm123");
            }
        }

        private void CrearAgente(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindByEmail("agente@hotmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "Agente",
                    Email = "agente@hotmail.com"
                };
                usermanager.Create(user, "agente123");
            }
        }

        private void AsignarPermisos(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // 1) SuperAdmin
            var superAdmin = usermanager.FindByEmail("superadmin@gmail.com");
            if (superAdmin != null)
            {
                string[] permisosSuper = { "SuperAdmin", "Create", "Edit", "Details", "Delete" };
                foreach (var rol in permisosSuper)
                {
                    if (!usermanager.IsInRole(superAdmin.Id, rol))
                    {
                        usermanager.AddToRole(superAdmin.Id, rol);
                    }
                }
            }

            // 2) ADM (Buscado por su usuario "ADM")
            var administrador = usermanager.FindByName("ADM");
            if (administrador != null)
            {
                string[] permisosAdm = { "ADM", "Create", "Edit", "Details" };
                foreach (var rol in permisosAdm)
                {
                    if (!usermanager.IsInRole(administrador.Id, rol))
                    {
                        usermanager.AddToRole(administrador.Id, rol);
                    }
                }
            }

            // 3) AGENTE
            var agente = usermanager.FindByEmail("agente@hotmail.com");
            if (agente != null)
            {
                string[] permisosAgente = { "AGENTE", "Details" };
                foreach (var rol in permisosAgente)
                {
                    if (!usermanager.IsInRole(agente.Id, rol))
                    {
                        usermanager.AddToRole(agente.Id, rol);
                    }
                }
            }
        }
    }
}