# ▶️ Cómo Ejecutar la Aplicación

## 🚀 Opción 1: Visual Studio (Recomendado)

### Pasos:
1. **Abre Visual Studio 2026**
   - Archivo → Abrir → Proyecto/Solución
   - Navega a: `C:\Users\Nat\source\repos\airbnb\`
   - Selecciona la carpeta o archivo `.csproj`

2. **Restaura paquetes NuGet**
   - Visual Studio lo hace automáticamente
   - Si no, ve a: Herramientas → Gestor de paquetes NuGet → Consola del Administrador de paquetes
   - Ejecuta: `Update-Package -Reinstall`

3. **Compila el proyecto**
   - Presiona: `Ctrl+Shift+B` o `Build > Build Solution`
   - Verifica que aparezca: "Compilación completada satisfactoriamente"

4. **Ejecuta la aplicación**
   - Presiona: `F5` o `Debug > Start Debugging`
   - La aplicación debería abrirse en una ventana

### Resultado esperado:
```
Ventana: "Gestor Dinámico de Datasets Airbnb - JSON"
- Botón azul: "📁 Cargar JSON"
- TextBox: "Búsqueda: ___"
- CheckBox: "🐾 Pet Friendly"
- Label: "Cargue un archivo JSON para comenzar"
- DataGridView: Vacío (esperando datos)
```

---

## 🖥️ Opción 2: PowerShell (Línea de Comandos)

### Pasos:

```powershell
# 1. Navega a la carpeta del proyecto
cd C:\Users\Nat\source\repos\airbnb\

# 2. Restaura los paquetes NuGet
dotnet restore

# 3. Compila el proyecto
dotnet build

# 4. Ejecuta la aplicación
dotnet run

# Alternativa: Compilar y ejecutar en un paso
dotnet run --configuration Release
```

### Resultado esperado:
- La aplicación se abrirá en una ventana separada
- Se mostrarán mensajes de compilación en la consola

---

## 🎯 Después de Ejecutar

### 1. Prueba básica (30 segundos)
```
1. Haz clic en "📁 Cargar JSON"
2. Selecciona: sample_airbnb.json
3. Deberías ver 5 propiedades en la tabla
```

### 2. Prueba de filtros (30 segundos más)
```
1. Escribe "luxury" en la búsqueda
   → Ver: 1 resultado (Luxury Villa)
2. Activa "🐾 Pet Friendly"
   → Ver: 3 resultados (propiedades pet-friendly)
3. Limpia y desactiva para volver a ver los 5
```

### 3. Carga tu propio JSON
```
1. Crea un archivo JSON personalizado
2. Clic en "📁 Cargar JSON"
3. Selecciona tu archivo
4. La app se adapta automáticamente
```

---

## 🐛 Problemas Comunes y Soluciones

### Problema: "Error de compilación"

**Solución 1: Restaurar NuGet**
```powershell
cd C:\Users\Nat\source\repos\airbnb\
dotnet nuget locals all --clear
dotnet restore
dotnet build
```

**Solución 2: Limpiar y recompilar**
```powershell
dotnet clean
dotnet build
```

**Solución 3: Verificar .NET 8**
```powershell
dotnet --version
# Debe mostrar 8.0.x o superior
```

Si no tienes .NET 8, descárgalo de: https://dotnet.microsoft.com/download

---

### Problema: "No se puede encontrar el archivo .NET"

**Causa:** .NET 8 no está instalado

**Solución:**
1. Abre: https://dotnet.microsoft.com/download/dotnet
2. Descarga ".NET 8 SDK" (no el runtime)
3. Instala siguiendo los pasos
4. Reinicia Visual Studio
5. Intenta compilar nuevamente

---

### Problema: "La ventana no aparece"

**Posibles causas:**
1. Se ejecutó pero no se vio
2. Hubo un error silencioso
3. La aplicación se cerró rápido

**Soluciones:**
1. Verifica la consola para mensajes de error
2. Agrega un breakpoint en `Form1.cs` línea 11 (constructor)
3. Usa `F5` en lugar de `Ctrl+F5` para ver errores
4. Revisa la ventana "Output" (Debug pane)

---

### Problema: "El JSON no carga"

**Verifica:**
1. ¿El archivo es JSON válido? (usa jsonlint.com)
2. ¿El archivo existe en la ruta indicada?
3. ¿Puedes leerlo con un editor de texto?
4. ¿Está codificado en UTF-8?

**Solución:**
```powershell
# Crea un JSON simple para probar
@'
[{"name": "Test", "price": 100}]
'@ | Out-File -FilePath "test.json" -Encoding UTF8

