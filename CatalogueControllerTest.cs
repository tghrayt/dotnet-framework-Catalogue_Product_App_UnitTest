using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Catalogue_Produit_App;
using Catalogue_Produit_App.Controllers;
using Catalogue_Produit_App.Service;
using Moq;

namespace Catalogue_Produit_Test_Unitaires

{
    [TestClass]
    public class CatalogueControllerTest
    {
        private Mock<ICategorieService> _categorieService = new Mock<ICategorieService>();
        [TestMethod]
        public void TestAjoutCatalogueMethod()
        {
            //Arrange

            CatalogueController controller = new CatalogueController(_categorieService.Object);

            //Act
            ViewResult result = controller.AjoutCatalogue() as ViewResult;
            string viewName = result.ViewName;

            //Assert
            Assert.AreEqual(viewName,"");
        }
    }
}
