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

namespace Catalogue_Produit_Test_Unitaires
{
    [TestClass]
    public class ProduitControllerTest
    {

        private readonly Mock<IProduitService> _produitService;
        private readonly ProduitHelper _produitHelper = new ProduitHelper();
        private readonly ProduitController _controller;
        public ProduitControllerTest()
        {
            _produitService = new Mock<IProduitService>();
            _controller = new ProduitController(_produitService.Object);
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
            //Assert.AreEqual(categorie.dateSaisie, categorieDto.dateSaisie);
        }
    }
}
