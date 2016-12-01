using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JPNFinalProject.Data.DatabaseModels;

namespace JPNFinalProject.Data
{
    public class SeedData
    {
        public static void Seed()
        {
            var streets = new List<string>()
            {
                "Møllebakken 17",
                "Møllebakken 48",
                "Bakkelunden 92",
                "Præstelund 60",
                "Mølleløkken 55"
            };
            var postalcodes = new List<string>()
            {
                "5260",
                "5250",
                "5700",
                "5800",
                "5000"

            };
            var cities = new List<string>()
            {
                "Odense S",
                "Odense SV",
                "Svendborg",
                "Nyborg",
                "Odense C"
            };

            var businessNames = new List<string>()
            {
                "Matas Odense S",
                "Matas Odense C",
                "Matas Nyborg",
                "Matas Svendborg"
            };

            var phonenumbers = new List<string>()
            {
                "61634047",
                "43257692",
                "78593871",
                "12893738",
                "56784721"
            };
            var businessEmail = new List<string>()
            {
                "kontakt@matass.test",
                "kontakt@matassv.test",
                "kontakt@matassvendborg.test",
                "kontakt@matasnyborg.test"
            };
            var personNames = new List<string>()
            {
                "Ida Iversen",
                "Mads Moelgaard",
                "Christa Christensen",
                "Lars Lersø",
                "Jette Jerspersen"
            };
            var categoryNames = new List<string>()
            {
                "Barbering",
                "Skraber",
                "Hudpleje",
                "Ansigtpleje"
            };

            // Creating 
            using (var context = new JPNFinalProjectContext())
            {
                //address
                for (int i = 0; i < streets.Count; i++)
                {
                    context.Address.Add(new Address()
                    {
                        Street = streets[i],
                        PostalCode =postalcodes[i],
                        City = cities[i],
                        Country = "Denmark"
                    });
                }
                //business
                context.SaveChanges();
                for (int i = 0; i < businessNames.Count; i++)
                {
                    context.Business.Add(new Business()
                    {
                        Name = businessNames[i],
                        Address = context.Address.Where(x => x.Street == streets[i]).FirstOrDefault(),
                        Phone = phonenumbers[i],
                        Email = businessEmail[i],
                        OperationalHour = "24/7"
                    });
                }
                //person
                context.SaveChanges();
                for (int i = 0; i < streets.Count; i++)
                {
                    context.Person.Add(new Person()
                    {
                        Name = personNames[i],
                        Email = "testtest@mail.test",
                        Password = "Kode1234",
                        Phone = phonenumbers[i],
                        Type = "????",
                        Address = context.Address.Where(x => x.Street == streets[i]).FirstOrDefault()
                    });
                }
                context.SaveChanges();
                // Productcategories
                context.ProductCategory.Add(new ProductCategory()
                {
                    Name = "Barbering",
                    ParentProductCategory = null
                });
                context.ProductCategory.Add(new ProductCategory()
                {
                    Name = "Skraber",
                    ParentProductCategory = context.ProductCategory.Where(x => x.Name == "Barbering").FirstOrDefault()
                });
                context.ProductCategory.Add(new ProductCategory()
                {
                    Name = "Hudpleje",
                    ParentProductCategory = null
                });
                context.ProductCategory.Add(new ProductCategory()
                {
                    Name = "Ansigtpleje",
                    ParentProductCategory = context.ProductCategory.Where(x => x.Name == "Hudpleje").FirstOrDefault()
                });
                context.SaveChanges();

                // products
                context.Product.Add(new Product()
                {
                    Name = "Matas Fugtighedscreme 250 ml",
                    Price = 54,
                    Description = "Matas Fugtighedscreme 250 ml",
                    ItemNumber = "34897537",
                    Amount = 500,
                    DeliveryTime = new DateTimeOffset(),
                    ProductCategory = context.ProductCategory.Where(x => x.Name == "Ansigtpleje").FirstOrDefault()
                });

                context.Product.Add(new Product()
                {
                    Name = "baKblade barberblade",
                    Price = 129,
                    Description = "3-pak barberblade til baKblade rygskraber",
                    ItemNumber = "34297537",
                    Amount = 50,
                    DeliveryTime = new DateTimeOffset(),
                    ProductCategory = context.ProductCategory.Where(x => x.Name == "Skraber").FirstOrDefault()
                });

                context.Product.Add(new Product()
                {
                    Name = "Barberians cph, Barber Kniv",
                    Price = 700,
                    Description = "Barberians cph, Barber Kniv",
                    ItemNumber = "14297537",
                    Amount = 5,
                    DeliveryTime = new DateTimeOffset(),
                    ProductCategory = context.ProductCategory.Where(x => x.Name == "Skraber").FirstOrDefault()
                });
                context.SaveChanges();

                // Product product
                context.BusinessProduct.Add(new BusinessProduct()
                {
                    Business = context.Business.Where(x => x.Name == "Matas Odense S").FirstOrDefault(),
                    Product = context.Product.Where(x=> x.Name == "Barberians cph, Barber Kniv").FirstOrDefault()

                });
                context.BusinessProduct.Add(new BusinessProduct()
                {
                    Business = context.Business.Where(x => x.Name == "Matas Nyborg").FirstOrDefault(),
                    Product = context.Product.Where(x => x.Name == "baKblade barberblade").FirstOrDefault()

                });
                context.BusinessProduct.Add(new BusinessProduct()
                {
                    Business = context.Business.Where(x => x.Name == "Matas Svendborg").FirstOrDefault(),
                    Product = context.Product.Where(x => x.Name == "Matas Fugtighedscreme 250 ml").FirstOrDefault()

                });
                context.SaveChanges();


            }

        }




    }
}
