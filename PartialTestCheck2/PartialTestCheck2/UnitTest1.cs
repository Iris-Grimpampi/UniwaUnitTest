using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PartialHomework;

namespace PartialTestCheck
{
    [TestClass]
    public class ValidityTest
    {
        PartialDll dll = new PartialDll();

        Product testProd;
        double testPriceTaxes;
        double testDiscount;
        double testTotalPrice;

        Product[] products;
        Product[] products2;

        Product[] toOrderRight;
        Product[] toOrderRight2;
        Product[] tempOrder;

        [TestInitialize]
        public void initProducts()
        {
            testProd = new Product("5201111111110", "Ελλάδα", 3, "Αναψυκτικά χωρίς αλκοόλ",
                14.50, 10);

            products = new Product[]
            {
                new Product("5201111111110", "Ελλάδα", 15, "Αναψυκτικά χωρίς αλκοόλ", 1.32, 10),
                new Product("5201566987995", "Ελλάδα", 48, "Ηλεκτρονικά", 56.99, 23),
                new Product("3871111111117", "Βοσνία-Ερζεγοβίνη", 21, "Τρόφιμα", 4.27, 2),
                new Product("5201221122129", "Ελλάδα", 3, "Βιβλία", 64.00, 15),
                new Product("6212223331111", "Συρία", 0, "Καλλυντικά", 2.11, 40),
                new Product("6212929339114", "Συρία", 1200, "Αλκοόλ", 26.60, 33),
            };

            products2 = new Product[]
            {
                new Product("0990123456788", "Stany Zjednoczone i Kanada", 50, "Αναψυκτικά χωρίς αλκοόλ", 2.6, 0),
                new Product("4701111111116", "Κιργιζία", 100, "Τρόφιμα", 2.00000001, 35),
                new Product("8671111111118", "Βόρεια Κορέα", 4, "Βιβλία", 56.66, 14),
            };

            toOrderRight = new Product[]
            {
                products[3], products[4]
            };

            toOrderRight2 = new Product[]
            {
                
            };

            tempOrder = new Product[] { };
        }

        [TestCleanup]
        public void cleanUp()
        {
            // Καθαρίζουμε τα αντικείμενα για να είναι έτοιμη η μνήμη για το επόμενο test
            dll = null;
            products = null;
            tempOrder = null;
        }

        [TestMethod]
        public void TestValidBarcode()
        {
            Assert.AreEqual(true, dll.CheckBarcode("4006381333931"));
        }
        [TestMethod]
        public void Test2ndValidBarcode()
        {
            Assert.AreEqual(true, dll.CheckBarcode("8855114626552"));
        }
        [TestMethod]
        public void Test0Digit()
        {
            Assert.AreEqual(false, dll.CheckBarcode(""));
        }
        [TestMethod]
        public void Test1Digit()
        {
            Assert.AreEqual(false, dll.CheckBarcode("1"));
        }
        [TestMethod]
        public void Test14Digit()
        {
            Assert.AreEqual(false, dll.CheckBarcode("01234567890123"));
        }
        [TestMethod]
        public void TestCharDigit()
        {
            Assert.AreEqual(false, dll.CheckBarcode("abcdefghijklm"));
        }

        [TestMethod]
        public void Test3Multi_OneUp()
        {
            Assert.AreEqual(false, dll.CheckBarcode("4006381333941"));
        }
        [TestMethod]
        public void Test1Multi_OneUp()
        {
            Assert.AreEqual(false, dll.CheckBarcode("4006381343931"));
        }
        [TestMethod]
        public void Test3Multi_OneDown()
        {
            Assert.AreEqual(false, dll.CheckBarcode("4006381333921"));
        }
        [TestMethod]
        public void Test1Multi_OneDown()
        {
            Assert.AreEqual(false, dll.CheckBarcode("4006381323931"));
        }
        [TestMethod]
        public void TestCheck_OneUp()
        {
            Assert.AreEqual(false, dll.CheckBarcode("4006381333932")); //valid 4006381333931
        }
        [TestMethod]
        public void TestCheck_OneDown()
        {
            Assert.AreEqual(false, dll.CheckBarcode("4006381333930")); //valid 4006381333931
        }
        [TestMethod]
        public void TestMinus()
        {
            Assert.AreEqual(false, dll.CheckBarcode("-006381333930"));
        }
        //Country Testing

