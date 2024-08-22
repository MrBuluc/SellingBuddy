using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using System.Globalization;
using System.IO.Compression;

namespace CatalogService.Persistence.Contexts
{
    public class CatalogContextSeed
    {
        public async Task SeedAsync(CatalogServiceDbContext context, IWebHostEnvironment environment, ILogger<CatalogContextSeed> logger)
        {
            await Policy.Handle<SqlException>().WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: retry => TimeSpan.FromSeconds(5), onRetry: (exception, timeSpan, retry, ctx) =>
            {
                logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", nameof(logger), exception.GetType().Name, exception.Message, retry, 3);
            }).ExecuteAsync(() => ProcessSeeding(context, Path.Combine(environment.ContentRootPath, "Infrastructure", "CatalogService.Persistence", "Setup", "SeedFiles"), "Pics", logger));
        }

        private async Task ProcessSeeding(CatalogServiceDbContext context, string setupDirPath, string picturePath, ILogger logger)
        {
            if (!context.Brands.Any())
            {
                await context.Brands.AddRangeAsync(GetBrandsFromFile(setupDirPath));
            }

            if (!context.Types.Any())
            {
                await context.Types.AddRangeAsync(GetTypesFromFile(setupDirPath));
            }

            if (!context.Items.Any())
            {
                await context.Items.AddRangeAsync(GetItemsFromFile(setupDirPath, context));

                GetItemPictures(setupDirPath, picturePath);
            }

            await context.SaveChangesAsync(CancellationToken.None);
        }

        private IEnumerable<Brand> GetBrandsFromFile(string contentPath)
        {
            static IEnumerable<Brand> GetPreconfiguredBrands() => new List<Brand>()
            {
                new() {Name = "Acer"},
                new() {Name = "TP-Link"},
                new() {Name = "Monster"},
                new() {Name = "Vestel"},
                new() {Name = "Nike"},
                new() {Name = "Google"},
                new() {Name = "Sony"},
                new() {Name = "Gucci"},
                new() {Name = "Zara"},
                new() {Name = "Nestlé"},
                new() {Name = "Starbucks"},
                new() {Name = "LG"},
                new() {Name = "Panasonic"},
                new() {Name = "Philips"},
                new() {Name = "Canon"},
                new() {Name = "Nikon"}
            };

            string fileName = Path.Combine(contentPath, "Brands.txt");
            if (!File.Exists(fileName))
            {
                return GetPreconfiguredBrands();
            }

            return File.ReadAllLines(fileName).Select(b => new Brand()
            {
                Name = b.Trim('"').Trim(),
            }).Where(b => b is not null) ?? GetPreconfiguredBrands();
        }

        private IEnumerable<Domain.Entities.Type> GetTypesFromFile(string contentPath)
        {
            static IEnumerable<Domain.Entities.Type> GetPreconfiguredTypes() => new List<Domain.Entities.Type>()
            {
                new() {Name = "Bag"},
                new() {Name = "Computer"},
                new() {Name = "Modem"},
                new() {Name = "Monitor"},
                new() {Name = "Tablet"},
                new() {Name = "Smart watch"},
                new() {Name = "Headphones"},
                new() {Name = "Gaming Console"},
                new() {Name = "Wallet"},
                new() {Name = "Watch"},
                new() {Name = "Sunglasses"},
                new() {Name = "Refrigerator"},
                new() {Name = "Washing Machine"},
                new() {Name = "Microwave Oven"},
                new() {Name = "Air Conditioner"}
            };

            string fileName = Path.Combine(contentPath, "Types.txt");
            if (!File.Exists(fileName))
            {
                return GetPreconfiguredTypes();
            }

            return File.ReadAllLines(fileName).Select(t => new Domain.Entities.Type()
            {
                Name = t.Trim('"').Trim(),
            }).Where(t => t is not null) ?? GetPreconfiguredTypes();
        }

