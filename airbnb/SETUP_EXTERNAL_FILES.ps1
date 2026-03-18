# 🚀 Instrucciones para Cargar Archivos JSON Externos
# Ejecuta desde PowerShell en la carpeta del proyecto

Write-Host "╔════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   Carga de Archivos JSON Externos - Guía de Inicio        ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""

Write-Host "📋 PASOS RÁPIDOS:" -ForegroundColor Yellow
Write-Host ""
Write-Host "1️⃣  Abre la aplicación ejecutando:" -ForegroundColor White
Write-Host "   dotnet run" -ForegroundColor Green
Write-Host ""

Write-Host "2️⃣  En la interfaz, haz clic en '📁 Cargar JSON'" -ForegroundColor White
Write-Host ""

Write-Host "3️⃣  Se abrirá un diálogo de archivos" -ForegroundColor White
Write-Host "   • Comienza en tu carpeta 'Documentos'" -ForegroundColor Gray
Write-Host "   • Puedes navegar a cualquier ubicación" -ForegroundColor Gray
Write-Host ""

Write-Host "4️⃣  Selecciona un archivo JSON:" -ForegroundColor White
Write-Host "   • sample_airbnb.json (incluido)" -ForegroundColor Cyan
Write-Host "   • sample_properties.json (incluido)" -ForegroundColor Cyan
Write-Host "   • Cualquier otro .json que tengas" -ForegroundColor Cyan
Write-Host ""

Write-Host "5️⃣  Haz clic en 'Abrir'" -ForegroundColor White
Write-Host ""

Write-Host "6️⃣  ¡Verás tus datos en la tabla!" -ForegroundColor White
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

Write-Host "📁 ARCHIVOS DE EJEMPLO INCLUIDOS:" -ForegroundColor Yellow
Write-Host ""
Write-Host "  📄 sample_airbnb.json" -ForegroundColor Cyan
Write-Host "     → Dataset original de propiedades Airbnb" -ForegroundColor Gray
Write-Host "     → 5 propiedades con todos los campos" -ForegroundColor Gray
Write-Host ""
Write-Host "  📄 sample_properties.json" -ForegroundColor Cyan
Write-Host "     → Dataset alternativo con campos diferentes" -ForegroundColor Gray
Write-Host "     → Demuestra flexibilidad de nombres de campos" -ForegroundColor Gray
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

Write-Host "✨ CREAR TU PROPIO ARCHIVO JSON:" -ForegroundColor Yellow
Write-Host ""
Write-Host "Opción 1: Desde cero" -ForegroundColor White
Write-Host "  1. Abre Notepad (Win+R → notepad)" -ForegroundColor Gray
Write-Host "  2. Copia este código:" -ForegroundColor Gray
Write-Host ""
Write-Host '     [' -ForegroundColor Green
Write-Host '       {"id": 1, "name": "Propiedad 1", "price_per_night": 100, "bedrooms": 2},' -ForegroundColor Green
Write-Host '       {"id": 2, "name": "Propiedad 2", "price_per_night": 150, "bedrooms": 3}' -ForegroundColor Green
Write-Host '     ]' -ForegroundColor Green
Write-Host ""
Write-Host "  3. Guarda como 'datos.json' en Documentos" -ForegroundColor Gray
Write-Host "  4. Cárgalo en la aplicación" -ForegroundColor Gray
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

Write-Host "🎯 CAMPOS QUE SE DETECTAN AUTOMÁTICAMENTE:" -ForegroundColor Yellow
Write-Host ""
Write-Host "  Precios:" -ForegroundColor White
Write-Host "    • price_per_night, price, cost, nightly_rate, rate" -ForegroundColor Gray
Write-Host ""
Write-Host "  Dormitorios:" -ForegroundColor White
Write-Host "    • bedrooms, rooms, bed_rooms, num_bedrooms" -ForegroundColor Gray
Write-Host ""
Write-Host "  Rating:" -ForegroundColor White
Write-Host "    • rating, review_rating, score, stars, average_rating" -ForegroundColor Gray
Write-Host ""
Write-Host "  Búsqueda de texto:" -ForegroundColor White
Write-Host "    • name, description, title, summary" -ForegroundColor Gray
Write-Host ""
Write-Host "  Pet-Friendly:" -ForegroundColor White
Write-Host "    • Busca en: amenities, house_rules" -ForegroundColor Gray
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

