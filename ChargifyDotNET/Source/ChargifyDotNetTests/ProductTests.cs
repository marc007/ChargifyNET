﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChargifyDotNetTests.Base;
using ChargifyNET;
using System.Collections;
using System.Net;

namespace ChargifyDotNetTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ProductTests : ChargifyTestBase
    {
        public ProductTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Get the list of products
        /// </summary>
        [TestMethod]
        public void Test_ListProducts()
        {
            IDictionary<int, IProduct> productList = Chargify.GetProductList();

            Assert.IsNotNull(productList);
            Assert.AreNotEqual(0, productList.Count);
        }

        /// <summary>
        /// Get a single product via the handle and Chargify ID
        /// </summary>
        [TestMethod]
        public void Test_GetSingleProductByHandle()
        {
            // Test using handle
            IProduct basicProduct = Chargify.LoadProduct("basic", true);

            Assert.IsNotNull(basicProduct);
            Assert.AreEqual("basic", basicProduct.Handle);
        }

        [TestMethod]
        public void Test_GetSingleProductByID()
        {
            // Test using the Chargify ID
            IProduct basicProduct = Chargify.LoadProduct("1302", false);

            Assert.IsNotNull(basicProduct);
            Assert.AreEqual("basic", basicProduct.Handle);
        }

        [TestMethod]
        public void Test_CreateProduct()
        {
            // Arrange
            var productFamily = Chargify.GetProductFamilyList().Values.FirstOrDefault();

            // Act
            try
            {
                string productName = "test-product" + Guid.NewGuid().ToString();
                var newProduct = Chargify.CreateProduct(productFamily.ID, productName, productName, 5000, 1, IntervalUnit.Month, string.Empty, "This is a test product, please archive");

                // Assert
                Assert.IsNotNull(newProduct);
                Assert.AreEqual(productName, newProduct.Handle);
            }
            catch (ChargifyException cEx)
            {
                Assert.Fail(string.Format("Call failed. {0}, {1}", cEx.ToString(), string.Join(", ", cEx.ErrorMessages.Select(m => m.Message))));
            }
        }
    }
}
