using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public interface IDataSeeder
{
    Task SeedAsync();
}

public class DataSeeder : IDataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Migration'ları kontrol et ve sadece gerekirse uygula
        var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await _context.Database.MigrateAsync();
        }
        
        // Veritabanını oluştur
        // Veritabanı var mı kontrol et
        if (!await _context.Database.CanConnectAsync())
        {
            // Veritabanı yoksa oluştur
            await _context.Database.EnsureCreatedAsync();
        }

        // Eğer müşteri veya fatura tablosunda veri yoksa seed işlemini başlat
        if (!await _context.Customers.AnyAsync() || !await _context.Invoices.AnyAsync())
        {
            // Veri ekleme
            var schemaScript = @"
insert into musteri_tanim_table (""ID"", ""UNVAN"")
values (127747, 'Asya Halicilik AS');
insert into musteri_tanim_table (""ID"", ""UNVAN"")
values (127269, 'Veri Bilisim LTD');
insert into musteri_tanim_table (""ID"", ""UNVAN"")
values (127098, 'Nano Bilgi Sistemleri');
insert into musteri_tanim_table (""ID"", ""UNVAN"")
values (129914, 'Expert Gümrükleme');

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (1, 127098, to_date('30-04-2021', 'dd-mm-yyyy'), 250000.00, to_date('03-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (2, 127098, to_date('30-04-2021', 'dd-mm-yyyy'), 250000.00, to_date('03-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (3, 127098, to_date('19-04-2021', 'dd-mm-yyyy'), 228500.00, to_date('20-04-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (4, 127098, to_date('09-07-2021', 'dd-mm-yyyy'), 43615.70, to_date('12-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (5, 127098, to_date('05-07-2021', 'dd-mm-yyyy'), 50000.00, to_date('05-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (6, 127098, to_date('02-07-2021', 'dd-mm-yyyy'), 40000.00, to_date('05-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (7, 127098, to_date('25-06-2021', 'dd-mm-yyyy'), 30000.00, to_date('28-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (8, 127098, to_date('25-06-2021', 'dd-mm-yyyy'), 80000.00, to_date('28-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (9, 127098, to_date('20-06-2021', 'dd-mm-yyyy'), 153935.65, to_date('22-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (10, 127098, to_date('18-06-2021', 'dd-mm-yyyy'), 115000.00, to_date('21-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (11, 127098, to_date('15-06-2021', 'dd-mm-yyyy'), 120000.00, to_date('16-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (12, 127098, to_date('11-06-2021', 'dd-mm-yyyy'), 50000.00, to_date('14-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (13, 127098, to_date('25-06-2021', 'dd-mm-yyyy'), 20000.00, to_date('28-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (14, 127098, to_date('31-05-2021', 'dd-mm-yyyy'), 100000.00, to_date('31-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (15, 127098, to_date('29-05-2021', 'dd-mm-yyyy'), 97154.19, to_date('01-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (16, 127098, to_date('29-05-2021', 'dd-mm-yyyy'), 60000.00, to_date('01-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (17, 127098, to_date('19-05-2021', 'dd-mm-yyyy'), 116000.00, to_date('28-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (18, 127098, to_date('18-05-2021', 'dd-mm-yyyy'), 133581.00, to_date('20-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (19, 127098, to_date('10-05-2021', 'dd-mm-yyyy'), 85000.00, to_date('11-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (20, 127098, to_date('07-05-2021', 'dd-mm-yyyy'), 125000.00, to_date('10-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (21, 127098, to_date('10-05-2021', 'dd-mm-yyyy'), 50600.00, to_date('11-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (22, 127098, to_date('08-05-2021', 'dd-mm-yyyy'), 150000.00, to_date('11-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (23, 127098, to_date('28-05-2021', 'dd-mm-yyyy'), 45000.00, to_date('31-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (24, 127098, to_date('30-04-2021', 'dd-mm-yyyy'), 130300.00, to_date('02-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (25, 127098, to_date('27-04-2021', 'dd-mm-yyyy'), 228527.07, to_date('28-04-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (26, 127098, to_date('30-04-2021', 'dd-mm-yyyy'), 150000.00, to_date('03-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (27, 127098, to_date('30-04-2021', 'dd-mm-yyyy'), 191400.69, to_date('03-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (28, 127098, to_date('09-04-2021', 'dd-mm-yyyy'), 10000.00, to_date('12-04-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (29, 127098, to_date('30-09-2021', 'dd-mm-yyyy'), 100000.00, to_date('01-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (30, 127098, to_date('08-06-2021', 'dd-mm-yyyy'), 120000.00, to_date('09-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (31, 127098, to_date('25-06-2021', 'dd-mm-yyyy'), 150000.00, to_date('28-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (32, 127098, to_date('09-07-2021', 'dd-mm-yyyy'), 146400.00, to_date('12-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (33, 127098, to_date('09-07-2021', 'dd-mm-yyyy'), 65000.00, to_date('12-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (34, 127098, to_date('02-07-2021', 'dd-mm-yyyy'), 65000.00, to_date('05-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (35, 127098, to_date('31-07-2021', 'dd-mm-yyyy'), 106000.00, to_date('03-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (36, 127098, to_date('31-07-2021', 'dd-mm-yyyy'), 100000.00, to_date('03-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (37, 127098, to_date('31-10-2021', 'dd-mm-yyyy'), 40000.00, to_date('02-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (38, 127098, to_date('31-07-2021', 'dd-mm-yyyy'), 60000.00, to_date('03-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (39, 127098, to_date('31-05-2021', 'dd-mm-yyyy'), 6000.00, to_date('01-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (40, 127098, to_date('10-05-2021', 'dd-mm-yyyy'), 18537.66, to_date('11-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (41, 127098, to_date('30-05-2021', 'dd-mm-yyyy'), 15000.00, to_date('01-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (42, 127098, to_date('03-07-2021', 'dd-mm-yyyy'), 75000.00, to_date('06-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (43, 127098, to_date('09-12-2021', 'dd-mm-yyyy'), 65000.00, to_date('10-12-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (44, 127098, to_date('12-07-2021', 'dd-mm-yyyy'), 31309.00, to_date('13-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (45, 127098, to_date('22-09-2021', 'dd-mm-yyyy'), 75000.00, to_date('23-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (46, 127098, to_date('10-07-2021', 'dd-mm-yyyy'), 250000.00, to_date('13-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (47, 127098, to_date('10-10-2021', 'dd-mm-yyyy'), 263000.00, to_date('12-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (48, 127098, to_date('10-07-2021', 'dd-mm-yyyy'), 70000.00, to_date('13-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (49, 127098, to_date('24-09-2021', 'dd-mm-yyyy'), 75000.00, to_date('27-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (50, 127098, to_date('31-10-2021', 'dd-mm-yyyy'), 36019.50, to_date('02-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (51, 127098, to_date('30-09-2021', 'dd-mm-yyyy'), 263000.00, to_date('01-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (52, 127098, to_date('09-07-2021', 'dd-mm-yyyy'), 220408.68, to_date('12-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (53, 127098, to_date('05-10-2021', 'dd-mm-yyyy'), 240000.00, to_date('06-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (54, 127098, to_date('09-07-2021', 'dd-mm-yyyy'), 107000.00, to_date('12-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (55, 127098, to_date('05-10-2021', 'dd-mm-yyyy'), 238000.00, to_date('05-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (56, 127098, to_date('15-09-2021', 'dd-mm-yyyy'), 100000.00, to_date('16-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (57, 127098, to_date('15-02-2021', 'dd-mm-yyyy'), 105425.00, to_date('16-02-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (58, 127098, to_date('22-03-2021', 'dd-mm-yyyy'), 154000.00, to_date('23-03-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (59, 127098, to_date('25-06-2021', 'dd-mm-yyyy'), 12500.00, to_date('28-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (60, 127098, to_date('30-06-2021', 'dd-mm-yyyy'), 180000.00, to_date('01-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (61, 127098, to_date('30-06-2021', 'dd-mm-yyyy'), 40000.00, to_date('01-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (62, 127098, to_date('15-06-2021', 'dd-mm-yyyy'), 100000.00, to_date('18-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (63, 127098, to_date('30-04-2021', 'dd-mm-yyyy'), 20000.00, to_date('06-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (64, 127098, to_date('30-06-2021', 'dd-mm-yyyy'), 35000.00, to_date('30-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (65, 127098, to_date('30-04-2021', 'dd-mm-yyyy'), 95000.00, to_date('03-05-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (66, 127098, to_date('15-04-2021', 'dd-mm-yyyy'), 104332.49, to_date('16-04-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (67, 127098, to_date('25-03-2021', 'dd-mm-yyyy'), 200000.00, to_date('26-03-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (68, 127098, to_date('31-03-2021', 'dd-mm-yyyy'), 25000.00, to_date('01-04-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (69, 127098, to_date('31-05-2021', 'dd-mm-yyyy'), 166000.00, to_date('01-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (70, 127098, to_date('10-08-2021', 'dd-mm-yyyy'), 100084.45, to_date('11-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (71, 127098, to_date('18-08-2021', 'dd-mm-yyyy'), 30000.00, to_date('19-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (72, 127098, to_date('15-08-2021', 'dd-mm-yyyy'), 134209.28, to_date('17-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (73, 127098, to_date('31-08-2021', 'dd-mm-yyyy'), 75810.00, to_date('01-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (74, 127098, to_date('31-08-2021', 'dd-mm-yyyy'), 70400.00, to_date('01-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (75, 127098, to_date('31-08-2021', 'dd-mm-yyyy'), 60000.00, to_date('01-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (76, 127098, to_date('30-07-2021', 'dd-mm-yyyy'), 167750.19, to_date('02-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (77, 127098, to_date('30-09-2021', 'dd-mm-yyyy'), 50000.00, to_date('01-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (78, 127098, to_date('30-09-2021', 'dd-mm-yyyy'), 50000.00, to_date('01-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (79, 127098, to_date('28-07-2021', 'dd-mm-yyyy'), 78657.00, to_date('29-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (80, 127098, to_date('22-07-2021', 'dd-mm-yyyy'), 78504.00, to_date('27-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (81, 127098, to_date('06-08-2021', 'dd-mm-yyyy'), 80000.00, to_date('09-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (82, 127098, to_date('24-09-2021', 'dd-mm-yyyy'), 75992.00, to_date('27-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (83, 127098, to_date('27-08-2021', 'dd-mm-yyyy'), 60000.00, to_date('31-08-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (84, 127098, to_date('31-08-2021', 'dd-mm-yyyy'), 50000.00, to_date('01-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (85, 127098, to_date('30-10-2021', 'dd-mm-yyyy'), 17790.00, to_date('02-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (86, 127098, to_date('24-09-2021', 'dd-mm-yyyy'), 80000.00, to_date('24-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (87, 127098, to_date('10-10-2021', 'dd-mm-yyyy'), 182000.00, to_date('12-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (88, 127098, to_date('30-10-2021', 'dd-mm-yyyy'), 20000.00, to_date('02-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (89, 127098, to_date('25-09-2021', 'dd-mm-yyyy'), 86829.61, to_date('28-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (90, 127098, to_date('28-09-2021', 'dd-mm-yyyy'), 100000.00, to_date('29-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (91, 127098, to_date('31-10-2021', 'dd-mm-yyyy'), 35000.00, to_date('02-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (92, 127098, to_date('16-10-2021', 'dd-mm-yyyy'), 50000.00, to_date('19-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (93, 127098, to_date('06-10-2021', 'dd-mm-yyyy'), 55000.00, to_date('07-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (94, 127098, to_date('30-09-2021', 'dd-mm-yyyy'), 17790.00, to_date('01-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (95, 127098, to_date('10-10-2021', 'dd-mm-yyyy'), 318000.00, to_date('12-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (96, 127098, to_date('31-10-2021', 'dd-mm-yyyy'), 22250.00, to_date('02-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (97, 127098, to_date('25-10-2021', 'dd-mm-yyyy'), 56000.00, to_date('26-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (98, 127098, to_date('24-09-2021', 'dd-mm-yyyy'), 23903.00, to_date('27-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (99, 127098, to_date('29-01-2022', 'dd-mm-yyyy'), 436000.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (100, 127098, to_date('30-01-2022', 'dd-mm-yyyy'), 275000.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (101, 127098, to_date('31-01-2022', 'dd-mm-yyyy'), 71001.08, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (102, 127098, to_date('31-01-2022', 'dd-mm-yyyy'), 20000.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (103, 127098, to_date('07-02-2022', 'dd-mm-yyyy'), 43590.00, to_date('08-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (104, 127098, to_date('28-02-2022', 'dd-mm-yyyy'), 250000.00, to_date('28-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (105, 127098, to_date('02-02-2022', 'dd-mm-yyyy'), 100000.00, to_date('04-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (106, 127098, to_date('23-09-2021', 'dd-mm-yyyy'), 31550.00, to_date('24-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (107, 127098, to_date('18-02-2021', 'dd-mm-yyyy'), 61051.55, to_date('19-02-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (108, 127098, to_date('19-07-2021', 'dd-mm-yyyy'), 50000.00, to_date('27-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (109, 127098, to_date('30-06-2021', 'dd-mm-yyyy'), 150000.00, to_date('01-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (110, 127098, to_date('18-05-2022', 'dd-mm-yyyy'), 27000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (111, 127098, to_date('21-10-2021', 'dd-mm-yyyy'), 87000.00, to_date('22-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (112, 127098, to_date('31-10-2020', 'dd-mm-yyyy'), 60000.00, to_date('02-11-2020', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (113, 127098, to_date('10-01-2021', 'dd-mm-yyyy'), 100000.00, to_date('12-01-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (114, 127269, to_date('23-04-2021', 'dd-mm-yyyy'), 24259.00, to_date('27-04-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (115, 127269, to_date('26-03-2021', 'dd-mm-yyyy'), 7507.63, to_date('29-03-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (116, 127269, to_date('24-09-2021', 'dd-mm-yyyy'), 11178.00, to_date('27-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (117, 127269, to_date('19-03-2021', 'dd-mm-yyyy'), 14778.00, to_date('22-03-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (118, 127269, to_date('26-02-2021', 'dd-mm-yyyy'), 6657.00, to_date('01-03-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (119, 127269, to_date('28-01-2022', 'dd-mm-yyyy'), 25900.00, to_date('31-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (120, 127269, to_date('24-12-2021', 'dd-mm-yyyy'), 46716.00, to_date('27-12-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (121, 127269, to_date('22-01-2021', 'dd-mm-yyyy'), 8098.00, to_date('25-01-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (122, 127269, to_date('24-09-2020', 'dd-mm-yyyy'), 24385.00, to_date('25-09-2020', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (123, 127747, to_date('20-03-2022', 'dd-mm-yyyy'), 120000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (124, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 150000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (125, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 25000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (126, 127747, to_date('19-02-2022', 'dd-mm-yyyy'), 9000.00, to_date('22-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (127, 127747, to_date('20-05-2022', 'dd-mm-yyyy'), 380000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (128, 127747, to_date('23-06-2022', 'dd-mm-yyyy'), 305200.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (129, 127747, to_date('11-06-2022', 'dd-mm-yyyy'), 250000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (130, 127747, to_date('15-06-2022', 'dd-mm-yyyy'), 250000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (131, 127747, to_date('30-04-2022', 'dd-mm-yyyy'), 29500.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (132, 127747, to_date('17-03-2022', 'dd-mm-yyyy'), 20000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (133, 127747, to_date('15-03-2022', 'dd-mm-yyyy'), 96520.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (134, 127747, to_date('05-04-2022', 'dd-mm-yyyy'), 20000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (135, 127747, to_date('30-04-2022', 'dd-mm-yyyy'), 80000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (136, 127747, to_date('12-05-2022', 'dd-mm-yyyy'), 40000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (137, 127747, to_date('30-05-2022', 'dd-mm-yyyy'), 80000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (138, 127747, to_date('19-05-2022', 'dd-mm-yyyy'), 40000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (139, 127747, to_date('27-05-2022', 'dd-mm-yyyy'), 270000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (140, 127747, to_date('20-05-2022', 'dd-mm-yyyy'), 225000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (141, 127747, to_date('11-05-2022', 'dd-mm-yyyy'), 225000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (142, 127747, to_date('30-01-2022', 'dd-mm-yyyy'), 100000.00, to_date('31-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (143, 127747, to_date('21-02-2022', 'dd-mm-yyyy'), 200000.00, to_date('22-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (144, 127747, to_date('06-02-2022', 'dd-mm-yyyy'), 200000.00, to_date('08-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (145, 127747, to_date('23-02-2022', 'dd-mm-yyyy'), 60000.00, to_date('24-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (146, 127747, to_date('30-01-2022', 'dd-mm-yyyy'), 20000.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (147, 127747, to_date('20-02-2022', 'dd-mm-yyyy'), 10000.00, to_date('22-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (148, 127747, to_date('29-01-2022', 'dd-mm-yyyy'), 22790.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (149, 127747, to_date('29-01-2022', 'dd-mm-yyyy'), 100000.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (150, 127747, to_date('28-02-2022', 'dd-mm-yyyy'), 17500.00, to_date('01-03-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (151, 127747, to_date('14-03-2022', 'dd-mm-yyyy'), 65000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (152, 127747, to_date('31-01-2022', 'dd-mm-yyyy'), 100000.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (153, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 25000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (154, 127747, to_date('28-02-2022', 'dd-mm-yyyy'), 11606.00, to_date('01-03-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (155, 127747, to_date('28-02-2022', 'dd-mm-yyyy'), 25000.00, to_date('01-03-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (156, 127747, to_date('28-02-2022', 'dd-mm-yyyy'), 10000.00, to_date('01-03-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (157, 127747, to_date('26-02-2022', 'dd-mm-yyyy'), 22000.00, to_date('01-03-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (158, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 50000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (159, 127747, to_date('23-03-2022', 'dd-mm-yyyy'), 30000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (160, 127747, to_date('28-02-2022', 'dd-mm-yyyy'), 150000.00, to_date('01-03-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (161, 127747, to_date('24-02-2022', 'dd-mm-yyyy'), 63200.00, to_date('25-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (162, 127747, to_date('14-02-2022', 'dd-mm-yyyy'), 65000.00, to_date('15-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (163, 127747, to_date('28-03-2022', 'dd-mm-yyyy'), 120000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (164, 127747, to_date('02-02-2022', 'dd-mm-yyyy'), 32000.00, to_date('03-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (165, 127747, to_date('25-01-2022', 'dd-mm-yyyy'), 62500.00, to_date('26-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (166, 127747, to_date('31-01-2022', 'dd-mm-yyyy'), 16750.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (167, 127747, to_date('23-02-2022', 'dd-mm-yyyy'), 46000.00, to_date('24-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (168, 127747, to_date('23-01-2022', 'dd-mm-yyyy'), 46000.00, to_date('25-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (169, 127747, to_date('31-01-2022', 'dd-mm-yyyy'), 19586.54, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (170, 127747, to_date('27-11-2021', 'dd-mm-yyyy'), 300000.00, to_date('30-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (171, 127747, to_date('28-12-2021', 'dd-mm-yyyy'), 300000.00, to_date('29-12-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (172, 127747, to_date('26-01-2022', 'dd-mm-yyyy'), 300000.00, to_date('27-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (173, 127747, to_date('31-12-2021', 'dd-mm-yyyy'), 50000.00, to_date('03-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (174, 127747, to_date('14-12-2021', 'dd-mm-yyyy'), 85000.00, to_date('15-12-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (175, 127747, to_date('11-12-2021', 'dd-mm-yyyy'), 11000.00, to_date('14-12-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (176, 127747, to_date('10-11-2021', 'dd-mm-yyyy'), 76812.00, to_date('11-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (177, 127747, to_date('06-11-2021', 'dd-mm-yyyy'), 15000.00, to_date('09-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (178, 127747, to_date('30-11-2021', 'dd-mm-yyyy'), 10000.00, to_date('01-12-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (179, 127747, to_date('24-09-2021', 'dd-mm-yyyy'), 39344.00, to_date('27-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (180, 127747, to_date('30-09-2021', 'dd-mm-yyyy'), 30000.00, to_date('01-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (181, 127747, to_date('22-09-2021', 'dd-mm-yyyy'), 100000.00, to_date('23-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (182, 127747, to_date('05-10-2021', 'dd-mm-yyyy'), 50000.00, to_date('06-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (183, 127747, to_date('02-10-2021', 'dd-mm-yyyy'), 25410.00, to_date('05-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (184, 127747, to_date('12-10-2021', 'dd-mm-yyyy'), 118000.52, to_date('13-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (185, 127747, to_date('07-10-2021', 'dd-mm-yyyy'), 40000.00, to_date('08-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (186, 127747, to_date('14-10-2021', 'dd-mm-yyyy'), 50000.00, to_date('15-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (187, 127747, to_date('16-10-2021', 'dd-mm-yyyy'), 29000.00, to_date('19-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (188, 127747, to_date('19-10-2021', 'dd-mm-yyyy'), 55230.89, to_date('20-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (189, 127747, to_date('19-10-2021', 'dd-mm-yyyy'), 23000.00, to_date('20-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (190, 127747, to_date('20-10-2021', 'dd-mm-yyyy'), 91130.90, to_date('21-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (191, 127747, to_date('31-01-2022', 'dd-mm-yyyy'), 300000.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (192, 127747, to_date('28-01-2022', 'dd-mm-yyyy'), 300000.00, to_date('31-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (193, 127747, to_date('10-12-2021', 'dd-mm-yyyy'), 85000.00, to_date('13-12-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (194, 127747, to_date('29-01-2022', 'dd-mm-yyyy'), 80000.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (195, 127747, to_date('30-01-2022', 'dd-mm-yyyy'), 87250.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (196, 127747, to_date('30-01-2022', 'dd-mm-yyyy'), 87250.00, to_date('01-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (197, 127747, to_date('20-02-2022', 'dd-mm-yyyy'), 164000.00, to_date('22-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (198, 127747, to_date('30-06-2022', 'dd-mm-yyyy'), 195000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (199, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 15000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (200, 127747, to_date('01-04-2022', 'dd-mm-yyyy'), 6120.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (201, 127747, to_date('09-04-2022', 'dd-mm-yyyy'), 28261.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (202, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 54689.23, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (203, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 6000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (204, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 47855.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (205, 127747, to_date('22-03-2022', 'dd-mm-yyyy'), 50000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (206, 127747, to_date('21-03-2022', 'dd-mm-yyyy'), 29579.53, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (207, 127747, to_date('25-03-2022', 'dd-mm-yyyy'), 12000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (208, 127747, to_date('22-03-2022', 'dd-mm-yyyy'), 39000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (209, 127747, to_date('18-03-2022', 'dd-mm-yyyy'), 30000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (210, 127747, to_date('20-03-2022', 'dd-mm-yyyy'), 25000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (211, 127747, to_date('20-04-2022', 'dd-mm-yyyy'), 20000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (212, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 50000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (213, 127747, to_date('19-03-2022', 'dd-mm-yyyy'), 34326.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (214, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 20000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (215, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 33000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (216, 127747, to_date('19-03-2022', 'dd-mm-yyyy'), 7378.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (217, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 12500.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (218, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 12500.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (219, 127747, to_date('15-04-2022', 'dd-mm-yyyy'), 50227.51, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (220, 127747, to_date('22-04-2022', 'dd-mm-yyyy'), 50000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (221, 127747, to_date('26-03-2022', 'dd-mm-yyyy'), 81000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (222, 127747, to_date('28-03-2022', 'dd-mm-yyyy'), 33000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (223, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 30000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (224, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 80000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (225, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 15000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (226, 127747, to_date('21-03-2022', 'dd-mm-yyyy'), 19606.34, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (227, 127747, to_date('01-06-2022', 'dd-mm-yyyy'), 200000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (228, 127747, to_date('02-06-2022', 'dd-mm-yyyy'), 200000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (229, 127747, to_date('31-05-2022', 'dd-mm-yyyy'), 125000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (230, 127747, to_date('22-05-2022', 'dd-mm-yyyy'), 5000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (231, 127747, to_date('31-05-2022', 'dd-mm-yyyy'), 30000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (232, 127747, to_date('31-05-2022', 'dd-mm-yyyy'), 200000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (233, 127747, to_date('15-05-2022', 'dd-mm-yyyy'), 40000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (234, 127747, to_date('30-06-2022', 'dd-mm-yyyy'), 161000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (235, 127747, to_date('23-06-2022', 'dd-mm-yyyy'), 128000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (236, 127747, to_date('20-02-2022', 'dd-mm-yyyy'), 300900.00, to_date('22-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (237, 127747, to_date('30-03-2022', 'dd-mm-yyyy'), 195434.52, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (238, 127747, to_date('28-02-2022', 'dd-mm-yyyy'), 11142.00, to_date('01-03-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (239, 127747, to_date('20-03-2022', 'dd-mm-yyyy'), 50000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (240, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 200000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (241, 127747, to_date('25-03-2022', 'dd-mm-yyyy'), 53545.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (242, 127747, to_date('20-01-2022', 'dd-mm-yyyy'), 200000.00, to_date('21-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (243, 127747, to_date('10-02-2022', 'dd-mm-yyyy'), 200000.00, to_date('11-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (244, 127747, to_date('04-02-2022', 'dd-mm-yyyy'), 180000.00, to_date('07-02-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (245, 127747, to_date('31-03-2022', 'dd-mm-yyyy'), 149200.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (246, 127747, to_date('07-05-2022', 'dd-mm-yyyy'), 200000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (247, 127747, to_date('14-05-2022', 'dd-mm-yyyy'), 154286.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (248, 127747, to_date('06-05-2022', 'dd-mm-yyyy'), 15000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (249, 127747, to_date('07-05-2022', 'dd-mm-yyyy'), 177000.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (250, 127747, to_date('19-03-2022', 'dd-mm-yyyy'), 508061.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (251, 127747, to_date('16-04-2022', 'dd-mm-yyyy'), 39029.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (252, 127747, to_date('28-05-2022', 'dd-mm-yyyy'), 32650.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (253, 127747, to_date('14-05-2022', 'dd-mm-yyyy'), 21500.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (254, 127747, to_date('09-04-2022', 'dd-mm-yyyy'), 5947.00, null);

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (255, 127747, to_date('10-10-2020', 'dd-mm-yyyy'), 205000.00, to_date('13-10-2020', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (256, 129914, to_date('28-01-2022', 'dd-mm-yyyy'), 20355.00, to_date('31-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (257, 129914, to_date('30-09-2021', 'dd-mm-yyyy'), 15000.00, to_date('30-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (258, 129914, to_date('29-06-2021', 'dd-mm-yyyy'), 11328.00, to_date('30-06-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (259, 129914, to_date('07-01-2022', 'dd-mm-yyyy'), 43260.00, to_date('10-01-2022', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (260, 129914, to_date('15-10-2021', 'dd-mm-yyyy'), 15000.00, to_date('18-10-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (261, 129914, to_date('27-09-2021', 'dd-mm-yyyy'), 21240.00, to_date('28-09-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (262, 129914, to_date('30-10-2021', 'dd-mm-yyyy'), 20000.00, to_date('02-11-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (263, 129914, to_date('20-07-2021', 'dd-mm-yyyy'), 10000.00, to_date('27-07-2021', 'dd-mm-yyyy'));

insert into musteri_fatura_table (""ID"", ""MUSTERI_ID"", ""FATURA_TARIHI"", ""FATURA_TUTARI"", ""ODEME_TARIHI"")
values (264, 129914, to_date('31-08-2021', 'dd-mm-yyyy'), 28500.00, to_date('01-09-2021', 'dd-mm-yyyy'));
";
            var data = await _context.Database.ExecuteSqlRawAsync(schemaScript);


            // Transaction'ı commit et
            await _context.SaveChangesAsync();
        }

    }
}