Write-Host "⚙️  AFTER LOADING:" -ForegroundColor Yellow
Write-Host ""
Write-Host "  🔍 Búsqueda:" -ForegroundColor White
Write-Host "     Escribe en el cuadro de búsqueda para encontrar propiedades" -ForegroundColor Gray
Write-Host ""
Write-Host "  🐾 Pet Friendly:" -ForegroundColor White
Write-Host "     Marca el checkbox para ver solo mascotas permitidas" -ForegroundColor Gray
Write-Host ""
Write-Host "  💰 Precio:" -ForegroundColor White
Write-Host "     Ajusta el rango de precios mínimo y máximo" -ForegroundColor Gray
Write-Host ""
Write-Host "  🛏️  Dormitorios:" -ForegroundColor White
Write-Host "     Filtra por número mínimo y máximo de dormitorios" -ForegroundColor Gray
Write-Host ""
Write-Host "  ⭐ Rating:" -ForegroundColor White
Write-Host "     Selecciona el rating mínimo deseado" -ForegroundColor Gray
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

Write-Host "⚠️  SI ALGO NO FUNCIONA:" -ForegroundColor Yellow
Write-Host ""
Write-Host "  ❌ 'No se pudo cargar el archivo JSON'" -ForegroundColor Red
Write-Host "     → Abre en https://jsonlint.com para validar" -ForegroundColor Gray
Write-Host "     → Asegúrate de que sea array [] u objeto {}" -ForegroundColor Gray
Write-Host ""
Write-Host "  ❌ 'Archivo no existe'" -ForegroundColor Red
Write-Host "     → Verifica la ubicación del archivo" -ForegroundColor Gray
Write-Host "     → Asegúrate de que tenga extensión .json" -ForegroundColor Gray
Write-Host ""
Write-Host "  ❌ 'Demasiado grande'" -ForegroundColor Red
Write-Host "     → Máximo 50 MB" -ForegroundColor Gray
Write-Host "     → Divide en archivos más pequeños" -ForegroundColor Gray
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

Write-Host "🔄 CAMBIAR DE ARCHIVO:" -ForegroundColor Yellow
Write-Host ""
Write-Host "  1. Haz clic en '📁 Cargar JSON' nuevamente" -ForegroundColor White
Write-Host "  2. Selecciona otro archivo" -ForegroundColor White
Write-Host "  3. Los datos anteriores se limpiarán automáticamente" -ForegroundColor White
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

Write-Host "📚 DOCUMENTACIÓN:" -ForegroundColor Yellow
Write-Host ""
Write-Host "  Lee estos archivos para más información:" -ForegroundColor White
Write-Host ""
Write-Host "  • LOADING_EXTERNAL_FILES.md" -ForegroundColor Cyan
Write-Host "    Guía completa en Markdown" -ForegroundColor Gray
Write-Host ""
Write-Host "  • README_EXTERNAL_FILES.md" -ForegroundColor Cyan
Write-Host "    Resumen técnico de cambios" -ForegroundColor Gray
Write-Host ""
Write-Host "  • CARGA_EXTERNA_GUIA.txt" -ForegroundColor Cyan
Write-Host "    Guía en formato texto plano" -ForegroundColor Gray
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

Write-Host "🎉 ¡LISTO PARA EMPEZAR!" -ForegroundColor Green
Write-Host ""
Write-Host "Ejecuta: " -ForegroundColor White -NoNewline
Write-Host "dotnet run" -ForegroundColor Green
Write-Host ""

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