        [TestMethod]
        public void CountryInvalidBarcodeTest()
        {
            Assert.AreEqual(null, dll.FindCountry("4006381333930")); //valid 4006381333931
        }
        [TestMethod]
        public void Country000Test()
        {
            Assert.AreEqual(null, dll.FindCountry("0001111111111"));
        }
        [TestMethod]
        public void Country001_USA()
        {
            Assert.AreEqual("Ηνωμένες Πολιτείες", dll.FindCountry("0012222222221"));
        }
        [TestMethod]
        public void Country019_USA()
        {
            Assert.AreEqual("Ηνωμένες Πολιτείες", dll.FindCountry("0191111111119"));
        }

        [TestMethod]
        public void Country020_Restricted()
        {
            Assert.AreEqual("Περιορισμένη διανομή", dll.FindCountry("0201111111115"));
        }

        [TestMethod]
        public void Country520_Greece()
        {
            // Έλεγχος για το ελληνικό πρόθεμα 520 
            Assert.AreEqual("Ελλάδα", dll.FindCountry("5201234567894"));
        }

        [TestMethod]
        public void Country978_ISBN_Books()
        {
            // Έλεγχος για το πρόθεμα βιβλίων 978 (ISBN)
            Assert.AreEqual("ISBN (βιβλία)", dll.FindCountry("9780123456786"));
        }

        [TestMethod]
        public void Country199_Null()
        {
            Assert.AreEqual(null, dll.FindCountry("1990123456787"));
        }

        [TestMethod]
        public void Country140_Null()
        {
            Assert.AreEqual(null, dll.FindCountry("1400123456781"));
        }

        [TestMethod]
        public void Country381_Null()
        {
            Assert.AreEqual(null, dll.FindCountry("3810123456786"));
        }

        [TestMethod]
        public void Country382_Null()
        {
            Assert.AreEqual(null, dll.FindCountry("3820123456785"));
        }

        //===CheckZipCode===

        string zipDistrict;

        [TestMethod]
        public void CheckZIPCode_NAN()
        {
            Assert.AreEqual(false, dll.CheckZipCode("aaaa", ref zipDistrict));
        }

        [TestMethod]
        public void CheckZIPCode_bool_Limassol()
        {
            Assert.AreEqual(true, dll.CheckZipCode("4566", ref zipDistrict));
        }

        [TestMethod]
        public void CheckZIPCode_distr_Limassol()
        {
            dll.CheckZipCode("4566", ref zipDistrict);
            Assert.AreEqual("Limassol District", (zipDistrict));
        }

        [TestMethod]
        public void CheckZIPCode_bool_999()
        {
            Assert.AreEqual(false, dll.CheckZipCode("999", ref zipDistrict));
        }

        [TestMethod]
        public void CheckZIPCode_distr_999()
        {
            dll.CheckZipCode("999", ref zipDistrict);
            Assert.AreEqual(null, (zipDistrict));
        }

        [TestMethod]
        public void CheckZIPCode_bool_Nicosia()
        {
            string region = "";
            // Έλεγχος ορίων Λευκωσίας (1000-2999)
            Assert.IsTrue(dll.CheckZipCode("2500", ref region));
        }

        [TestMethod]
        public void CheckZIPCode_distr_Nicosia()
        {
            string region = "";
            // Έλεγχος ορίων Λευκωσίας (1000-2999)
            dll.CheckZipCode("2500", ref region);
            Assert.AreEqual("Nicosia District", region);
        }

