using Catalogue_Produit_App.DAO;
using Catalogue_Produit_App.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Catalogue_Produit_Test_Unitaires
{
    public static class UnityConfig
    {
        [ClassInitialize]
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ICategorieService, CategorieService>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}