# 🚀 Guía Rápida - Gestor de Airbnb JSON

## Inicio Rápido (30 segundos)

1. **Ejecuta la aplicación** - Presiona `F5` en Visual Studio
2. **Carga datos** - Haz clic en "📁 Cargar JSON"
3. **Selecciona JSON** - Elige `sample_airbnb.json` (ya incluido en el proyecto)
4. **¡Listo!** - Verás 5 propiedades de ejemplo en la tabla

## Prueba los Filtros

### Prueba 1: Pet Friendly
- ✅ Activa el CheckBox "🐾 Pet Friendly"
- Resultado: Solo muestra 3 propiedades que permiten mascotas (ids: 1, 3, 5)

### Prueba 2: Búsqueda General
- Escribe "luxury" en el cuadro de búsqueda
- Resultado: Muestra solo "Luxury Villa with Pool"

### Prueba 3: Búsqueda + Pet Friendly
- Escribe "pet" y activa el CheckBox
- Resultado: Muestra solo "Pet-Friendly Studio"

### Prueba 4: Búsqueda por Tipo de Propiedad
- Escribe "villa" o "apartment"
- Resultado: Filtra por tipo de propiedad

## Estructura de Archivos Generados

```
airbnb/
├── Form1.cs                    ← Lógica principal y UI
├── Form1.Designer.cs           ← Controles WinForms
├── JsonDataProcessor.cs        ← Lógica de datos (separada)
├── airbnb.csproj               ← Configuración del proyecto
├── sample_airbnb.json          ← Datos de ejemplo
└── README.md                   ← Documentación completa
```

## Características Clave Implementadas

| Característica | Descripción | Estado |
|---|---|---|
| **Carga Dinámica** | Carga cualquier JSON flexible | ✅ |
| **Columnas Dinámicas** | Se generan automáticamente | ✅ |
| **Pet Friendly Filter** | Busca "pets", "mascotas", "allowed" | ✅ |
| **Búsqueda General** | Tipo Google en múltiples campos | ✅ |
| **Filtrado en Tiempo Real** | Sin necesidad de botón de buscar | ✅ |
| **Manejo de Errores** | Try-Catch inteligente | ✅ |
| **Indicador de Estado** | Muestra registros filtrados | ✅ |
| **Valor Complejos** | Normaliza arrays y objetos | ✅ |

## Personalización Fácil

### Cambiar Campos de Búsqueda
Edita `JsonDataProcessor.cs`, línea ~100:
```csharp
var searchColumns = new[] { "name", "description", "title" };
```

### Agregar Palabras Clave Pet-Friendly
Edita `JsonDataProcessor.cs`, línea ~107:
```csharp
var petKeywords = new[] { "pets", "mascotas", "allowed", "dog", "cat" };
```

### Cambiar Columnas Relevantes
Edita `JsonDataProcessor.cs`, línea ~108:
```csharp
var relevantColumns = new[] { "amenities", "house_rules", "description" };
```

## Pruebas Adicionales

### Con tu propio JSON:
1. Crea un archivo JSON válido en tu formato
2. Haz clic en "📁 Cargar JSON"
3. Selecciona tu archivo
4. La app se adapta automáticamente a la estructura

**Ejemplo JSON personalizado:**
```json
[
  {
    "titulo": "Mi propiedad",
    "descripcion": "Casa con piscina y mascotas permitidas",
    "servicios": ["WiFi", "Pets allowed", "Pool"]
  }
]
```

## Troubleshooting

| Problema | Solución |
|---|---|
| No se carga el JSON | Verifica que sea válido (usa jsonlint.com) |
| Filtro no funciona | Asegúrate de que el JSON tenga las columnas esperadas |
| Aplicación lenta | Los datasets muy grandes (>10K registros) pueden requerir optimización |
| Columnas no visibles | Amplía el DataGridView o ajusta el ancho de columnas |

## Código Destacado

### Procesamiento Dinámico
```csharp
// Detecta automáticamente columnas
foreach (var row in data)
{
    foreach (var key in row.Keys)
    {
        _availableColumns.Add(key);
    }
}
```

### Normalización de Valores
```csharp
// Convierte arrays a texto legible
Newtonsoft.Json.Linq.JArray arr => 
    string.Join(", ", arr.Select(x => x.ToString().Trim('"')))
```

### Filtrado Inteligente
```csharp
// Busca sin errores si columna no existe
if (row.TryGetValue(col, out var value) && value != null)
{
    // Procesa seguramente
}
```

---

**¡La aplicación está lista para usar! 🎉**
