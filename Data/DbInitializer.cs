using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect__Savina_Ioana.Models;


namespace Proiect__Savina_Ioana.Data
{
    public class DbInitializer
    {
        public static void Initialize(RestaurantContext context)
        {
            context.Database.EnsureCreated();
            if (context.Food.Any())
            {
                return;
            }
            var foods = new Food[]
            {
 new Food{Dish="Orez cu legume",Chef="Andrei Pop",Price=Decimal.Parse("13")},
 new Food{Dish="Cartofi prajiti",Chef="Marilena Morar",Price=Decimal.Parse("15")},
 new Food{Dish="Salata bulgareasca",Chef="Marian Andreea",Price=Decimal.Parse("20")},
 new Food{Dish="Snitel de pui",Chef="David Andreea",Price=Decimal.Parse("23")},
 new Food{Dish="Cascaval pane",Chef="Petrescu Paul",Price=Decimal.Parse("22")},
 new Food{Dish="Musaca de cartofi",Chef="Matei Laura",Price=Decimal.Parse("25")},
            };
            foreach (Food b in foods)
            {
                context.Food.Add(b);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

 new Customer{CustomerID=10,Name="Miahilescu Antonia",BirthDate=DateTime.Parse("2000-07-11")},
 new Customer{CustomerID=11,Name="Popovici Maria",BirthDate=DateTime.Parse("1998-04-18")},

            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
 new Order{FoodID=2,CustomerID=10,OrderDate=DateTime.Parse("12-15-2020")},
 new Order{FoodID=1,CustomerID=11,OrderDate=DateTime.Parse("10-14-2020")},
 new Order{FoodID=3,CustomerID=10,OrderDate=DateTime.Parse("01-25-2020")},
 new Order{FoodID=4,CustomerID=11,OrderDate=DateTime.Parse("12-12-2020")},
 new Order{FoodID=4,CustomerID=11,OrderDate=DateTime.Parse("08-15-2020")},
 new Order{FoodID=5,CustomerID=10,OrderDate=DateTime.Parse("10-24-2020")},
 };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();
            var owners = new Owners[]
            {

 new Owners{OwnerName="Casa piratilor",Adress="Str. Garii, Cluj-Napoca"},
 new Owners{OwnerName="Mado",Adress="Str. Memorandumului, Cluj-Napoca"},
 new Owners{OwnerName="Spartan",Adress="Str.Aviatiei, Cluj-Napoca"},
            };
            foreach (Owners p in owners)
            {
                context.Owners.Add(p);
            }
            context.SaveChanges();
            var ownedfood = new OwnedFood[]
            {
 new OwnedFood {FoodID = foods.Single(c => c.Dish == "Orez cu legume" ).ID,
     OwnerID = owners.Single(i => i.OwnerName =="Casa piratilor").ID},
 new OwnedFood {FoodID = foods.Single(c => c.Dish == "Cartofi prajiti" ).ID,
OwnerID = owners.Single(i => i.OwnerName =="Mado").ID},
 new OwnedFood {
 FoodID = foods.Single(c => c.Dish == "Salata bulgareasca" ).ID,
 OwnerID = owners.Single(i => i.OwnerName =="Spartan").ID},
 new OwnedFood {
 FoodID = foods.Single(c => c.Dish == "Snitel de pui" ).ID,
OwnerID = owners.Single(i => i.OwnerName == "Mado").ID},
 new OwnedFood {
 FoodID = foods.Single(c => c.Dish == "Cascaval pane" ).ID,
OwnerID = owners.Single(i => i.OwnerName == "Spartan").ID},
 new OwnedFood {
 FoodID = foods.Single(c => c.Dish == "Musaca de cartofi" ).ID,
 OwnerID = owners.Single(i => i.OwnerName == "Spartan").ID},
            };
            foreach (OwnedFood pb in ownedfood)
            {
                context.OwnedFood.Add(pb);
            }
            context.SaveChanges();
        }
    }
}