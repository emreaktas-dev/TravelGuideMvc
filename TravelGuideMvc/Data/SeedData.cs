using Microsoft.EntityFrameworkCore;
using TravelGuideMvc.Models;
using TravelGuideMvc.Models.Enums;

namespace TravelGuideMvc.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext db)
        {
            await db.Database.MigrateAsync();

            // Daha önce seed atılmışsa tekrar basma
            if (await db.Cities.AnyAsync() || await db.Categories.AnyAsync())
                return;

            // --- Cities ---
            var bishkek = new City { Name = "Bishkek", Slug = "bishkek" };
            var karakol = new City { Name = "Karakol", Slug = "karakol" };
            var cholponAta = new City { Name = "Cholpon-Ata", Slug = "cholpon-ata" };

            db.Cities.AddRange(bishkek, karakol, cholponAta);

            // --- Categories ---
            var nature = new Category { Name = "Nature", Slug = "nature" };
            var history = new Category { Name = "History", Slug = "history" };
            var culture = new Category { Name = "Culture", Slug = "culture" };

            db.Categories.AddRange(nature, history, culture);

            await db.SaveChangesAsync();

            // --- Places ---
            var alaArcha = new Place
            {
                Name = "Ala Archa National Park",
                Slug = "ala-archa-national-park",
                ShortDescription = "A popular national park near Bishkek, ideal for hiking and nature trips.",
                Description = "Ala Archa is one of the most visited nature destinations near Bishkek with trails and scenic views.",
                CityId = bishkek.Id,
                CategoryId = nature.Id,
                Latitude = 42.6229,
                Longitude = 74.4776,
                Address = "Ala-Archa Gorge",
                OpeningHours = "Daily",
                PriceInfo = "Paid entrance (varies)",
                BestTimeToVisit = "Spring–Autumn"
            };

            var issykKul = new Place
            {
                Name = "Issyk-Kul Lake (Cholpon-Ata)",
                Slug = "issyk-kul-lake-cholpon-ata",
                ShortDescription = "Famous alpine lake with beaches and resorts.",
                Description = "Issyk-Kul is a large alpine lake surrounded by mountains. Cholpon-Ata is a popular resort town on its shore.",
                CityId = cholponAta.Id,
                CategoryId = nature.Id,
                Latitude = 42.6527,
                Longitude = 77.0819,
                Address = "Cholpon-Ata, Issyk-Kul Region",
                OpeningHours = "Always open",
                PriceInfo = "Free (beach services may be paid)",
                BestTimeToVisit = "Summer"
            };

            var karakolMuseum = new Place
            {
                Name = "Karakol Historical Museum",
                Slug = "karakol-historical-museum",
                ShortDescription = "Local museum showcasing regional history and culture.",
                Description = "A small museum displaying artifacts and information about Karakol and surrounding regions.",
                CityId = karakol.Id,
                CategoryId = history.Id,
                Latitude = 42.4907,
                Longitude = 78.3936,
                Address = "Karakol Center",
                OpeningHours = "Mon–Sat 09:00–17:00",
                PriceInfo = "Low entrance fee",
                BestTimeToVisit = "All year"
            };

            db.Places.AddRange(alaArcha, issykKul, karakolMuseum);
            await db.SaveChangesAsync();

            // --- Place Images (örnek URL - şimdilik placeholder) ---
            db.PlaceImages.AddRange(
                new PlaceImage { PlaceId = alaArcha.Id, Url = "https://picsum.photos/seed/alaarcha/900/600", Order = 1 },
                new PlaceImage { PlaceId = issykKul.Id, Url = "https://picsum.photos/seed/issykkul/900/600", Order = 1 },
                new PlaceImage { PlaceId = karakolMuseum.Id, Url = "https://picsum.photos/seed/karakol/900/600", Order = 1 }
            );

            // --- Service Points ---
            db.ServicePoints.AddRange(
                new ServicePoint
                {
                    Type = ServicePointType.Hotel,
                    Name = "Bishkek Central Hotel",
                    Description = "Central location, good for tourists.",
                    Phone = "+996 312 000 000",
                    Address = "Bishkek Center",
                    Latitude = 42.8746,
                    Longitude = 74.6122,
                    CityId = bishkek.Id
                },
                new ServicePoint
                {
                    Type = ServicePointType.Restaurant,
                    Name = "Traditional Kyrgyz Restaurant",
                    Description = "Try local dishes like beshbarmak.",
                    Phone = "+996 312 111 111",
                    Address = "Bishkek",
                    Latitude = 42.8760,
                    Longitude = 74.6050,
                    CityId = bishkek.Id
                },
                new ServicePoint
                {
                    Type = ServicePointType.Hospital,
                    Name = "City Hospital No.1",
                    Description = "Emergency and general healthcare services.",
                    Phone = "+996 312 103 103",
                    Address = "Bishkek",
                    Latitude = 42.8700,
                    Longitude = 74.6100,
                    CityId = bishkek.Id
                },
                new ServicePoint
                {
                    Type = ServicePointType.ATM,
                    Name = "ATM - City Center",
                    Description = "24/7 ATM",
                    Address = "Bishkek Center",
                    Latitude = 42.8750,
                    Longitude = 74.6110,
                    CityId = bishkek.Id
                }
            );

            // --- Emergency Contacts ---
            db.EmergencyContacts.AddRange(
                new EmergencyContact { Title = "Emergency (General)", PhoneNumber = "112", Notes = "General emergency line", Priority = 1 },
                new EmergencyContact { Title = "Police", PhoneNumber = "102", Notes = "Police emergency", Priority = 2 },
                new EmergencyContact { Title = "Ambulance", PhoneNumber = "103", Notes = "Medical emergency", Priority = 3 },
                new EmergencyContact { Title = "Fire Department", PhoneNumber = "101", Notes = "Fire emergency", Priority = 4 }
            );

            // --- Culture Articles ---
            db.CultureArticles.AddRange(
                new CultureArticle
                {
                    Title = "SIM Card & Internet in Kyrgyzstan",
                    Slug = "sim-card-and-internet",
                    Language = "EN",
                    Category = "SimCard",
                    Content = "You can buy a local SIM card at the airport or city centers. Bring your passport for registration.",
                    IsPublished = true,
                    PublishedAt = DateTime.UtcNow
                },
                new CultureArticle
                {
                    Title = "Money & Exchange Tips",
                    Slug = "money-and-exchange-tips",
                    Language = "EN",
                    Category = "Money",
                    Content = "Exchange offices are common in major cities. Compare rates and avoid exchanging large amounts on the street.",
                    IsPublished = true,
                    PublishedAt = DateTime.UtcNow
                },
                new CultureArticle
                {
                    Title = "Kyrgyz Culture Basics",
                    Slug = "kyrgyz-culture-basics",
                    Language = "EN",
                    Category = "Culture",
                    Content = "Hospitality is important. In rural areas, modest clothing and respectful behavior are appreciated.",
                    IsPublished = true,
                    PublishedAt = DateTime.UtcNow
                }
            );



            await db.SaveChangesAsync();
        }
    }
}
