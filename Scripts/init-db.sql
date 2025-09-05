-- Müşteri Bakiye Seyri - Database Schema Initialization
-- Bu script sadece database'i ve boş tabloları oluşturur
-- Veriler .NET DataSeeder tarafından yüklenir

-- Database oluşturma (eğer yoksa)
SELECT 'CREATE DATABASE MusteriBakiyeSeyri' 
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'MusteriBakiyeSeyri');

-- Veritabanı hazır mesajı
DO $$
BEGIN
    RAISE NOTICE '🗄️ PostgreSQL database hazırlandı.';
    RAISE NOTICE '📋 Tablolar Entity Framework Migration ile oluşturulacak.';
    RAISE NOTICE '📊 Veriler .NET DataSeeder ile yüklenecek.';
END $$;