        [TestMethod]
        public void CheckZIPCode_bool_Kyrenia()
        {
            string region = "";
            // Έλεγχος ορίων Κερύνειας (9000-9999)
            Assert.IsTrue(dll.CheckZipCode("9500", ref region));
        }

        [TestMethod]
        public void CheckZIPCode_distr_Kyrenia()
        {
            string region = "";
            // Έλεγχος ορίων Κερύνειας (9000-9999)
            dll.CheckZipCode("9500", ref region);
            Assert.AreEqual("Kyrenia District", region);
        }
        [TestMethod]
        public void CheckZIPCode_bool_Famagusta()
        {
            string region = "";
            // Έλεγχος ορίων Κερύνειας (9000-9999)
            Assert.IsTrue(dll.CheckZipCode("5000", ref region));
        }

        [TestMethod]
        public void CheckZIPCode_distr_Famagusta()
        {
            string region = "";
            // Έλεγχος ορίων Κερύνειας (9000-9999)
            dll.CheckZipCode("5000", ref region);
            Assert.AreEqual("Famagusta District", region);
        }

        [TestMethod]
        public void CheckZIPCode_bool_Larnaca()
        {
            string region = "";
            // Έλεγχος ορίων Κερύνειας (9000-9999)
            Assert.IsTrue(dll.CheckZipCode("6000", ref region));
        }

        [TestMethod]
        public void CheckZIPCode_distr_Larnaca()
        {
            string region = "";
            // Έλεγχος ορίων Κερύνειας (9000-9999)
            dll.CheckZipCode("6000", ref region);
            Assert.AreEqual("Larnaca District", region);
        }

        [TestMethod]
        public void CheckZIPCode_bool_Paphos()
        {
            string region = "";
            // Έλεγχος ορίων Κερύνειας (9000-9999)
            Assert.IsTrue(dll.CheckZipCode("8000", ref region));
        }

        [TestMethod]
        public void CheckZIPCode_distr_Paphos()
        {
            string region = "";
            // Έλεγχος ορίων Κερύνειας (9000-9999)
            dll.CheckZipCode("8000", ref region);
            Assert.AreEqual("Paphos District", region);
        }

        //===CalculateCost===

        [TestMethod]
        public void CalculateCostProduct1_PrTax()
        {
            dll.CalculateCost(testProd, ref testPriceTaxes, ref testDiscount, ref testTotalPrice);
            Assert.AreEqual(16.385, testPriceTaxes, 0.0000001);
        }
        [TestMethod]
        public void CalculateCostProduct1_Disc()
        {
            dll.CalculateCost(testProd, ref testPriceTaxes, ref testDiscount, ref testTotalPrice);
            Assert.AreEqual(1.6385, testDiscount, 0.0000001);
        }
        [TestMethod]
        public void CalculateCostProduct1_PrTot()
        {
            dll.CalculateCost(testProd, ref testPriceTaxes, ref testDiscount, ref testTotalPrice);
            Assert.AreEqual(14.7465, testTotalPrice, 0.0000001);
        }

        // 1. Test για τον κανονικό ΦΠΑ 24%
        [TestMethod]
        public void CalculateCost_Electronics_24Percent()
        {
            Product laptop = new Product("5201566987995", "Ελλάδα", 5, "Ηλεκτρονικά", 100.0, 0);
            double tax = 0, disc = 0, final = 0;
            dll.CalculateCost(laptop, ref tax, ref disc, ref final);
            Assert.AreEqual(124.0, tax, 0.0000001); // 100 + 24%
        }

        [TestMethod]
        public void CalculateCost_Books_6Percent_VAT()
        {
            // Έλεγχος υπερμειωμένου ΦΠΑ 6% για την κατηγορία Βιβλία
            Product book = new Product("9781111111111", "ISBN (βιβλία)", 10, "Βιβλία", 100.0, 0);
            double tax = 0, disc = 0, final = 0;
            dll.CalculateCost(book, ref tax, ref disc, ref final);
            Assert.AreEqual(106.0, tax, 0.0000001);
        }

