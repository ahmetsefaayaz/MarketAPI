# --- 1. Aşama: Build (Derleme) ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Tüm dosyaları container içine kopyala
# (Solution ana dizininde olduğumuz için her şeyi alır)
COPY . .

# Projeyi derle ve yayınla
# DİKKAT: Buradaki dosya yolunun senin Program.cs içeren ana projen olduğundan emin ol.
# Eğer ana projenin adı farklıysa aşağıyı güncelle:
RUN dotnet publish "MarketAPI.Presentation/MarketAPI.Presentation.csproj" -c Release -o /app/publish

# --- 2. Aşama: Runtime (Çalıştırma) ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Build aşamasında oluşturulan çıktıları al
COPY --from=build /app/publish .

# Port ayarı (Container içi port)
EXPOSE 8080

# Uygulamayı başlat (Buradaki .dll ismi de proje adınla aynı olmalı)
ENTRYPOINT ["dotnet", "MarketAPI.Presentation.dll"]