#!/bin/bash
set -e

echo "🚀 Müşteri Bakiye Seyri uygulaması başlatılıyor..."

# Wait for PostgreSQL to be ready
echo "📡 PostgreSQL bağlantısı kontrol ediliyor..."
until pg_isready -h postgres -p 5432 -U postgres; do
  echo "⏳ PostgreSQL'in hazır olması bekleniyor..."
  sleep 2
done

echo "✅ PostgreSQL hazır!"

# Wait a bit more to ensure database is fully ready
sleep 5

echo "📋 Migration'lar otomatik uygulanacak..."
echo "ℹ️  Migration'lar uygulama başlatılırken DataSeeder tarafından yapılacak."

echo "🗄️ Uygulama başlatılıyor..."

# Execute the main command
exec "$@"