        [TestMethod]
        public void CalculateTotalCost_Prds1()
        {
            Assert.AreEqual(141.81821, dll.CalculateTotalCost(products), 0.000001);
        }

        [TestMethod]
        public void CalculateTotalCost_Prds2()
        {
            Assert.AreEqual(56.05825, dll.CalculateTotalCost(products2), 0.000001);
        }

        //===AmountofProducts===
        [TestMethod]
        public void AmountofProducts_Prds1()
        {
            Assert.AreEqual(3, dll.AmountofProducts(products, "Ελλάδα"));
        }

        [TestMethod]
        public void AmountofProducts_NonExistentCountry()
        {
            // Έλεγχος για χώρα που δεν υπάρχει στον πίνακα προϊόντων 
            Assert.AreEqual(0, dll.AmountofProducts(products, "Γερμανία"));
        }

        [TestMethod]
        public void AmountofProducts_CaseInsensitive()
        {
            // Έλεγχος αν η αναζήτηση χώρας αγνοεί πεζά/κεφαλαία 
            Assert.AreEqual(3, dll.AmountofProducts(products, "ελλάδα"));
        }

        [TestMethod]
        public void AmountofProducts_Null_Country()
        {
            // Έλεγχος αν η αναζήτηση χώρας αγνοεί πεζά/κεφαλαία 
            Assert.AreEqual(0, dll.AmountofProducts(products, null));
        }

        //===ProductsToOrder===

        [TestMethod]
        public void ProductsToOrder_Prds1_Int()
        {
            Assert.AreEqual(false, dll.ProductsToOrder(products, 4, ref tempOrder));
        }
        [TestMethod]
        public void ProductsToOrder_Prds1_Ord()
        {
            dll.ProductsToOrder(products, 4, ref tempOrder);
            CollectionAssert.AreEqual(toOrderRight, tempOrder);
        }
        [TestMethod]
        public void ProductsToOrder_Prds2_Int()
        {
            Assert.AreEqual(true, dll.ProductsToOrder(products2, 4, ref tempOrder));
        }
        [TestMethod]
        public void ProductsToOrder_Prds2_Ord()
        {
            dll.ProductsToOrder(products2, 4, ref tempOrder);
            CollectionAssert.AreEqual(toOrderRight2, tempOrder);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ProductsToOrder_Null()
        {
            Assert.AreEqual(false, dll.ProductsToOrder(null, 4, ref tempOrder));
        }
        [TestMethod]
        public void ProductsToOrder_Minus()
        {
            Assert.AreEqual(true, dll.ProductsToOrder(products, -4, ref tempOrder));
        }

        //===Product init===

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidCategoryException()
        {
            new Product("5201111111110", "Ελλάδα", 10, "ΛάθοςΚατηγορία", 10.0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNegativePriceException()
        {
            new Product("5201111111110", "Ελλάδα", 10, "Τρόφιμα", -10.0, 0);
        }

        // 2. Test για το Short Barcode Exception
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProductConstructor_ShortBarcode_ThrowsException()
        {
            new Product("123", "Ελλάδα", 10, "Τρόφιμα", 10.0, 0);
        }
       
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProductConstructor_NullBarcode_ThrowsException()
        {
            // Έλεγχος αν "σκάει" σφάλμα όταν το barcode είναι null 
            new Product(null, "Ελλάδα", 10, "Τρόφιμα", 10.0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProductConstructor_InvalidDiscountHigh_ThrowsException()
        {
            // Έλεγχος αν "σκάει" σφάλμα για έκπτωση πάνω από 100% 
            new Product("5201111111110", "Ελλάδα", 10, "Τρόφιμα", 10.0, 150);
        }
    }


}
