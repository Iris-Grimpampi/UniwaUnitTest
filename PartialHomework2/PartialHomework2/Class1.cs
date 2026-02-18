using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace PartialHomework
{
    public class Product
    {
        public string Barcode;     //EAN code 13
        public string Country;     //EAN country
        public int Stock;          //0-inf
        public string Category;    //Ηλεκτρονικά – Καλλυντικά
                                   //Αλκοόλ – Τρόφιμα
                                   //Αναψυκτικά χωρίς αλκοόλ – Βιβλία
        public double Price;       //0-inf
        public int Discount;       //0-100
        //ΦΠΑ
        //Κανονικός 24%     Ηλεκτρονικά, καλλυντικά, αλκοόλ
        //Μειωμένος 13%     Τρόφιμα, αναψυκτικά χωρίς αλκοόλ
        //Υπερμειωμένος 6%  Bιβλία

        public Product(string Barcode, string Country, int Stock,
            string Category, double Price, int Discount)
        {
            long tempNum;
            if (Barcode == null) throw new ArgumentNullException("The barcode can't be null");
            if (Barcode.Length != 13) throw new ArgumentException("The barcode must be exactly 13 numbers");
            if (!Int64.TryParse(Barcode, out tempNum)) throw new ArgumentException("The barcode must have only numbers");

            if (Country == null) throw new ArgumentNullException("The country can't be null");

            if (Stock < 0) throw new ArgumentException("The stock must be zero or a positive number");

            String[] categories = { "Ηλεκτρονικά", "Καλλυντικά", "Αλκοόλ", "Τρόφιμα",
                    "Αναψυκτικά χωρίς αλκοόλ", "Βιβλία" };
            if (!categories.Contains(Category)) throw new ArgumentException("The category isn't one of the available ones");

            if (Price < 0) throw new ArgumentException("The price must must be zero or a positive number");

            if (Discount < 0) throw new ArgumentException("The discount must be zero or a positive number");
            if (Discount > 100) throw new ArgumentException("The discount can't be above 100%");

            this.Barcode = Barcode;
            this.Country = Country;
            this.Stock = Stock;
            this.Category = Category;
            this.Price = Price;
            this.Discount = Discount;

        }

    }
    public class PartialDll
    {
        public bool CheckBarcode(string Barcode)
        {
            long tempNum;
            int[] eachNum = new int[13];
            if (Barcode.Length != 13)
            {
                return false;
            }
            if (!Int64.TryParse(Barcode, out tempNum))
            {
                return false;
            }

            char[] eachChar = Barcode.ToCharArray();

            for (int i = 0; i < Barcode.Length; i++)
            {
                eachNum[i] = eachChar[i] - '0';
            }

            int checkDigit = eachNum[12];
            int sumOfBar = 0;

            for (int i = eachNum.Length - 2; i >= 0; i--)
            {
                if (i % 2 == 1)
                {
                    sumOfBar = sumOfBar + 3 * eachNum[i];
                }
                else
                {
                    sumOfBar = sumOfBar + 1 * eachNum[i];
                }

            }

            int checkActual = (10 - (sumOfBar % 10)) % 10;

            if (checkDigit == checkActual)
            {
                return true;
            }

            return false;
        }

        public string FindCountry(string Barcode)
        {
            if (CheckBarcode(Barcode) == false)
            {
                return null;
            }

            string first3Chars = Barcode.Substring(0, 3);
            int first3Num = Int32.Parse(first3Chars);

            if (first3Num <= 0)
            {
                return null;
            }
            else if (first3Num <= 19)
            {
                return "Ηνωμένες Πολιτείες";
            }
            else if (first3Num <= 29)
            {
                return "Περιορισμένη διανομή";
            }
            else if (first3Num <= 39)
            {
                return "Ναρκωτικά των Ηνωμένων Πολιτειών";
            }
            else if (first3Num <= 49)
            {
                return "Περιορισμένη διανομή";
            }
            else if (first3Num <= 59)
            {
                return "Κουπόνια";
            }
            else if (first3Num <= 99)
            {
                return "Stany Zjednoczone i Kanada";
            }
            else if (first3Num <= 139)
            {
                return "Ηνωμένες Πολιτείες (διατηρείται για το μέλλον)";
            }
            else if (first3Num <= 199)
            {
                return null;
            }
            else if (first3Num <= 299)
            {
                return "Περιορισμένη διανομή";
            }
            else if (first3Num <= 379)
            {
                return "Γαλλία και Μονακό";
            }
            else if (first3Num <= 379)
            {
                return null;
            }
            else if (first3Num <= 380)
            {
                return "Βουλγαρία";
            }
            else if (first3Num <= 382)
            {
                return null;
            }
            else if (first3Num <= 383)
            {
                return "Σλοβενία";
            }
            else if (first3Num <= 384)
            {
                return null;
            }
            else if (first3Num <= 385)
            {
                return "Κροατία";
            }
            else if (first3Num <= 386)
            {
                return null;
            }
            else if (first3Num <= 387)
            {
                return "Βοσνία-Ερζεγοβίνη";
            }
            else if (first3Num <= 388)
            {
                return null;
            }
            else if (first3Num <= 389)
            {
                return "Μαυροβούνιο";
            }
            else if (first3Num <= 399)
            {
                return null;
            }
            else if (first3Num <= 440)
            {
                return "Γερμανία";
            }
            else if (first3Num <= 449)
            {
                return null;
            }
            else if (first3Num <= 450)
            {
                return "Ιαπωνία";
            }
            else if (first3Num <= 459)
            {
                return null;
            }
            else if (first3Num <= 469)
            {
                return "Ρωσία";
            }
            else if (first3Num <= 470)
            {
                return "Κιργιζία";
            }
            else if (first3Num <= 471)
            {
                return "Ταϊβάν";
            }
            else if (first3Num <= 473)
            {
                return null;
            }
            else if (first3Num <= 474)
            {
                return "Εσθονία";
            }
            else if (first3Num <= 475)
            {
                return "Λετονία";
            }
            else if (first3Num <= 476)
            {
                return "Αζερμπαϊτζάν";
            }
            else if (first3Num <= 477)
            {
                return "Λιθουανία";
            }
            else if (first3Num <= 478)
            {
                return "Ουζμπεκιστάν";
            }
            else if (first3Num <= 479)
            {
                return "Σρι Λάνκα";
            }
            else if (first3Num <= 480)
            {
                return "Φιλιππίνες";
            }
            else if (first3Num <= 481)
            {
                return "Λευκορωσία";
            }
            else if (first3Num <= 482)
            {
                return "Ουκρανία";
            }
            else if (first3Num <= 483)
            {
                return null;
            }
            else if (first3Num <= 484)
            {
                return "Μολδαβία";
            }
            else if (first3Num <= 485)
            {
                return "Αρμενία";
            }
            else if (first3Num <= 486)
            {
                return "Γεωργία";
            }
            else if (first3Num <= 487)
            {
                return "Καζακστάν";
            }
            else if (first3Num <= 488)
            {
                return null;
            }
            else if (first3Num <= 489)
            {
                return "Χονγκ Κονγκ";
            }
            else if (first3Num <= 490)
            {
                return "Ιαπωνία";
            }
            else if (first3Num <= 499)
            {
                return null;
            }
            else if (first3Num <= 509)
            {
                return "Ηνωμένο Βασίλειο";
            }
            else if (first3Num <= 519)
            {
                return null;
            }
            else if (first3Num <= 520)
            {
                return "Ελλάδα";
            }
            else if (first3Num <= 527)
            {
                return null;
            }
            else if (first3Num <= 528)
            {
                return "Λίβανος";
            }
            else if (first3Num <= 529)
            {
                return "Κύπρος";
            }
            else if (first3Num <= 530)
            {
                return null;
            }
            else if (first3Num <= 531)
            {
                return "Μακεδονία";
            }
            else if (first3Num <= 534)
            {
                return null;
            }
            else if (first3Num <= 535)
            {
                return "Μάλτα";
            }
            else if (first3Num <= 538)
            {
                return null;
            }
            else if (first3Num <= 539)
            {
                return "Ιρλανδία";
            }
            else if (first3Num <= 549)
            {
                return "Belgas και Λουξεμβούργο";
            }
            else if (first3Num <= 559)
            {
                return null;
            }
            else if (first3Num <= 560)
            {
                return "Πορτογαλία";
            }
            else if (first3Num <= 568)
            {
                return null;
            }
            else if (first3Num <= 569)
            {
                return "Ισλανδία";
            }
            else if (first3Num <= 579)
            {
                return "Δανία";
            }
            else if (first3Num <= 589)
            {
                return null;
            }
            else if (first3Num <= 590)
            {
                return "Πολωνία";
            }
            else if (first3Num <= 593)
            {
                return null;
            }
            else if (first3Num <= 594)
            {
                return "Ρουμανία";
            }
            else if (first3Num <= 598)
            {
                return null;
            }
            else if (first3Num <= 599)
            {
                return "Ουγγαρία";
            }
            else if (first3Num <= 601)
            {
                return "Νότια Αφρική";
            }
            else if (first3Num <= 607)
            {
                return null;
            }
            else if (first3Num <= 608)
            {
                return "Μπαχρέιν";
            }
            else if (first3Num <= 609)
            {
                return "Μαυρίκιος";
            }
            else if (first3Num <= 610)
            {
                return null;
            }
            else if (first3Num <= 611)
            {
                return "Μαρόκο";
            }
            else if (first3Num <= 612)
            {
                return null;
            }
            else if (first3Num <= 613)
            {
                return "Algieria";
            }
            else if (first3Num <= 615)
            {
                return null;
            }
            else if (first3Num <= 616)
            {
                return "Κένυα";
            }
            else if (first3Num <= 618)
            {
                return null;
            }
            else if (first3Num <= 619)
            {
                return "Τυνησία";
            }
            else if (first3Num <= 620)
            {
                return null;
            }
            else if (first3Num <= 621)
            {
                return "Συρία";
            }
            else if (first3Num <= 622)
            {
                return "Αίγυπτος";
            }
            else if (first3Num <= 623)
            {
                return null;
            }
            else if (first3Num <= 624)
            {
                return "Λιβύη";
            }
            else if (first3Num <= 625)
            {
                return "Ιορδανία";
            }
            else if (first3Num <= 626)
            {
                return "Ιράν";
            }
            else if (first3Num <= 627)
            {
                return "Κουβέιτ";
            }
            else if (first3Num <= 628)
            {
                return "Σαουδική Αραβία";
            }
            else if (first3Num <= 629)
            {
                return "Emirates";
            }
            else if (first3Num <= 639)
            {
                return null;
            }
            else if (first3Num <= 649)
            {
                return "Φινλανδία";
            }
            else if (first3Num <= 689)
            {
                return null;
            }
            else if (first3Num <= 695)
            {
                return "Κίνα";
            }
            else if (first3Num <= 699)
            {
                return null;
            }
            else if (first3Num <= 709)
            {
                return "Νορβηγία";
            }
            else if (first3Num <= 728)
            {
                return null;
            }
            else if (first3Num <= 729)
            {
                return "Ισραήλ";
            }
            else if (first3Num <= 739)
            {
                return "Σουηδία";
            }
            else if (first3Num <= 740)
            {
                return "Γουατεμάλα";
            }
            else if (first3Num <= 741)
            {
                return "Σαλβαδόρ";
            }
            else if (first3Num <= 742)
            {
                return "Ονδούρα";
            }
            else if (first3Num <= 743)
            {
                return "Νικαράγουα";
            }
            else if (first3Num <= 744)
            {
                return "Costarica";
            }
            else if (first3Num <= 745)
            {
                return null;
            }
            else if (first3Num <= 746)
            {
                return "Δομινικανή Δημοκρατία";
            }
            else if (first3Num <= 749)
            {
                return null;
            }
            else if (first3Num <= 750)
            {
                return "Μεξικό";
            }
            else if (first3Num <= 753)
            {
                return null;
            }
            else if (first3Num <= 754)
            {
                return "Παναμάς";
            }
            else if (first3Num <= 758)
            {
                return null;
            }
            else if (first3Num <= 759)
            {
                return "Βενεζουέλα";
            }
            else if (first3Num <= 769)
            {
                return "Ελβετία";
            }
            else if (first3Num <= 770)
            {
                return "Κολομβία";
            }
            else if (first3Num <= 772)
            {
                return null;
            }
            else if (first3Num <= 773)
            {
                return "Ουρουγουάη";
            }
            else if (first3Num <= 774)
            {
                return null;
            }
            else if (first3Num <= 775)
            {
                return "Περού";
            }
            else if (first3Num <= 776)
            {
                return null;
            }
            else if (first3Num <= 777)
            {
                return "Βολιβία";
            }
            else if (first3Num <= 779)
            {
                return "Αργεντινή";
            }
            else if (first3Num <= 780)
            {
                return "Χιλή";
            }
            else if (first3Num <= 783)
            {
                return null;
            }
            else if (first3Num <= 784)
            {
                return "Παραγουάη";
            }
            else if (first3Num <= 788)
            {
                return null;
            }
            else if (first3Num <= 789)
            {
                return "Εκουαδόρ";
            }
            else if (first3Num <= 790)
            {
                return "Βραζιλία";
            }
            else if (first3Num <= 799)
            {
                return null;
            }
            else if (first3Num <= 839)
            {
                return "Ιταλία";
            }
            else if (first3Num <= 849)
            {
                return "Ισπανία";
            }
            else if (first3Num <= 850)
            {
                return "Κούβα";
            }
            else if (first3Num <= 857)
            {
                return null;
            }
            else if (first3Num <= 858)
            {
                return "Σλοβακία";
            }
            else if (first3Num <= 859)
            {
                return "Δημοκρατία της Τσεχίας";
            }
            else if (first3Num <= 860)
            {
                return "Γιουγκοσλαβία";
            }
            else if (first3Num <= 866)
            {
                return null;
            }
            else if (first3Num <= 867)
            {
                return "Βόρεια Κορέα";
            }
            else if (first3Num <= 869)
            {
                return "Τουρκία";
            }
            else if (first3Num <= 879)
            {
                return "Ολλανδία";
            }
            else if (first3Num <= 884)
            {
                return null;
            }
            else if (first3Num <= 885)
            {
                return "Ταϊλάνδη";
            }
            else if (first3Num <= 887)
            {
                return null;
            }
            else if (first3Num <= 888)
            {
                return "Σιγκαπούρη";
            }
            else if (first3Num <= 889)
            {
                return null;
            }
            else if (first3Num <= 890)
            {
                return "Ινδία";
            }
            else if (first3Num <= 892)
            {
                return null;
            }
            else if (first3Num <= 893)
            {
                return "Βιετνάμ";
            }
            else if (first3Num <= 898)
            {
                return null;
            }
            else if (first3Num <= 899)
            {
                return "Ινδονησία";
            }
            else if (first3Num <= 919)
            {
                return "Αυστρία";
            }
            else if (first3Num <= 929)
            {
                return null;
            }
            else if (first3Num <= 939)
            {
                return "Αυστραλία";
            }
            else if (first3Num <= 949)
            {
                return "Νέα Ζηλανδία";
            }
            else if (first3Num <= 954)
            {
                return null;
            }
            else if (first3Num <= 955)
            {
                return "Μαλαισία";
            }
            else if (first3Num <= 957)
            {
                return null;
            }
            else if (first3Num <= 958)
            {
                return "Μακάο";
            }
            else if (first3Num <= 976)
            {
                return null;
            }
            else if (first3Num <= 977)
            {
                return "ISSN (περιοδικά)";
            }
            else if (first3Num <= 978)
            {
                return "ISBN (βιβλία)";
            }
            else if (first3Num <= 979)
            {
                return "ISMN (μουσικές εκδόσεις)";
            }
            else if (first3Num <= 980)
            {
                return "αποδείξεις επιστροφής";
            }
            else if (first3Num <= 982)
            {
                return "κοινός νομισματικός χώρος κουπόνια";
            }
            else if (first3Num <= 989)
            {
                return null;
            }
            else if (first3Num <= 999)
            {
                return "κουπόνια";
            }





            return null;
        }

        public bool CheckZipCode(string ZipCode, ref string Region)
        {
            int zipNum;
            if (!Int32.TryParse(ZipCode, out zipNum))
            {
                return false;
            }
            if (zipNum < 1000 || zipNum > 9999)
            {
                return false;
            }

            if (zipNum <= 2999)
            {
                Region = "Nicosia District";
                return true;
            }
            else if (zipNum <= 4999)
            {
                Region = "Limassol District";
                return true;
            }
            else if (zipNum <= 5999)
            {
                Region = "Famagusta District";
                return true;
            }
            else if (zipNum <= 7999)
            {
                Region = "Larnaca District";
                return true;
            }
            else if (zipNum <= 8999)
            {
                Region = "Paphos District";
                return true;
            }
            else if (zipNum <= 9999)
            {
                Region = "Kyrenia District";
                return true;
            }


            return false;
        }

        public void CalculateCost(Product Prd, ref double PriceWithTaxes,
            ref double Discount, ref double FinalPrice)
        {
            double priceNoTaxes = Prd.Price;
            double discountOne = Prd.Discount / 100d;
            int fpa = 0;

            if (Prd.Category.Equals("Ηλεκτρονικά") ||
                Prd.Category.Equals("Καλλυντικά") ||
                Prd.Category.Equals("Αλκοόλ"))
            {
                fpa = 24;
            }
            else if (Prd.Category.Equals("Τρόφιμα") ||
                Prd.Category.Equals("Αναψυκτικά χωρίς αλκοόλ"))
            {
                fpa = 13;
            }
            else if (Prd.Category.Equals("Βιβλία"))
            {
                fpa = 6;
            }

            double fpaOne = fpa / 100d;

            PriceWithTaxes = priceNoTaxes + (priceNoTaxes * fpaOne);
            Discount = PriceWithTaxes * discountOne;
            FinalPrice = PriceWithTaxes - Discount;

            return;
        }

        public double CalculateTotalCost(Product[] Prds)
        {
            double totalCost = 0;
            foreach (var productItem in Prds)
            {
                double pTax = 0, disc = 0, final = 0;
                // Χρήση της έτοιμης μεθόδου για να υπολογιστεί σωστά και η έκπτωση
                CalculateCost(productItem, ref pTax, ref disc, ref final);
                totalCost += final;
            }
            return totalCost;
        }

        public int AmountofProducts(Product[] Prds, string Country)
        {
            if (Country == null) return 0; // <--- Πρόσθεσε αυτό για ασφάλεια
            int totalProduct = 0;
            foreach (var item in Prds)
            {
                if (Country.Equals(item.Country, StringComparison.OrdinalIgnoreCase))
                {
                    totalProduct++;
                }
            }
            return totalProduct;
        }

        public bool ProductsToOrder(Product[] Prds, int StockLimit, ref Product[] PrdsToOrder)
        {
            bool isThereEnough = true;
            List<Product> tempPrdsToOrder = new List<Product>();
            foreach (var item in Prds)
            {
                if (item.Stock < StockLimit)
                {
                    isThereEnough = false;
                    tempPrdsToOrder.Add(item);
                }
            }
            PrdsToOrder = tempPrdsToOrder.ToArray();
            return isThereEnough;
        }
    }
}
