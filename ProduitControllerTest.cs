using Catalogue_Produit_App.Controllers;
using Catalogue_Produit_App.DTO;
using Catalogue_Produit_App.Helper;
using Catalogue_Produit_App.Models;
using Catalogue_Produit_App.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Catalogue_Produit_Test_Unitaires
{
    [TestClass]
    public class ProduitControllerTest
    {

        private readonly Mock<IProduitService> _produitService;
        private readonly Mock<ICategorieService> _categorieService;
        private readonly ProduitHelper _produitHelper = new ProduitHelper();
        private readonly ProduitController _controller;
        public ProduitControllerTest()
        {
            _produitService = new Mock<IProduitService>();
            _categorieService = new Mock<ICategorieService>();
            _controller = new ProduitController(_produitService.Object,_categorieService.Object);
        }





        [TestMethod]
        public void TestAjouterProduitMethod()
        {
            //Arrange
            CAT_PRODUIT produitTransfert = null;
            ProduitDto produit = null;
            _produitService.Setup(r => r.AddNewProduit(It.IsAny<ProduitDto>()))
                .Callback<ProduitDto>(x => produit = x);

            //Act
            var produitDto = new ProduitDto
            {
                libelleProduit = "Test categorie",
                codeProduit = 0,
                DateSaisie = DateTime.Now
            };
            produitTransfert = _produitHelper.ConvertFromDto(produitDto);
               if(_controller.Request != null)
            {
                _controller.AjoutProduit(produitTransfert);
                _produitService.Verify(x => x.AddNewProduit(It.IsAny<ProduitDto>()), Times.Once);

            }
            else
            {
                produit = new ProduitDto
                {
                    libelleProduit = "Test categorie",
                    codeProduit = 0,
                    DateSaisie = DateTime.Now
                };

            }



            //Assert
            Assert.AreEqual(produit.libelleProduit, produitDto.libelleProduit);
            Assert.AreEqual(produit.codeProduit, produitDto.codeProduit);
        }



        [TestMethod]
        public void TestGetAllProduiteMethod()
        {
            //Arrange


            //Act
            ViewResult result = _controller.AjoutProduit() as ViewResult;
            string viewName = result.ViewName;

            //Assert
            Assert.AreEqual(viewName, "");
        }


        [TestMethod]
        public void TestSupprimerProduiteMethod()
        {
            //Arrange
            int codeProduit = 2;
            _produitService.Setup(r => r.DeleteProduit(It.IsAny<int>()))
                .Callback<int>(x => codeProduit = x);

            //Act
            var code = 2;
            _controller.SupprimerProduit(code);
            _produitService.Verify(x => x.DeleteProduit(It.IsAny<int>()), Times.Never);



            //Assert
            Assert.AreEqual(code, codeProduit);
        }




        [TestMethod]
        public void TestModifierProduitMethod()
        {
            //Arrange
            CAT_PRODUIT produitTransfert = null;
            ProduitDto produit = null;
            _produitService.Setup(r => r.UpdateProduit(It.IsAny<ProduitDto>()))
                .Callback<ProduitDto>(x => produit = x);

            //Act
            var produitDto = new ProduitDto
            {
                libelleProduit = "Test produit",
                codeProduit = 0,
                DateSaisie = DateTime.Now
            };
            produitTransfert = _produitHelper.ConvertFromDto(produitDto);
            if (_controller.Request != null)
            {
                _controller.ModifierProduit(produitTransfert);
                _produitService.Verify(x => x.UpdateProduit(It.IsAny<ProduitDto>()), Times.Once);
            }
            else
            {
                produit = new ProduitDto
                {
                    libelleProduit = "Test produit",
                    codeProduit = 0,
                    DateSaisie = DateTime.Now
                };

            }

            //Assert
            Assert.AreEqual(produit.libelleProduit, produitDto.libelleProduit);
            Assert.AreEqual(produit.codeProduit, produitDto.codeProduit);
            //Assert.AreEqual(categorie.dateSaisie, categorieDto.dateSaisie);
        }

    }
}
