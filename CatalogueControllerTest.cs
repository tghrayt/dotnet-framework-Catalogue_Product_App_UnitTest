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
using Catalogue_Produit_App.Models;
using Catalogue_Produit_App.Helper;
using Catalogue_Produit_App.DTO;

namespace Catalogue_Produit_Test_Unitaires

{
    [TestClass]
    public class CatalogueControllerTest
    {
        private readonly Mock<ICategorieService> _categorieService;
        private readonly CategorieHelper _categorieHelper = new CategorieHelper();
        private readonly CatalogueController _controller;
        public CatalogueControllerTest()
        {
            _categorieService = new Mock<ICategorieService>();
            _controller = new CatalogueController(_categorieService.Object);
        }
        [TestMethod]
        public void TestGetAllCatalogueMethod()
        {
            //Arrange


            //Act
            ViewResult result = _controller.AjoutCatalogue() as ViewResult;
            string viewName = result.ViewName;

            //Assert
            Assert.AreEqual(viewName,"");
        }



        [TestMethod]
        public void TestAjouterCatalogueMethod()
        {
            //Arrange
            CAT_CATEGORIE categorieTransfert = null;
            CategorieDto categorie = null;
            _categorieService.Setup(r => r.AddNewCategorie(It.IsAny<CategorieDto>()))
                .Callback<CategorieDto>(x => categorie = x);

            //Act
            var categorieDto = new CategorieDto
            {
                libelleCategorie = "Test categorie",
                codeCategorie = 0,
                dateSaisie = DateTime.Now
            };
            categorieTransfert = _categorieHelper.ConvertFromDto(categorieDto);
            _controller.AjoutCatalogue(categorieTransfert);
            _categorieService.Verify(x => x.AddNewCategorie(It.IsAny<CategorieDto>()), Times.Once);
           


            //Assert
            Assert.AreEqual(categorie.libelleCategorie, categorieDto.libelleCategorie);
            Assert.AreEqual(categorie.codeCategorie, categorieDto.codeCategorie);
            //Assert.AreEqual(categorie.dateSaisie, categorieDto.dateSaisie);
        }




        [TestMethod]
        public void TestSupprimerCatalogueMethod()
        {
            //Arrange
            int codeCategorie = 2;
            _categorieService.Setup(r => r.DeleteCategorie(It.IsAny<int>()))
                .Callback<int>(x => codeCategorie = x);

            //Act
            var code =2;
            _controller.SupprimerCatalogue(code);
            _categorieService.Verify(x => x.DeleteCategorie(It.IsAny<int>()), Times.Never);



            //Assert
            Assert.AreEqual(code, codeCategorie);
        }


        [TestMethod]
        public void TestModifierCatalogueMethod()
        {
            //Arrange
            CAT_CATEGORIE categorieTransfert = null;
            CategorieDto categorie = null;
            _categorieService.Setup(r => r.UpdateCategorie(It.IsAny<CategorieDto>()))
                .Callback<CategorieDto>(x => categorie = x);

            //Act
            var categorieDto = new CategorieDto
            {
                libelleCategorie = "Test categorie",
                codeCategorie = 0,
                dateSaisie = DateTime.Now
            };
            categorieTransfert = _categorieHelper.ConvertFromDto(categorieDto);
            _controller.ModifierCatalogue(categorieTransfert);
            _categorieService.Verify(x => x.UpdateCategorie(It.IsAny<CategorieDto>()), Times.Once);



            //Assert
            Assert.AreEqual(categorie.libelleCategorie, categorieDto.libelleCategorie);
            Assert.AreEqual(categorie.codeCategorie, categorieDto.codeCategorie);
            //Assert.AreEqual(categorie.dateSaisie, categorieDto.dateSaisie);
        }

    }
}