        private IEnumerable<Item> GetItemsFromFile(string contentPath, CatalogServiceDbContext context)
        {
            static IEnumerable<Item> GetPreconfiguredItems() => new List<Item>()
            {
                new() {TypeId = 2, BrandId = 3, AvailableStock = 100, Description = "12. Nesil Intel® Core™ i7-12700H 45 Watt\r\n12. Nesil Intel® Core™ i7-12700H 4.7 Ghz’e kadar saat hızıyla, 6 adet performans ve 8 adet verimlilik çekirdeğiyle hem performansta yeni bir standardı belirliyor hem de gündelik kullanımda enerji tüketimini düşürüyor. 45 Watt güç tüketimiyle ister en zorlu oyunlarda istersen de iş saatlerinde hiçbir engele takılmadan notebookunu tam performans kullan.", Name = "Tulpar T7 V20.8.4\r\n17,3\" Oyun Bilgisayarı", Price = 37599, PictureFileName = "T7_V20.7_i7_12GEN.png"},
                new() {TypeId = 3, BrandId = 2, AvailableStock = 100, Description = "Kablosuz Eğlence İçin AC WiFi Teknolojisini Yükseltin\r\nGüçlü bir DSL modem yönlendiricisi olarak Archer VR300, 1200 Mb/s'ye varan birleşik çift bantlı kablosuz bağlantı hızları sağlayarak kablosuz yeteneklerinden tam anlamıyla yararlanır. 2.4GHz'de 300 Mb/s, günlük internet kullanımınız ve e-postalarınız için mükemmeldir. Diğer yandan 5GHz'de 867 Mb/s hız, kesintisiz HD akış ve oyun keyfi sunar.", Name = "TP-LINK TP-Link Archer VR300, AC1200 Mbps, 4 Ethernet Portu ve 4 Harici Anten, Tether Uygulaması ile Kolay Kurulum, Dual-Band Fiber", Price = 1599, PictureFileName = "9873614078002.jpg"},
                new() {TypeId = 5, BrandId = 6, AvailableStock = 100, Description = "Hoparlörlü şarj yuvası, Pixel tabletinizin her zaman gerekli güce sahip olmasını ve her zaman kullanıma hazır olmasını sağlar. Ve yüksek kaliteli hoparlör sayesinde odayı dolduran sesle müzik dinleyebilirsiniz", Name = "Google Hazel Şarj Yuvalı Pixel Tablet, Hoparlörlü (11 inç Ekran, 128 GB Bellek, Android, 8 GB RAM)", Price = 32000, PictureFileName = "51rv9CMsRnL._AC_SL1000_.jpg"},
                new() {TypeId = 2, BrandId = 3, AvailableStock = 100, Description = "En Yeni, En Gerçekçi\r\nYüksek performanslı oyun deneyimi arayanların ihtiyaç duyduğu 8 GB GPU ve 140 WATT’a sahip Nvidia RTX4070 ekran kartlı Semruk ile oyunu daha gerçekçi hisset.", Name = "Semruk S7 V9.2.3\r\n17\" Oyun Bilgisayarı", Price = 91399, PictureFileName = "S7_V9_i9.png"},
                new() {TypeId = 3, BrandId = 2, AvailableStock = 100, Description = "Wi-Fi 6 ile Hızınızı Artırın\r\nAX1800 Çift Bant Wi-Fi 6 VDSL/ADSL Modem Router", Name = "TP-LINK TP-Link Archer VX1800v, AX1800 Mbps, 4 Gigabit LAN Portları + 1 USB 2.0 Port, MU-MIMO, Super VDSL, Tether Uygulaması", Price = 2585, PictureFileName = "110000498029424.jpg"},
                new() {TypeId = 2, BrandId = 1, AvailableStock = 100, Description = "Performans\r\n\r\nEkstra Güçlü\r\nSwift 14 AI, olağanüstü hız ve tepki süresi sunar: Güçlü, verimli ve performans açısından optimize edilmiş en yeni ARM tabanlı silikon mimarisine sahip olan bu cihaz, zorlu işlemler ve zahmetsiz çoklu görevler için tasarlanmıştır.", Name = "Swift 14 AI", Price = 56824.44M, PictureFileName = "swift-14-ai-pc-acer-ai-performance-gallery-special-angle-2-img.png"},
                new() {TypeId = 2, BrandId = 1, AvailableStock = 100, Description = "Yeni Nesil Performansın Gücü\r\nYeni nesil Intel® Core™ Ultra 9 işlemci1 ve dahili Intel® Arc™ grafik kartı1 ile donatılmış olan yapay zeka uyumlu Swift Go 16, artık daha yüksek güç verimliliği ile %45 oranına varan performans artışı2 sunuyor. En zorlu görevleri kolaylıkla tamamlamanıza yardımcı olur.", Name = "Swift Go 16", Price = 34499, PictureFileName = "acer-laptop-swift-go-16-Intel-advanced-graphics-with-intel-arc.png"},
                new() {TypeId = 7, BrandId = 7, AvailableStock = 100, Description = "Massive bass. Ultimate vibe.\r\nFeel like you’ve dived into the front row of the concert, and turn the moment on with the ULT POWER SOUND series. Built for music lovers, it produces powerful deep sound designed to make your heart tremble. Prepare to feel the bass.", Name = "ULT POWER SOUND series | ULT WEAR Wireless Noise Canceling Headphones | Black", Price = 6110.65M, PictureFileName = "ult-wearblack.jpg"},
                new() {TypeId = 1, BrandId = 5, AvailableStock = 100, Description = "With a drawcord closure and zippered sides, the Nike Brasilia Training Gymsack offers secure, easy-access storage for your gear. The drawstring backpack helps keep your gear dry with a mesh ventilation panel and water-resistant fabric.", Name = "Nike Brasilia Training Gymsack", Price = 857.22M, PictureFileName = "61BxOsksPrL._AC_SX569_.jpg"},
                new() {TypeId = 3, BrandId = 2, AvailableStock = 100, Description = "Süper VDSL- İnternet Erişiminin Geleceği\r\n \r\n\r\nArcher VR2100, en yeni nesil DSL teknolojisi olan Super VDSL (VDSL2 Profile 35b) özelliğine sahiptir. Downstream 350Mbps' ye kadar arttırıldı, yani önceki VDSL2 lere göre 3.5 kat daha hızlı. Entegre DSL bağlantı noktası tüm standart DSL bağlantılarını da destekler.*\r\n*Archer VR2100, VDSL2, ADSL2 +, ADSL2 ve ADSL ile uyumludur.", Name = "TP-LINK TP-Link Archer VR2100 AC2100 Mbps, 4 Gigabit LAN Portları + 1 USB 3.0 Port, MU-MIMO, Super VDSL, Tether Uygulaması ile Kola", Price = 2820, PictureFileName = "10698972397618.jpg"},
                new() {TypeId = 3, BrandId = 2, AvailableStock = 100, Description = "Hepsi Bir Arada Modem\r\n \r\n\r\nADSL2 + modem ve NAT Router ile birleştiğinde, TD-W8961N, ev ve küçük işletmeler için mükemmel bir güvenilirlik ve düşük maliyetli bir çözüm sunan tam bir ADSL2 + / ADSL2 / ADSL standardı sağlayan inanılmaz derecede sağlam bir hepsi bir arada cihazdır.", Name = "TP-LINK TD-W8961N, 300Mbps ADSL/ADSL2 + Modem Router", Price = 827.16M, PictureFileName = "10577782210610.jpg"},
                new() {TypeId = 11, BrandId = 5, AvailableStock = 100, Description = "Designed using insights from competitive runners, the Stssy collaboration sunglasses have a wrap-around, 1-piece contoured lens that provides zero distraction and deeper coverage. Plus, theyre adjustable where you need itin the nose pad and temples for a comfortable custom fit.", Name = "Nike Victory Elite X Stüssy Mirrored", Price = 6077, PictureFileName = "dvz5l8skkv5hocxsj8ci.jpg"}
            };

            string fileName = Path.Combine(contentPath, "Items.txt");

            if (!File.Exists(fileName))
            {
                return GetPreconfiguredItems();
            }

            Dictionary<string, int> typeIdLookup = context.Types.ToDictionary(t => t.Name, t => t.Id);
            Dictionary<string, int> brandIdLookup = context.Brands.ToDictionary(b => b.Name, b => b.Id);

            return File.ReadAllLines(fileName)
                .Skip(1) // skip header row
                .Select(i => i.Split(";"))
                .Select(i => new Item()
                {
                    TypeId = typeIdLookup[i[0].Trim()],
                    BrandId = brandIdLookup[i[1].Trim()],
                    Description = i[2].Trim('"').Trim(),
                    Name = i[3].Trim('"').Trim(),
                    Price = Decimal.Parse(i[4].Trim('"').Trim(), System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture),
                    PictureFileName = i[5].Trim('"').Trim(),
                    AvailableStock = string.IsNullOrEmpty(i[6].Trim()) ? 0 : int.Parse(i[6].Trim()),
                    OnReorder = Convert.ToBoolean(i[7].Trim()),
                });
        }

        private static void GetItemPictures(string contentPath, string? picturePath = "pics")
        {
            foreach (FileInfo file in new DirectoryInfo(picturePath!).GetFiles())
            {
                file.Delete();
            }

            ZipFile.ExtractToDirectory(Path.Combine(contentPath, "Items.zip"), picturePath!);
        }
    }
}