# Intenta cargar este archivo en la app
```

---

### Problema: "Los filtros no funcionan"

**Verifica:**
1. ¿Hay datos cargados en la tabla?
2. ¿El JSON tiene las columnas esperadas?
3. ¿Escribiste en el TextBox correcto?

**Solución:**
1. Recarga el JSON
2. Verifica que tenga columnas: name, description, amenities, house_rules
3. Intenta con `sample_airbnb.json` primero

---

## 📊 Verificar Instalación Correcta

```powershell
# 1. Verificar .NET 8
dotnet --version
# Resultado: 8.0.x o superior ✅

# 2. Verificar SDK
dotnet --info
# Debe listar: .NET SDK 8.0.x ✅

# 3. Verificar que se puede compilar
cd C:\Users\Nat\source\repos\airbnb\
dotnet build
# Resultado: "Build succeeded" ✅

# 4. Verificar NuGet
dotnet package search Newtonsoft.Json
# Debe encontrar el paquete ✅
```

---

## 💡 Tips Útiles

### Ejecutar en modo Release (más rápido)
```powershell
dotnet run --configuration Release
```

### Ver logs detallados de compilación
```powershell
dotnet build --verbosity detailed
```

### Ejecutar tests si existen
```powershell
dotnet test
```

### Crear ejecutable standalone
```powershell
dotnet publish -c Release -r win-x64 --self-contained
# Resultado: archivo .exe en publish/
```

---

## 🎓 Estructura de ejecución

```
Visual Studio abre
    ↓
Carga airbnb.csproj
    ↓
Restaura paquetes (Newtonsoft.Json)
    ↓
Compila código C# → .dll
    ↓
Carga entry point (Program.cs)
    ↓
Instancia Form1 → ShowDialog()
    ↓
UI esperando input
    ↓
Usuario carga JSON → Process
    ↓
Muestra datos → Filtrado disponible
```

---

## 🚀 Checklists Pre-Ejecución

### Antes de ejecutar por primera vez
- [ ] Visual Studio 2022+ instalado
- [ ] .NET 8 SDK instalado
- [ ] Proyecto abierto en VS
- [ ] sample_airbnb.json existe en la carpeta

### Antes de compartir el proyecto
- [ ] Compilación exitosa (Ctrl+Shift+B)
- [ ] Sin warnings en el Output
- [ ] Pruebas básicas funcionan (F5)
- [ ] Documentación actualizada

---

## 📞 Ayuda Adicional

Si necesitas ayuda:

1. **Verificar la consola de errores**
   - Debug → Windows → Output
   - Busca mensajes de error

2. **Revisar archivos de log**
   - `airbnb/obj/Debug/net8.0-windows/`

3. **Validar JSON**
   - Copia el contenido en: https://jsonlint.com/

4. **Consultar documentación**
   - Ver: README.md (características)
   - Ver: QUICK_START.md (tutorial rápido)
   - Ver: TEST_PLAN.md (casos de prueba)

---

## ✅ Checklist Final

Si todo funciona correctamente:

- ✅ Aplicación se abre sin errores
- ✅ Botón "Cargar JSON" responde
- ✅ DataGridView se puebla con datos
- ✅ Búsqueda filtra en tiempo real
- ✅ Pet Friendly checkbox funciona
- ✅ Status label se actualiza
- ✅ Normalización de datos visible
- ✅ Sin crashes durante operaciones

¡**LA APLICACIÓN ESTÁ LISTA PARA USAR!** 🎉

---

**¡Diviértete explorando Airbnb datos!** 🏠
