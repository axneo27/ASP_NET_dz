using Microsoft.EntityFrameworkCore;
using dz1.Models;

namespace dz1.Initializer
{
    public static class Seeder
    {
        public static async void Seed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            if (!context.Categories.Any())
            {
                try
                {
                    var categories = new Category[5]
                {
                    new Category { Name = "Відеокарти" },
                    new Category { Name = "Процесори" },
                    new Category { Name = "Материнські плати" },
                    new Category { Name = "Блоки живлення" },
                    new Category { Name = "Оперативна пам'ять" }
                };

                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();

                    var products = new Product[125]
                    {
                    // ================== Відеокарти ==================
                    new Product { Name = "NVIDIA GeForce RTX 4070", Amount = 10, Description = "Потужна відеокарта для геймінгу", Price = 24000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 4060 Ti", Amount = 15, Description = "Оптимальний вибір для Full HD ігор", Price = 18000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 4080", Amount = 7, Description = "Флагманська модель для 4K", Price = 48000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 7900 XT", Amount = 12, Description = "Відеокарта від AMD з високою продуктивністю", Price = 35000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 7800 XT", Amount = 8, Description = "Баланс ціни та якості", Price = 29000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 4090", Amount = 5, Description = "Найпотужніша відеокарта на ринку", Price = 75000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 3050", Amount = 20, Description = "Бюджетна карта для повсякденних задач", Price = 9500, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 7600", Amount = 14, Description = "Чудовий варіант для 1080p", Price = 12000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce GTX 1660 Super", Amount = 18, Description = "Популярна бюджетна відеокарта", Price = 8000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 6700 XT", Amount = 9, Description = "Середній клас від AMD", Price = 21000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 3060", Amount = 13, Description = "Оптимальна відеокарта для геймерів", Price = 14000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 3060 Ti", Amount = 11, Description = "Краща продуктивність за помірні гроші", Price = 16500, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 3070", Amount = 6, Description = "Відеокарта для 2K ігор", Price = 20000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 3070 Ti", Amount = 5, Description = "Потужність для сучасних ігор", Price = 23000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 3080", Amount = 4, Description = "Флагман минулого покоління", Price = 30000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 6800", Amount = 7, Description = "Конкурент 3080 від AMD", Price = 27000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 6800 XT", Amount = 6, Description = "Високий FPS у 2K", Price = 31000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 6950 XT", Amount = 3, Description = "Надпотужна відеокарта AMD", Price = 40000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce GTX 1650", Amount = 19, Description = "Стара, але надійна", Price = 6000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce GTX 1050 Ti", Amount = 25, Description = "Для офісу та простих ігор", Price = 4000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 6500 XT", Amount = 17, Description = "Бюджетний вибір", Price = 7000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 6400", Amount = 22, Description = "Проста відеокарта", Price = 5500, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 4060", Amount = 10, Description = "Свіжа модель для геймерів", Price = 16000, CategoryId = categories[0].Id },
                    new Product { Name = "AMD Radeon RX 7700 XT", Amount = 8, Description = "Середній клас 2023 року", Price = 24000, CategoryId = categories[0].Id },
                    new Product { Name = "NVIDIA GeForce RTX 4050", Amount = 12, Description = "Очікувана бюджетна модель", Price = 12000, CategoryId = categories[0].Id },
                
                    // ================== Процесори ==================
                    new Product { Name = "Intel Core i9-13900K", Amount = 6, Description = "Флагманський процесор Intel", Price = 28000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i7-13700K", Amount = 8, Description = "Висока продуктивність", Price = 21000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i5-13600K", Amount = 10, Description = "Середній клас для ігор", Price = 16000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i3-13100", Amount = 12, Description = "Бюджетний процесор", Price = 7000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen 9 7950X", Amount = 5, Description = "Флагман від AMD", Price = 25000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen 9 7900X", Amount = 7, Description = "Потужний багатоядерний CPU", Price = 20000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen 7 7700X", Amount = 9, Description = "Оптимальний баланс для ігор", Price = 15000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen 5 7600X", Amount = 11, Description = "Середній клас Ryzen", Price = 12000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Pentium Gold G7400", Amount = 14, Description = "Простий і дешевий", Price = 3000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Celeron G6900", Amount = 18, Description = "Для офісних ПК", Price = 2500, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i9-12900K", Amount = 5, Description = "Попереднє покоління флагмана", Price = 20000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i7-12700K", Amount = 8, Description = "Потужність за доступною ціною", Price = 16000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i5-12400F", Amount = 15, Description = "Бюджетний ігровий процесор", Price = 9000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen 9 5900X", Amount = 6, Description = "Топовий Ryzen минулого покоління", Price = 15000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen 7 5800X", Amount = 7, Description = "8 ядер для геймінгу та роботи", Price = 12000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen 5 5600", Amount = 9, Description = "Доступний варіант для ігор", Price = 8000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen 3 4100", Amount = 12, Description = "Бюджетний чотириядерний CPU", Price = 4500, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Xeon W-2295", Amount = 4, Description = "Серверний процесор", Price = 35000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i9-11900K", Amount = 5, Description = "Флагман попереднього покоління", Price = 14000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen Threadripper 3990X", Amount = 2, Description = "64 ядра для професіоналів", Price = 120000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Ryzen Threadripper 3970X", Amount = 3, Description = "Потужність для робочих станцій", Price = 80000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i5-11400F", Amount = 10, Description = "Середній сегмент", Price = 7000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i3-10100F", Amount = 12, Description = "Бюджетний процесор", Price = 4000, CategoryId = categories[1].Id },
                    new Product { Name = "AMD Athlon 3000G", Amount = 20, Description = "Найпростіший процесор для офісу", Price = 2000, CategoryId = categories[1].Id },
                    new Product { Name = "Intel Core i9-14900K", Amount = 4, Description = "Нове покоління i9", Price = 32000, CategoryId = categories[1].Id },
                
                    // ================== Материнські плати ==================
                    new Product { Name = "ASUS ROG Strix Z790-E Gaming", Amount = 8, Description = "Материнська плата для топових систем", Price = 15000, CategoryId = categories[2].Id },
                    new Product { Name = "MSI MPG B760 Gaming Plus", Amount = 12, Description = "Плата для геймерських ПК", Price = 8500, CategoryId = categories[2].Id },
                    new Product { Name = "Gigabyte X670 Aorus Master", Amount = 6, Description = "Підтримка Ryzen 7000", Price = 18000, CategoryId = categories[2].Id },
                    new Product { Name = "ASRock B550 Phantom Gaming", Amount = 10, Description = "Надійна плата середнього класу", Price = 6000, CategoryId = categories[2].Id },
                    new Product { Name = "ASUS TUF Gaming B550-Plus", Amount = 11, Description = "Ігрова плата з надійними компонентами", Price = 7000, CategoryId = categories[2].Id },
                    new Product { Name = "MSI MAG B550 Tomahawk", Amount = 9, Description = "Популярна плата для Ryzen", Price = 7500, CategoryId = categories[2].Id },
                    new Product { Name = "Gigabyte Z690 Aorus Elite", Amount = 7, Description = "Підтримка Alder Lake", Price = 12000, CategoryId = categories[2].Id },
                    new Product { Name = "ASUS Prime Z790-P", Amount = 5, Description = "Універсальна плата для Intel", Price = 10000, CategoryId = categories[2].Id },
                    new Product { Name = "ASRock X570 Taichi", Amount = 6, Description = "Плата для ентузіастів", Price = 14000, CategoryId = categories[2].Id },
                    new Product { Name = "MSI PRO B660M-A", Amount = 14, Description = "Доступна материнська плата", Price = 5000, CategoryId = categories[2].Id },
                    new Product { Name = "Gigabyte B450M DS3H", Amount = 18, Description = "Бюджетна плата для Ryzen", Price = 3500, CategoryId = categories[2].Id },
                    new Product { Name = "ASUS ROG Crosshair VIII Hero", Amount = 4, Description = "Топова плата для Ryzen", Price = 20000, CategoryId = categories[2].Id },
                    new Product { Name = "MSI X570 Gaming Edge", Amount = 8, Description = "Високий рівень стабільності", Price = 11000, CategoryId = categories[2].Id },
                    new Product { Name = "Gigabyte Z590 Aorus Pro", Amount = 9, Description = "Плата для 11-го покоління Intel", Price = 9500, CategoryId = categories[2].Id },
                    new Product { Name = "ASRock H610M-HDV", Amount = 20, Description = "Дуже бюджетна плата", Price = 2500, CategoryId = categories[2].Id },
                    new Product { Name = "ASUS ROG Maximus Z790 Hero", Amount = 3, Description = "Ентузіастський рівень", Price = 22000, CategoryId = categories[2].Id },
                    new Product { Name = "MSI MPG X670E Carbon WiFi", Amount = 5, Description = "Сучасна AM5 плата", Price = 19000, CategoryId = categories[2].Id },
                    new Product { Name = "Gigabyte B650M Gaming X", Amount = 10, Description = "Нова платформа Ryzen", Price = 8000, CategoryId = categories[2].Id },
                    new Product { Name = "ASRock Z690 Extreme", Amount = 6, Description = "Плата для Intel 12-го покоління", Price = 13000, CategoryId = categories[2].Id },
                    new Product { Name = "ASUS TUF Gaming Z590-Plus", Amount = 7, Description = "Ігрова плата для Intel", Price = 9000, CategoryId = categories[2].Id },
                    new Product { Name = "MSI H510M-A Pro", Amount = 15, Description = "Базова материнка для Intel", Price = 3000, CategoryId = categories[2].Id },
                    new Product { Name = "Gigabyte X570S Aorus Elite", Amount = 8, Description = "Оновлена версія X570", Price = 9500, CategoryId = categories[2].Id },
                    new Product { Name = "ASRock B450 Steel Legend", Amount = 9, Description = "Надійна бюджетна плата", Price = 4000, CategoryId = categories[2].Id },
                    new Product { Name = "ASUS Prime B450M-K", Amount = 20, Description = "Проста і дешева", Price = 2800, CategoryId = categories[2].Id },
                    new Product { Name = "MSI MEG Z790 Godlike", Amount = 2, Description = "Абсолютний флагман", Price = 35000, CategoryId = categories[2].Id },
                
                    // ================== Блоки живлення ==================
                    new Product { Name = "Corsair RM850x", Amount = 12, Description = "Якісний блок живлення 850W", Price = 5500, CategoryId = categories[3].Id },
                    new Product { Name = "Seasonic Focus GX-750", Amount = 10, Description = "Надійний блок 750W", Price = 4500, CategoryId = categories[3].Id },
                    new Product { Name = "be quiet! Straight Power 11 1000W", Amount = 5, Description = "Тиха робота і стабільність", Price = 8000, CategoryId = categories[3].Id },
                    new Product { Name = "Cooler Master MWE Gold 650W", Amount = 14, Description = "Доступний блок живлення", Price = 3000, CategoryId = categories[3].Id },
                    new Product { Name = "Thermaltake Toughpower GF1 850W", Amount = 9, Description = "Висока ефективність", Price = 6000, CategoryId = categories[3].Id },
                    new Product { Name = "EVGA SuperNOVA 750 G6", Amount = 8, Description = "Модульний блок", Price = 5000, CategoryId = categories[3].Id },
                    new Product { Name = "Corsair AX1600i", Amount = 2, Description = "Найпотужніший блок живлення", Price = 15000, CategoryId = categories[3].Id },
                    new Product { Name = "Seasonic Prime TX-1000", Amount = 3, Description = "Титанова сертифікація", Price = 12000, CategoryId = categories[3].Id },
                    new Product { Name = "be quiet! Pure Power 11 600W", Amount = 18, Description = "Доступне рішення", Price = 2500, CategoryId = categories[3].Id },
                    new Product { Name = "Cooler Master V850 Gold", Amount = 6, Description = "Якісна серія V850", Price = 5500, CategoryId = categories[3].Id },
                    new Product { Name = "Thermaltake Smart RGB 700W", Amount = 16, Description = "З підсвіткою", Price = 2800, CategoryId = categories[3].Id },
                    new Product { Name = "EVGA 600 BR", Amount = 20, Description = "Бюджетний блок", Price = 1800, CategoryId = categories[3].Id },
                    new Product { Name = "Corsair VS550", Amount = 22, Description = "Для офісних ПК", Price = 1600, CategoryId = categories[3].Id },
                    new Product { Name = "Seasonic S12III 500W", Amount = 19, Description = "Надійний і простий", Price = 1700, CategoryId = categories[3].Id },
                    new Product { Name = "be quiet! Dark Power Pro 12 1200W", Amount = 4, Description = "Топовий сегмент", Price = 10000, CategoryId = categories[3].Id },
                    new Product { Name = "Cooler Master GX III 850W", Amount = 7, Description = "Сучасне рішення", Price = 5200, CategoryId = categories[3].Id },
                    new Product { Name = "Thermaltake Toughpower PF1 1200W", Amount = 5, Description = "Потужний блок живлення", Price = 9000, CategoryId = categories[3].Id },
                    new Product { Name = "EVGA SuperNOVA 650 G5", Amount = 9, Description = "80+ Gold сертифікат", Price = 4000, CategoryId = categories[3].Id },
                    new Product { Name = "Corsair HX1000i", Amount = 6, Description = "Для ентузіастів", Price = 8500, CategoryId = categories[3].Id },
                    new Product { Name = "Seasonic Focus PX-850", Amount = 8, Description = "Якісна серія Focus", Price = 5800, CategoryId = categories[3].Id },
                    new Product { Name = "be quiet! System Power 9 500W", Amount = 21, Description = "Недорогий блок", Price = 1500, CategoryId = categories[3].Id },
                    new Product { Name = "Cooler Master Elite V3 500W", Amount = 23, Description = "Бюджетний вибір", Price = 1400, CategoryId = categories[3].Id },
                    new Product { Name = "Thermaltake Litepower 550W", Amount = 25, Description = "Дешева модель", Price = 1200, CategoryId = categories[3].Id },
                    new Product { Name = "EVGA 500 W1", Amount = 17, Description = "Базовий варіант", Price = 1300, CategoryId = categories[3].Id },
                    new Product { Name = "Corsair RM1000x", Amount = 5, Description = "1000W для потужних ПК", Price = 7500, CategoryId = categories[3].Id },
                
                    // ================== Оперативна пам'ять ==================
                    new Product { Name = "Corsair Vengeance LPX 16GB DDR4-3200", Amount = 15, Description = "Надійна пам'ять для геймерів", Price = 2500, CategoryId = categories[4].Id },
                    new Product { Name = "G.Skill Ripjaws V 32GB DDR4-3600", Amount = 12, Description = "Висока швидкість", Price = 4000, CategoryId = categories[4].Id },
                    new Product { Name = "Kingston Fury Beast 16GB DDR5-5200", Amount = 10, Description = "Сучасна DDR5 пам'ять", Price = 5000, CategoryId = categories[4].Id },
                    new Product { Name = "TeamGroup T-Force Delta RGB 32GB DDR4-3600", Amount = 9, Description = "З підсвіткою", Price = 4200, CategoryId = categories[4].Id },
                    new Product { Name = "Patriot Viper Steel 16GB DDR4-3200", Amount = 18, Description = "Якісна бюджетна пам'ять", Price = 2400, CategoryId = categories[4].Id },
                    new Product { Name = "Crucial Ballistix 32GB DDR4-3200", Amount = 7, Description = "Надійність від Crucial", Price = 3800, CategoryId = categories[4].Id },
                    new Product { Name = "ADATA XPG Spectrix D50 16GB DDR4-3600", Amount = 11, Description = "RGB пам'ять", Price = 2600, CategoryId = categories[4].Id },
                    new Product { Name = "G.Skill Trident Z5 RGB 32GB DDR5-6000", Amount = 6, Description = "Топова DDR5 пам'ять", Price = 7000, CategoryId = categories[4].Id },
                    new Product { Name = "Corsair Dominator Platinum RGB 32GB DDR4-3600", Amount = 5, Description = "Преміальна серія", Price = 6500, CategoryId = categories[4].Id },
                    new Product { Name = "Kingston Fury Impact 16GB DDR4-3200 SO-DIMM", Amount = 13, Description = "Пам'ять для ноутбуків", Price = 2300, CategoryId = categories[4].Id },
                    new Product { Name = "Patriot Signature Line 8GB DDR4-2666", Amount = 20, Description = "Базова пам'ять", Price = 1200, CategoryId = categories[4].Id },
                    new Product { Name = "TeamGroup Elite Plus 16GB DDR4-3200", Amount = 15, Description = "Проста і надійна", Price = 2100, CategoryId = categories[4].Id },
                    new Product { Name = "ADATA Premier 8GB DDR4-3200", Amount = 25, Description = "Для офісу та дому", Price = 1100, CategoryId = categories[4].Id },
                    new Product { Name = "Crucial DDR5 32GB-4800", Amount = 10, Description = "DDR5 базового рівня", Price = 4500, CategoryId = categories[4].Id },
                    new Product { Name = "G.Skill Aegis 16GB DDR4-3000", Amount = 19, Description = "Бюджетний варіант", Price = 1900, CategoryId = categories[4].Id },
                    new Product { Name = "Corsair Vengeance RGB Pro 32GB DDR4-3200", Amount = 8, Description = "RGB серія", Price = 3600, CategoryId = categories[4].Id },
                    new Product { Name = "Kingston Fury Renegade 32GB DDR5-6000", Amount = 5, Description = "Флагман DDR5", Price = 7200, CategoryId = categories[4].Id },
                    new Product { Name = "Patriot Viper RGB 16GB DDR4-3600", Amount = 14, Description = "Якісна RGB пам'ять", Price = 2700, CategoryId = categories[4].Id },
                    new Product { Name = "TeamGroup T-Force Vulcan Z 16GB DDR4-3200", Amount = 16, Description = "Надійна бюджетна серія", Price = 2000, CategoryId = categories[4].Id },
                    new Product { Name = "ADATA XPG Lancer 32GB DDR5-6000", Amount = 7, Description = "Сучасна DDR5 пам'ять", Price = 6800, CategoryId = categories[4].Id },
                    new Product { Name = "Crucial Ballistix MAX 32GB DDR4-4000", Amount = 6, Description = "Для оверклокерів", Price = 5800, CategoryId = categories[4].Id },
                    new Product { Name = "G.Skill Ripjaws S5 16GB DDR5-5200", Amount = 12, Description = "DDR5 нового покоління", Price = 5400, CategoryId = categories[4].Id },
                    new Product { Name = "Corsair ValueSelect 8GB DDR4-2400", Amount = 21, Description = "Найдоступніша пам'ять", Price = 1000, CategoryId = categories[4].Id },
                    new Product { Name = "Patriot Viper 4 Blackout 16GB DDR4-3200", Amount = 18, Description = "Чорна серія", Price = 2200, CategoryId = categories[4].Id },
                    new Product { Name = "TeamGroup Elite DDR5 16GB-4800", Amount = 9, Description = "Базовий DDR5", Price = 4200, CategoryId = categories[4].Id },
                    };

                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}
