using ConAuntenticacion.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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

            // CÓDIGO INSERTADO
            ApplicationDbContext db = new ApplicationDbContext();

            CrearRoles(db);
            CrearSuperUsuario(db);
            CrearAdministrador(db);
            CrearGerente(db);
            AsignarPermisos(db);

            db.Dispose();
        }

        private void CrearRoles(ApplicationDbContext db)
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!rolemanager.RoleExists("view"))
            {
                rolemanager.Create(new IdentityRole("view"));
            }
            if (!rolemanager.RoleExists("create"))
            {
                rolemanager.Create(new IdentityRole("create"));
            }

            if (!rolemanager.RoleExists("edit"))
            {
                rolemanager.Create(new IdentityRole("edit"));
            }
            if (!rolemanager.RoleExists("delete"))
            {
                rolemanager.Create(new IdentityRole("delete"));
            }

        }

        private void CrearSuperUsuario(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            // CORREGIDO: Buscar por UserName ("SuperAdmin") en lugar del correo
            var user = usermanager.FindByName("SuperAdmin@hotmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "SuperAdmin",
                    Email = "superadmin@hotmail.com"
                };
                usermanager.Create(user, "abcABC123*");
            }
        }

        private void CrearAdministrador(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindByName("administrador@hotmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "Administrador",
                    Email = "administrador@hotmail.com"
                };
                usermanager.Create(user, "administrador123");
            }
        }

        private void CrearGerente(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindByName("gerente@hotmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "Gerente",
                    Email = "gerente@hotmail.com"
                };
                usermanager.Create(user, "gerente123");
            }
        }

        private void AsignarPermisos(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // 1) gerente
            var gerente = usermanager.FindByName("Gerente");
            if (gerente != null)
            {
                if (!usermanager.IsInRole(gerente.Id, "view"))
                {
                    usermanager.AddToRole(gerente.Id, "view");
                }
            }

            // 2) superadmin
            var superAdmin = usermanager.FindByName("SuperAdmin@hotmail.com");
            if (superAdmin != null)
            {
                if (!usermanager.IsInRole(superAdmin.Id, "view"))
                {
                    usermanager.AddToRole(superAdmin.Id, "view");
                }
                if (!usermanager.IsInRole(superAdmin.Id, "edit"))
                {
                    usermanager.AddToRole(superAdmin.Id, "edit");
                }
                if (!usermanager.IsInRole(superAdmin.Id, "delete"))
                {
                    usermanager.AddToRole(superAdmin.Id, "delete");
                }
                if (!usermanager.IsInRole(superAdmin.Id, "create"))
                {
                    usermanager.AddToRole(superAdmin.Id, "create");
                }
            }

            // 3) administrador
            var administrador = usermanager.FindByName("Administrador");
            if (administrador != null)
            {
                // CORREGIDO: Se usa el ID del administrador y los roles de edit y delete según la guía
                if (!usermanager.IsInRole(administrador.Id, "edit"))
                {
                    usermanager.AddToRole(administrador.Id, "edit");
                }
                if (!usermanager.IsInRole(administrador.Id, "delete"))
                {
                    usermanager.AddToRole(administrador.Id, "delete");
                }
            }
        }
    }
}