﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void TestGetAllCatalogueCatalogueMethod()
        {
            //Arrange


            //Act
            ViewResult result = _controller.AjoutCatalogue() as ViewResult;
            string viewName = result.ViewName;

            //Assert
            Assert.AreEqual(viewName,"");
        }



        [TestMethod]
        public void TestAjouterCatalogueCatalogueMethod()
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
    }
}
