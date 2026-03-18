# 📂 Cargando Archivos JSON Externos

La aplicación ahora soporta **cargar cualquier archivo JSON externo** de tu sistema. Aquí está cómo hacerlo:

## ✅ Cómo Cargar un Archivo JSON Externo

1. **Abre la aplicación**
2. **Haz clic en el botón "📁 Cargar JSON"**
3. **Selecciona cualquier archivo JSON de tu sistema**
4. **El archivo se cargará y mostrará automáticamente**

## 📋 Estructura JSON Requerida

La aplicación acepta **dos estructuras principales**:

### 1️⃣ Array de Objetos (Recomendado)
```json
[
  {
    "id": 1,
    "name": "Propiedad 1",
    "price_per_night": 100,
    "bedrooms": 2,
    "rating": 4.5
  },
  {
    "id": 2,
    "name": "Propiedad 2",
    "price_per_night": 150,
    "bedrooms": 3,
    "rating": 4.8
  }
]
```

### 2️⃣ Objeto Único
```json
{
  "id": 1,
  "name": "Propiedad Única",
  "price_per_night": 200,
  "bedrooms": 4,
  "rating": 5.0
}
```

## 🎯 Campos Soportados Dinámicamente

La aplicación **detecta automáticamente** cualquier campo en tu JSON. Los siguientes campos reciben **filtrado especial**:

| Campo | Tipo | Filtrado |
|-------|------|----------|
| `price_per_night`, `price`, `cost`, `nightly_rate`, `rate` | Número | Precio mín/máx |
| `bedrooms`, `rooms`, `bed_rooms`, `num_bedrooms` | Número | Dormitorios mín/máx |
| `rating`, `review_rating`, `score`, `stars`, `average_rating` | Decimal | Rating mínimo |
| `property_type` | Texto | Tipo de propiedad |
| `name`, `description`, `title`, `summary` | Texto | Búsqueda de texto |
| `amenities`, `house_rules` | Array/Texto | Filtro Pet-Friendly |

## 📁 Ubicaciones Recomendadas para Guardar Archivos JSON

- **Windows**: `C:\Users\TuNombre\Documents\` (recomendado)
- **Windows**: `C:\Users\TuNombre\Downloads\`
- Cualquier ubicación en tu disco

## 💡 Ejemplo Completo: Tu Propio Dataset

Crea un archivo llamado `mis_propiedades.json`:

```json
[
  {
    "id": 101,
    "name": "Apartamento Moderno en Centro",
    "description": "Hermoso apartamento con WiFi y cocina completa",
    "property_type": "Apartment",
    "price_per_night": 89,
    "bedrooms": 2,
    "bathrooms": 1,
    "amenities": ["WiFi", "Pets allowed", "Kitchen"],
    "house_rules": "Mascotas permitidas, no fumar",
    "rating": 4.7
  },
  {
    "id": 102,
    "name": "Casa en Playa",
    "description": "Casa frente al mar, perfecta para vacaciones",
    "property_type": "House",
    "price_per_night": 250,
    "bedrooms": 4,
    "bathrooms": 2,
    "amenities": ["WiFi", "Beach Access", "Parking"],
    "house_rules": "No se permiten mascotas",
    "rating": 4.9
  },
  {
    "id": 103,
    "name": "Studio Pet-Friendly",
    "description": "Pequeño pero acogedor, mascotas bienvenidas",
    "property_type": "Studio",
    "price_per_night": 65,
    "bedrooms": 1,
    "bathrooms": 1,
    "amenities": ["WiFi", "Pets allowed"],
    "house_rules": "Mascotas bienvenidas",
    "rating": 4.5
  }
]
```

Luego:
1. **Guarda este archivo** como `mis_propiedades.json`
2. **Abre la aplicación**
3. **Haz clic en "📁 Cargar JSON"**
4. **Selecciona `mis_propiedades.json`**
5. **¡Tus datos aparecerán automáticamente!**

## ⚙️ Filtrado Automático

Una vez cargado el archivo, puedes:
- **🔍 Buscar**: Escribe en el cuadro de búsqueda
- **🐾 Pet-Friendly**: Marca el checkbox para ver solo propiedades que acepten mascotas
- **💰 Precio**: Ajusta el rango de precios
- **🛏️ Dormitorios**: Filtra por número de dormitorios
- **⭐ Rating**: Selecciona rating mínimo
- **🏠 Tipo**: Filtra por tipo de propiedad

## ⚠️ Limitaciones

- **Tamaño máximo**: 50 MB
- **Formato**: Debe ser JSON válido (no JSON5 ni comentarios)
- **Estructura**: Debe ser un array `[]` u objeto `{}`
- **Objetos vacíos**: Se ignoran automáticamente

## 🆘 Solución de Problemas

### "Error: No se pudo cargar el archivo JSON"
- Verifica que el archivo sea JSON válido
- Abre el archivo en un editor de texto y busca errores de sintaxis
- Asegúrate de que uses comillas dobles `"` (no simples `'`)

### "El archivo es demasiado grande"
- Divide tu JSON en múltiples archivos más pequeños
- Carga uno a la vez

### "El archivo no existe"
- Verifica la ruta y nombre del archivo
- Asegúrate de usar la extensión `.json`

## 🔄 Cambiar de Archivo

Para cargar otro archivo JSON:
1. Haz clic nuevamente en **"📁 Cargar JSON"**
2. Selecciona el nuevo archivo
3. Los datos anteriores se borrarán automáticamente
4. Los filtros se resetearán

---

**¡Disfruta analizando tus datos JSON!** 🎉
